using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseAssigmentQuestionOption
    {
        public EnrollCourseAssigmentQuestionOption()
        {
            EnrollCourseAssigmentQuestionOptionTranslations = new HashSet<EnrollCourseAssigmentQuestionOptionTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public int? QuestionId { get; set; }

        public virtual EnrollCourseAssigmentQuestion Question { get; set; }
        public virtual ICollection<EnrollCourseAssigmentQuestionOptionTranslation> EnrollCourseAssigmentQuestionOptionTranslations { get; set; }
    }
}
