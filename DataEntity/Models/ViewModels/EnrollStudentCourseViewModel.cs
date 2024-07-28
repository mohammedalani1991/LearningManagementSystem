using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
 public   class EnrollStudentCourseViewModel
    {
        public EnrollStudentCourseViewModel() { }

        public EnrollStudentCourseViewModel(EnrollStudentCourse enrollStudentCourse) {
            Id = enrollStudentCourse.Id;
            CourseId = enrollStudentCourse.CourseId;
            StudentId = enrollStudentCourse.StudentId;
            CreatedOn = enrollStudentCourse.CreatedOn;
            Status = enrollStudentCourse.Status;
            DeletedOn = enrollStudentCourse.DeletedOn;
            Price = enrollStudentCourse.Price;
            IsPass = enrollStudentCourse.IsPass;
            Mark = enrollStudentCourse.Mark;
            NeedApproval = enrollStudentCourse.NeedApproval;
            
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public decimal? Price { get; set; }
        public bool? IsPass { get; set; }
        public double? Mark { get; set; }
        public bool? NeedApproval { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }
    }
}
