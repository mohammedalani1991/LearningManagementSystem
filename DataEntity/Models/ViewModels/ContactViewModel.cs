using DataEntity.Models.EfModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntity.Models.ViewModels
{
  public class ContactViewModel
    {
        private EmployeeViewModel employeeViewModel;

        //public ContactViewModel(TrainerViewModel trainerViewModel)
        //{
        //    CreatedOn = DateTime.Now;
        //    FullName = trainerViewModel.Contact.FirstName +" "+ trainerViewModel.Contact.SecondName + " " 
        //        + trainerViewModel.Contact.ThirdName + " " + trainerViewModel.Contact.LastName;
        //    GenderId = trainerViewModel.Contact.GenderId;
        //    Mobile = trainerViewModel.Contact.Mobile;
        //    Status = trainerViewModel.Status;
        //    BranchId = trainerViewModel.Contact.BranchId;
        //    LanguageId = trainerViewModel.LanguageId.Value;
        //    Email = trainerViewModel.Contact.Email;
        //    FirstName = trainerViewModel.Contact.FirstName;
        //    SecondName = trainerViewModel.Contact.SecondName;
        //    ThirdName = trainerViewModel.Contact.ThirdName;
        //    LastName = trainerViewModel.Contact.LastName;
        //}

        public ContactViewModel()
        { }
        public ContactViewModel(ContactViewModel contactViewModel) {
            CreatedOn = contactViewModel.CreatedOn;
            FullName = contactViewModel.FullName;
            FirstName = contactViewModel.FirstName;
            SecondName = contactViewModel.SecondName;
            ThirdName = contactViewModel.ThirdName;
            LastName = contactViewModel.LastName;
            GenderId = contactViewModel.GenderId;
            Mobile = contactViewModel.Mobile;
            Status = contactViewModel.Status;
            TypeId = contactViewModel.TypeId;
            BranchId = contactViewModel.BranchId;
            LanguageId = contactViewModel.LanguageId;
            Email = contactViewModel.Email;
            IdNumber = contactViewModel.IdNumber;
            CityId = contactViewModel.CityId;
            CountryId = contactViewModel.CountryId;
            PhoneNumberCode = contactViewModel.PhoneNumberCode;

        }

        public ContactViewModel(RegisterViewModel registerViewModel)
        {
            FullName = registerViewModel.FullName;
            FirstName = registerViewModel.FirstName;
            SecondName = registerViewModel.SecondName;
            ThirdName = registerViewModel.ThirdName;
            LastName = registerViewModel.LastName;
            GenderId = registerViewModel.GenderId;
            Mobile = registerViewModel.PhoneNumber;
            Status = registerViewModel.Status;
            LanguageId = registerViewModel.LanguageId;
            Email = registerViewModel.Email;
            CityId = registerViewModel.CityId;
            CountryId = registerViewModel.CountryId;
            PhoneNumberCode = registerViewModel.PhoneNumberCode;
        }

        public ContactViewModel(StudentViewModel studentViewModel) {
            CreatedOn = studentViewModel.CreatedOn;
            FullName = studentViewModel.FullName;
            FirstName = studentViewModel.FirstName;
            SecondName = studentViewModel.SecondName;
            ThirdName = studentViewModel.ThirdName;
            LastName = studentViewModel.LastName;
            GenderId = studentViewModel.GenderId;
            Mobile = studentViewModel.Mobile;
            Status = studentViewModel.Status;
            TypeId = studentViewModel.TypeId;
            BranchId = studentViewModel.BranchId;
            LanguageId = studentViewModel.LanguageId.Value;
            Email = studentViewModel.Email;
            PhoneNumberCode = studentViewModel.PhoneNumberCode;
        }

        public ContactViewModel(ContactTranslation contactTran)
        {
            Id = contactTran.ContactId;
            CreatedOn = DateTime.Now;
            FullName = contactTran.FullName;
            FirstName = contactTran.FirstName;
            SecondName = contactTran.SecondName;
            ThirdName = contactTran.ThirdName;
            LastName = contactTran.LastName;
            GenderId = contactTran.Contact.GenderId;
            Mobile = contactTran.Contact.Mobile;
            Status = contactTran.Contact.Status;
            BranchId = contactTran.Contact.BranchId;
            LanguageId = contactTran.LanguageId;
            Email = contactTran.Contact.Email;
            IdNumber = contactTran.Contact.IdNumber;
            CityId = contactTran.Contact.CityId;
            CountryId = contactTran.Contact.CountryId;
            PhoneNumberCode = contactTran.Contact.PhoneNumberCode;
        }

        public ContactViewModel(Contact contact)
        {
            Id = contact.Id;
            CreatedOn = contact.CreatedOn;
            FullName = contact.FullName;
            FirstName = contact.FirstName;
            SecondName = contact.SecondName;
            ThirdName = contact.ThirdName;
            LastName = contact.LastName;
            GenderId = contact.GenderId;
            Email = contact.Email;
            Mobile = contact.Mobile;
            Status = contact.Status;
            BranchId = contact.BranchId;
            IdNumber = contact.IdNumber;
            CountryId = contact.CountryId;
            CityId = contact.CityId;
            PhoneNumberCode = contact.PhoneNumberCode;
        }

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Mobile { get; set; }
        public string PhoneNumberCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? GenderId { get; set; }
        public int? BranchId { get; set; }
        public int? TypeId { get; set; }
        public string Email { get; set; }
    }
}
