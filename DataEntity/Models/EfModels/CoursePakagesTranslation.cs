using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CoursePakagesTranslation
    {
        public int Id { get; set; }
        public int CoursePackagesId { get; set; }
        public int LanguageId { get; set; }
        public string PackageName { get; set; }
        public string Notes { get; set; }

        public virtual CoursePackage CoursePackages { get; set; }
    }
}
