using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class LecturesContentViewModel
    {

        public virtual SectionOfCourseViewModel SectionOfCourseViewModel { get; set; }
        public virtual List<LectureViewModel> LectureViewModel { get; set; }

    }
}
