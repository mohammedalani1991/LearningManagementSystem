using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Module
    {
        public Module()
        {
            ModuleTranslations = new HashSet<ModuleTranslation>();
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public string BaseUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<ModuleTranslation> ModuleTranslations { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
