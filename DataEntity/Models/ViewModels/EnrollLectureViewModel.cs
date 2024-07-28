using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollLectureViewModel
    {
        public EnrollLectureViewModel()
        {

        }

        public EnrollLectureViewModel(EnrollLectureTranslation enrollLecture)
        {
            Id = enrollLecture.EnrollLectureId;
            ForEditModleID = enrollLecture.EnrollLectureId; ;
            LectureName = enrollLecture.LectureName;
            Description = enrollLecture.Description;
            CreatedBy = enrollLecture.EnrollLecture.CreatedBy;
            CreatedOn = enrollLecture.EnrollLecture.CreatedOn;
            Status = enrollLecture.EnrollLecture.Status;
            LanguageId = enrollLecture.LanguageId;
            EnrollSectionId = enrollLecture.EnrollLecture.EnrollSectionId;
            EnrollCourseId = enrollLecture.EnrollLecture.EnrollCourseId;

        }

        public EnrollLectureViewModel(EnrollLecture EnrollLecture)
        {
            Id = EnrollLecture.Id;
            ForEditModleID = EnrollLecture.Id;
            LectureName = EnrollLecture.LectureName;
            CreatedBy = EnrollLecture.CreatedBy;
            CreatedOn = EnrollLecture.CreatedOn;
            Status = EnrollLecture.Status;
            Description = EnrollLecture.Description;
            EnrollSectionId = EnrollLecture.EnrollSectionId;
            EnrollCourseId = EnrollLecture.EnrollCourseId;
        }


        public int Id { get; set; }
        public int? ForEditModleID { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int EnrollSectionId { get; set; }
        public int EnrollCourseId { get; set; }
        public int Page { get; set; }
        public int? Order { get; set; }
        public List<EnrollCourseResourceViewModel> EnrollCourseResourceViewModel { get; set; }


    }
}
