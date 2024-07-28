using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IPermissionService
    {
        IPagedList<Permission> GetPermissions(string searchText, int? page, int languageId, int pagination);
        void AddPermission(PermissionViewModel permission);
        PermissionViewModel GetPermissionById(int id, int languageId);
        Permission GetPermissionById(int id);
        void EditPermission(PermissionViewModel permission, Permission permiss);
        void DeletePermission(Permission permiss);
    }
}
