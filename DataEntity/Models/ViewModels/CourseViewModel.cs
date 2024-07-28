using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            TemplateIds = new List<int>();
        }

        public CourseViewModel(CourseTranslation course)
        {
            Id = course.CourseId;          
            CreatedBy = course.Course.CreatedBy;
            CreatedOn = course.Course.CreatedOn;
            Status = course.Course.Status;
            CourseName = course.CourseName;
            CourseDuration = course.Course.CourseDuration;
            CoursePrice = course.Course.CoursePrice;
            AcquiredSkills = course.AcquiredSkills;
            TargetGroup = course.TargetGroup;
            Notes = course.Notes;
            Requirements = course.Requirements;
            CategoryId = course.Course.CategoryId;
            CategoryName = (course.Course.CategoryId == null || course.Course?.Category == null) ? "--":course.Course?.Category?.Name;
            LearningMethodId = course.Course.LearningMethodId;
            LanguageId = course.LanguageId;
            ImageUrl = course.Course.ImageUrl;
            ShowInHomePage = course.Course.ShowInHomePage??false;
            QuestionDescription = course.QuestionDescription;
            NeedQuestion = course.Course.NeedQuestion ?? false;
            TemplateIds = course.Course.CourseCertifications.Select(r => r.TemplateHtmlId).ToList();
            SuccessMark =course.Course.SuccessMark;
            AssignmentMark = course.Course.AssignmentMark;
        }

        public CourseViewModel(Course course)
        {
            Id = course.Id;
            CreatedBy = course.CreatedBy;
            CreatedOn = course.CreatedOn;
            Status = course.Status;
            CourseName = course.CourseName;
            CourseDuration = course.CourseDuration;
            CoursePrice = course.CoursePrice;
            AcquiredSkills = course.AcquiredSkills;
            TargetGroup = course.TargetGroup;
            Notes = course.Notes;
            Requirements = course.Requirements;
            CategoryId = course.CategoryId;
            CategoryName = (course.CategoryId == null || course.Category == null) ? "--" : course.Category.Name;
            LearningMethodId = course.LearningMethodId;
            ImageUrl = course.ImageUrl;
            QuestionDescription = course.QuestionDescription;
            ShowInHomePage = course.ShowInHomePage ?? false;
            NeedQuestion = course.NeedQuestion ?? false;
            TemplateIds = course.CourseCertifications.Select(r=>r.TemplateHtmlId).ToList();
            SuccessMark = course.SuccessMark;
            AssignmentMark = course.AssignmentMark;
        }

        public CourseViewModel(Course course ,decimal? courseExchangePrice = 0)
        {
            Id = course.Id;
            CreatedBy = course.CreatedBy;
            CreatedOn = course.CreatedOn;
            Status = course.Status;
            CourseName = course.CourseName;
            CourseDuration = course.CourseDuration;
            CoursePrice = course.CoursePrice;
            AcquiredSkills = course.AcquiredSkills;
            TargetGroup = course.TargetGroup;
            Notes = course.Notes;
            Requirements = course.Requirements;
            CategoryId = course.CategoryId;
            CategoryName = (course.CategoryId == null || course.Category == null) ? "--" : course.Category.Name;
            LearningMethodId = course.LearningMethodId;
            ImageUrl = course.ImageUrl;
            QuestionDescription = course.QuestionDescription;
            ShowInHomePage = course.ShowInHomePage ?? false;
            NeedQuestion = course.NeedQuestion ?? false;
            CourseExchangePrice = courseExchangePrice;
            TemplateIds = course.CourseCertifications.Select(r => r.TemplateHtmlId).ToList();
            SuccessMark = course.SuccessMark;
            AssignmentMark = course.AssignmentMark;
        }



        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string CourseName { get; set; }
        public int? CourseDuration { get; set; }
        public decimal? CoursePrice { get; set; }
        public decimal? CourseExchangePrice { get; set; }
        public string AcquiredSkills { get; set; }
        public string TargetGroup { get; set; }
        public string Notes { get; set; }
        public string Requirements { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? LearningMethodId { get; set; }
        public int LanguageId { get; set; }       
        public int Page { get; set; }
        public string ImageUrl { get; set; }
        public bool ShowInHomePage { get; set; }
        public bool NeedQuestion { get; set; }
        public string QuestionDescription { get; set; }
        public int PrerequisiteCourses_Id { get; set; }
        public int PrerequisiteCourseId { get; set; }
        public decimal? SuccessMark { get; set; }
        public decimal? AssignmentMark { get; set; }
        public List<int> TemplateIds { get; set; }

    }
}
