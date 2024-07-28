using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalQuestion
    {
        public PracticalQuestion()
        {
            PracticalEnrollmentExamStudentSubjectRatings = new HashSet<PracticalEnrollmentExamStudentSubjectRating>();
            PracticalExamQuestions = new HashSet<PracticalExamQuestion>();
            PracticalQuestionTranslations = new HashSet<PracticalQuestionTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public decimal? Mark { get; set; }
        public bool? IsDiscountFromTotal { get; set; }
        public int? Type { get; set; }
        public bool? Main { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PracticalEnrollmentExamStudentSubjectRating> PracticalEnrollmentExamStudentSubjectRatings { get; set; }
        public virtual ICollection<PracticalExamQuestion> PracticalExamQuestions { get; set; }
        public virtual ICollection<PracticalQuestionTranslation> PracticalQuestionTranslations { get; set; }
    }
}
