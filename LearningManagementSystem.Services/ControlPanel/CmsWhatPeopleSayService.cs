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
    public class CmsWhatPeopleSayService : ICmsWhatPeopleSayService
    {
        private readonly ISettingService _settingService;
        public CmsWhatPeopleSayService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CmsWhatPeopleSay> GetCmsWhatPeopleSays(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsWhatPeopleSays = db.CmsWhatPeopleSays.Include(r => r.CmsWhatPeopleSayTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    cmsWhatPeopleSays = cmsWhatPeopleSays.Where(r => r.PersonName.Contains(searchText) || r.PersonDetails.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        cmsWhatPeopleSays = cmsWhatPeopleSays.Where(r => r.PersonName.Contains(searchText) || r.PersonDetails.Contains(searchText));
                    }
                    else
                    {
           
                        cmsWhatPeopleSays = cmsWhatPeopleSays.Where(r => r.CmsWhatPeopleSayTranslations.Any(t => (t.PersonName.Contains(searchText) || t.PersonDetails.Contains(searchText)) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cmsWhatPeopleSays;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsWhatPeopleSayTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.PersonName = trans.PersonName;
                            item.PersonDetails = trans.PersonDetails;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public CmsWhatPeopleSay GetCmsWhatPeopleSayById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsWhatPeopleSay = db.CmsWhatPeopleSays.FirstOrDefault(x => x.Id == id);
                return cmsWhatPeopleSay;
            }
        }
        public CmsWhatPeopleSayViewModel GetCmsWhatPeopleSayById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CmsWhatPeopleSayTranslations.Include(r => r.People).FirstOrDefault(r => r.LanguageId == languageId && r.PeopleId == id);
                    if (aboutTran != null)
                    {
                        return new CmsWhatPeopleSayViewModel(aboutTran);
                    }
                }
                var cmsWhatPeopleSay = db.CmsWhatPeopleSays.FirstOrDefault(x => x.Id == id);
                return new CmsWhatPeopleSayViewModel(cmsWhatPeopleSay);
            }
        }

        public void AddCmsWhatPeopleSay(CmsWhatPeopleSayViewModel cmsWhatPeopleSayViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsWhatPeopleSay = new CmsWhatPeopleSay()
                {
                    CreatedOn = DateTime.Now,
                    Status = cmsWhatPeopleSayViewModel.Status,
                    PersonName = cmsWhatPeopleSayViewModel.PersonName,
                    PersonDetails = cmsWhatPeopleSayViewModel.PersonDetails,
                    Description = cmsWhatPeopleSayViewModel.Description,
                    ImageUrl = cmsWhatPeopleSayViewModel.ImageUrl,
                    ShowInHomePage = cmsWhatPeopleSayViewModel.ShowInHomePage,
                    CreatedBy = cmsWhatPeopleSayViewModel.CreatedBy,
                };
                db.CmsWhatPeopleSays.Add(cmsWhatPeopleSay);
                db.SaveChanges();

                cmsWhatPeopleSay.Id = cmsWhatPeopleSay.Id;

                if (cmsWhatPeopleSayViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmsWhatPeopleSayTran = new CmsWhatPeopleSayTranslation()
                    {
                        PersonName = cmsWhatPeopleSayViewModel.PersonName,
                        PersonDetails = cmsWhatPeopleSayViewModel.PersonDetails,
                        LanguageId = cmsWhatPeopleSayViewModel.LanguageId,
                        Description = cmsWhatPeopleSayViewModel.Description,
                        PeopleId = cmsWhatPeopleSay.Id
                    };
                    db.CmsWhatPeopleSayTranslations.Add(cmsWhatPeopleSayTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCmsWhatPeopleSay(CmsWhatPeopleSayViewModel cmsWhatPeopleSayViewModel, CmsWhatPeopleSay cmsWhatPeopleSay)
        {
            using (var db = new LearningManagementSystemContext())
            {

                cmsWhatPeopleSay.ImageUrl = cmsWhatPeopleSayViewModel.ImageUrl;
                cmsWhatPeopleSay.ShowInHomePage = cmsWhatPeopleSayViewModel.ShowInHomePage;
                cmsWhatPeopleSay.Status = cmsWhatPeopleSayViewModel.Status;

                if (cmsWhatPeopleSayViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    cmsWhatPeopleSay.PersonName = cmsWhatPeopleSayViewModel.PersonName;
                    cmsWhatPeopleSay.PersonDetails = cmsWhatPeopleSayViewModel.PersonDetails;
                    cmsWhatPeopleSay.Description = cmsWhatPeopleSayViewModel.Description;
                }

                db.Entry(cmsWhatPeopleSay).State = EntityState.Modified;
                db.SaveChanges();
                if (cmsWhatPeopleSayViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmsWhatPeopleSayTranslation = db.CmsWhatPeopleSayTranslations.FirstOrDefault(r =>
                        r.LanguageId == cmsWhatPeopleSayViewModel.LanguageId &&
                        r.PeopleId == cmsWhatPeopleSayViewModel.Id);
                    if (cmsWhatPeopleSayTranslation != null)
                    {
                        cmsWhatPeopleSayTranslation.PersonName = cmsWhatPeopleSayViewModel.PersonName;
                        cmsWhatPeopleSayTranslation.PersonDetails = cmsWhatPeopleSayViewModel.PersonDetails;
                        cmsWhatPeopleSayTranslation.Description = cmsWhatPeopleSayViewModel.Description;
                        db.Entry(cmsWhatPeopleSayTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var cmsWhatPeopleSayTran = new CmsWhatPeopleSayTranslation()
                        {
                            PersonName = cmsWhatPeopleSayViewModel.PersonName,
                            PersonDetails=cmsWhatPeopleSayViewModel.PersonDetails,
                            LanguageId = cmsWhatPeopleSayViewModel.LanguageId,
                            Description = cmsWhatPeopleSayViewModel.Description,
                            PeopleId = cmsWhatPeopleSayViewModel.Id
                        };
                        db.CmsWhatPeopleSayTranslations.Add(cmsWhatPeopleSayTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsWhatPeopleSay(CmsWhatPeopleSay cmsWhatPeopleSay)
        {
            using (var db = new LearningManagementSystemContext())
            {
                cmsWhatPeopleSay.Status = (int)GeneralEnums.StatusEnum.Deleted;
                cmsWhatPeopleSay.DeletedOn = DateTime.Now;
                db.Entry(cmsWhatPeopleSay).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public async Task<List<CmsWhatPeopleSay>> GetAllCmsWhatPeopleSays(bool showInHome,int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmsWhatPeopleSays = db.CmsWhatPeopleSays.Include(r => r.CmsWhatPeopleSayTranslations).Where(r =>
                      r.Status == (int)GeneralEnums.StatusEnum.Active &&  (showInHome ? r.ShowInHomePage == true : 1 == 1));

                var result = cmsWhatPeopleSays;
                var output =await result.OrderByDescending(r => r.Id).ToListAsync();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsWhatPeopleSayTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.PersonName = trans.PersonName;
                            item.PersonDetails = trans.PersonDetails;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
    }
}
