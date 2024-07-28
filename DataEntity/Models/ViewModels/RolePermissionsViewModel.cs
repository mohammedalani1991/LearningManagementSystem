using System.Collections.Generic;

namespace DataEntity.Models.ViewModels
{
    public class RolePermissionsViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleFriendlyName { get; set; }
        public string RoleDescription { get; set; }
        public string Permissions { get; set; }
        public List<PermissionGroups> PermissionGroups { get; set; }
    }
    public class PermissionGroups
    {
        public string PageName { get; set; }
        public PermissionViewModel PermissionsKey { get; set; }
    }
}
