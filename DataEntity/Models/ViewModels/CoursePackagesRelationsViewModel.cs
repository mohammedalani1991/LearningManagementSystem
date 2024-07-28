using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursePackagesRelationsViewModel
    {
        public CoursePackagesRelationsViewModel()
        {

        }


        public CoursePackagesRelationsViewModel(CoursePackagesRelation coursePackagesRelations)
        {
            CoursePackagesId = coursePackagesRelations.CoursePackagesId;
            CourseId = coursePackagesRelations.CourseId;
        }


        public int CoursePackagesId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual CoursePackage CoursePackages { get; set; }
    }
}
