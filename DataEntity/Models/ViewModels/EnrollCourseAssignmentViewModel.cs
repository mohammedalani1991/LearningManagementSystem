using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseAssignmentViewModel
    {
        public EnrollCourseAssignmentViewModel()
        {
        }

        public EnrollCourseAssignmentViewModel(EnrollCourseAssigment  assigment)
        {
            Id= assigment.Id;
            Name= assigment.Name;
            Description= assigment.Description;
            PublishDate= assigment.PublishDate;
            PublishEndDate= assigment.PublishEndDate;
            EnrollTeacherCourseId =assigment.EnrollTeacherCourseId;
            Status= assigment.Status;
            CreatedBy= assigment.CreatedBy;
            CreatedOn= assigment.CreatedOn;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Description1 { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishEndDate { get; set; }
        public int LanguageId { get; set; }
        public bool IsOnlineLearningMethod;
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

    }
}
