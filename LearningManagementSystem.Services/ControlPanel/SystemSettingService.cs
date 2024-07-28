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

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SystemSettingService: ISystemSettingService
    {
        private readonly ISettingService _settingService;
        public SystemSettingService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<SystemSetting> GetSystemSettings(string searchText, int? page,int languageId,int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var settings = db.SystemSettings.Include(r => r.SuperAdmin)
                    .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.SuperAdmin.Show != false).Include(a => a.SystemSettingTranslations).AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText))
                    settings = settings.Where(r => r.Name.Contains(searchText) || r.Value.Contains(searchText));

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = settings;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.SystemSettingTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Value = trans.Value;
                        }
                        
                    }
                }
                return output;
            }
        }
        public void AddSystemSetting(SettingViewModel setting)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var systemSetting = new SystemSetting
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = setting.CreatedBy,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    DeletedOn = setting.DeletedOn,
                    Name = setting.Name,
                    Value = setting.Value,
                    TypeId = setting.TypeId,
                };
                db.SystemSettings.Add(systemSetting);
                db.SaveChanges();

                setting.Id = systemSetting.Id;
                if (setting.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systemTran = new SystemSettingTranslation()
                    {
                        Name = setting.Name,
                        Value = setting.Value,
                        LanguageId = setting.LanguageId,
                        SettingId = setting.Id
                    };
                    db.SystemSettingTranslations.Add(systemTran);
                    db.SaveChanges();
                }
            }
        }
        public SystemSetting GetSystemSetting(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.SystemSettings.Find(id);
            }
        }
        public SettingViewModel GetSystemSetting(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systenTran =
                        db.SystemSettingTranslations.Include(r => r.Setting).FirstOrDefault(r => r.LanguageId == languageId && r.SettingId == id);
                    if (systenTran != null)
                    {
                        return new SettingViewModel(systenTran);
                    }
                }
                var system = db.SystemSettings.Find(id);
                return new SettingViewModel(system);
            }
        }
        public void EditSystemSetting(SettingViewModel systemSettingViewModel, SystemSetting systemSetting)
        {
            using (var db = new LearningManagementSystemContext())
            {
                systemSetting.TypeId = systemSettingViewModel.TypeId;
                systemSetting.Value = systemSettingViewModel.Value;
                systemSetting.SuperAdminId = systemSettingViewModel.SuperAdminId;

                if (systemSettingViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    systemSetting.Name = systemSettingViewModel.Name;
                }
                db.Entry(systemSetting).State = EntityState.Modified;
                db.SaveChanges();
                if (systemSettingViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systemTran = db.SystemSettingTranslations.FirstOrDefault(r =>
                        r.LanguageId == systemSettingViewModel.LanguageId &&
                        r.SettingId == systemSettingViewModel.Id);
                    if (systemTran != null)
                    {
                        systemTran.Name = systemSettingViewModel.Name;
                        systemTran.Value = systemSettingViewModel.Value;
                        db.Entry(systemTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var systemTran1 = new SystemSettingTranslation()
                        {
                            Name = systemSettingViewModel.Name,
                            Value = systemSettingViewModel.Value,
                            LanguageId = systemSettingViewModel.LanguageId,
                            SettingId = systemSettingViewModel.Id
                        };
                        db.SystemSettingTranslations.Add(systemTran1);
                    }

                    db.SaveChanges();
                }
            }
        }
        public void DeleteSystemSetting(SystemSetting systemSettingDeleted)
        {
            using (var db = new LearningManagementSystemContext())
            {
                systemSettingDeleted.Status = (int)GeneralEnums.StatusEnum.Deleted;
                systemSettingDeleted.DeletedOn = DateTime.Now;
                db.Entry(systemSettingDeleted).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
