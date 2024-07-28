using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class MarkAdoptionForExam
    {
        public int Id { get; set; }
        public int? ExamTemplateId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public bool? Value { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual ExamTemplate ExamTemplate { get; set; }
    }
}
