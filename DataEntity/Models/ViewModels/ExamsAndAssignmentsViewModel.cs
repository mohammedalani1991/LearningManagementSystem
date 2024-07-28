using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class ExamsAndAssignmentsViewModel
    {
        public int Id { get; set; }
        public int EnrollCourseId { get; set; }
        public int SemesterId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string SemesterName { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? PublicationEndDate { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public int NumberOfStudents { get; set; }
        public int NumberOfSubmitted { get; set; }
        public int? NumberOfSuccess { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }

    }
}
