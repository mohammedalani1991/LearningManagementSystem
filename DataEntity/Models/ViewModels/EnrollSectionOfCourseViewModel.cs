using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollSectionOfCourseViewModel
    {
        public EnrollSectionOfCourseViewModel()
        {

        }

        public EnrollSectionOfCourseViewModel(EnrollSectionOfCourseTranslation EnrollSectionOfCourse)
        {
            Id = EnrollSectionOfCourse.EnrollSectionId;
            ForEditModleID = EnrollSectionOfCourse.EnrollSectionId;
            SectionName = EnrollSectionOfCourse.SectionName;
            Description = EnrollSectionOfCourse.Description;
            CreatedBy = EnrollSectionOfCourse.EnrollSection.CreatedBy;
            CreatedOn = EnrollSectionOfCourse.EnrollSection.CreatedOn;
            Status = EnrollSectionOfCourse.EnrollSection.Status;
            LanguageId = EnrollSectionOfCourse.LanguageId;
            EnrollCourseId = EnrollSectionOfCourse.EnrollSection.EnrollCourseId;

        }

        public EnrollSectionOfCourseViewModel(EnrollSectionOfCourse EnrollSectionOfCourse)
        {
            Id = EnrollSectionOfCourse.Id;
            ForEditModleID = EnrollSectionOfCourse.Id;
            SectionName = EnrollSectionOfCourse.SectionName;
            CreatedBy = EnrollSectionOfCourse.CreatedBy;
            CreatedOn = EnrollSectionOfCourse.CreatedOn;
            Status = EnrollSectionOfCourse.Status;
            Description = EnrollSectionOfCourse.Description;
            EnrollCourseId = EnrollSectionOfCourse.EnrollCourseId;

        }


        public int Id { get; set; }
        public int ForEditModleID { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int EnrollCourseId { get; set; }
        public int Page { get; set; }
        public List<EnrollLectureViewModel> EnrollLectureViewModel { get; set; }
        public List<Course> ListCourse { get; set; }
        public List<EnrollLecture> EnrollLecture { get; set; }


    }
}
