using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public  class EnrollCourseTimeCustomizationViewModel
    {

        public EnrollCourseTimeCustomizationViewModel()
        {

        }

        public EnrollCourseTimeCustomizationViewModel(EnrollCourseTimeCustomization enrollCourseTimeCustomization)
        {
            Id = enrollCourseTimeCustomization.Id;
            EnrollCourseId = enrollCourseTimeCustomization.EnrollCourseId;
            CreatedOn = enrollCourseTimeCustomization.CreatedOn;
            Status = enrollCourseTimeCustomization.Status;
            DayId = enrollCourseTimeCustomization.DayId;
            Date = enrollCourseTimeCustomization.Date;
            FromTime = enrollCourseTimeCustomization.FromTime;
            ToTime = enrollCourseTimeCustomization.ToTime;
            CreatedBy = enrollCourseTimeCustomization.CreatedBy;
            LearningMethodId = enrollCourseTimeCustomization.LearningMethodId;
        }

        public int Id { get; set; }
        public int EnrollCourseId { get; set; }
        public int DayId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public int? LearningMethodId { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
    }
}
