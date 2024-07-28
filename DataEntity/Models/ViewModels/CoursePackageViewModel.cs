using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursePackageViewModel
    {
        public CoursePackageViewModel()
        {

        }
        public CoursePackageViewModel(CoursePakagesTranslation CoursePakagesTranslation)
        {
            Id = CoursePakagesTranslation.CoursePackagesId;
            ForEditModleID = CoursePakagesTranslation.CoursePackagesId;
            CreatedBy = CoursePakagesTranslation.CoursePackages.CreatedBy;
            CreatedOn = CoursePakagesTranslation.CoursePackages.CreatedOn;
            Status = CoursePakagesTranslation.CoursePackages.Status;
            PackageName = CoursePakagesTranslation.PackageName;
            Notes = CoursePakagesTranslation.Notes;
            LanguageId = CoursePakagesTranslation.LanguageId;
        }

        public CoursePackageViewModel(CoursePackage CoursePackages)
        {
            Id = CoursePackages.Id;
            ForEditModleID = CoursePackages.Id;
            CreatedBy = CoursePackages.CreatedBy;
            CreatedOn = CoursePackages.CreatedOn;
            Status = CoursePackages.Status;
            PackageName = CoursePackages.PackageName;
            Notes = CoursePackages.Notes;
        }



        public int Id { get; set; }
        public int ForEditModleID { get; set; }
        public int LanguageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string PackageName { get; set; }
        public string Notes { get; set; }
        public int Page { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
        public List<CoursePackagesRelation> CoursePackagesRelations { get; set; }

    }
}
