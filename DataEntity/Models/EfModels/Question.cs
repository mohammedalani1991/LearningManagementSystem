using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Question
    {
        public Question()
        {
            ExamQuestions = new HashSet<ExamQuestion>();
            QuestionOptions = new HashSet<QuestionOption>();
            QuestionTranslations = new HashSet<QuestionTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int? Mark { get; set; }
        public int? CategoryId { get; set; }
        public int? CourseId { get; set; }
        public int Type { get; set; }
        public bool? CertifiedBankQuestion { get; set; }
        public int? TeacherId { get; set; }

        public virtual Trainer Teacher { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<QuestionTranslation> QuestionTranslations { get; set; }
    }
}
