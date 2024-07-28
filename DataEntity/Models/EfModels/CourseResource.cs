using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CourseResource
    {
        public CourseResource()
        {
            CourseResourceTranslations = new HashSet<CourseResourceTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
        public int LectureId { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }

        public virtual Course Course { get; set; }
        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<CourseResourceTranslation> CourseResourceTranslations { get; set; }
    }
}
