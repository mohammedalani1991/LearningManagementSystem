using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentExamAnswer
    {
        public EnrollStudentExamAnswer()
        {
            EnrollStudentExamAnswerOptions = new HashSet<EnrollStudentExamAnswerOption>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseExamQuestionId { get; set; }
        public string Answer { get; set; }
        public double Mark { get; set; }
        public int EnrollStudentExamId { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual EnrollCourseExamQuestion EnrollCourseExamQuestion { get; set; }
        public virtual EnrollStudentExam EnrollStudentExam { get; set; }
        public virtual ICollection<EnrollStudentExamAnswerOption> EnrollStudentExamAnswerOptions { get; set; }
    }
}
