using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Lecture
    {
        public Lecture()
        {
            CourseResources = new HashSet<CourseResource>();
            EnrollCourseQuizzes = new HashSet<EnrollCourseQuiz>();
            LectureTranslations = new HashSet<LectureTranslation>();
            SectionOfCourseQuizzes = new HashSet<SectionOfCourseQuiz>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public int? Order { get; set; }

        public virtual SectionOfCourse Section { get; set; }
        public virtual ICollection<CourseResource> CourseResources { get; set; }
        public virtual ICollection<EnrollCourseQuiz> EnrollCourseQuizzes { get; set; }
        public virtual ICollection<LectureTranslation> LectureTranslations { get; set; }
        public virtual ICollection<SectionOfCourseQuiz> SectionOfCourseQuizzes { get; set; }
    }
}
