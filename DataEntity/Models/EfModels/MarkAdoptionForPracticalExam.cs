using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class MarkAdoptionForPracticalExam
    {
        public int Id { get; set; }
        public int? PracticalExamId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public bool? Value { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual PracticalExam PracticalExam { get; set; }
    }
}
