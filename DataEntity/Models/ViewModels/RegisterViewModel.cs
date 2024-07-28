using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumberCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int EducationalLevelId { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
    }
}
