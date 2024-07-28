using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CmsCategoryProjectService : ICmsCategoryProjectService
    {
        private readonly ISettingService _settingService;

        public CmsCategoryProjectService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CmsProjectCategory> GetCmsCategoryProjects(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProjectes = db.CmsProjectCategories.Include(r => r.CmsProjectCategoryTranslations).Where(r =>
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
                  
                        CmsProjectes = CmsProjectes.Where(r => r.CmsProjectCategoryTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
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
                        var trans = item.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description=trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public CmsProjectCategory GetCmsCategoryProjectById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsProjectCategory = db.CmsProjectCategories.Find(id);
                return CmsProjectCategory;
            }
        }
        public CmsCategoryProjectViewModel GetCmsCategoryProjectById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran = db.CmsProjectCategoryTranslations.Include(r => r.ProjectCategory).FirstOrDefault(r => r.LanguageId == languageId && r.ProjectCategoryId == id);
                    if (aboutTran != null)
                    {
                        return new CmsCategoryProjectViewModel(aboutTran);
                    }
                }
                var CmsProjectCategory = db.CmsProjectCategories.Find(id);
                return new CmsCategoryProjectViewModel(CmsProjectCategory);
            }
        }

        public void AddCmsCategoryProject(CmsCategoryProjectViewModel CmsCategoryProjectViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {


                var CmsProjectCategory = new CmsProjectCategory()
                {
                    CreatedOn = DateTime.Now,
                    Status = CmsCategoryProjectViewModel.Status,
                    Name = CmsCategoryProjectViewModel.Name,
                    Description = CmsCategoryProjectViewModel.Description,
                    CreatedBy = CmsCategoryProjectViewModel.CreatedBy,

                };
                db.CmsProjectCategories.Add(CmsProjectCategory);
                db.SaveChanges();

                CmsProjectCategory.Id = CmsProjectCategory.Id;

                if (CmsCategoryProjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CmsProjectTran = new CmsProjectCategoryTranslation()
                    {
                        Name = CmsCategoryProjectViewModel.Name,
                        Description = CmsCategoryProjectViewModel.Description,
                        LanguageId = CmsCategoryProjectViewModel.LanguageId,
                        ProjectCategoryId = CmsProjectCategory.Id
                    };
                    db.CmsProjectCategoryTranslations.Add(CmsProjectTran);
                    db.SaveChanges();
                }

            }
        }


        public void EditCmsCategoryProject(CmsCategoryProjectViewModel CmsCategoryProjectViewModel, CmsProjectCategory CmsProjectCategory)
        {
            using (var db = new LearningManagementSystemContext())
            {
                CmsProjectCategory.Status = CmsCategoryProjectViewModel.Status;

                if (CmsCategoryProjectViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    CmsProjectCategory.Name = CmsCategoryProjectViewModel.Name;
                    CmsProjectCategory.Description = CmsCategoryProjectViewModel.Description;

                }

                db.Entry(CmsProjectCategory).State = EntityState.Modified;
                db.SaveChanges();
                if (CmsCategoryProjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CmsProjectCategoryTranslation = db.CmsProjectCategoryTranslations.FirstOrDefault(r =>
                        r.LanguageId == CmsCategoryProjectViewModel.LanguageId &&
                        r.ProjectCategoryId == CmsCategoryProjectViewModel.Id);
                    if (CmsProjectCategoryTranslation != null)
                    {
                        CmsProjectCategoryTranslation.Name = CmsCategoryProjectViewModel.Name;
                        CmsProjectCategoryTranslation.Description = CmsCategoryProjectViewModel.Description;

                        db.Entry(CmsProjectCategoryTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var CmsProjectTran = new CmsProjectCategoryTranslation()
                        {
                            Name = CmsCategoryProjectViewModel.Name,
                            Description = CmsCategoryProjectViewModel.Description,
                            LanguageId = CmsCategoryProjectViewModel.LanguageId,
                            ProjectCategoryId = CmsCategoryProjectViewModel.Id
                        };
                        db.CmsProjectCategoryTranslations.Add(CmsProjectTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsCategoryProject(CmsProjectCategory cmsProjectCategory)
        {
            using (var db = new LearningManagementSystemContext())
            {
                cmsProjectCategory.Status = (int)GeneralEnums.StatusEnum.Deleted;
                cmsProjectCategory.DeletedOn = DateTime.Now;
                db.Entry(cmsProjectCategory).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}