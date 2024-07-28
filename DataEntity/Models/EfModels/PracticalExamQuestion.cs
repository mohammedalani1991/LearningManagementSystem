using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalExamQuestion
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int PracticalExamId { get; set; }
        public int PracticalQuestionId { get; set; }

        public virtual PracticalExam PracticalExam { get; set; }
        public virtual PracticalQuestion PracticalQuestion { get; set; }
    }
}
