using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentAssigmentAnswer
    {
        public EnrollStudentAssigmentAnswer()
        {
            EnrollStudentAssigmentAnswerOptions = new HashSet<EnrollStudentAssigmentAnswerOption>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseAssigmentQuestionId { get; set; }
        public string Answer { get; set; }
        public int EnrollStudentAssigmentId { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual EnrollCourseAssigmentQuestion EnrollCourseAssigmentQuestion { get; set; }
        public virtual EnrollStudentAssigment EnrollStudentAssigment { get; set; }
        public virtual ICollection<EnrollStudentAssigmentAnswerOption> EnrollStudentAssigmentAnswerOptions { get; set; }
    }
}
