using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollTeacherCourseTranlation
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string SectionName { get; set; }
        public int LanguageId { get; set; }
        public int EnrollCourseId { get; set; }
        public string NotesForEnrolled { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
    }
}
