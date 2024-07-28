using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Student
    {
        public Student()
        {
            EnrollStudentCourses = new HashSet<EnrollStudentCourse>();
            StudentAttendances = new HashSet<StudentAttendance>();
            StudentBalanceHistories = new HashSet<StudentBalanceHistory>();
            StudentExpulsionHistories = new HashSet<StudentExpulsionHistory>();
            StudentNotes = new HashSet<StudentNote>();
            StudentTranslations = new HashSet<StudentTranslation>();
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Work { get; set; }
        public int EducationalLevelId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string WorkPlace { get; set; }
        public string Email { get; set; }
        public string ExtraMobile { get; set; }
        public int? CollegeId { get; set; }
        public int? SpecialtyId { get; set; }
        public bool? IsExternalStudy { get; set; }
        public bool? IsMedicalPast { get; set; }
        public bool? IsFastSubscription { get; set; }
        public string MedicalDescription { get; set; }
        public int? TrainingConsultantId { get; set; }
        public decimal? Balance { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<EnrollStudentCourse> EnrollStudentCourses { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<StudentBalanceHistory> StudentBalanceHistories { get; set; }
        public virtual ICollection<StudentExpulsionHistory> StudentExpulsionHistories { get; set; }
        public virtual ICollection<StudentNote> StudentNotes { get; set; }
        public virtual ICollection<StudentTranslation> StudentTranslations { get; set; }
    }
}
