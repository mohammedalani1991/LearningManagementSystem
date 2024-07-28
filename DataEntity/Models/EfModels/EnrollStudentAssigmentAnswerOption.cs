using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentAssigmentAnswerOption
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollStudentAssigmentAnswerId { get; set; }
        public int QusetionOptionId { get; set; }

        public virtual EnrollStudentAssigmentAnswer EnrollStudentAssigmentAnswer { get; set; }
    }
}
