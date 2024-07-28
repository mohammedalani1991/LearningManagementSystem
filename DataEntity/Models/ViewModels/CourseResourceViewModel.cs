using System;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;

namespace DataEntity.Models.ViewModels
{
    public class CourseResourceViewModel
    {
        public CourseResourceViewModel()
        {

        }

        public CourseResourceViewModel(CourseResourceTranslation CourseResource)
        {
            Id = CourseResource.CourseResourceId;
            ForEditModleID = CourseResource.CourseResourceId;
            Name = CourseResource.Name;
            Description = CourseResource.Description;
            CreatedBy = CourseResource.CourseResource.CreatedBy;
            CreatedOn = CourseResource.CourseResource.CreatedOn;
            Status = CourseResource.CourseResource.Status;
            LanguageId = CourseResource.LanguageId;
            Type = CourseResource.CourseResource.Type;
            Link = CourseResource.CourseResource.Link;
            CourseId = CourseResource.CourseResource.CourseId;
            LectureId = CourseResource.CourseResource.LectureId;
            Source = (CourseResource.CourseResource.Link.ToLower().Contains("/document/") ? (int)GeneralEnums.ResourceSourceEnum.UploadFile : (int)GeneralEnums.ResourceSourceEnum.ExternalUrl);

        }

        public CourseResourceViewModel(CourseResource CourseResource)
        {
            Id = CourseResource.Id;
            ForEditModleID = CourseResource.Id;
            Name = CourseResource.Name;
            CreatedBy = CourseResource.CreatedBy;
            CreatedOn = CourseResource.CreatedOn;
            Status = CourseResource.Status;
            Description = CourseResource.Description;
            LectureId = CourseResource.LectureId;
            Type = CourseResource.Type;
            Link = CourseResource.Link;
            CourseId = CourseResource.CourseId;
            LectureId = CourseResource.LectureId;
            Source = (CourseResource.Link.ToLower().Contains("/document/") ? (int)GeneralEnums.ResourceSourceEnum.UploadFile : (int)GeneralEnums.ResourceSourceEnum.ExternalUrl);
        }


        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int LectureId { get; set; }
        public int Source { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
        public int Page { get; set; }
        public int? ForEditModleID { get; set; }


    }
}
