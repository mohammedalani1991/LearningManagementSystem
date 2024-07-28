using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SuperAdmin
    {
        public SuperAdmin()
        {
            Permissions = new HashSet<Permission>();
            SystemSettings = new HashSet<SystemSetting>();
        }

        public int Id { get; set; }
        public bool Show { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<SystemSetting> SystemSettings { get; set; }
    }
}
