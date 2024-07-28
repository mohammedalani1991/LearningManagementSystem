using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Subject
    {
        public Subject()
        {
            PracticalEnrollmentExamStudentSubjects = new HashSet<PracticalEnrollmentExamStudentSubject>();
            PracticalExamCourseSubjects = new HashSet<PracticalExamCourseSubject>();
            SubjectTranslations = new HashSet<SubjectTranslation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? TypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? ExamTypeId { get; set; }

        public virtual ICollection<PracticalEnrollmentExamStudentSubject> PracticalEnrollmentExamStudentSubjects { get; set; }
        public virtual ICollection<PracticalExamCourseSubject> PracticalExamCourseSubjects { get; set; }
        public virtual ICollection<SubjectTranslation> SubjectTranslations { get; set; }
    }
}
