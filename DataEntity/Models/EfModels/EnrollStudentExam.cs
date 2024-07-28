using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentExam
    {
        public EnrollStudentExam()
        {
            EnrollStudentExamAnswers = new HashSet<EnrollStudentExamAnswer>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public double? FinalMark { get; set; }
        public double? MarkHeGot { get; set; }
        public double? MarkAfterConversion { get; set; }
        public int EnrollCourseExamId { get; set; }
        public int EnrollStudentCourseId { get; set; }

        public virtual EnrollCourseExam EnrollCourseExam { get; set; }
        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual ICollection<EnrollStudentExamAnswer> EnrollStudentExamAnswers { get; set; }
    }
}
