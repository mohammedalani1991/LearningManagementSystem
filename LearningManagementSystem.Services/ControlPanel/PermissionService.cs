using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class PermissionService: IPermissionService
    {
        private readonly ISettingService _settingService;
        public PermissionService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IPagedList<Permission> GetPermissions(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English,int pagination=24)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var permissions = db.Permissions.Include(r=>r.SuperAdmin)
                    .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.SuperAdmin.Show != false).Include(a => a.Module).Include(a => a.PermissionTranslations).AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    permissions = permissions
                        .Where(r => r.PageUrl.Contains(searchText) || r.PageName.Contains(searchText)
                                                                   || r.PermissionKey.Contains(searchText) || r.Description.Contains(searchText));
                }

                var result = permissions;

                int pageSize = pagination;
                var pageNumber = (page ?? 1);

                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.PermissionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.PageName = trans.PageName;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public void AddPermission(PermissionViewModel permissionViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var permis = new Permission()
                {
                    CreatedBy = permissionViewModel.CreatedBy,
                    CreatedOn = DateTime.Now,
                    Status = permissionViewModel.Status,
                    PageUrl = permissionViewModel.PageUrl ?? string.Empty,
                    Description = permissionViewModel.Description,
                    PageName = permissionViewModel.PageName,
                    PermissionKey = permissionViewModel.PermissionKey,
                    ModuleId = permissionViewModel.ModuleId
                };
                db.Permissions.Add(permis);
                db.SaveChanges();

                if (permissionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var permissionTran = new PermissionTranslation()
                    {
                        PageName = permissionViewModel.PageName,
                        Description = permissionViewModel.Description,
                        LanguageId = permissionViewModel.LanguageId,
                        PermissionId = permis.Id
                    };
                    db.PermissionTranslations.Add(permissionTran);
                    db.SaveChanges();
                }

            }
        }
        public Permission GetPermissionById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.Permissions.FirstOrDefault(r => r.Id == id);
            }
        }
        public void EditPermission(PermissionViewModel permissionViewModel, Permission permiss)
        {
            using (var db = new LearningManagementSystemContext())
            {
                permiss.Status = permissionViewModel.Status;
                permiss.PageUrl = permissionViewModel.PageUrl ?? string.Empty;
               
                permiss.PermissionKey = permissionViewModel.PermissionKey;
                permiss.ModuleId = permissionViewModel.ModuleId;
                permiss.SuperAdminId = permissionViewModel.SuperAdminId;

                if (permissionViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    permiss.Description = permissionViewModel.Description;
                    permiss.PageName = permissionViewModel.PageName;
                }

                db.Entry(permiss).State = EntityState.Modified;
                db.SaveChanges();

                if (permissionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var permissionTranslation = db.PermissionTranslations.FirstOrDefault(r =>
                        r.LanguageId == permissionViewModel.LanguageId &&
                        r.PermissionId == permissionViewModel.Id);
                    if (permissionTranslation != null)
                    {
                        permissionTranslation.Description = permissionViewModel.Description;
                        permissionTranslation.PageName = permissionViewModel.PageName;
                        db.Entry(permissionTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var permissionTran = new PermissionTranslation()
                        {
                            PageName = permissionViewModel.PageName,
                            Description = permissionViewModel.Description,
                            LanguageId = permissionViewModel.LanguageId,
                            PermissionId = permissionViewModel.Id
                        };
                        db.PermissionTranslations.Add(permissionTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void DeletePermission(Permission permiss)
        {
            using (var db = new LearningManagementSystemContext())
            {
                permiss.Status = (int)GeneralEnums.StatusEnum.Deleted;
                permiss.DeletedOn = DateTime.Now;
                db.Entry(permiss).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public PermissionViewModel GetPermissionById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var permissionTran =
                        db.PermissionTranslations.Include(r => r.Permission).FirstOrDefault(r => r.LanguageId == languageId && r.PermissionId == id);
                    if (permissionTran != null)
                    {
                        return new PermissionViewModel(permissionTran);
                    }
                }

                var permissions = db.Permissions.Find(id);

                return new PermissionViewModel(permissions);
            }
        }
    }
}
