using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;

namespace DataEntity.Models.ViewModels
{
    public class CourseCategoryViewModel
    {
        public CourseCategoryViewModel()
        {

        }

        public CourseCategoryViewModel(CourseCategoryTranslation coursecategory)
        {
            Id = coursecategory.CategoryId;
            Name = coursecategory.Name;
            Description = coursecategory.Description;
            LanguageId = coursecategory.LanguageId;
            Status = coursecategory.Category.Status;
            ImageUrl = coursecategory.Category.ImageUrl;
            ParentId = (coursecategory.Category.ParentId == null) ? 0 : coursecategory.Category.ParentId.Value;
            ShowInHomePage = coursecategory.Category.ShowInHomePage.Value;
            ParentName = (coursecategory.Category.ParentId == null) ? "--" : coursecategory.Category.Parent.Name;
            CreatedBy = coursecategory.Category.CreatedBy;
            CreatedOn = coursecategory.Category.CreatedOn;

        }

        public CourseCategoryViewModel(CourseCategory coursecategory)
        {
            Id = coursecategory.Id;
            Name = coursecategory.Name;
            Description = coursecategory.Description;
            ImageUrl = coursecategory.ImageUrl;
            ParentId = (coursecategory.ParentId == null) ? 0 : coursecategory.ParentId.Value;
            ShowInHomePage = coursecategory.ShowInHomePage.Value;
            CreatedBy = coursecategory.CreatedBy;
            CreatedOn = coursecategory.CreatedOn;
            Status = coursecategory.Status;
            ParentName = (coursecategory.ParentId == null) ? "--" : coursecategory.Parent.Name;
        }



        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int Page { get; set; }
        public string ParentName { get; set; }
        public List<CourseCategory> ListCourseCategorys { get; set; }


    }
}
