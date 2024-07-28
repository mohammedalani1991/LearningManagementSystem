using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursePackagesGeneralViewModel
    {

        public int LanguageId { get; set; }
        public int Status { get; set; }
        public virtual List<CoursePackageViewModel> CoursePackageViewModel { get; set; }

    }
}
