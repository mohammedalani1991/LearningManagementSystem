using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services.Helpers
{
    public class EntityHelper
    {
        public static int AddProfile(UserProfileViewModel userProfileViewModel, LearningManagementSystemContext context)
        {
            var userProfile = new UserProfile
            {
                CreatedOn = DateTime.Now,
                Status = userProfileViewModel.Status,
                //Email = userProfileViewModel.Email,
                JobId = userProfileViewModel.JobId,
                ProfilePhoto = userProfileViewModel.ProfilePhoto,
                PreferedLanguageId = userProfileViewModel.PreferedLanguageId,
                Username = userProfileViewModel.Email,
                ContactId = userProfileViewModel.ContactId

            };
            context.UserProfiles.Add(userProfile);
            context.SaveChanges();

            var user = context.AspNetUsers.Where(a => a.UserName.Equals(userProfile.Username)).SingleOrDefault();
            var roles = context.AspNetUserRoles.Where(a => a.UserId == user.Id).ToList();
            if (userProfileViewModel.RoleIds != null && userProfileViewModel.RoleIds.Count() > 0)
            {
                for (int i = 0; i < userProfileViewModel.RoleIds.Count(); i++)
                {
                    var role = new AspNetUserRole()
                    {
                        UserId = user.Id,
                        RoleId = userProfileViewModel.RoleIds[i],
                    };
                    context.AspNetUserRoles.Add(role);
                }
                context.SaveChanges();
            }
            else
            {
                //Role-Student by default
               var role = new AspNetUserRole()
               {
                   UserId = user.Id,
                   RoleId = "2",
               };
                context.AspNetUserRoles.Add(role);
                context.SaveChanges();
            }
            var userProfileTran = new UserProfileTranslation()
            {
                LanguageId = userProfileViewModel.LanguageId,
                IsDefault = userProfileViewModel.LanguageId == CultureHelper.GetDefaultLanguageId(),
                UserProfileId = userProfile.Id,
            };
            context.UserProfileTranslations.Add(userProfileTran);

            if (!userProfileTran.IsDefault)
            {
                var userProfileTran1 = new UserProfileTranslation()
                {
                    LanguageId = CultureHelper.GetDefaultLanguageId(),
                    IsDefault = true,
                    UserProfileId = userProfile.Id,
                };
                context.UserProfileTranslations.Add(userProfileTran1);
            }

            context.SaveChanges();

            return userProfile.Id;
        }
        public static List<ContactU> GetContacts(int take = 5, int language = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var contacts = db.ContactUs
                        .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).OrderBy(r => r.Id).Take(take).ToList();
                    return contacts;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting contacts take({take}) ");
                    return new List<ContactU>();

                }
            }
        }

        public static List<UserProfileViewModel> GetUsers(int take = 5, int language = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var users = db.UserProfileTranslations.Include(r => r.UserProfile)
                        .Where(r => r.LanguageId == language &&
                                    r.UserProfile.Status != (int)GeneralEnums.StatusEnum.Deleted).OrderByDescending(r => r.Id).Take(take).AsQueryable();
                    var result = users.ToList().Select(r => new UserProfileViewModel(r)).ToList();
                    var existingIds = result.Select(r => r.Id).ToList();
                    if (language != CultureHelper.GetDefaultLanguageId() && users.Count() < take)
                    {
                        var defaultLanguageMissingItems = db.UserProfileTranslations.Include(r => r.UserProfile)
                            .Where(r => r.LanguageId == CultureHelper.GetDefaultLanguageId() && !existingIds.Contains(r.UserProfileId) &&
                                        r.UserProfile.Status != (int)GeneralEnums.StatusEnum.Deleted).Take(take).ToList()
                            .Select(r => new UserProfileViewModel(r)).ToList();
                        result.AddRange(defaultLanguageMissingItems);
                    }
                    return result.Take(take).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Users take({take}) ");
                    return new List<UserProfileViewModel>();

                }
            }
        }
    }
}
