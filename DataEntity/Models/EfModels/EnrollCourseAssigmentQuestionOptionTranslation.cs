using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseAssigmentQuestionOptionTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int? OptionId { get; set; }

        public virtual EnrollCourseAssigmentQuestionOption Option { get; set; }
    }
}
