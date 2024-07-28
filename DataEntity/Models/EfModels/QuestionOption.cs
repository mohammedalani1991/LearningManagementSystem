using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class QuestionOption
    {
        public QuestionOption()
        {
            EnrollStudentExamAnswerOptions = new HashSet<EnrollStudentExamAnswerOption>();
            QuestionOptionTranslations = new HashSet<QuestionOptionTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<EnrollStudentExamAnswerOption> EnrollStudentExamAnswerOptions { get; set; }
        public virtual ICollection<QuestionOptionTranslation> QuestionOptionTranslations { get; set; }
    }
}
