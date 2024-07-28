using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class StudentAttendanceViewModel
    {
        public StudentAttendanceViewModel()
        {
            EnrollStudentCourseIds = new List<int>();
            AbsenceNotes = new List<string>();
        }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int EnrollStudentCourseId { get; set; }
        public string AbsenceNote { get; set; }
        public bool? IsPresent { get; set; }
        public DateTime? Date { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public List<int> EnrollStudentCourseIds { get; set; }
        public List<int> EnrollStudentCourseIds1 { get; set; }
        public List<string> AbsenceNotes { get; set; }

    }
}
