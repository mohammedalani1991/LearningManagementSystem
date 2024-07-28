using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ExamQuestion
    {
        public ExamQuestion()
        {
            EnrollCourseExamQuestions = new HashSet<EnrollCourseExamQuestion>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? TemplateId { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ExamTemplate Template { get; set; }
        public virtual ICollection<EnrollCourseExamQuestion> EnrollCourseExamQuestions { get; set; }
    }
}
