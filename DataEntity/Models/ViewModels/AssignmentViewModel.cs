using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public AssignmentViewModel()
        {

        }

        public AssignmentViewModel(AssignmentTranslation assignment)
        {
            Id = assignment.AssignmentId;
            Name = assignment.Name;
            Description = assignment.Description;
            CourseId= assignment.Assignment.CourseId;
            CreatedBy = assignment.Assignment.CreatedBy;
            CreatedOn = assignment.Assignment.CreatedOn;
            SubmissionDate = assignment.Assignment.SubmissionDate;
            ExpiryDate = assignment.Assignment.ExpiryDate;
            Status = assignment.Assignment.Status;
            LanguageId = assignment.LanguageId;
            CourseName = assignment.Assignment.Course.CourseName;
           
        }

        public AssignmentViewModel(Assignment assignment)
        {
            Id = assignment.Id;
            Name = assignment.Name;
            Description = assignment.Description;
            CreatedBy = assignment.CreatedBy;
            CreatedOn = assignment.CreatedOn;
            Status = assignment.Status;
            CourseId = assignment.CourseId;
            SubmissionDate = assignment.SubmissionDate;
            ExpiryDate = assignment.ExpiryDate;
            CourseName = assignment.Course.CourseName;
        }



        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int Page { get; set; }
        public List<Course> ListCourse { get; set; }

    }
}
