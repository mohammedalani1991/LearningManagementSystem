using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentCourseAttachment
    {
        public int Id { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string FileAttached { get; set; }
        public string Notes { get; set; }

        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
    }
}
