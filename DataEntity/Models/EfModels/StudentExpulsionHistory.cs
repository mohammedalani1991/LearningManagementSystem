using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class StudentExpulsionHistory
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public int? TypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual Student Student { get; set; }
    }
}
