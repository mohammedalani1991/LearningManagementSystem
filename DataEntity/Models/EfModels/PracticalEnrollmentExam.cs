using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalEnrollmentExam
    {
        public PracticalEnrollmentExam()
        {
            PracticalEnrollmentExamStudents = new HashSet<PracticalEnrollmentExamStudent>();
            PracticalEnrollmentExamTrainers = new HashSet<PracticalEnrollmentExamTrainer>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? PracticalExamId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public int? TypeId { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? MarkAdoption { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual PracticalExam PracticalExam { get; set; }
        public virtual ICollection<PracticalEnrollmentExamStudent> PracticalEnrollmentExamStudents { get; set; }
        public virtual ICollection<PracticalEnrollmentExamTrainer> PracticalEnrollmentExamTrainers { get; set; }
    }
}
