using System;
using System.Linq;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystem.Core.SystemEnums;

namespace LearningManagementSystem.Services.Helpers
{
    public class AuthenticationHelper
    {
        public static bool CheckSuperAuthentication(string pageName)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var auth = db.SuperAdmins.FirstOrDefault(x => x.Name == pageName);
                    if (auth != null)
                    {
                        if (auth.Show)
                            return true;
                        return false;
                    }

                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, "Error while check super authentication!");
                return false;
            }
        }

        public static bool CheckAuthentication(string pageName, string permissionKey, string userName)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.UserName == userName);
                    if (user == null)
                    {
                        //User Not Exist
                        return false;
                    }

                    var userRoles = db.AspNetUserRoles.Where(x => x.UserId == user.Id).ToList();
                    foreach (var role in userRoles)
                    {
                        var permission = db.Permissions.FirstOrDefault(x => x.PageName.Equals(pageName) && x.PermissionKey.Equals(permissionKey));
                        if (permission == null || role == null)
                        {
                            return false;
                        }
                        var userHasPermissions = db.RolePermissions.Any(x => x.RoleId == role.RoleId && x.PermissionId == permission.Id);
                        if (userHasPermissions)
                        {
                            return true;
                        }
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(userName, ex, "Error while check authentication!");
                return false;
            }
        }

        public static bool CheckAdministratorRole(string userName)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var userId = db.AspNetUsers.FirstOrDefault(r => r.UserName == userName).Id;
                    var AdminRole = db.AspNetRoles.Where(r => r.Name == "Administrator" || r.Name == "MiniAdmin").Select(r => r.Id);
                    if (AdminRole != null && AdminRole.Count() > 0)
                    {
                        var isAdmin = db.AspNetUserRoles.Any(r => AdminRole.Contains(r.RoleId) && r.UserId == userId);
                        if (isAdmin)
                            return true;
                        return false;
                    }
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(userName, ex, "Error while check CheckAdminRole!");
                return false;
            }
        }

        public static bool CheckIfEmployee(string userName)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var contact = db.Contacts.FirstOrDefault(r => r.Email == userName && r.ContactTypes.Any(s => s.TypeId == (int)GeneralEnums.ContactTypeEnum.Employee || s.TypeId == (int)GeneralEnums.ContactTypeEnum.Admin));
                    if (contact != null)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(userName, ex, "Error while check CheckIfEmployee!");
                return false;
            }
        }

        public static bool CheckIsAdvisor(string username)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var account = db.UserProfiles.Include(a => a.Contact).FirstOrDefault(r =>
                        r.Username == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);

                    var contect = db.Contacts.Include(r => r.ContactTypes).FirstOrDefault(r => r.Id == account.ContactId);
                    if (contect.ContactTypes.Any(r => r.TypeId == (int)GeneralEnums.ContactTypeEnum.Advisors))
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, "Error while check CheckIsAdvisor!");
                return false;
            }
        }
        public static bool CheckIsTrainer(string username)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var account = db.UserProfiles.Include(a => a.Contact).FirstOrDefault(r =>
                        r.Username == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);

                    var contect = db.Contacts.Include(r => r.ContactTypes).FirstOrDefault(r => r.Id == account.ContactId);
                    if (contect.ContactTypes.Any(r => r.TypeId == (int)GeneralEnums.ContactTypeEnum.Trainer))
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, "Error while check CheckIsTrainer!");
                return false;
            }
        }
        public static bool CheckIsStudent(string username)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var account = db.UserProfiles.Include(a => a.Contact).FirstOrDefault(r =>
                        r.Username == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);

                    var contect = db.Contacts.Include(r => r.ContactTypes).FirstOrDefault(r => r.Id == account.ContactId);
                    if (contect.ContactTypes.Any(r => r.TypeId == (int)GeneralEnums.ContactTypeEnum.Student))
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, "Error while check CheckIsStudent!");
                return false;
            }
        }
    }
}
