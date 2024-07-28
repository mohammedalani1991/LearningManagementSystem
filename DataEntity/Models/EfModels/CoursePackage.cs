using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CoursePackage
    {
        public CoursePackage()
        {
            CoursePackagesRelations = new HashSet<CoursePackagesRelation>();
            CoursePakagesTranslations = new HashSet<CoursePakagesTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string PackageName { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<CoursePackagesRelation> CoursePackagesRelations { get; set; }
        public virtual ICollection<CoursePakagesTranslation> CoursePakagesTranslations { get; set; }
    }
}
