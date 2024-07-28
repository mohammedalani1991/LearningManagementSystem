using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollStudentCourse
    {
        public EnrollStudentCourse()
        {
            CourseAttendances = new HashSet<CourseAttendance>();
            EnrollCourseQuizPointes = new HashSet<EnrollCourseQuizPointe>();
            EnrollStudentAlerts = new HashSet<EnrollStudentAlert>();
            EnrollStudentAssigments = new HashSet<EnrollStudentAssigment>();
            EnrollStudentCourseAttachments = new HashSet<EnrollStudentCourseAttachment>();
            EnrollStudentExams = new HashSet<EnrollStudentExam>();
            PracticalEnrollmentExamStudents = new HashSet<PracticalEnrollmentExamStudent>();
            StudentBalanceHistories = new HashSet<StudentBalanceHistory>();
            StudentExpulsionHistories = new HashSet<StudentExpulsionHistory>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public decimal? Price { get; set; }
        public bool? IsPass { get; set; }
        public double? Mark { get; set; }
        public bool? NeedApproval { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }
        public DateTime? ExpelledDate { get; set; }

        public virtual EnrollTeacherCourse Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<CourseAttendance> CourseAttendances { get; set; }
        public virtual ICollection<EnrollCourseQuizPointe> EnrollCourseQuizPointes { get; set; }
        public virtual ICollection<EnrollStudentAlert> EnrollStudentAlerts { get; set; }
        public virtual ICollection<EnrollStudentAssigment> EnrollStudentAssigments { get; set; }
        public virtual ICollection<EnrollStudentCourseAttachment> EnrollStudentCourseAttachments { get; set; }
        public virtual ICollection<EnrollStudentExam> EnrollStudentExams { get; set; }
        public virtual ICollection<PracticalEnrollmentExamStudent> PracticalEnrollmentExamStudents { get; set; }
        public virtual ICollection<StudentBalanceHistory> StudentBalanceHistories { get; set; }
        public virtual ICollection<StudentExpulsionHistory> StudentExpulsionHistories { get; set; }
    }
}
