using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollStudentAlertViewModel
    {
        public EnrollStudentAlertViewModel()
        {

        }
        

        public EnrollStudentAlertViewModel(EnrollStudentAlert enrollStudentAlert)
        {
            Id = enrollStudentAlert.Id;
            AlertTypeId = enrollStudentAlert.AlertTypeId;
            Title = enrollStudentAlert.Title;
            Description = enrollStudentAlert.Description;
            CreatedBy = enrollStudentAlert.CreatedBy;
            CreatedOn = enrollStudentAlert.CreatedOn;
            Status = enrollStudentAlert.Status;
            EnrollStudentCourseId = enrollStudentAlert.EnrollStudentCourseId;
            EnrollTeacherCourseId = enrollStudentAlert.EnrollTeacherCourseId;

        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AlertTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public int Page { get; set; }
    }
}
