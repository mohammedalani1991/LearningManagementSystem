using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CourseResourceTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int CourseResourceId { get; set; }
        public string Description { get; set; }

        public virtual CourseResource CourseResource { get; set; }
    }
}
