using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCoursesContentViewModel
    {

        public virtual EnrollTeacherCourseViewModel EnrollTeacherCourseViewModel { get; set; }
        public virtual List<EnrollSectionOfCourseViewModel> EnrollSectionOfCourseViewModel { get; set; }

    }
}
