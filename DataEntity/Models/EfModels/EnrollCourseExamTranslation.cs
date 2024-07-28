using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseExamTranslation
    {
        public int Id { get; set; }
        public int EnrollCourseExamId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual EnrollCourseExam EnrollCourseExam { get; set; }
    }
}
