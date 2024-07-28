using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalEnrollmentExamStudent
    {
        public PracticalEnrollmentExamStudent()
        {
            PracticalEnrollmentExamStudentSubjects = new HashSet<PracticalEnrollmentExamStudentSubject>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? PracticalEnrollmentExamId { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public decimal? Mark { get; set; }
        public decimal? MarkAfterConversion { get; set; }

        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual PracticalEnrollmentExam PracticalEnrollmentExam { get; set; }
        public virtual ICollection<PracticalEnrollmentExamStudentSubject> PracticalEnrollmentExamStudentSubjects { get; set; }
    }
}
