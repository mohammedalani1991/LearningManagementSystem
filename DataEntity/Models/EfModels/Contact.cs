using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Contact
    {
        public Contact()
        {
            Attendances = new HashSet<Attendance>();
            CommunicationLogs = new HashSet<CommunicationLog>();
            ContactTranslations = new HashSet<ContactTranslation>();
            ContactTypes = new HashSet<ContactType>();
            Emails = new HashSet<Email>();
            Employees = new HashSet<Employee>();
            EnrollCourseAllowUserRates = new HashSet<EnrollCourseAllowUserRate>();
            GeneralizationEmployees = new HashSet<GeneralizationEmployee>();
            InvoicesPays = new HashSet<InvoicesPay>();
            Notifications = new HashSet<Notification>();
            Students = new HashSet<Student>();
            Trainers = new HashSet<Trainer>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public int? BranchId { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Language { get; set; }
        public int? NationalityId { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string PhoneNumberCode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<CommunicationLog> CommunicationLogs { get; set; }
        public virtual ICollection<ContactTranslation> ContactTranslations { get; set; }
        public virtual ICollection<ContactType> ContactTypes { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<EnrollCourseAllowUserRate> EnrollCourseAllowUserRates { get; set; }
        public virtual ICollection<GeneralizationEmployee> GeneralizationEmployees { get; set; }
        public virtual ICollection<InvoicesPay> InvoicesPays { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
