using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollSectionOfCourseTranslation
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int EnrollSectionId { get; set; }
        public int LanguageId { get; set; }

        public virtual EnrollSectionOfCourse EnrollSection { get; set; }
    }
}
