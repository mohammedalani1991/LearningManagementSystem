using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Permission
    {
        public Permission()
        {
            PermissionTranslations = new HashSet<PermissionTranslation>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string PageUrl { get; set; }
        public string PageName { get; set; }
        public string PermissionKey { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? ModuleId { get; set; }
        public int? SuperAdminId { get; set; }

        public virtual Module Module { get; set; }
        public virtual SuperAdmin SuperAdmin { get; set; }
        public virtual ICollection<PermissionTranslation> PermissionTranslations { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
