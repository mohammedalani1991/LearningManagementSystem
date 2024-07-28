using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CourseTranslation
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string AcquiredSkills { get; set; }
        public string TargetGroup { get; set; }
        public string Notes { get; set; }
        public int LanguageId { get; set; }
        public string CourseName { get; set; }
        public string Requirements { get; set; }
        public string QuestionDescription { get; set; }

        public virtual Course Course { get; set; }
    }
}
