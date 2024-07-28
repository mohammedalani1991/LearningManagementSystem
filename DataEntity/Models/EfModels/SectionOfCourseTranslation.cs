using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SectionOfCourseTranslation
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public int LanguageId { get; set; }

        public virtual SectionOfCourse Section { get; set; }
    }
}
