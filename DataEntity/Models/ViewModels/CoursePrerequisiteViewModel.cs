using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CoursePrerequisiteViewModel
    {
        public CoursePrerequisiteViewModel()
        {

        }


        public CoursePrerequisiteViewModel(CoursePrerequisite coursePrerequisite)
        {
            Id = coursePrerequisite.Id;
            ForEditModleID = coursePrerequisite.Id;
            CreatedBy = coursePrerequisite.CreatedBy;
            CreatedOn = coursePrerequisite.CreatedOn;
            Status = coursePrerequisite.Status;
            CourseId = coursePrerequisite.CourseId;
            CourseName = (coursePrerequisite.Course == null) ? null : coursePrerequisite.Course.CourseName;
            PrerequisiteCourseName = (coursePrerequisite.PrerequisiteCourse == null) ? null : coursePrerequisite.PrerequisiteCourse.CourseName;
            PrerequisiteCourseId = coursePrerequisite.PrerequisiteCourseId;
            PrerequisiteNotes = coursePrerequisite.PrerequisiteCourse.Notes;
        }



        public int Id { get; set; }
        public int ForEditModleID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string PrerequisiteCourseName { get; set; }
        public int PrerequisiteCourseId { get; set; }
        public int Page { get; set; }
        public string PrerequisiteNotes { get; set; }
        public List<CourseViewModel> PrerequisiteCourses  { get; set; }
        public List<EnrollCourseExam> PrerequisiteExam { get; set; }

    }
}
