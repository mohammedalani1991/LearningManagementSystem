using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
 public   class EnrollStudentCourseAttachmentViewModel
    {
        public EnrollStudentCourseAttachmentViewModel() { }

        public EnrollStudentCourseAttachmentViewModel(EnrollStudentCourseAttachment enrollStudentCourseAttachment) {
            Id = enrollStudentCourseAttachment.Id;
            EnrollStudentCourseId = enrollStudentCourseAttachment.EnrollStudentCourseId;
            CreatedOn = enrollStudentCourseAttachment.CreatedOn;
            CreatedBy = enrollStudentCourseAttachment.CreatedBy;
            Status = enrollStudentCourseAttachment.Status;
            FileAttached = enrollStudentCourseAttachment.FileAttached;
            Notes = enrollStudentCourseAttachment.Notes;
        }

        public int Id { get; set; }
        public int? EnrollStudentCourseId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string FileAttached { get; set; }
        public string Notes { get; set; }

    }
}
