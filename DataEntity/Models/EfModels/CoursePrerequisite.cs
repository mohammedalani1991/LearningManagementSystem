using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CoursePrerequisite
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int CourseId { get; set; }
        public int PrerequisiteCourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Course PrerequisiteCourse { get; set; }
    }
}
