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
    public class CmsCateryService : ICmsCateryService
    {
        private readonly ISettingService _settingService;
        public CmsCateryService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CmsCatery> GetCmsCaterys(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English,int pagination=25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmscaterys = db.CmsCateries.Include(r=>r.CmsCateryTranslations).Include(d => d.Parent).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    cmscaterys = cmscaterys.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        cmscaterys = cmscaterys.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        cmscaterys = cmscaterys.Where(r => r.CmsCateryTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cmscaterys;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsCateryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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
        public CmsCatery GetCmsCateryById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmscatery = db.CmsCateries.Include(r => r.Parent).FirstOrDefault(x => x.Id == id);
                return cmscatery;
            }
        }
        public CmsCateryViewModel GetCmsCateryById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CmsCateryTranslations.Include(r => r.Catery).ThenInclude(p => p.Parent).FirstOrDefault(r => r.LanguageId == languageId && r.CateryId == id);
                    if (aboutTran != null)
                    {
                        return new CmsCateryViewModel(aboutTran);
                    }
                }
                var cmscatery = db.CmsCateries.Include(r => r.Parent).FirstOrDefault(x => x.Id == id);
                return new CmsCateryViewModel(cmscatery);
            }
        }

        public void AddCmsCatery(CmsCateryViewModel cmscateryViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmscatery = new CmsCatery()
                {
                    CreatedOn = DateTime.Now,
                    Status = cmscateryViewModel.Status,
                    Name = cmscateryViewModel.Name,
                    Description = cmscateryViewModel.Description,
                    ImageUrl= cmscateryViewModel.ImageUrl,
                    ParentId= (cmscateryViewModel.ParentId == 0)? (int?)null : cmscateryViewModel.ParentId,
                    ShowInHomePage= cmscateryViewModel.ShowInHomePage,
                    CreatedBy = cmscateryViewModel.CreatedBy,
                };
                db.CmsCateries.Add(cmscatery);
                db.SaveChanges();

                cmscatery.Id = cmscatery.Id;

                if (cmscateryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmscateryTran = new CmsCateryTranslation()
                    {
                        Name = cmscateryViewModel.Name,
                        LanguageId = cmscateryViewModel.LanguageId,
                        Description= cmscateryViewModel.Description,
                        CateryId = cmscatery.Id
                    };
                    db.CmsCateryTranslations.Add(cmscateryTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCmsCatery(CmsCateryViewModel cmscateryViewModel, CmsCatery cmscatery)
        {
            using (var db = new LearningManagementSystemContext())
            {
               
                cmscatery.ImageUrl = cmscateryViewModel.ImageUrl;
                cmscatery.ParentId = (cmscateryViewModel.ParentId == 0) ? (int?)null : cmscateryViewModel.ParentId;
                cmscatery.ShowInHomePage = cmscateryViewModel.ShowInHomePage;
                cmscatery.Status = cmscateryViewModel.Status;

                if (cmscateryViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    cmscatery.Name = cmscateryViewModel.Name;
                    cmscatery.Description = cmscateryViewModel.Description;
                }

                db.Entry(cmscatery).State = EntityState.Modified;
                db.SaveChanges();
                if (cmscateryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var cmscateryTranslation = db.CmsCateryTranslations.FirstOrDefault(r =>
                        r.LanguageId == cmscateryViewModel.LanguageId &&
                        r.CateryId == cmscateryViewModel.Id);
                    if (cmscateryTranslation != null)
                    {
                        cmscateryTranslation.Name = cmscateryViewModel.Name;
                        cmscateryTranslation.Description = cmscateryViewModel.Description;
                        db.Entry(cmscateryTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var cmscateryTran = new CmsCateryTranslation()
                        {
                            Name = cmscateryViewModel.Name,
                            LanguageId = cmscateryViewModel.LanguageId,
                            Description= cmscateryViewModel.Description,
                            CateryId = cmscateryViewModel.Id
                        };
                        db.CmsCateryTranslations.Add(cmscateryTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCmsCatery(CmsCatery cmscatery)
        {
            using (var db = new LearningManagementSystemContext())
            {
                cmscatery.Status = (int)GeneralEnums.StatusEnum.Deleted;
                cmscatery.DeletedOn = DateTime.Now;
                db.Entry(cmscatery).State = EntityState.Modified;
                db.SaveChanges();
                if(cmscatery.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    DeletedParentIDCmsCatery(cmscatery.Id);
                }
            }
        }
        public void DeletedParentIDCmsCatery(int DeletedCmsCateryID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CmsCateriesList = db.CmsCateries.Where(x => x.ParentId == DeletedCmsCateryID).ToList();
                foreach (var CmsCate in CmsCateriesList)
                {
                    CmsCate.ParentId = null;
                    db.Entry(CmsCate).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public List<CmsCatery> GetAllCmsCaterys(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cmscaterys = db.CmsCateries.Include(r => r.CmsCateryTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                var result = cmscaterys;
                var output = result.OrderByDescending(r => r.Id).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CmsCateryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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
    }
}
