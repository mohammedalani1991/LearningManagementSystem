
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IRolesPermissionService
    {
        IPagedList<AspNetRole> GetRoles(string searchText, int? page, int pagination);
        IPagedList<AspNetRole> GetRoles(string searchText, int? page);
        bool AddRole(RoleViewModel role, out AspNetRole newRole);
        bool DeleteRole(string id);
        AspNetRole GetRoleById(int id);
        AspNetRole GetRoleById(string id);
        void EditRole(RolePermissionsViewModel permission);
        void EditRole(List<RolePermission> permissions, string roleId, string roleName);
        RolePermissionsViewModel GetRoleViewModel(int id);
        RoleViewModel GetRoleViewModelById(string id);
        void EditRole(AspNetRoleViewModel aspNetRole);
    }
}
