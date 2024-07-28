using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalEnrollmentExamStudentSubjectRating
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int PracticalEnrollmentExamStudentSubjectId { get; set; }
        public int PracticalQuestionId { get; set; }
        public decimal? Mark { get; set; }
        public int? NoOfErrors { get; set; }

        public virtual PracticalEnrollmentExamStudentSubject PracticalEnrollmentExamStudentSubject { get; set; }
        public virtual PracticalQuestion PracticalQuestion { get; set; }
    }
}
