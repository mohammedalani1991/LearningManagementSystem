using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services.General
{
    public class SettingService: ISettingService
    {
        private readonly LearningManagementSystemContext _db;

        public SettingService(LearningManagementSystemContext context)
        {
            _db = context;
        }

        public SettingViewModel GetOrCreate(string name, string defaultValue, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            var setting = _db.SystemSettings.FirstOrDefault(r =>
                r.Name == name && r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    Value = defaultValue,
                    Name = name
                };
                _db.SystemSettings.Add(setting);
                _db.SaveChanges();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systemTran = new SystemSettingTranslation()
                    {
                        Name = name,
                        Value = defaultValue,
                        LanguageId = languageId,
                        SettingId = setting.Id
                    };
                    _db.SystemSettingTranslations.Add(systemTran);
                    _db.SaveChanges();
                }

                var sysSetting = new SettingViewModel()
                {
                    Id = setting.Id,
                    Name = name,
                    Value = defaultValue
                };

                return sysSetting;
            }
            else
            {
                return new SettingViewModel()
                {
                    Id = setting.Id,
                    Name = setting.Name,
                    Value = setting.Value
                };
            }
        }

        public async Task<List<SettingViewModel>> GetMultipleSystemSettings(string[] name ,int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            
            using (var db = new LearningManagementSystemContext())
            {
                var allSetting =db.SystemSettings.Where(r =>  r.Status == (int)GeneralEnums.StatusEnum.Active).Select(r=>r.Name);
                var settingNoAdded = name.Where(m=> !allSetting.Contains(m));

                foreach (var item in settingNoAdded)
                {
                    GetOrCreate(item, "", languageId);
                }

                var setting = db.SystemSettings.Include(r => r.SystemSettingTranslations).Where(r =>
                      name.Contains(r.Name) && r.Status == (int)GeneralEnums.StatusEnum.Active);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in setting)
                    {
                        var trans = item.SystemSettingTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Value = trans.Value;
                        }
                    }
                }
                return await setting.Select(r =>new SettingViewModel(r)).ToListAsync();
            }
        }
        public bool SetSettingValue(string name, string value)
        {
            var setting = _db.SystemSettings.FirstOrDefault(s =>
                s.Name == name && s.Status == (int)GeneralEnums.StatusEnum.Active);
            if (setting == null)
            {
                return false;
            }
            setting.Value = value;
            _db.SaveChanges();
            return true;
        }
    }
}
