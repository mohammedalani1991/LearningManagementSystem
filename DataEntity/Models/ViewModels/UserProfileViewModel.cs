using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {

        }

        public UserProfileViewModel(UserProfileTranslation userProfile)
        {
            Id = userProfile.UserProfileId;
            Username = userProfile.UserProfile.Username;
            //Email = userProfile.UserProfile.Email;
            ContactId = userProfile.UserProfile.ContactId;
            CreatedOn = userProfile.UserProfile.CreatedOn;
            Status = userProfile.UserProfile.Status;
            LastLogin = userProfile.UserProfile.LastLogin;
            ProfilePhoto = userProfile.UserProfile.ProfilePhoto;
            PreferedLanguageId = userProfile.UserProfile.PreferedLanguageId;
            LanguageId = userProfile.LanguageId;

        }

        public UserProfileViewModel(UserProfile userProfile)
        {
            Id = userProfile.Id;
            Username = userProfile.Username;
            //Email = userProfile.Email;
            ContactId = userProfile.ContactId;
            CreatedOn = userProfile.CreatedOn;
            Status = userProfile.Status;
            LastLogin = userProfile.LastLogin;
            ProfilePhoto = userProfile.ProfilePhoto;
            PreferedLanguageId = userProfile.PreferedLanguageId;
        } 

        //public UserProfileViewModel(TrainerViewModel trainerViewModel)
        //{
        //    Id = trainerViewModel.UserProfileId??0;
        //    Username = trainerViewModel.Contact.FirstName;
        //    Password = trainerViewModel.Password;
        //    ContactId = trainerViewModel.ContactId;
        //    ConfirmPassword = trainerViewModel.ConfirmPassword;
        //    Email = trainerViewModel.Contact.Email;
        //    IdNumber = trainerViewModel.IdNumber;
        //    CreatedOn = trainerViewModel.CreatedOn;
        //    Status = trainerViewModel.Status;
        //    PreferedLanguageId = trainerViewModel.LanguageId;
        //    LanguageId = trainerViewModel.LanguageId.Value;
        //    RoleIds = trainerViewModel.RoleIds;
        //}

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberCode { get; set; }
        public string Mobile { get; set; }
        public string IdNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public int? GenderLookupId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ProfilePhoto { get; set; }
        public int? PreferedLanguageId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LanguageId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<string> RoleIds { get; set; }
        public int? ContactId { get; set; }
        public int? JobId { get; set; }
    }
}
