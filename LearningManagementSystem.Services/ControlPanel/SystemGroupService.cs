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
using LearningManagementSystem.Core;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SystemGroupService : ISystemGroupService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _db;
        public SystemGroupService(ISettingService settingService, LearningManagementSystemContext context)
        {
            _settingService = settingService;
            _db = context;

        }
        public IPagedList<SystemGroup> GetSystemGroups(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var systemGroups = _db.SystemGroups.Include(s =>s.SystemGroupTranslations)
                .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted );

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                systemGroups = systemGroups.Where(r =>
                    (r.Description.Contains(searchText) || r.Name.Contains(searchText)));
            }

            var result = systemGroups;

            int pageSize = pagination;
            var pageNumber = (page ?? 1);

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in output)
                {
                    var trans = item.SystemGroupTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Name = trans.Name;
                        item.Description = trans.Description;
                    }
                }
            }

            return output;
        }


        public void AddSystemGroup(SystemGroupViewModel model)
        {
            var systemGroup = new SystemGroup()
            {
                CreatedOn = DateTime.Now,
                DeletedOn = model.DeletedOn,
                CreatedBy = model.CreatedBy,
                Status = model.Status,
                Name = model.Name,
                Description = model.Description,
            };
            _db.SystemGroups.Add(systemGroup);
            _db.SaveChanges();

            model.Id = systemGroup.Id;

            var systemTrans = new SystemGroupTranslation()
            {
                Description = model.Description,
                Name = model.Name,
                LanguageId = model.LanguageId,
                IsDefault = model.LanguageId == CultureHelper.GetDefaultLanguageId(),
                SystemGroupId = model.Id
            };
            _db.SystemGroupTranslations.Add(systemTrans);

            if (!systemTrans.IsDefault)
            {
                var tran1 = new SystemGroupTranslation()
                {
                    Description = model.Description,
                    LanguageId = CultureHelper.GetDefaultLanguageId(),
                    IsDefault = true,
                    SystemGroupId = model.Id
                };
                _db.SystemGroupTranslations.Add(tran1);
            }
            UpdateUsers(model.Users, model.Id);
            _db.SaveChanges();
        }

        public void EditSystemGroup(SystemGroupViewModel systemGroupViewModel, SystemGroup systemGroup)
        {
            using (var db = new LearningManagementSystemContext())
            {
                systemGroup.Status = systemGroupViewModel.Status;

                if (systemGroupViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    systemGroup.Description = systemGroupViewModel.Description;
                    systemGroup.Name = systemGroupViewModel.Name;
                    
                }

                db.Entry(systemGroup).State = EntityState.Modified;
                db.SaveChanges();

                if (systemGroupViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systemGroupTranslation = db.SystemGroupTranslations.FirstOrDefault(r =>
                        r.LanguageId == systemGroupViewModel.LanguageId &&
                        r.SystemGroupId == systemGroupViewModel.Id);
                    if (systemGroupTranslation != null)
                    {
                        systemGroupTranslation.Description = systemGroupViewModel.Description;
                        systemGroupTranslation.Name = systemGroupViewModel.Description;
                        db.Entry(systemGroupTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var systemGroupTran = new SystemGroupTranslation()
                        {
                            Description = systemGroupViewModel.Description,
                            LanguageId = systemGroupViewModel.LanguageId,
                            SystemGroupId = systemGroup.Id,
                            Name = systemGroupViewModel.Name
                        };
                        db.SystemGroupTranslations.Add(systemGroupTran);
                    }

                    db.SaveChanges();
                }

            }


            UpdateUsers(systemGroupViewModel.Users, systemGroupViewModel.Id);


            _db.SaveChanges();
        }
        public SystemGroup GetSystemGroupById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var profile = db.SystemGroups.Find(id);
                return profile;
            }
        }
        public SystemGroupViewModel GetSystemGroupWithLanguageById(int id)
        {
            SystemGroup model = _db.SystemGroups.Find(id);
            var result = new SystemGroupViewModel(model);

            result.LanguageId = CultureHelper.GetDefaultLanguageId();

            var trans = _db.SystemGroupTranslations.FirstOrDefault(r => r.SystemGroupId == id && r.IsDefault);
            if (trans != null)
            {
                result.LanguageId = trans.LanguageId;
            }
            return result;
        }
        public void DeleteSystemGroupById(int id)
        {
            SystemGroup model = _db.SystemGroups.Find(id);
            if (model != null)
            {
                model.Status = (int)GeneralEnums.StatusEnum.Deleted;
                model.DeletedOn = DateTime.Now;
                _db.SaveChanges();
            }
        }


        public int GetSystemRole(string username)
        {
            var aspUser = _db.AspNetUsers.Include(r => r.AspNetUserRoles)
                    .FirstOrDefault(r => r.UserName == username);
                var aspRoles = _db.AspNetUserRoles.Include(r => r.Role).FirstOrDefault(r => r.UserId == aspUser.Id);
                return Int32.Parse(aspRoles.RoleId);
            
        }

        public int UsersNumber(int organizationId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var systemGroup = db.SystemGroups.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Id == organizationId).ToList();
                return systemGroup.Count;
            }

        }

        public SystemGroupViewModel GetSystemGroupWithLanguageById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var systemGroupTran =
                        db.SystemGroupTranslations.Include(r => r.SystemGroup).FirstOrDefault(r => r.LanguageId == languageId && r.SystemGroupId == id);
                    if (systemGroupTran != null)
                    {
                        return new SystemGroupViewModel(systemGroupTran);
                    }
                }

                var systemGroup = db.SystemGroups.Find(id);

                return new SystemGroupViewModel(systemGroup);
            }
        }

        #region Users

        public List<ListItem> GetSystemGroupUsers(int systemGroupId)
        {
            var groupUsers = _db.SystemGroupUsers.Where(x => x.SystemGroupId == systemGroupId)
                .Select(x => x.UserProfile.Id).ToArray();

            var userProfiles = _db.UserProfiles.ToList();

            List<ListItem> items = new List<ListItem>();
            foreach (var user in userProfiles)
            {
                bool selected = groupUsers.Contains(user.Id);

                items.Add(new ListItem() { Value = user.Id.ToString(), Text = user.Username, Selected = selected });
            }
            return items;

        }
       //we use this in memory just
        private void UpdateUsers(List<int> Users, int systemGroupId)
        {
            var groupUsers = _db.SystemGroupUsers.Where(x => x.SystemGroupId == systemGroupId);

            _db.SystemGroupUsers.RemoveRange(groupUsers);

            var userProfiles = _db.UserProfiles.Where(x => Users.Contains(x.Id)).Select(x => x.Id);

            foreach (var userProfileId in userProfiles)
            {
                _db.SystemGroupUsers.Add(new SystemGroupUser() { SystemGroupId = systemGroupId, UserProfileId = userProfileId });
            }
        }

     
        #endregion
    }
}
