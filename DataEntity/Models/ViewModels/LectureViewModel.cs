using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class LectureViewModel
    {
        public LectureViewModel()
        {

        }

        public LectureViewModel(LectureTranslation Lecture)
        {
            Id = Lecture.LectureId;
            ForEditModleID = Lecture.LectureId;
            LectureName = Lecture.LectureName;
            Description = Lecture.Description;
            CreatedBy = Lecture.Lecture.CreatedBy;
            CreatedOn = Lecture.Lecture.CreatedOn;
            Status = Lecture.Lecture.Status;
            Order = Lecture.Lecture.Order;
            LanguageId = Lecture.LanguageId;
        }

        public LectureViewModel(Lecture Lecture)
        {
            Id = Lecture.Id;
            ForEditModleID = Lecture.Id;
            LectureName = Lecture.LectureName;
            CreatedBy = Lecture.CreatedBy;
            CreatedOn = Lecture.CreatedOn;
            Status = Lecture.Status;
            Description = Lecture.Description;
            SectionId = Lecture.SectionId;
            Order = Lecture.Order;
        }


        public int Id { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int SectionId { get; set; }
        public int Page { get; set; }
        public int? Order { get; set; }

        public int? ForEditModleID { get; set; }
        public List<CourseResourceViewModel> CourseResourceViewModel { get; set; }

    }
}
