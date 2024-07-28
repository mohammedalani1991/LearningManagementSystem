using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalExam
    {
        public PracticalExam()
        {
            MarkAdoptionForPracticalExams = new HashSet<MarkAdoptionForPracticalExam>();
            PracticalEnrollmentExams = new HashSet<PracticalEnrollmentExam>();
            PracticalExamCourses = new HashSet<PracticalExamCourse>();
            PracticalExamQuestions = new HashSet<PracticalExamQuestion>();
            PracticalExamTranslations = new HashSet<PracticalExamTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public decimal? Mark { get; set; }
        public decimal? MarkAfterConversion { get; set; }
        public int? TypeId { get; set; }

        public virtual ICollection<MarkAdoptionForPracticalExam> MarkAdoptionForPracticalExams { get; set; }
        public virtual ICollection<PracticalEnrollmentExam> PracticalEnrollmentExams { get; set; }
        public virtual ICollection<PracticalExamCourse> PracticalExamCourses { get; set; }
        public virtual ICollection<PracticalExamQuestion> PracticalExamQuestions { get; set; }
        public virtual ICollection<PracticalExamTranslation> PracticalExamTranslations { get; set; }
    }
}
