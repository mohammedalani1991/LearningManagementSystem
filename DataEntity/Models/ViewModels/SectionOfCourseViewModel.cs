using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class SectionOfCourseViewModel
    {
        public SectionOfCourseViewModel()
        {

        }

        public SectionOfCourseViewModel(SectionOfCourseTranslation SectionOfCourse)
        {
            Id = SectionOfCourse.SectionId;
            ForEditModleID = SectionOfCourse.SectionId;
            ForEnrollModleID = SectionOfCourse.Id.ToString();
            SectionName = SectionOfCourse.SectionName;
            Description = SectionOfCourse.Description;
            CreatedBy = SectionOfCourse.Section.CreatedBy;
            CreatedOn = SectionOfCourse.Section.CreatedOn;
            Status = SectionOfCourse.Section.Status;
            LanguageId = SectionOfCourse.LanguageId;
            CourseId = SectionOfCourse.Section.CourseId;

        }

        public SectionOfCourseViewModel(SectionOfCourse SectionOfCourse)
        {
            Id = SectionOfCourse.Id;
            ForEditModleID = SectionOfCourse.Id;
            ForEnrollModleID = SectionOfCourse.Id.ToString();
            SectionName = SectionOfCourse.SectionName;
            CreatedBy = SectionOfCourse.CreatedBy;
            CreatedOn = SectionOfCourse.CreatedOn;
            Status = SectionOfCourse.Status;
            Description = SectionOfCourse.Description;
            CourseId = SectionOfCourse.CourseId;

        }


        public int Id { get; set; }
        public int ForEditModleID { get; set; }
        public string ForEnrollModleID { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int CourseId { get; set; }
        public int Page { get; set; }
        public List<LectureViewModel> LectureViewModel { get; set; }
        public List<Course> ListCourse { get; set; }
        public List<Lecture> Listlecture { get; set; }


    }
}
