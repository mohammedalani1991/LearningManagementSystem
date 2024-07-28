using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursePrerequisiteGeneralViewModel
    {

        public int LanguageId { get; set; }
        public int Status { get; set; }
        public virtual List<CoursePrerequisiteViewModel> CoursePrerequisiteViewModel { get; set; }

    }
}
