using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class TeacherAttendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public bool? Attended { get; set; }
        public string Note { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
    }
}
