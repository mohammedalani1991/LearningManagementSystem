using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseTimeViewModel
    {

        public EnrollCourseTimeViewModel()
        {

        }

        public EnrollCourseTimeViewModel(EnrollCourseTime enrollCourseTime)
        {
            Id = enrollCourseTime.Id;
            EnrollCourseId = enrollCourseTime.EnrollCourseId;
            CreatedOn = enrollCourseTime.CreatedOn;
            Status = enrollCourseTime.Status;
            DayId = enrollCourseTime.DayId;
            FromTime = enrollCourseTime.FromTime;
            ToTime = enrollCourseTime.ToTime;
            CreatedBy = enrollCourseTime.CreatedBy;
            LearningMethodId = enrollCourseTime.LearningMethodId;
        }


        public int Id { get; set; }
        public int EnrollCourseId { get; set; }
        public int DayId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public int? LearningMethodId { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
    }
}
