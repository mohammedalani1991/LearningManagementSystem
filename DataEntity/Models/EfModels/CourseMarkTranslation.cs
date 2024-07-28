using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CourseMarkTranslation
    {
        public int Id { get; set; }
        public int? CourseMarkId { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }

        public virtual CourseMark CourseMark { get; set; }
    }
}
