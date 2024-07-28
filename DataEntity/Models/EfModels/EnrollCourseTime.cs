using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseTime
    {
        public int Id { get; set; }
        public int EnrollCourseId { get; set; }
        public int DayId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? LearningMethodId { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
    }
}
