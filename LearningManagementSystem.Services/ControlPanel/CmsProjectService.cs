using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static iTextSharp.text.pdf.AcroFields;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CmsProjectService : ICmsProjectService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;

        public CmsProjectService(ISettingService settingService, LearningManagementSystemContext context)
        {
            _settingService = settingService;
            _context = context;
        }
        public IPagedList<CmsProject> GetCmsProjects(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProjectes = db.CmsProjects.Include(r => r.CmsProjectTranslations).Include(r => r.ProjectCategory.CmsProjectCategoryTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    CmsProjectes = CmsProjectes.Where(r => r.Name.Contains(searchText));
                //}

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        CmsProjectes = CmsProjectes.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {

                        CmsProjectes = CmsProjectes.Where(r => r.CmsProjectTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = CmsProjectes;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }

                        var transProjectCategory = item.ProjectCategory?.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transProjectCategory != null)
                        {
                            item.ProjectCategory.Name = transProjectCategory.Name;
                        }
                    }
                }
                return output;
            }
        }
        public async Task<IPagedList<CmsProject>> GetActiveProjects(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProjectes = db.CmsProjects.Include(r => r.CmsProjectTranslations).Include(r => r.ProjectCategory.CmsProjectCategoryTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    CmsProjectes = CmsProjectes.Where(r => r.Name.Contains(searchText));
                }


                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = CmsProjectes;
                var output = await result.OrderByDescending(r => r.Id).ToPagedListAsync(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }

                        var transProjectCategory = item.ProjectCategory?.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transProjectCategory != null)
                        {
                            item.ProjectCategory.Name = transProjectCategory.Name;
                            item.ProjectCategory.Description = transProjectCategory.Description;
                        }
                    }
                }
                return output;
            }
        }

        public List<CmsProject> GetProjectForHome(int languageId)
        {
            var CmsProjectes = _context.CmsProjects.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.ShowInHomePage == true
            && r.PublishDate <= DateTime.Now && r.EndDate >= DateTime.Now).Include(r => r.CmsProjectTranslations);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in CmsProjectes)
                {
                    var trans = item.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Name = trans.Name;
                        item.Description = trans.Description;
                    }
                }

            return CmsProjectes.OrderBy(r => r.SortOrder).ToList();
        }

        public CmsProject GetCmsProjectById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProject = db.CmsProjects.Include(r => r.CmsProjectCosts.Where(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted)).Include(r => r.CmsProjectDonors).Include(r => r.CmsProjectResources).FirstOrDefault(r => r.Id == id);
                return CmsProject;
            }
        }
        public CmsProjectViewModel GetCmsProjectById(int id, int languageId)
        {
            var CmsProject = _context.CmsProjects.Include(r => r.CmsProjectTranslations).Include(r => r.CmsProjectResources)
                .Include(r => r.CmsProjectCosts.Where(s=>s.Status!=(int)GeneralEnums.StatusEnum.Deleted)).Include(r => r.CmsProjectDonors)
                .Include(r => r.ProjectCategory.CmsProjectCategoryTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (CmsProject != null && languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in CmsProject.CmsProjectCosts)
                {
                    var trans = _context.CmsProjectCostTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.ProjectCostId == item.Id);
                    if (trans != null)
                        item.Name = trans.Name;
                }
                if (CmsProject.ProjectCategory.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId) != null)
                    CmsProject.ProjectCategory.Name = CmsProject.ProjectCategory.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId).Name;

                var tran = CmsProject.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (tran != null)
                {
                    CmsProject.Name = tran.Name;
                    CmsProject.Description = tran.Description;
                    CmsProject.SecondDescription = tran.SecondDescription;
                    CmsProject.Keyword = tran.Keyword;
                }
            }

            return new CmsProjectViewModel(CmsProject);
        }

        public List<CmsProjectCost> GetCmsProjectCosts(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProjectCosts = db.CmsProjectCosts.Include(r => r.CmsProjectCostTranslations).Where(r => r.ProjectId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                foreach (var item in CmsProjectCosts)
                {
                    var trans = item.CmsProjectCostTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }

                return CmsProjectCosts.ToList();
            }
        }

        public void AddCmsProject(CmsProjectViewModel CmsProjectViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProject = new CmsProject()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = CmsProjectViewModel.Name,
                    Description = CmsProjectViewModel.Description,
                    Keyword = CmsProjectViewModel.Keyword,
                    ShortDescription = CmsProjectViewModel.ShortDescription,
                    CreatedBy = CmsProjectViewModel.CreatedBy,
                    ImageUrl = CmsProjectViewModel.ImageUrl,
                    PublishDate = CmsProjectViewModel.PublishDate,
                    EndDate = CmsProjectViewModel.EndDate,
                    SortOrder = CmsProjectViewModel.SortOrder,
                    ShowInHomePage = CmsProjectViewModel.ShowInHomePage,
                    IsFeatured = CmsProjectViewModel.IsFeatured,
                    PaymentType = CmsProjectViewModel.PaymentType,
                    ProjectCost = CmsProjectViewModel.ProjectCost,
                    OneObjectFees = CmsProjectViewModel.OneObjectFees,
                    ProjectStatus = CmsProjectViewModel.ProjectStatus,
                    ProjectCategoryId = CmsProjectViewModel.ProjectCategoryId,
                    SecondDescription = CmsProjectViewModel.SecondDescription,

                };
                db.CmsProjects.Add(CmsProject);
                db.SaveChanges();

                CmsProject.Id = CmsProject.Id;

                if (CmsProjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CmsProjectTran = new CmsProjectTranslation()
                    {
                        Name = CmsProjectViewModel.Name,
                        Description = CmsProjectViewModel.Description,
                        Keyword = CmsProjectViewModel.Keyword,
                        ShortDescription = CmsProjectViewModel.ShortDescription,
                        SecondDescription = CmsProjectViewModel.SecondDescription,
                        LanguageId = CmsProjectViewModel.LanguageId,
                        ProjectId = CmsProject.Id
                    };
                    db.CmsProjectTranslations.Add(CmsProjectTran);
                    db.SaveChanges();
                }

            }
        }


        public void EditCmsProject(CmsProjectViewModel CmsProjectViewModel, CmsProject CmsProject)
        {
            using (var db = new LearningManagementSystemContext())
            {
                CmsProject.ImageUrl = CmsProjectViewModel.ImageUrl;
                CmsProject.PublishDate = CmsProjectViewModel.PublishDate;
                CmsProject.EndDate = CmsProjectViewModel.EndDate;
                CmsProject.SortOrder = CmsProjectViewModel.SortOrder;
                CmsProject.ShowInHomePage = CmsProjectViewModel.ShowInHomePage;
                CmsProject.IsFeatured = CmsProjectViewModel.IsFeatured;
                CmsProject.PaymentType = CmsProjectViewModel.PaymentType;
                CmsProject.ProjectCost = CmsProjectViewModel.ProjectCost;
                CmsProject.OneObjectFees = CmsProjectViewModel.OneObjectFees;
                CmsProject.ProjectStatus = CmsProjectViewModel.ProjectStatus;
                CmsProject.ProjectCategoryId = CmsProjectViewModel.ProjectCategoryId;
                CmsProject.Status = CmsProjectViewModel.Status;

                if (CmsProjectViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    CmsProject.Name = CmsProjectViewModel.Name;
                    CmsProject.Description = CmsProjectViewModel.Description;
                    CmsProject.Keyword = CmsProjectViewModel.Keyword;
                    CmsProject.ShortDescription = CmsProjectViewModel.ShortDescription;
                    CmsProject.SecondDescription = CmsProjectViewModel.SecondDescription;
                }

                db.Entry(CmsProject).State = EntityState.Modified;
                db.SaveChanges();
                if (CmsProjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CmsProjectTranslation = db.CmsProjectTranslations.FirstOrDefault(r =>
                        r.LanguageId == CmsProjectViewModel.LanguageId &&
                        r.ProjectId == CmsProjectViewModel.Id);
                    if (CmsProjectTranslation != null)
                    {
                        CmsProjectTranslation.Name = CmsProjectViewModel.Name;
                        CmsProjectTranslation.Description = CmsProjectViewModel.Description;
                        CmsProjectTranslation.Keyword = CmsProjectViewModel.Keyword;
                        CmsProjectTranslation.ShortDescription = CmsProjectViewModel.ShortDescription;
                        CmsProjectTranslation.SecondDescription = CmsProjectViewModel.SecondDescription;

                        db.Entry(CmsProjectTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var CmsProjectTran = new CmsProjectTranslation()
                        {
                            Name = CmsProjectViewModel.Name,
                            Description = CmsProjectViewModel.Description,
                            LanguageId = CmsProjectViewModel.LanguageId,
                            Keyword = CmsProjectViewModel.Keyword,
                            ShortDescription = CmsProjectViewModel.ShortDescription,
                            SecondDescription = CmsProjectViewModel.SecondDescription,
                            ProjectId = CmsProjectViewModel.Id
                        };
                        db.CmsProjectTranslations.Add(CmsProjectTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsProject(CmsProject CmsProject)
        {
            using (var db = new LearningManagementSystemContext())
            {
                CmsProject.Status = (int)GeneralEnums.StatusEnum.Deleted;
                CmsProject.DeletedOn = DateTime.Now;
                db.Entry(CmsProject).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<CmsProject> GetCmsProjectsList(int languageId)
        {
            var CmsProjectes = _context.CmsProjects.Include(r => r.CmsProjectTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in CmsProjectes)
                {
                    var trans = item.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Name = trans.Name;
                        item.Description = trans.Description;
                        item.SecondDescription = trans.SecondDescription;
                    }
                }

            return CmsProjectes.OrderBy(r => r.SortOrder).ToList();
        }

        public void EditResources(CmsProject project, CmsProjectResourcesViewModel viewModel)
        {
            _context.CmsProjectResources.RemoveRange(project.CmsProjectResources);
            _context.SaveChanges();

            foreach (var item in viewModel.Link)
            {
                var resorces = new CmsProjectResource()
                {
                    ProjectId = project.Id,
                    Link = item,
                };
                _context.CmsProjectResources.Add(resorces);
            }

            _context.SaveChanges();
        }

        public void EditProjectCosts(CmsProjectCostsViewModel costs)
        {
            var projectCost = _context.CmsProjectCosts.Where(r => r.ProjectId == costs.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            var list = projectCost.Where(p => !costs.CmsProjectCosts.Select(s => s.Id).Contains(p.Id)).ToList();
            foreach (var item in list)
            {
                item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                _context.Entry(item).State = EntityState.Modified;
            }

            foreach (var item in costs.CmsProjectCosts)
            {
                var cost = _context.CmsProjectCosts.Include(r => r.CmsProjectCostTranslations).FirstOrDefault(r => r.Id == item.Id);
                if (cost != null)
                {
                    cost.Cost = (item.IsOther.Value ? 0 : item.Cost);
                    cost.IsOther = item.IsOther;
                    if (costs.LanguageId == CultureHelper.GetDefaultLanguageId())
                        cost.Name = item.Name;

                    _context.Entry(cost).State = EntityState.Modified;
                    if (costs.LanguageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var costTranslation = cost.CmsProjectCostTranslations.FirstOrDefault(r => r.LanguageId == costs.LanguageId);
                        if (costTranslation != null)
                        {
                            costTranslation.Name = item.Name;
                            _context.Entry(costTranslation).State = EntityState.Modified;
                        }
                        else
                        {
                            var costTran = new CmsProjectCostTranslation()
                            {
                                Name = item.Name,
                                LanguageId = costs.LanguageId,
                                ProjectCostId = cost.Id
                            };
                            _context.CmsProjectCostTranslations.Add(costTran);
                        }
                    }
                }
                else
                {
                    var resorces = new CmsProjectCost()
                    {
                        ProjectId = costs.Id,
                        Name = item.Name,
                        Cost = (item.IsOther.Value ? 0 : item.Cost),
                        IsOther = item.IsOther,
                        CreatedOn = DateTime.Now,
                        CreatedBy = costs.CreatedBy,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                    };
                    _context.CmsProjectCosts.Add(resorces);
                }
            }
            _context.SaveChanges();
        }
        public void AddProjectDonars(DonationViewModel donation)
        {
            var donan = new CmsProjectDonor()
            {
                Cost = donation.Cost,
                ProjectCostId = donation.Item,
                Email = donation.Email,
                Name = donation.Name,
                CreatedBy = donation.Email,
                CreatedOn = DateTime.Now,
                Mobile = donation.Phone,
                ProjectId = donation.Id,
            };
            _context.CmsProjectDonors.Add(donan);
            _context.SaveChanges();
        }
    }
}