using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Course
    {
        public Course()
        {
            Assignments = new HashSet<Assignment>();
            CertificateAdoptions = new HashSet<CertificateAdoption>();
            CourseCertifications = new HashSet<CourseCertification>();
            CourseMarks = new HashSet<CourseMark>();
            CoursePrerequisiteCourses = new HashSet<CoursePrerequisite>();
            CoursePrerequisitePrerequisiteCourses = new HashSet<CoursePrerequisite>();
            CourseResources = new HashSet<CourseResource>();
            CourseTranslations = new HashSet<CourseTranslation>();
            EnrollTeacherCourses = new HashSet<EnrollTeacherCourse>();
            ExamTemplates = new HashSet<ExamTemplate>();
            PracticalExamCourses = new HashSet<PracticalExamCourse>();
            SectionOfCourses = new HashSet<SectionOfCourse>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CourseName { get; set; }
        public int? CourseDuration { get; set; }
        public decimal? CoursePrice { get; set; }
        public string AcquiredSkills { get; set; }
        public string TargetGroup { get; set; }
        public string Notes { get; set; }
        public string Requirements { get; set; }
        public int? CategoryId { get; set; }
        public int? LearningMethodId { get; set; }
        public string ImageUrl { get; set; }
        public bool? ShowInHomePage { get; set; }
        public bool? NeedQuestion { get; set; }
        public string QuestionDescription { get; set; }
        public decimal? SuccessMark { get; set; }
        public decimal? AssignmentMark { get; set; }
        public decimal? ListeningExamMark { get; set; }

        public virtual CourseCategory Category { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<CertificateAdoption> CertificateAdoptions { get; set; }
        public virtual ICollection<CourseCertification> CourseCertifications { get; set; }
        public virtual ICollection<CourseMark> CourseMarks { get; set; }
        public virtual ICollection<CoursePrerequisite> CoursePrerequisiteCourses { get; set; }
        public virtual ICollection<CoursePrerequisite> CoursePrerequisitePrerequisiteCourses { get; set; }
        public virtual ICollection<CourseResource> CourseResources { get; set; }
        public virtual ICollection<CourseTranslation> CourseTranslations { get; set; }
        public virtual ICollection<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }
        public virtual ICollection<ExamTemplate> ExamTemplates { get; set; }
        public virtual ICollection<PracticalExamCourse> PracticalExamCourses { get; set; }
        public virtual ICollection<SectionOfCourse> SectionOfCourses { get; set; }
    }
}
