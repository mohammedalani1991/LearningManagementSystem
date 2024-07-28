using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class StudentBalanceHistory
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Balance { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual EnrollStudentCourse EnrollStudentCourse { get; set; }
        public virtual Student Student { get; set; }
    }
}
