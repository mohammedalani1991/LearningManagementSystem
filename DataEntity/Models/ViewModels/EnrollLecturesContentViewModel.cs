using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollLecturesContentViewModel
    {

        public virtual EnrollSectionOfCourseViewModel EnrollSectionOfCourseViewModel { get; set; }
        public virtual List<EnrollLectureViewModel> EnrollLectureViewModel { get; set; }

    }
}
