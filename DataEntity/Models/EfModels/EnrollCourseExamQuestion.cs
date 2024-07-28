using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseExamQuestion
    {
        public EnrollCourseExamQuestion()
        {
            EnrollStudentExamAnswers = new HashSet<EnrollStudentExamAnswer>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseExamId { get; set; }
        public int QuestionId { get; set; }
        public double? Mark { get; set; }

        public virtual EnrollCourseExam EnrollCourseExam { get; set; }
        public virtual ExamQuestion Question { get; set; }
        public virtual ICollection<EnrollStudentExamAnswer> EnrollStudentExamAnswers { get; set; }
    }
}
