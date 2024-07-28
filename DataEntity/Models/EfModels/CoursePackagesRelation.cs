using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CoursePackagesRelation
    {
        public int Id { get; set; }
        public int CoursePackagesId { get; set; }
        public int CourseId { get; set; }

        public virtual EnrollTeacherCourse Course { get; set; }
        public virtual CoursePackage CoursePackages { get; set; }
    }
}
