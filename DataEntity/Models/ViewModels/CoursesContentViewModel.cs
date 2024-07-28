using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursesContentViewModel
    {

        public virtual CourseViewModel CourseViewModel { get; set; }
        public virtual List<SectionOfCourseViewModel> SectionOfCourseViewModel { get; set; }

    }
}
