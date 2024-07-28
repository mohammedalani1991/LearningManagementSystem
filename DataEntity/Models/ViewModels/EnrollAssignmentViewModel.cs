using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollAssignmentViewModel
    {
        public EnrollAssignmentViewModel()
        {

        }

        public EnrollAssignmentViewModel(EnrollAssignmentTranslation enrollAssignment)
        {
            Id = enrollAssignment.EnrollAssignmentId;
            ForEditModleID = enrollAssignment.EnrollAssignmentId;
            Name = enrollAssignment.Name;
            Description = enrollAssignment.Description;
            CreatedBy = enrollAssignment.EnrollAssignment.CreatedBy;
            CreatedOn = enrollAssignment.EnrollAssignment.CreatedOn;
            SubmissionDate = enrollAssignment.EnrollAssignment.SubmissionDate;
            ExpiryDate = enrollAssignment.EnrollAssignment.ExpiryDate;
            Status = enrollAssignment.EnrollAssignment.Status;
            LanguageId = enrollAssignment.LanguageId;
            EnrollCourseId = enrollAssignment.EnrollAssignment.EnrollCourseId;

        }

        public EnrollAssignmentViewModel(EnrollAssignment enrollAssignment)
        {
            Id = enrollAssignment.Id;
            ForEditModleID = enrollAssignment.Id;
            Name = enrollAssignment.Name;
            Description = enrollAssignment.Description;
            CreatedBy = enrollAssignment.CreatedBy;
            CreatedOn = enrollAssignment.CreatedOn;
            SubmissionDate = enrollAssignment.SubmissionDate;
            ExpiryDate = enrollAssignment.ExpiryDate;
            Status = enrollAssignment.Status;
            EnrollCourseId = enrollAssignment.EnrollCourseId;

        }


        public int Id { get; set; }
        public int ForEditModleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int EnrollCourseId { get; set; }
        public int Page { get; set; }
    }
}
