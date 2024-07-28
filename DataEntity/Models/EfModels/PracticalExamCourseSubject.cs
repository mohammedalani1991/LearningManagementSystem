using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalExamCourseSubject
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? PracticalExamCourseId { get; set; }
        public int? SubjectId { get; set; }

        public virtual PracticalExamCourse PracticalExamCourse { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
