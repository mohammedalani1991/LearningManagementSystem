
delete from RolePermissions where PermissionId in (
select Id from Permission where PageName='ContactUs' and (PermissionKey = 'Edit' or PermissionKey = 'Delete' or PermissionKey = 'Create' ) and [Status]=1
)

delete from Permission where PageName='ContactUs' and (PermissionKey = 'Edit' or PermissionKey = 'Delete' or PermissionKey = 'Create' ) and [Status]=1