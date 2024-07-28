using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseResourceViewModel
    {
        public EnrollCourseResourceViewModel()
        {

        }

        public EnrollCourseResourceViewModel(EnrollCourseResourceTranslation enrollCourseResource)
        {
            Id = enrollCourseResource.EnrollCourseResourceId;
            ForEditModleID = enrollCourseResource.EnrollCourseResourceId; 
            Name = enrollCourseResource.Name;
            Type = enrollCourseResource.EnrollCourseResource.Type;
            Link = enrollCourseResource.EnrollCourseResource.Link;
            Description = enrollCourseResource.Description;
            CreatedBy = enrollCourseResource.EnrollCourseResource.CreatedBy;
            CreatedOn = enrollCourseResource.EnrollCourseResource.CreatedOn;
            Status = enrollCourseResource.EnrollCourseResource.Status;
            LanguageId = enrollCourseResource.LanguageId;
            EnrollLectureId = enrollCourseResource.EnrollCourseResource.EnrollLectureId;
            EnrollCourseId = enrollCourseResource.EnrollCourseResource.EnrollCourseId;
            Source = (enrollCourseResource.EnrollCourseResource.Link.ToLower().Contains("/document/") ? (int)GeneralEnums.ResourceSourceEnum.UploadFile : (int)GeneralEnums.ResourceSourceEnum.ExternalUrl);

        }

        public EnrollCourseResourceViewModel(EnrollCourseResource enrollCourseResource)
        {
            Id = enrollCourseResource.Id;
            ForEditModleID = enrollCourseResource.Id;
            Name = enrollCourseResource.Name;
            Type = enrollCourseResource.Type;
            Link = enrollCourseResource.Link;
            Description = enrollCourseResource.Description;
            CreatedBy = enrollCourseResource.CreatedBy;
            CreatedOn = enrollCourseResource.CreatedOn;
            Status = enrollCourseResource.Status;
            EnrollLectureId = enrollCourseResource.EnrollLectureId;
            EnrollCourseId = enrollCourseResource.EnrollCourseId;
            Source = (enrollCourseResource.Link.ToLower().Contains("/document/") ? (int)GeneralEnums.ResourceSourceEnum.UploadFile : (int)GeneralEnums.ResourceSourceEnum.ExternalUrl);
        }


        public int Id { get; set; }
        public int? ForEditModleID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
        public int EnrollLectureId { get; set; }
        public int EnrollCourseId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int Source { get; set; }
        public int LanguageId { get; set; }
        
        



    }
}
