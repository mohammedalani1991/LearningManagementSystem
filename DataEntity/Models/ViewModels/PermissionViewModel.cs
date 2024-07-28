using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class PermissionViewModel
    {
        public PermissionViewModel()
        {
        }
        public PermissionViewModel(Permission permission)
        {
            Id = permission.Id;
            PageUrl = permission.PageUrl;
            PageName = permission.PageName;
            PermissionKey = permission.PermissionKey;
            Description = permission.Description;
            CreatedOn = permission.CreatedOn;
            CreatedBy = permission.CreatedBy;
            Status = permission.Status;
            SuperAdminId = permission.SuperAdminId;
        }

        public PermissionViewModel(PermissionTranslation permission)
        {
            Id = permission.Id;
            PageUrl = permission.Permission.PageUrl;
            PageName = permission.PageName;
            PermissionKey = permission.Permission.PermissionKey;
            Description = permission.Description;
            CreatedOn = permission.Permission.CreatedOn;
            CreatedBy = permission.Permission.CreatedBy;
            Status = permission.Permission.Status;
            LanguageId = permission.LanguageId;
            SuperAdminId = permission.Permission.SuperAdminId;
        }

        public PermissionViewModel(Permission permission, bool isSelected)
        {
            Id = permission.Id;
            PageUrl = permission.PageUrl;
            PageName = permission.PageName;
            PermissionKey = permission.PermissionKey;
            Description = permission.Description;
            CreatedOn = permission.CreatedOn;
            CreatedBy = permission.CreatedBy;
            Status = permission.Status;
            IsSelected = isSelected;
            LanguageId = LanguageId;
            ModuleId = permission.ModuleId;
            SuperAdminId = permission.SuperAdminId;
        }
        public int Id { get; set; }
        public int? ModuleId { get; set; }
        public string PageUrl { get; set; }
        public string PageName { get; set; }
        public int LanguageId { get; set; }
        public string PermissionKey { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsSelected { get; set; }
        public int? SuperAdminId { get; set; }
    }
}
