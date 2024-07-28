using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemSetting
    {
        public SystemSetting()
        {
            SystemSettingTranslations = new HashSet<SystemSettingTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? TypeId { get; set; }
        public int? BranchId { get; set; }
        public int? SuperAdminId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual SuperAdmin SuperAdmin { get; set; }
        public virtual ICollection<SystemSettingTranslation> SystemSettingTranslations { get; set; }
    }
}
