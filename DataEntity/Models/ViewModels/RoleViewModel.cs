using System.Collections.Generic;

namespace DataEntity.Models.ViewModels
{
    public class RoleViewModel
    {
        public int UserId { get; set; }
        public string RoleId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
}
