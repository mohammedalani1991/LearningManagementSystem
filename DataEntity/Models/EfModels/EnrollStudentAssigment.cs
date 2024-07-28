using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentAssigment
    {
        public EnrollStudentAssigment()
        {
            EnrollStudentAssigmentAnswers = new HashSet<EnrollStudentAssigmentAnswer>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseAssigmentId { get; set; }
        public int EnrollStudentCourseId { get; set; }

        public virtual EnrollCourseAssigment EnrollCourseAssigment { get; set; }
        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual ICollection<EnrollStudentAssigmentAnswer> EnrollStudentAssigmentAnswers { get; set; }
    }
}
