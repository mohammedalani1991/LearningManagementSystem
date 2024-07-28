using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollmentCourseViewModel
    {
        public virtual EnrollTeacherCourseViewModel EnrollTeacherCourseViewModel { get; set; }
        public virtual CourseViewModel CourseViewModel { get; set; }
        public virtual List<TrainerViewModel> TrainerViewModel { get; set; }
        public virtual List<EnrollCourseTimeViewModel> EnrollCourseTimeViewModel { get; set; }
        public virtual List<EnrollCourseTimeCustomizationViewModel> EnrollCourseTimeCustomizationViewModel { get; set; }

    }
}
