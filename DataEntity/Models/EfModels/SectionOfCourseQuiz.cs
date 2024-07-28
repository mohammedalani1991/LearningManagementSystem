using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SectionOfCourseQuiz
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? SectionOfCourseId { get; set; }
        public int? LectureId { get; set; }
        public bool? QuestionOne { get; set; }
        public bool? QuestionTwo { get; set; }
        public bool? QuestionThree { get; set; }
        public int? Order { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual SectionOfCourse SectionOfCourse { get; set; }
    }
}
