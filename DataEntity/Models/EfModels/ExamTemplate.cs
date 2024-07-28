using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ExamTemplate
    {
        public ExamTemplate()
        {
            EnrollCourseExams = new HashSet<EnrollCourseExam>();
            ExamQuestions = new HashSet<ExamQuestion>();
            ExamTemplateTranslations = new HashSet<ExamTemplateTranslation>();
            MarkAdoptionForExams = new HashSet<MarkAdoptionForExam>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public int? CategoryId { get; set; }
        public int? CourseId { get; set; }
        public double? FinalMark { get; set; }
        public double? MarkAfterConversion { get; set; }
        public bool? Shuffle { get; set; }

        public virtual CourseCategory Category { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<EnrollCourseExam> EnrollCourseExams { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
        public virtual ICollection<ExamTemplateTranslation> ExamTemplateTranslations { get; set; }
        public virtual ICollection<MarkAdoptionForExam> MarkAdoptionForExams { get; set; }
    }
}
