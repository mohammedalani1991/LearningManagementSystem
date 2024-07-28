using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class RolesPermissionService: IRolesPermissionService
    {
        private readonly ISettingService _settingService;
        public RolesPermissionService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<AspNetRole> GetRoles(string searchText, int? page, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var roles = db.AspNetRoles.ToList();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    roles = roles.Where(r => r.Name.Contains(searchText)).ToList();
                }
                var result = roles;

                var pageSize = pagination;
                var pageNumber = page ?? 1;
                return result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            }
        }

        public IPagedList<AspNetRole> GetRoles(string searchText, int? page)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var roles = db.AspNetRoles.ToList();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    roles = roles.Where(r => r.Name.Contains(searchText)).ToList();
                }
                var result = roles;

                var pageSize = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = page ?? 1;
                return result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            }
        }

        public bool AddRole(RoleViewModel role, out AspNetRole newRole)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var existRole = db.AspNetRoles.Any(x => x.Id == role.RoleId || x.Name == role.Name);
                if (existRole)
                {
                    newRole = null;
                    //Role Already Exist
                    return true;
                }

                newRole = new AspNetRole
                {
                    Id = role.RoleId,
                    Name = role.Name,
                    NormalizedName = role.FriendlyName
                };
                db.AspNetRoles.Add(newRole);
                db.SaveChanges();
                return false;
            }
        }

        public bool DeleteRole(string id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var role = GetRoleById(id);
                if (role == null)
                {
                    return true;
                }

                var rolePermissions = db.RolePermissions.Where(x => x.RoleId == id).ToList();
                foreach (var permission in rolePermissions)
                {
                    db.RolePermissions.Remove(permission);
                }

                db.SaveChanges();
                db.AspNetRoles.Remove(role);
                db.SaveChanges();
                return false;
            }
        }

        public AspNetRole GetRoleById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.AspNetRoles.Find(id.ToString());
            }
        }
        public AspNetRole GetRoleById(string id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.AspNetRoles.Find(id.ToString());
            }
        }

        public void EditRole(RolePermissionsViewModel permission)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var role = db.AspNetRoles.Find(permission.Id.ToString());
                var rolePermissions = db.RolePermissions.Where(x => x.RoleId == role.Id);
                foreach (var rolePermission in rolePermissions)
                {
                    db.RolePermissions.Remove(rolePermission);
                }

                db.SaveChanges();
                if (role != null)
                {
                    if (!string.IsNullOrWhiteSpace(permission.RoleName))
                    {
                        role.Name = permission.RoleName;
                    }

                    db.Entry(role).State = EntityState.Modified;
                    var permissions = permission.Permissions.Split(',');
                    var newRolePermission = new RolePermission();
                    foreach (var perm in permissions)
                    {
                        var permId = int.Parse(perm);
                        var existRolePermission =
                            db.RolePermissions.Any(x => x.RoleId == role.Id.ToString() && x.PermissionId == permId);
                        if (existRolePermission)
                        {
                            //Role Permission Already Exist
                            continue;
                        }

                        newRolePermission.RoleId = role.Id;
                        newRolePermission.PermissionId = int.Parse(perm);
                        db.RolePermissions.Add(newRolePermission);
                        db.SaveChanges();
                    }
                }
            }
        }

        public void EditRole(List<RolePermission> permissions,  string roleId, string roleName)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var role = db.AspNetRoles.Find(roleId);
                var rolePermissions = db.RolePermissions.Where(x => x.RoleId == role.Id);
                foreach (var rolePermission in rolePermissions)
                {
                    db.RolePermissions.Remove(rolePermission);
                }

                db.SaveChanges();
                if (role != null)
                {
                    if (!string.IsNullOrWhiteSpace(roleName))
                    {
                        role.Name = roleName;
                    }

                    db.Entry(role).State = EntityState.Modified;
                    
                  
                    foreach (var perm in permissions)
                    {
                        db.RolePermissions.Add(perm);
                    }
                    db.SaveChanges();
                }
            }
        }
        public void EditRole(AspNetRoleViewModel aspNetRole)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var role = db.AspNetRoles.Include(r=>r.AspNetUserRoles).Include(r=>r.AspNetRoleClaims).Include(r=>r.RolePermissions).FirstOrDefault(r=>r.Id==aspNetRole.Id);
                
                if (role != null)
                {
                    role.Name = aspNetRole.Name;
                    role.NormalizedName = aspNetRole.NormalizedName;
                    db.Entry(role).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        public RolePermissionsViewModel GetRoleViewModel(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var permissionsGroups = db.Permissions.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                var rolePermissions = db.RolePermissions.Where(x =>
                    x.RoleId == id.ToString() && x.Permission.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                var currentRole = db.AspNetRoles.FirstOrDefault(x => x.Id == id.ToString());
                if (currentRole == null)
                {
                    //Role not exist
                    return null;
                }

                var result = new RolePermissionsViewModel
                {
                    Id = id,
                    RoleName = currentRole.Name,
                    RoleFriendlyName = currentRole.NormalizedName,
                    PermissionGroups = new List<PermissionGroups>()
                };
                foreach (var permissions in permissionsGroups)
                {
                    result.PermissionGroups.Add(new PermissionGroups
                    {
                        PageName = permissions.PageName,
                        PermissionsKey = new PermissionViewModel()
                        {
                            Id = permissions.Id,
                            Status = permissions.Status,
                            PageUrl = permissions.PageUrl,
                            PermissionKey = permissions.PermissionKey,
                            PageName = permissions.PageName,
                            Description = permissions.Description,
                            CreatedBy = permissions.CreatedBy,
                            CreatedOn = permissions.CreatedOn,
                            DeletedOn = permissions.DeletedOn,
                            IsSelected = rolePermissions.Any(r => r.PermissionId == permissions.Id),
                        }
                    });
                }

                return result;
            }
        }

        public RoleViewModel GetRoleViewModelById(string id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var aspNetRoles =  db.AspNetRoles
                    .FirstOrDefault(m => m.Id == id);
                if (aspNetRoles == null)
                {
                    return new RoleViewModel();
                }

                var permissionsGroups = db.Permissions.Include(r=>r.SuperAdmin).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.SuperAdmin.Show != false).ToList();
                var rolePermissions = db.RolePermissions.Where(x =>
                    x.RoleId == id.ToString() && x.Permission.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                var role = new RoleViewModel
                {
                    RoleId = aspNetRoles.Id,
                    Name = aspNetRoles.Name,
                    FriendlyName = aspNetRoles.NormalizedName,
                    ConcurrencyStamp = aspNetRoles.ConcurrencyStamp,
                    Permissions = new List<PermissionViewModel>()
                };

                foreach (var x in permissionsGroups)
                {
                    role.Permissions.Add(new PermissionViewModel(x, rolePermissions.Any(r => r.PermissionId == x.Id)));
                }

                return role;
            }
        }
    }
}
