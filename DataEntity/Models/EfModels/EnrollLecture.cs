using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollLecture
    {
        public EnrollLecture()
        {
            EnrollCourseExams = new HashSet<EnrollCourseExam>();
            EnrollCourseResources = new HashSet<EnrollCourseResource>();
            EnrollLectureTranslations = new HashSet<EnrollLectureTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public int EnrollSectionId { get; set; }
        public int EnrollCourseId { get; set; }
        public int? Order { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
        public virtual EnrollSectionOfCourse EnrollSection { get; set; }
        public virtual ICollection<EnrollCourseExam> EnrollCourseExams { get; set; }
        public virtual ICollection<EnrollCourseResource> EnrollCourseResources { get; set; }
        public virtual ICollection<EnrollLectureTranslation> EnrollLectureTranslations { get; set; }
    }
}
