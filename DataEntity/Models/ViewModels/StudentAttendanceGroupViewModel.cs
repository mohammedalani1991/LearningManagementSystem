using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class StudentAttendanceGroupViewModel
    {
        public int Id { get; set; }
        public int SectionCourseId { get; set; }
        public int StudentId { get; set; }
        public bool? IsPresent { get; set; }
        public string Program { get; set; }
        public string Section { get; set; }
        public int PresentDays { get; set; }
        public int TotalDaysCount { get; set; }
        public string Student { get; set; }
        public int? ProgramTypeId { get; set; }
        public int ProgramId { get; set; }
    }
}
