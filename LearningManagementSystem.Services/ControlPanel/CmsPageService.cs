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
    public class CmsPageService : ICmsPageService
    {
        private readonly ISettingService _settingService;
        public CmsPageService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CmsPage> GetCmsPages(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspagess = db.CmsPages.Include(r => r.CmsPageTranslations).Include(b => b.Catery).Where(r =>
                        r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    cmspagess = cmspagess.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        cmspagess = cmspagess.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {

                        cmspagess = cmspagess.Where(r => r.CmsPageTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cmspagess;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsPageTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }
                return output;
            }
        }

        public IPagedList<CmsPage> GetActiveCmsPages(string searchText, int categoryId, DateTime? fromDate, DateTime? toDate, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspagess = db.CmsPages.Include(r => r.CmsPageTranslations).Include(b => b.Catery).Where(r =>
                        r.Status == (int)GeneralEnums.StatusEnum.Active && r.CateryId == categoryId);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                        cmspagess = cmspagess.Where(r => r.Name.Contains(searchText));
                    else
                        cmspagess = cmspagess.Where(r => r.CmsPageTranslations.FirstOrDefault(r=>r.LanguageId == languageId).Name.Contains(searchText));
                }

                if (fromDate != default)
                {
                    cmspagess = cmspagess.Where(r => r.PublishDate >= fromDate);
                }

                if (toDate != default)
                {
                    cmspagess = cmspagess.Where(r => r.PublishDate <= toDate);
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cmspagess;
                var output = result.OrderByDescending(r => r.SortOrder).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsPageTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.ShortDescription = trans.ShortDescription;
                            item.Description = trans.Description;

                        }
                    }
                }
                return output;
            }
        }

        public List<CmsPage> GetCmsPageForHome(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspages = db.CmsPages.Include(b => b.Catery).Where(r => r.ShowInHomePage == true && r.Status == (int)GeneralEnums.StatusEnum.Active && (r.Catery.Name == "Blogs" || r.Catery.Name == "News")).Include(r => r.CmsPageTranslations);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in cmspages)
                    {
                        var trans = item.CmsPageTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.ShortDescription = trans.ShortDescription;
                            item.Description = trans.Description;

                        }
                    }
                }
                return cmspages.ToList();
            }
        }


        public CmsPage GetCmsPageById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspages = db.CmsPages.Include(b => b.Catery).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return cmspages;
            }
        }


        public CmsPage GetActiveCmsPageById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspage = db.CmsPages.Include(r => r.CmsPageTranslations).Include(b => b.Catery).FirstOrDefault(r => r.Id == id && r.Status == (int)GeneralEnums.StatusEnum.Active);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {

                    var trans = cmspage.CmsPageTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        cmspage.Name = trans.Name;
                        cmspage.ShortDescription = trans.ShortDescription;
                        cmspage.Description = trans.Description;
                    }

                }

                return cmspage;

            }
        }

        public CmsPageViewModel GetCmsPageById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CmsPageTranslations.Include(r => r.Page).ThenInclude(b => b.Catery).FirstOrDefault(r => r.LanguageId == languageId && r.PageId == id && r.Page.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (aboutTran != null)
                    {
                        return new CmsPageViewModel(aboutTran);
                    }
                }
                var cmspages = db.CmsPages.Include(r => r.Catery).FirstOrDefault(b => b.Id == id && b.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return new CmsPageViewModel(cmspages);
            }
        }

        public void AddCmsPage(CmsPageViewModel cmspagesViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmspages = new CmsPage()
                {
                    CreatedOn = DateTime.Now,
                    Status = cmspagesViewModel.Status,
                    Name = cmspagesViewModel.Name,
                    Description = cmspagesViewModel.Description,
                    ShortDescription = cmspagesViewModel.ShortDescription,
                    ImageUrl = cmspagesViewModel.ImageUrl,
                    SortOrder = cmspagesViewModel.SortOrder,
                    AllowComment = cmspagesViewModel.AllowComment,
                    IsFeatured = cmspagesViewModel.IsFeatured,
                    ShowInHomePage = cmspagesViewModel.ShowInHomePage,
                    Keyword = cmspagesViewModel.Keyword,
                    CateryId = (cmspagesViewModel.CateryId == 0) ? (int?)null : cmspagesViewModel.CateryId,
                    CreatedBy = cmspagesViewModel.CreatedBy,
                    PublishDate = cmspagesViewModel.PublishDate,
                    EndDate = cmspagesViewModel.EndDate
                };
                db.CmsPages.Add(cmspages);
                db.SaveChanges();

                cmspages.Id = cmspages.Id;

                if (cmspagesViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmspagesTran = new CmsPageTranslation()
                    {
                        Name = cmspagesViewModel.Name,
                        LanguageId = cmspagesViewModel.LanguageId,
                        Description = cmspagesViewModel.Description,
                        ShortDescription = cmspagesViewModel.ShortDescription,
                        PageId = cmspages.Id
                    };
                    db.CmsPageTranslations.Add(cmspagesTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCmsPage(CmsPageViewModel cmspagesViewModel, CmsPage cmspages)
        {
            using (var db = new LearningManagementSystemContext())
            {

                cmspages.ImageUrl = cmspagesViewModel.ImageUrl;
                cmspages.AllowComment = cmspagesViewModel.AllowComment;
                cmspages.IsFeatured = cmspagesViewModel.IsFeatured;
                cmspages.ShowInHomePage = cmspagesViewModel.ShowInHomePage;
                cmspages.Keyword = cmspagesViewModel.Keyword;
                cmspages.CateryId = (cmspagesViewModel.CateryId == 0) ? (int?)null : cmspagesViewModel.CateryId;
                cmspages.SortOrder = cmspagesViewModel.SortOrder;
                cmspages.PublishDate = cmspagesViewModel.PublishDate;
                cmspages.EndDate = cmspagesViewModel.EndDate;
                cmspages.Status = cmspagesViewModel.Status;

                if (cmspagesViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    cmspages.Name = cmspagesViewModel.Name;
                    cmspages.Description = cmspagesViewModel.Description;
                    cmspages.ShortDescription = cmspagesViewModel.ShortDescription;
                }

                db.Entry(cmspages).State = EntityState.Modified;
                db.SaveChanges();
                if (cmspagesViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmspagesTranslation = db.CmsPageTranslations.FirstOrDefault(r =>
                        r.LanguageId == cmspagesViewModel.LanguageId &&
                        r.PageId == cmspagesViewModel.Id);
                    if (cmspagesTranslation != null)
                    {
                        cmspagesTranslation.Name = cmspagesViewModel.Name;
                        cmspagesTranslation.Description = cmspagesViewModel.Description;
                        cmspagesTranslation.ShortDescription = cmspagesViewModel.ShortDescription;
                        db.Entry(cmspagesTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var cmspagesTran = new CmsPageTranslation()
                        {
                            Name = cmspagesViewModel.Name,
                            LanguageId = cmspagesViewModel.LanguageId,
                            Description = cmspagesViewModel.Description,
                            ShortDescription = cmspagesViewModel.ShortDescription,
                            PageId = cmspagesViewModel.Id
                        };
                        db.CmsPageTranslations.Add(cmspagesTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsPage(CmsPage cmspages)
        {
            using (var db = new LearningManagementSystemContext())
            {
                cmspages.Status = (int)GeneralEnums.StatusEnum.Deleted;
                cmspages.DeletedOn = DateTime.Now;
                db.Entry(cmspages).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
