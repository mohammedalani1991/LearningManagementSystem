using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemSettingTranslation
    {
        public int Id { get; set; }
        public int SettingId { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual SystemSetting Setting { get; set; }
    }
}
