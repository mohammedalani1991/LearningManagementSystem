using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseAssigmentQuestionTranslation
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual EnrollCourseAssigmentQuestion Question { get; set; }
    }
}
