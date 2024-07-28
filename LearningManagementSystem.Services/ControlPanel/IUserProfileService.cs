using System.Collections.Generic;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IUserProfileService
    {
        IPagedList<UserProfile> GetUserProfiles(string searchText, int? page, int RoleId);
        UserProfile GetUserProfileByUsername(string username);
        UserProfile GetUserProfileByEmail(string email);
        bool GetContactEmail(string email);
        EditProfileViewModel GetUserByUsername(string username, string language = "English");
        bool IsIdentityExist(string idNumber);
        void AddUserProfile(UserProfileViewModel profile);
        UserProfileViewModel GetUserProfileViewModel(UserProfile profile);
        void EditUserProfile(UserProfileViewModel profile);
        void UpdateUserProfile(UserProfileViewModel profile);
        void UpdateUserProfile(EditProfileViewModel profile);
        void UpdateUserProfile(UserProfile profile);
        UserProfile GetUserProfileById(int id);
        UserProfileViewModel GetUserProfileById(int id, int languageId);

        void DeleteUserProfileById(UserProfile profile);
        void ReactivateAccount(int id);
        List<AspNetRole> GetActiveRoles();
        AspNetUser AddRoleToUser(RoleViewModel role);
        AspNetUser DeleteUserRole(RoleViewModel role);
        List<AspNetUserRole> GetUserRoles(int userId);
        bool DeleteUserAndRoles(string username);
        void RemoveUserProfileById(int id);
        int GetUserRole(string userName);
        UserProfile GetUserProfileByEmployeeId(int id);
        void ChangeEmail(string oldEmail, string newEmail);
    }
}
