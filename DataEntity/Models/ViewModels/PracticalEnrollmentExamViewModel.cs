using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class PracticalEnrollmentExamViewModel
    {
        public PracticalEnrollmentExamViewModel()
        {
            
        }
        public PracticalEnrollmentExamViewModel(PracticalEnrollmentExam practical)
        {
            StartDate = practical.StartDate;
            EndDate = practical.EndDate;    
            TypeId = practical.TypeId;
            PracticalExamId = practical.Id;
            EnrollTeacherCourseId = practical.EnrollTeacherCourseId;
            Status= practical.Status;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? PracticalExamId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public int? TypeId { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
