using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalEnrollmentExamTrainer
    {
        public int Id { get; set; }
        public int? PracticalEnrollmentExamId { get; set; }
        public int? TrainerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual PracticalEnrollmentExam PracticalEnrollmentExam { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
