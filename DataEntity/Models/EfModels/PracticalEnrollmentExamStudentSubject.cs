using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalEnrollmentExamStudentSubject
    {
        public PracticalEnrollmentExamStudentSubject()
        {
            PracticalEnrollmentExamStudentSubjectRatings = new HashSet<PracticalEnrollmentExamStudentSubjectRating>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? PracticalEnrollmentExamStudentId { get; set; }
        public int? SubjectId { get; set; }
        public decimal? Mark { get; set; }
        public decimal? DisountMarkTotal { get; set; }

        public virtual PracticalEnrollmentExamStudent PracticalEnrollmentExamStudent { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<PracticalEnrollmentExamStudentSubjectRating> PracticalEnrollmentExamStudentSubjectRatings { get; set; }
    }
}
