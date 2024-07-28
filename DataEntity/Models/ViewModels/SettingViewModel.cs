using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class SettingViewModel
    {
        public SettingViewModel()
        {

        }
        public SettingViewModel(SystemSettingTranslation setting)
        {
            Id = setting.SettingId;
            Name = setting.Name;
            Value = setting.Value;
            CreatedBy = setting.Setting.CreatedBy;
            CreatedOn = setting.Setting.CreatedOn;
            Status = setting.Setting.Status;
            LanguageId = setting.LanguageId;
            TypeId = setting.Setting.TypeId;
            SuperAdminId = setting.Setting.SuperAdminId;
            
        }
        public SettingViewModel(SystemSetting setting)
        {
            Id = setting.Id;
            Name = setting.Name;
            Value = setting.Value;
            CreatedBy = setting.CreatedBy;
            CreatedOn = setting.CreatedOn;
            Status = setting.Status;
            TypeId = setting.TypeId;
            SuperAdminId = setting.SuperAdminId;
            
        }
        public int Id { get; set; }
        public int? TypeId { get; set; }
        
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? SuperAdminId { get; set; }
    }
}
