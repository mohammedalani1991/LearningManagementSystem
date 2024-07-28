using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseAssigmentQuestion
    {
        public EnrollCourseAssigmentQuestion()
        {
            EnrollCourseAssigmentQuestionOptions = new HashSet<EnrollCourseAssigmentQuestionOption>();
            EnrollCourseAssigmentQuestionTranslations = new HashSet<EnrollCourseAssigmentQuestionTranslation>();
            EnrollStudentAssigmentAnswers = new HashSet<EnrollStudentAssigmentAnswer>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseAssigmentId { get; set; }
        public string QuestionName { get; set; }
        public int QuestionType { get; set; }

        public virtual EnrollCourseAssigment EnrollCourseAssigment { get; set; }
        public virtual ICollection<EnrollCourseAssigmentQuestionOption> EnrollCourseAssigmentQuestionOptions { get; set; }
        public virtual ICollection<EnrollCourseAssigmentQuestionTranslation> EnrollCourseAssigmentQuestionTranslations { get; set; }
        public virtual ICollection<EnrollStudentAssigmentAnswer> EnrollStudentAssigmentAnswers { get; set; }
    }
}
