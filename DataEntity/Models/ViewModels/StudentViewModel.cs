using DataEntity.Models.EfModels;
using System;

namespace DataEntity.Models.ViewModels
{
 public class StudentViewModel
    {
        public StudentViewModel()
        {

        }

        public StudentViewModel(StudentTranslation studentTran, ContactTranslation contactTran)
        {
            Id = studentTran.StudentId;
            ContactId = contactTran.ContactId;
            CreatedOn = contactTran.Contact.CreatedOn;
            CreatedBy = studentTran.Student.CreatedBy;
            Status = studentTran.Student.Status;
            BirthDate = studentTran.Student.BirthDate;
            BirthPlace = studentTran.BirthPlace;
            Work = studentTran.Work;
            EducationalLevelId = studentTran.Student.EducationalLevelId;
            Country = studentTran.Country;
            Address = studentTran.Address;
            WorkPlace = studentTran.WorkPlace;
            Email = studentTran.Student.Email;
            ExtraMobile = studentTran.Student.ExtraMobile;
            LanguageId = studentTran.LanguageId;
            FullName = contactTran.FullName;
            FirstName = contactTran.FirstName;
            SecondName = contactTran.SecondName;
            ThirdName = contactTran.ThirdName;
            LastName = contactTran.LastName;
            GenderId = contactTran.Contact.GenderId;
            Mobile = contactTran.Contact.Mobile;
            Status = contactTran.Contact.Status;
            BranchId = contactTran.Contact.BranchId;
            IdNumber = contactTran.Contact.IdNumber;
            TrainingConsultantId = studentTran.Student.TrainingConsultantId;
        }
        public StudentViewModel(Student student, Contact contact)
        {
            Id = student.Id;
            ContactId = contact.Id;
            CreatedOn = contact.CreatedOn;
            CreatedBy = student.CreatedBy;
            Status = contact.Status;
            BirthDate = student.BirthDate;
            BirthPlace = student.BirthPlace;
           
            Work = student.Work;
            EducationalLevelId = student.EducationalLevelId;
            Country = student.Country;
            Address = student.Address;
            WorkPlace = student.WorkPlace;
            Email = student.Email;
            ExtraMobile = student.ExtraMobile;
            FullName = contact.FullName;
            FirstName = contact.FirstName;
            SecondName = contact.SecondName;
            ThirdName = contact.ThirdName;
            LastName = contact.LastName;
            GenderId = contact.GenderId;
            Mobile = contact.Mobile;
            Status = contact.Status;
            BranchId = contact.BranchId;
            IdNumber= contact.IdNumber;
            TrainingConsultantId = student.TrainingConsultantId;
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string IdNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Work { get; set; }
        public int? EducationalLevelId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string WorkPlace { get; set; }
        public string Email { get; set; }
        public string ExtraMobile { get; set; }
        public int? ProgramId { get; set; }
        public int? RegimentId { get; set; }
        public decimal? SubscriptionPrice { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public DateTime? FirstInstallmentDate { get; set; }
        public int? InstallmentsNumber { get; set; }
        public decimal? CoverageValue { get; set; }
        public decimal? CoverageRatio { get; set; }
        public int? RightTimeId { get; set; }
        public int? PaymentWayId { get; set; }
        public int? LanguageId { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyStudentId { get;  set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public int? BranchId { get; set; }
        public int? TypeId { get; set; }
        public bool? IsSelect { get; set; }
        public int? CollegeId { get; set; }
        public int? SpecialtyId { get; set; }
        public bool? IsExternalStudy { get; set; }
        public bool? IsMedicalPast { get; set; }
        public bool? isFastSubscription { get; set; }
        public string Note { get; set; }
        public int? SubscriptionId { get; set; }
        public int? DiscountId { get; set; }
        public int? CommunicationChannelId { get; set; }
        public int? FirstSubChannelId { get; set; }
        public int? SecondSubChannelId { get; set; }
        public bool? IsTawjehe { get; set; }
        public decimal? Average { get; set; }
        public string ChairNumber { get; set; }
        public int? CampaignId { get; set; }
        public int? TrainingConsultantId { get; set; }
        public string PhoneNumberCode { get; set; }

    }
}
