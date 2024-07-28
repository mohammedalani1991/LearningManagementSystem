using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SectionOfCourse
    {
        public SectionOfCourse()
        {
            Lectures = new HashSet<Lecture>();
            SectionOfCourseQuizzes = new HashSet<SectionOfCourseQuiz>();
            SectionOfCourseTranslations = new HashSet<SectionOfCourseTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public int? Order { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<SectionOfCourseQuiz> SectionOfCourseQuizzes { get; set; }
        public virtual ICollection<SectionOfCourseTranslation> SectionOfCourseTranslations { get; set; }
    }
}
