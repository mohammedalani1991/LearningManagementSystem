using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentAlert
    {
        public int Id { get; set; }
        public int? AlertTypeId { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
    }
}
