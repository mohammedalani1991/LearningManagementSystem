using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemGroup
    {
        public SystemGroup()
        {
            SystemGroupTranslations = new HashSet<SystemGroupTranslation>();
            SystemGroupUsers = new HashSet<SystemGroupUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<SystemGroupTranslation> SystemGroupTranslations { get; set; }
        public virtual ICollection<SystemGroupUser> SystemGroupUsers { get; set; }
    }
}
