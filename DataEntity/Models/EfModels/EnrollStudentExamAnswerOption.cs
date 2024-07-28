using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentExamAnswerOption
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollStudentExamAnswerId { get; set; }
        public int QusetionOptionId { get; set; }

        public virtual EnrollStudentExamAnswer EnrollStudentExamAnswer { get; set; }
        public virtual QuestionOption QusetionOption { get; set; }
    }
}
