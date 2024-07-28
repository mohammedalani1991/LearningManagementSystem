using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ManagementRate
    {
        public ManagementRate()
        {
            ManagementRateLines = new HashSet<ManagementRateLine>();
        }

        public int Id { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public decimal? Percent { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual ICollection<ManagementRateLine> ManagementRateLines { get; set; }
    }
}
