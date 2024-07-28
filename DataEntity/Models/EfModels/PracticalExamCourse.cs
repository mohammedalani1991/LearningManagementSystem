using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalExamCourse
    {
        public PracticalExamCourse()
        {
            PracticalExamCourseSubjects = new HashSet<PracticalExamCourseSubject>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int PracticalExamId { get; set; }
        public int CourseId { get; set; }
        public int? SubjectMark { get; set; }

        public virtual Course Course { get; set; }
        public virtual PracticalExam PracticalExam { get; set; }
        public virtual ICollection<PracticalExamCourseSubject> PracticalExamCourseSubjects { get; set; }
    }
}
