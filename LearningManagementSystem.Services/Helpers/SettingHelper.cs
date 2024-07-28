using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearningManagementSystem.Core.SystemEnums;

namespace LearningManagementSystem.Services.Helpers
{
    public class SettingHelper
    {
        public static SettingViewModel GetOrCreate(string name, string defaultValue, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var setting = db.SystemSettings.FirstOrDefault(r =>
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
                    db.SystemSettings.Add(setting);
                    db.SaveChanges();

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
        }

        public static bool SetSettingValue(string name, string value)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var setting = db.SystemSettings.FirstOrDefault(s =>
                    s.Name == name && s.Status == (int)GeneralEnums.StatusEnum.Active);
                if (setting == null)
                {
                    return false;
                }
                setting.Value = value;
                db.SaveChanges();
            }
            return true;
        }
    }
}
