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
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CmsSliderService : ICmsSliderService
    {
        private readonly ISettingService _settingService;
        public CmsSliderService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CmsSlider> GetCmsSliders(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English,int pagination=25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmssliders = db.CmsSliders.Include(r=>r.CmsSliderTranslations).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    cmssliders = cmssliders.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        cmssliders = cmssliders.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
         
                        cmssliders = cmssliders.Where(r => r.CmsSliderTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cmssliders;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsSliderTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }

        public async Task<List<CmsSlider>> GetPublicSliders( int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmssliders = db.CmsSliders.Include(r => r.CmsSliderTranslations).Where(r =>
                      r.Status == (int)GeneralEnums.StatusEnum.Active);

              
                var result = cmssliders;
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in result)
                    {
                        var trans = item.CmsSliderTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }
                }
                return await result.OrderBy(r=>r.SortOrder).ToListAsync();
            }
        }
        public CmsSlider GetCmsSliderById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsslider = db.CmsSliders.Find(id);
                return cmsslider;
            }
        }
        public CmsSliderViewModel GetCmsSliderById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CmsSliderTranslations.Include(r => r.Slider).FirstOrDefault(r => r.LanguageId == languageId && r.SliderId == id);
                    if (aboutTran != null)
                    {
                        return new CmsSliderViewModel(aboutTran);
                    }
                }
                var cmsslider = db.CmsSliders.Find(id);
                return new CmsSliderViewModel(cmsslider);
            }
        }

        public void AddCmsSlider(CmsSliderViewModel cmssliderViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsslider = new CmsSlider()
                {
                    CreatedOn = DateTime.Now,
                    Status = cmssliderViewModel.Status,
                    Name = cmssliderViewModel.Name,
                    Description = cmssliderViewModel.Description,
                    ImageUrl= cmssliderViewModel.ImageUrl,
                    Image2Url = cmssliderViewModel.Image2Url,
                    ReadMoreLink = cmssliderViewModel.ReadMoreLink,
                    SortOrder = cmssliderViewModel.SortOrder,
                    CreatedBy = cmssliderViewModel.CreatedBy,
                };
                db.CmsSliders.Add(cmsslider);
                db.SaveChanges();

                cmsslider.Id = cmsslider.Id;

                if (cmssliderViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmssliderTran = new CmsSliderTranslation()
                    {
                        Name = cmssliderViewModel.Name,
                        LanguageId = cmssliderViewModel.LanguageId,
                        Description= cmssliderViewModel.Description,
                        SliderId = cmsslider.Id
                    };
                    db.CmsSliderTranslations.Add(cmssliderTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCmsSlider(CmsSliderViewModel cmssliderViewModel, CmsSlider cmsslider)
        {
            using (var db = new LearningManagementSystemContext())
            {
               
                cmsslider.ImageUrl = cmssliderViewModel.ImageUrl;
                cmsslider.Image2Url = cmssliderViewModel.Image2Url;
                cmsslider.ReadMoreLink = cmssliderViewModel.ReadMoreLink;
                cmsslider.SortOrder = cmssliderViewModel.SortOrder;
                cmsslider.Status = cmssliderViewModel.Status;

                if (cmssliderViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    cmsslider.Name = cmssliderViewModel.Name;
                    cmsslider.Description = cmssliderViewModel.Description;
                }

                db.Entry(cmsslider).State = EntityState.Modified;
                db.SaveChanges();
                if (cmssliderViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmssliderTranslation = db.CmsSliderTranslations.FirstOrDefault(r =>
                        r.LanguageId == cmssliderViewModel.LanguageId &&
                        r.SliderId == cmssliderViewModel.Id);
                    if (cmssliderTranslation != null)
                    {
                        cmssliderTranslation.Name = cmssliderViewModel.Name;
                        cmssliderTranslation.Description = cmssliderViewModel.Description;
                        db.Entry(cmssliderTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var cmssliderTran = new CmsSliderTranslation()
                        {
                            Name = cmssliderViewModel.Name,
                            LanguageId = cmssliderViewModel.LanguageId,
                            Description= cmssliderViewModel.Description,
                            SliderId = cmssliderViewModel.Id
                        };
                        db.CmsSliderTranslations.Add(cmssliderTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsSlider(CmsSlider cmsslider)
        {
            using (var db = new LearningManagementSystemContext())
            {
                cmsslider.Status = (int)GeneralEnums.StatusEnum.Deleted;
                cmsslider.DeletedOn = DateTime.Now;
                db.Entry(cmsslider).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
