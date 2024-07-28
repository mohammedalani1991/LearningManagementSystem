using DataEntity.Models.EfModels;
using System;

namespace DataEntity.Models.ViewModels
{
    public class EditProfileViewModel
    {
        private EditProfileViewModel employeeViewModel;

        public EditProfileViewModel()
        { }

        public int ContactId { get; set; }
        public int LanguageId { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public string Address { get; set; }
        public int? PreferedLanguageId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int EducationalLevelId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberCode { get; set; }
        public string Signature { get; set; }
        public int StudentId { get; set; }
    }
}
