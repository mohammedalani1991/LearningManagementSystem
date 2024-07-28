using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ExamTemplateTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public int ExamId { get; set; }

        public virtual ExamTemplate Exam { get; set; }
    }
}
