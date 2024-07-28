using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CourseMark
    {
        public CourseMark()
        {
            CourseMarkTranslations = new HashSet<CourseMarkTranslation>();
        }

        public int Id { get; set; }
        public int? CourseId { get; set; }
        public decimal? Value { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public decimal? ValueTo { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<CourseMarkTranslation> CourseMarkTranslations { get; set; }
    }
}
