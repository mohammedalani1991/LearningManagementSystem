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
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class AboutDicService : IAboutDicService
    {
        private readonly ISettingService _settingService;
        public AboutDicService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<AboutDic> GetAboutDic(string searchText, int? page)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var abouts = db.AboutDics.Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    abouts = abouts.Where(r => r.Name.Contains(searchText));
                }

                var result = abouts;

                var pageSize = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = (page ?? 1);


                return result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            }

        }
        public AboutDic GetAboutDicById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var aboutDic = db.AboutDics.Find(id);
                return aboutDic;
            }
        }
        public AboutDicViewModel GetAboutDicById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.AboutDicTranslations.Include(r => r.AboutDic).FirstOrDefault(r => r.LanguageId == languageId && r.AboutDicId == id);
                    if (aboutTran != null)
                    {
                        return new AboutDicViewModel(aboutTran);
                    }
                }
                var aboutDic = db.AboutDics.Find(id);
                return new AboutDicViewModel(aboutDic);
            }
        }

        public AboutDicViewModel GetAboutDicByCode(string code, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.AboutDicTranslations.Include(r => r.AboutDic).FirstOrDefault(r => r.LanguageId == languageId && r.AboutDic.GroupName == code && r.AboutDic.Status==(int)(int)GeneralEnums.StatusEnum.Active);
                    if (aboutTran != null)
                    {
                        return new AboutDicViewModel(aboutTran);
                    }
                }
                var aboutDic = db.AboutDics.FirstOrDefault(r=> r.GroupName == code  && r.Status == (int)(int)GeneralEnums.StatusEnum.Active);
                return aboutDic!=null? new AboutDicViewModel(aboutDic):null;
            }
        }
        public async Task<AboutDicViewModel> GetAboutDicByCodeForHomePage(string code, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran = await
                         db.AboutDicTranslations.Include(r => r.AboutDic).FirstOrDefaultAsync(r => r.LanguageId == languageId && r.AboutDic.GroupName == code && r.AboutDic.Status == (int)GeneralEnums.StatusEnum.Active);
                    if (aboutTran != null)
                    {
                        return  new AboutDicViewModel( aboutTran);
                    }
                }
                var aboutDic = await db.AboutDics.FirstOrDefaultAsync(r => r.GroupName == code && r.Status == (int)(int)GeneralEnums.StatusEnum.Active);
                return  aboutDic != null ?  new AboutDicViewModel(aboutDic) : null;
            }
        }
        
        public void AddAboutDic(AboutDicViewModel aboutDics)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var about = new AboutDic()
                {
                    CreatedOn = DateTime.Now,
                    Status = aboutDics.Status,
                    Name = aboutDics.Name,
                    Value = aboutDics.Value,
                    SortOrder = aboutDics.SortOrder,
                    GroupName = aboutDics.GroupName,
                    CreatedBy = aboutDics.CreatedBy,

                };
                db.AboutDics.Add(about);
                db.SaveChanges();

                aboutDics.Id = about.Id;

                if (aboutDics.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutDicTran = new AboutDicTranslation()
                    {
                        Name = aboutDics.Name,
                        Value = aboutDics.Value,
                        LanguageId = aboutDics.LanguageId,
                        AboutDicId = aboutDics.Id
                    };
                    db.AboutDicTranslations.Add(aboutDicTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditAboutDic(AboutDicViewModel aboutDicViewModel, AboutDic aboutDic)
        {
            using (var db = new LearningManagementSystemContext())
            {
                aboutDic.Status = aboutDicViewModel.Status;
                aboutDic.SortOrder = aboutDicViewModel.SortOrder;
                if (aboutDicViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    aboutDic.Name = aboutDicViewModel.Name;
                    aboutDic.Value = aboutDicViewModel.Value;
                    aboutDic.GroupName = aboutDicViewModel.GroupName;
                }

                db.Entry(aboutDic).State = EntityState.Modified;
                db.SaveChanges();
                if (aboutDicViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran = db.AboutDicTranslations.FirstOrDefault(r =>
                        r.LanguageId == aboutDicViewModel.LanguageId &&
                        r.AboutDicId == aboutDicViewModel.Id);
                    if (aboutTran != null)
                    {
                        aboutTran.Name = aboutDicViewModel.Name;
                        aboutTran.Value = aboutDicViewModel.Value;
                        db.Entry(aboutTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var aboutDicTran = new AboutDicTranslation()
                        {
                            Name = aboutDicViewModel.Name,
                            Value = aboutDicViewModel.Value,
                            LanguageId = aboutDicViewModel.LanguageId,
                            AboutDicId = aboutDicViewModel.Id
                        };
                        db.AboutDicTranslations.Add(aboutDicTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteAboutDic(AboutDic aboutDic)
        {
            using (var db = new LearningManagementSystemContext())
            {
                aboutDic.Status = (int)GeneralEnums.StatusEnum.Deleted;
                aboutDic.DeletedOn = DateTime.Now;
                db.Entry(aboutDic).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
