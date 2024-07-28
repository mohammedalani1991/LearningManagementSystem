using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseExam
    {
        public EnrollCourseExam()
        {
            EnrollCourseExamQuestions = new HashSet<EnrollCourseExamQuestion>();
            EnrollCourseExamTranslations = new HashSet<EnrollCourseExamTranslation>();
            EnrollStudentExams = new HashSet<EnrollStudentExam>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishEndDate { get; set; }
        public bool? IsPrerequisite { get; set; }
        public int? ExamTemplateId { get; set; }
        public int? TestTypeId { get; set; }
        public int? EnrollLectureId { get; set; }
        public bool? Shuffle { get; set; }
        public bool? MarkAdoption { get; set; }
        public double? ExamFinalMark { get; set; }

        public virtual EnrollLecture EnrollLecture { get; set; }
        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual ExamTemplate ExamTemplate { get; set; }
        public virtual ICollection<EnrollCourseExamQuestion> EnrollCourseExamQuestions { get; set; }
        public virtual ICollection<EnrollCourseExamTranslation> EnrollCourseExamTranslations { get; set; }
        public virtual ICollection<EnrollStudentExam> EnrollStudentExams { get; set; }
    }
}
