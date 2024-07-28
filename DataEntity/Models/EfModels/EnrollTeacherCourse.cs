using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollTeacherCourse
    {
        public EnrollTeacherCourse()
        {
            AcademicSupervisionRates = new HashSet<AcademicSupervisionRate>();
            CourseAttendances = new HashSet<CourseAttendance>();
            CoursePackagesRelations = new HashSet<CoursePackagesRelation>();
            EnrollAssignments = new HashSet<EnrollAssignment>();
            EnrollCourseAllowUserRates = new HashSet<EnrollCourseAllowUserRate>();
            EnrollCourseAssigments = new HashSet<EnrollCourseAssigment>();
            EnrollCourseExams = new HashSet<EnrollCourseExam>();
            EnrollCourseQuizzes = new HashSet<EnrollCourseQuiz>();
            EnrollCourseResources = new HashSet<EnrollCourseResource>();
            EnrollCourseTimeCustomizations = new HashSet<EnrollCourseTimeCustomization>();
            EnrollCourseTimes = new HashSet<EnrollCourseTime>();
            EnrollLectures = new HashSet<EnrollLecture>();
            EnrollSectionOfCourses = new HashSet<EnrollSectionOfCourse>();
            EnrollStudentAlerts = new HashSet<EnrollStudentAlert>();
            EnrollStudentCourses = new HashSet<EnrollStudentCourse>();
            EnrollTeacherCourseTranlations = new HashSet<EnrollTeacherCourseTranlation>();
            InvoicesPays = new HashSet<InvoicesPay>();
            ManagementRates = new HashSet<ManagementRate>();
            MarkAdoptionForExams = new HashSet<MarkAdoptionForExam>();
            MarkAdoptionForPracticalExams = new HashSet<MarkAdoptionForPracticalExam>();
            PracticalEnrollmentExams = new HashSet<PracticalEnrollmentExam>();
            SenangPays = new HashSet<SenangPay>();
            StudentAttendances = new HashSet<StudentAttendance>();
            StudentSubscriptions = new HashSet<StudentSubscription>();
            TeacherAttendances = new HashSet<TeacherAttendance>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CourseName { get; set; }
        public int? LearningMethodId { get; set; }
        public int TeacherId { get; set; }
        public int? SemesterId { get; set; }
        public string SectionName { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? PublicationEndDate { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public int CourseId { get; set; }
        public int? BranchId { get; set; }
        public int? CountOfStudent { get; set; }
        public int AgeAllowedForRegistration { get; set; }
        public int AgeGroup { get; set; }
        public string NotesForEnrolled { get; set; }
        public int? CalculationTypeId { get; set; }
        public bool? IsCourseDone { get; set; }
        public bool? CertificateAdoption { get; set; }
        public int? AgeGroupTo { get; set; }

        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Trainer Teacher { get; set; }
        public virtual ICollection<AcademicSupervisionRate> AcademicSupervisionRates { get; set; }
        public virtual ICollection<CourseAttendance> CourseAttendances { get; set; }
        public virtual ICollection<CoursePackagesRelation> CoursePackagesRelations { get; set; }
        public virtual ICollection<EnrollAssignment> EnrollAssignments { get; set; }
        public virtual ICollection<EnrollCourseAllowUserRate> EnrollCourseAllowUserRates { get; set; }
        public virtual ICollection<EnrollCourseAssigment> EnrollCourseAssigments { get; set; }
        public virtual ICollection<EnrollCourseExam> EnrollCourseExams { get; set; }
        public virtual ICollection<EnrollCourseQuiz> EnrollCourseQuizzes { get; set; }
        public virtual ICollection<EnrollCourseResource> EnrollCourseResources { get; set; }
        public virtual ICollection<EnrollCourseTimeCustomization> EnrollCourseTimeCustomizations { get; set; }
        public virtual ICollection<EnrollCourseTime> EnrollCourseTimes { get; set; }
        public virtual ICollection<EnrollLecture> EnrollLectures { get; set; }
        public virtual ICollection<EnrollSectionOfCourse> EnrollSectionOfCourses { get; set; }
        public virtual ICollection<EnrollStudentAlert> EnrollStudentAlerts { get; set; }
        public virtual ICollection<EnrollStudentCourse> EnrollStudentCourses { get; set; }
        public virtual ICollection<EnrollTeacherCourseTranlation> EnrollTeacherCourseTranlations { get; set; }
        public virtual ICollection<InvoicesPay> InvoicesPays { get; set; }
        public virtual ICollection<ManagementRate> ManagementRates { get; set; }
        public virtual ICollection<MarkAdoptionForExam> MarkAdoptionForExams { get; set; }
        public virtual ICollection<MarkAdoptionForPracticalExam> MarkAdoptionForPracticalExams { get; set; }
        public virtual ICollection<PracticalEnrollmentExam> PracticalEnrollmentExams { get; set; }
        public virtual ICollection<SenangPay> SenangPays { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<StudentSubscription> StudentSubscriptions { get; set; }
        public virtual ICollection<TeacherAttendance> TeacherAttendances { get; set; }
    }
}
