using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseAllowUserRateViewModel
    {
        public EnrollCourseAllowUserRateViewModel()
        {

        }
        

        public EnrollCourseAllowUserRateViewModel(EnrollCourseAllowUserRate courseAllowUserRate)
        {
            Id = courseAllowUserRate.Id;
            ContactId = courseAllowUserRate.ContactId;
            RateTypeId = courseAllowUserRate.RateTypeId;
            CreatedBy = courseAllowUserRate.CreatedBy;
            CreatedOn = courseAllowUserRate.CreatedOn;
            Status = courseAllowUserRate.Status;
            EnrollTeacherCourseId = courseAllowUserRate.EnrollTeacherCourseId;

        }


        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? RateTypeId { get; set; }
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public int Page { get; set; }
        public decimal? Total { get; set; }
        public string FullName { get; set; }
        public string CourseName { get; set; }
        public string CourseCategory { get; set; }
        public string SemesterName { get; set; }
        public string SectionName { get; set; }
        public string Rate { get; set; }
        public string User { get; set; }
    }
}
