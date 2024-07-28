using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ISettingService _settingService;
        public UserProfileService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<UserProfile> GetUserProfiles(string searchText, int? page, int RoleId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var userProfiles = db.UserProfiles.Include(a => a.Contact).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    userProfiles = userProfiles.Where(r =>
                        (r.Contact.Email.Contains(searchText) || r.Username.Contains(searchText)));
                }


                int pageSize = int.Parse(_settingService.GetOrCreate(Core.Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = (page ?? 1);

                var result = userProfiles.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                return result;
            }

        }
        public UserProfile GetUserProfileByUsername(string username)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var account = db.UserProfiles.Include(a => a.Contact.Students).Include(s => s.Contact.Trainers).FirstOrDefault(r =>
                    r.Username == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return account;
            }
        }

        public UserProfile GetUserProfileByEmail(string email)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var account = db.UserProfiles.Include(a => a.Contact.Students).Include(s => s.Contact.Trainers).FirstOrDefault(r =>
                    r.Contact.Email == email && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return account;
            }
        }

        public bool GetContactEmail(string email)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contact = db.Contacts.FirstOrDefault(r => r.Email == email && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (contact != null)
                    return true;
                return false;
            }
        }

        public EditProfileViewModel GetUserByUsername(string username, string language = "English")
        {
            using (var db = new LearningManagementSystemContext())
            {

                var user = db.UserProfiles.Include(a => a.Contact.ContactTranslations).FirstOrDefault(r =>
                      r.Username == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);
                var aspUser = db.AspNetUsers.FirstOrDefault(s => s.UserName == user.Contact.Email);
                if (user != null)
                {
                    var emp = db.Employees.FirstOrDefault(a => a.ContactId == user.ContactId && a.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var trainer = db.Trainers.FirstOrDefault(a => a.ContactId == user.ContactId && a.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var student = db.Students.FirstOrDefault(s => s.ContactId == user.ContactId);
                    EditProfileViewModel editProfileView = new EditProfileViewModel();
                    editProfileView.ContactId = user.ContactId.GetValueOrDefault(0);
                    editProfileView.FullName = user.Contact.FirstName + " " + user.Contact.SecondName + " " + user.Contact.ThirdName + " " + user.Contact.LastName;
                    editProfileView.FirstName = user.Contact.FirstName;
                    editProfileView.SecondName = user.Contact.SecondName;
                    editProfileView.ThirdName = user.Contact.ThirdName;
                    editProfileView.LastName = user.Contact.LastName;
                    editProfileView.Mobile = user.Contact.Mobile;
                    editProfileView.IdNumber = user.Contact.IdNumber;
                    editProfileView.ProfilePhoto = user.ProfilePhoto;
                    editProfileView.PreferedLanguageId = user.PreferedLanguageId;
                    editProfileView.CityId = user.Contact.CityId;
                    editProfileView.GenderId = user.Contact.GenderId;
                    editProfileView.CountryId = user.Contact.CountryId;
                    editProfileView.PhoneNumber = user.Contact.Mobile;
                    editProfileView.PhoneNumberCode = user.Contact.PhoneNumberCode;
                    if (student != null)
                    {
                        editProfileView.BirthDate = student.BirthDate;
                        editProfileView.StudentId = student.Id;
                        editProfileView.EducationalLevelId = student.EducationalLevelId;
                    }

                    if (emp != null)
                    {
                        editProfileView.Address = emp.Address;
                    }
                    if (trainer != null)
                    {
                        editProfileView.Signature = trainer.Signature;
                    }
                    if (language == "عربي")
                    {
                        var tran = user.Contact.ContactTranslations.FirstOrDefault();
                        if (tran != null)
                            editProfileView.FullName = tran.FirstName + " " + tran.SecondName + " " + tran.ThirdName + " " + tran.LastName;
                    }

                    return editProfileView;
                }

                return null;

            }
        }


        public bool IsIdentityExist(string idNumber)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.UserProfiles.Include(a => a.Contact).Any(r => r.Contact.IdNumber == idNumber);
            }
        }
        public void AddUserProfile(UserProfileViewModel profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var profil = new UserProfile()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Username = profile.Username,
                    //Email = profile.Email,
                    LastLogin = profile.LastLogin,
                    PreferedLanguageId = profile.PreferedLanguageId,
                    ProfilePhoto = profile.ProfilePhoto
                };
                db.UserProfiles.Add(profil);
                db.SaveChanges();

                profile.Id = profil.Id;

                if (profile.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var profilTrans = new UserProfileTranslation()
                    {
                        LanguageId = profile.LanguageId,
                        UserProfileId = profile.Id
                    };
                    db.UserProfileTranslations.Add(profilTrans);
                    db.SaveChanges();
                }
            }
        }
        public UserProfileViewModel GetUserProfileViewModel(UserProfile profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var aspUser = db.AspNetUsers.FirstOrDefault(r => r.UserName == profile.Username);

                var result = new UserProfileViewModel()
                {
                    Id = profile.Id,
                    Username = profile.Username,
                    //Email = profile.Email,
                    Status = profile.Status,
                    LastLogin = profile.LastLogin,
                    ProfilePhoto = profile.ProfilePhoto,
                    IdNumber = profile.Contact.IdNumber,
                    PreferedLanguageId = profile.PreferedLanguageId,
                };

                var userRoles = aspUser?.AspNetUserRoles.ToList().Select(r =>
                    new RoleViewModel()
                    {
                        Name = r.Role.Name,
                        UserId = profile.Id,
                        RoleId = r.RoleId
                    }).ToList();
                result.Roles = userRoles;
                return result;
            }
        }
        public void EditUserProfile(UserProfileViewModel profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (profile.LanguageId == 0) profile.LanguageId = CultureHelper.GetDefaultLanguageId();
                var prof = db.UserProfiles.Find(profile.Id);
                if (prof != null && prof.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    //prof.Email = profile.Email;
                    prof.PreferedLanguageId = profile.PreferedLanguageId;

                    prof.Status = profile.Status;
                    db.Entry(prof).State = EntityState.Modified;
                    db.SaveChanges();
                }

                var user = db.AspNetUsers.Where(a => a.UserName.Equals(prof.Username)).SingleOrDefault();
                var roles = db.AspNetUserRoles.Where(a => a.UserId == user.Id).ToList();
                if (profile.RoleIds != null && profile.RoleIds.Count() > 0)
                {
                    if (roles != null)
                    {
                        for (int i = 0; i < roles.Count(); i++)
                        {
                            db.AspNetUserRoles.Remove(roles[i]);
                        }
                    }
                    for (int i = 0; i < profile.RoleIds.Count(); i++)
                    {
                        var role = new AspNetUserRole()
                        {
                            UserId = user.Id,
                            RoleId = profile.RoleIds[i],
                        };

                        db.AspNetUserRoles.Add(role);
                    }
                    db.SaveChanges();
                }
                else
                {

                    if (roles != null)
                    {
                        for (int i = 0; i < roles.Count(); i++)
                        {
                            db.AspNetUserRoles.Remove(roles[i]);
                        }
                        db.SaveChanges();
                    }
                }

                if (profile.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var profilesTran =
                        db.UserProfileTranslations.FirstOrDefault(r =>
                            r.LanguageId == profile.LanguageId && r.UserProfileId == profile.Id);
                    if (profilesTran != null)
                    {

                        db.Entry(profilesTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var profileTran = new UserProfileTranslation()
                        {

                            UserProfileId = profile.Id,
                            LanguageId = profile.LanguageId,
                        };
                        db.UserProfileTranslations.Add(profileTran);
                    }

                    db.SaveChanges();
                }
            }
        }
        public void UpdateUserProfile(UserProfileViewModel profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (profile.LanguageId == 0) profile.LanguageId = CultureHelper.GetDefaultLanguageId();
                var prof = db.UserProfiles.Find(profile.Id);
                if (prof != null && prof.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (profile.LanguageId == CultureHelper.GetDefaultLanguageId())
                    {

                    }

                    //prof.Email = profile.Email;
                    prof.ProfilePhoto = profile.ProfilePhoto;
                    prof.PreferedLanguageId = profile.PreferedLanguageId;
                    prof.Status = profile.Status;
                    db.Entry(prof).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (profile.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var profilesTran =
                        db.UserProfileTranslations.FirstOrDefault(r =>
                            r.LanguageId == profile.LanguageId && r.UserProfileId == profile.Id);
                    if (profilesTran != null)
                    {
                        db.Entry(profilesTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var profileTran = new UserProfileTranslation()
                        {
                            UserProfileId = profile.Id,
                            LanguageId = profile.LanguageId,
                        };
                        db.UserProfileTranslations.Add(profileTran);
                    }
                    db.SaveChanges();
                }
            }
        }

        public void UpdateUserProfile(EditProfileViewModel profile)
        {
            using (var db = new LearningManagementSystemContext())
            {

                if (profile.LanguageId == 0) profile.LanguageId = CultureHelper.GetDefaultLanguageId();
                var contact = db.Contacts.Where(a => a.Id == profile.ContactId).FirstOrDefault();
                var userPrfofile = db.UserProfiles.Where(a => a.ContactId == profile.ContactId).FirstOrDefault();

                if (contact != null && contact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    var IsStudent = AuthenticationHelper.CheckIsStudent(contact.Email);
                    var IsTrainer = AuthenticationHelper.CheckIsTrainer(contact.Email);
                    var aspUser = db.AspNetUsers.FirstOrDefault(r => r.UserName == contact.Email);

                    var student = db.Students.FirstOrDefault(s => s.ContactId == contact.Id);
                    var employee = db.Employees.FirstOrDefault(a => a.ContactId == profile.ContactId && a.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var trainer = db.Trainers.FirstOrDefault(a => a.ContactId == profile.ContactId && a.Status != (int)GeneralEnums.StatusEnum.Deleted);

                    userPrfofile.ProfilePhoto = profile.ProfilePhoto;
                    aspUser.PhoneNumber = profile.PhoneNumber;
                    userPrfofile.PreferedLanguageId = profile.PreferedLanguageId;
                    db.Entry(userPrfofile).State = EntityState.Modified;
                    contact.FirstName = IsStudent ? contact.FirstName : profile.FirstName;
                    contact.SecondName = IsStudent ? contact.SecondName : profile.SecondName;
                    contact.ThirdName = IsStudent ? contact.ThirdName : profile.ThirdName;
                    contact.LastName = IsStudent ? contact.LastName : profile.LastName;
                    contact.Mobile = profile.PhoneNumber;
                    contact.PhoneNumberCode = profile.PhoneNumberCode;
                    contact.IdNumber = profile.IdNumber;
                    contact.CountryId = profile.CountryId;
                    contact.CityId = profile.CityId;
                    contact.GenderId = profile.GenderId;
                    contact.GenderId = profile.GenderId;
                    contact.FullName = IsStudent ? $"{contact.FirstName} {contact.SecondName} {contact.ThirdName} {contact.LastName}" : $"{profile.FirstName} {profile.SecondName} {profile.ThirdName} {profile.LastName}";
                    db.Entry(contact).State = EntityState.Modified;

                    if (profile.LanguageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var contactTrans = db.ContactTranslations.FirstOrDefault(a => a.ContactId == profile.ContactId);
                        if (contactTrans != null)
                        {
                            contactTrans.FirstName = IsStudent ? contactTrans.FirstName : profile.FirstName;
                            contactTrans.SecondName = IsStudent ? contactTrans.SecondName : profile.SecondName;
                            contactTrans.ThirdName = IsStudent ? contactTrans.ThirdName : profile.ThirdName;
                            contactTrans.LastName = IsStudent ? contactTrans.LastName : profile.LastName;
                            contactTrans.FullName = IsStudent ? $"{contactTrans.FirstName} {contactTrans.SecondName} {contactTrans.ThirdName} {contactTrans.LastName}" : $"{profile.FirstName} {profile.SecondName} {profile.ThirdName} {profile.LastName}";
                            db.Entry(contactTrans).State = EntityState.Modified;

                        }
                        else
                        {
                            var newContactTrans = new ContactTranslation()
                            {
                                FirstName = IsStudent ? contact.FirstName : profile.FirstName,
                                SecondName = IsStudent ? contact.SecondName : profile.SecondName,
                                ThirdName = IsStudent ? contact.ThirdName : profile.ThirdName,
                                LastName = IsStudent ? contact.LastName : profile.LastName,
                                FullName = IsStudent ? $"{contact.FirstName} {contact.SecondName} {contact.ThirdName} {contact.LastName}" : $"{profile.FirstName} {profile.SecondName} {profile.ThirdName} {profile.LastName}",
                                ContactId = contact.Id,
                                LanguageId = profile.LanguageId,

                            };
                            db.ContactTranslations.Add(newContactTrans);
                        }

                    }

                    if (employee != null)
                    {
                        employee.Address = employee.Address;
                        db.Entry(employee).State = EntityState.Modified;
                    }

                    if (IsTrainer && trainer != null)
                    {
                        trainer.Signature = profile.Signature;
                        db.Entry(trainer).State = EntityState.Modified;
                    }
                    else if (IsStudent && student != null)
                    {
                        student.BirthDate = profile.BirthDate;
                        student.EducationalLevelId = profile.EducationalLevelId;
                    }
                    db.SaveChanges();

                }
            }
        }

        public void UpdateUserProfile(UserProfile profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public UserProfile GetUserProfileById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var profile = db.UserProfiles.Include(r => r.Contact).FirstOrDefault(r => r.Id == id);
                return profile;
            }
        }

        public UserProfile GetUserProfileByEmployeeId(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var profile = db.UserProfiles.Include(r => r.Contact.Employees).FirstOrDefault(r => r.Contact.Employees.FirstOrDefault(r => r.Id == id).Id == id);
                return profile;
            }
        }

        public UserProfileViewModel GetUserProfileById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var profileTran =
                        db.UserProfileTranslations.Include(r => r.UserProfile).FirstOrDefault(r => r.LanguageId == languageId && r.UserProfileId == id);
                    if (profileTran != null)
                    {
                        return new UserProfileViewModel(profileTran);
                    }
                }
                var profileLookup = db.UserProfiles.Find(id);
                return new UserProfileViewModel(profileLookup);
            }
        }
        public void DeleteUserProfileById(UserProfile profile)
        {
            using (var db = new LearningManagementSystemContext())
            {
                profile.Status = (int)GeneralEnums.StatusEnum.Deleted;
                profile.DeletedOn = DateTime.Now;
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ReactivateAccount(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var userProfile = db.UserProfiles.FirstOrDefault(r => r.ContactId == id && r.Status == (int)GeneralEnums.StatusEnum.Deleted);
                if (userProfile != null)
                {
                    userProfile.Status = (int)GeneralEnums.StatusEnum.Active;
                    userProfile.DeletedOn = null;
                    db.Entry(userProfile).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public List<AspNetRole> GetActiveRoles()
        {
            using (var db = new LearningManagementSystemContext())
            {
                var roles = db.AspNetRoles.ToList();
                return roles;
            }
        }

        public AspNetUser AddRoleToUser(RoleViewModel role)
        {
            using (var db = new LearningManagementSystemContext())
            {
                string roleId = role.RoleId;
                var userProfile = GetUserProfileById(role.UserId);
                var aspUser = db.AspNetUsers.Include(r => r.AspNetUserRoles).FirstOrDefault(r => r.UserName == userProfile.Username);
                if (!db.AspNetUserRoles.Any(r => r.UserId == aspUser.Id && r.RoleId == roleId))
                {
                    db.AspNetUserRoles.Add(new AspNetUserRole()
                    {
                        RoleId = roleId,
                        UserId = aspUser.Id
                    });
                    db.SaveChanges();
                }

                return aspUser;
            }
        }
        public AspNetUser DeleteUserRole(RoleViewModel role)
        {
            using (var db = new LearningManagementSystemContext())
            {
                string roleId = role.RoleId;
                var userProfile = GetUserProfileById(role.UserId);
                var aspUser = db.AspNetUsers.Include(r => r.AspNetUserRoles).FirstOrDefault(r => r.UserName == userProfile.Username);
                var aspRole = db.AspNetUserRoles.FirstOrDefault(r => r.UserId == aspUser.Id && r.RoleId == roleId);
                if (aspRole != null)
                {
                    db.AspNetUserRoles.Remove(aspRole);
                    db.SaveChanges();
                }
                return aspUser;
            }
        }
        public List<AspNetUserRole> GetUserRoles(int userId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var userProfile = GetUserProfileById(userId);
                var aspUser = db.AspNetUsers.Include(r => r.AspNetUserRoles)
                    .FirstOrDefault(r => r.UserName == userProfile.Username);
                if (aspUser != null)
                {
                    var aspRoles = db.AspNetUserRoles.Include(r => r.Role).Where(r => r.UserId == aspUser.Id).ToList();
                    return aspRoles;
                }
                return null;
            }
        }


        public bool DeleteUserAndRoles(string username)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var aspUser = db.AspNetUsers.FirstOrDefault(r => r.UserName == username);
                if (aspUser == null) return false;
                var aspUserRoles = db.AspNetUserRoles.Where(r => r.UserId == aspUser.Id);
                db.AspNetUserRoles.RemoveRange(aspUserRoles);
                db.AspNetUsers.Remove(aspUser);

                db.SaveChanges();
                return true;
            }
        }

        public void RemoveUserProfileById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var userProfile = db.UserProfiles.Include(r => r.UserProfileTranslations).FirstOrDefault(r => r.Id == id);
                if (userProfile != null)
                {
                    db.UserProfileTranslations.RemoveRange(userProfile.UserProfileTranslations);
                    db.UserProfiles.Remove(userProfile);
                }

                db.SaveChanges();
            }
        }

        public int GetUserRole(string username)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var aspUser = db.AspNetUsers.Include(r => r.AspNetUserRoles)
                    .FirstOrDefault(r => r.UserName == username);
                var aspRoles = db.AspNetUserRoles.Include(r => r.Role).FirstOrDefault(r => r.UserId == aspUser.Id);
                return Int32.Parse(aspRoles.RoleId);
            }
        }

        public void ChangeEmail(string oldEmail, string newEmail)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var userProfile = db.UserProfiles.FirstOrDefault(s => s.Username == oldEmail);
                if (userProfile != null)
                {
                    userProfile.Username = newEmail;
                    db.Entry(userProfile).State = EntityState.Modified;
                }

                var contacts = db.Contacts.FirstOrDefault(s => s.Email == oldEmail);
                if (contacts != null)
                {
                    contacts.Email = newEmail;
                    db.Entry(contacts).State = EntityState.Modified;
                }

                var students = db.Students.FirstOrDefault(s => s.Email == oldEmail);
                if (students != null)
                {
                    students.Email = newEmail;
                    db.Entry(students).State = EntityState.Modified;
                }

                db.SaveChanges();

            }
        }
    }
}
