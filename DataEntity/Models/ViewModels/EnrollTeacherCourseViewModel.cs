using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class EnrollTeacherCourseViewModel
    {

        public EnrollTeacherCourseViewModel()
        {

        }
        public EnrollTeacherCourseViewModel(EnrollTeacherCourseTranlation enrollTeacherCourseTranlation)
        {
            Id = enrollTeacherCourseTranlation.EnrollCourseId;
            CreatedOn = enrollTeacherCourseTranlation.EnrollCourse.CreatedOn;
            CreatedBy = enrollTeacherCourseTranlation.EnrollCourse.CreatedBy;
            Status = enrollTeacherCourseTranlation.EnrollCourse.Status;
            CourseName = enrollTeacherCourseTranlation.CourseName;
            LearningMethodId = enrollTeacherCourseTranlation.EnrollCourse.LearningMethodId;
            TeacherId = enrollTeacherCourseTranlation.EnrollCourse.TeacherId;
            SemesterId = enrollTeacherCourseTranlation.EnrollCourse.SemesterId;
            SectionName = enrollTeacherCourseTranlation.SectionName;
            PublicationDate = enrollTeacherCourseTranlation.EnrollCourse.PublicationDate;
            PublicationEndDate = enrollTeacherCourseTranlation.EnrollCourse.PublicationEndDate;
            WorkStartDate = enrollTeacherCourseTranlation.EnrollCourse.WorkStartDate;
            WorkEndDate = enrollTeacherCourseTranlation.EnrollCourse.WorkEndDate;
            CourseId = enrollTeacherCourseTranlation.EnrollCourse.CourseId;
            BranchId = enrollTeacherCourseTranlation.EnrollCourse.BranchId;
            CountOfStudent = enrollTeacherCourseTranlation.EnrollCourse.CountOfStudent;
            LanguageId = enrollTeacherCourseTranlation.LanguageId;
            AgeAllowedForRegistration = enrollTeacherCourseTranlation.EnrollCourse.AgeAllowedForRegistration;
            AgeGroup = enrollTeacherCourseTranlation.EnrollCourse.AgeGroup;
            NotesForEnrolled = enrollTeacherCourseTranlation.NotesForEnrolled;
            CalculationTypeId = enrollTeacherCourseTranlation.EnrollCourse.CalculationTypeId;
            IsCourseDone = enrollTeacherCourseTranlation.EnrollCourse.IsCourseDone;
            CertificateAdoption = enrollTeacherCourseTranlation.EnrollCourse.CertificateAdoption;
            AgeGroupTo = enrollTeacherCourseTranlation.EnrollCourse.AgeGroupTo;
        }

        public EnrollTeacherCourseViewModel(EnrollTeacherCourse enrollTeacherCourse)
        {
            Id = enrollTeacherCourse.Id;
            CreatedOn = enrollTeacherCourse.CreatedOn;
            CreatedBy = enrollTeacherCourse.CreatedBy;
            Status = enrollTeacherCourse.Status;
            CourseName = enrollTeacherCourse.CourseName;
            LearningMethodId = enrollTeacherCourse.LearningMethodId;
            TeacherId = enrollTeacherCourse.TeacherId;
            SemesterId = enrollTeacherCourse.SemesterId;
            SectionName = enrollTeacherCourse.SectionName;
            PublicationDate = enrollTeacherCourse.PublicationDate;
            PublicationEndDate = enrollTeacherCourse.PublicationEndDate;
            WorkStartDate = enrollTeacherCourse.WorkStartDate;
            WorkEndDate = enrollTeacherCourse.WorkEndDate;
            CourseId = enrollTeacherCourse.CourseId;
            BranchId = enrollTeacherCourse.BranchId;
            CountOfStudent = enrollTeacherCourse.CountOfStudent;
            AgeAllowedForRegistration = enrollTeacherCourse.AgeAllowedForRegistration;
            AgeGroup = enrollTeacherCourse.AgeGroup;
            NotesForEnrolled = enrollTeacherCourse.NotesForEnrolled;
            CalculationTypeId =enrollTeacherCourse.CalculationTypeId;
            IsCourseDone = enrollTeacherCourse.IsCourseDone;
            CertificateAdoption = enrollTeacherCourse.CertificateAdoption;
            AgeGroupTo = enrollTeacherCourse.AgeGroupTo;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
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
        public int LanguageId { get; set; }

        public string IslinkedSemester { get; set; }
        public string IslinkedSection { get; set; }

        public int AgeAllowedForRegistration { get; set; }
        public int AgeGroup { get; set; }

        public string NotesForEnrolled { get; set; }
        public string TeacherName { get; set; }
        public string SemesterName { get; set; }

        public int? CalculationTypeId { get; set; }
        public bool? IsCourseDone { get; set; }
        public bool? CertificateAdoption { get; set; }
        public int? AgeGroupTo { get; set; }

        public List<EnrollSectionOfCourse> ListEnrollSectionOfCourse { get; set; }
        public List<EnrollCourseTime> ListEnrollCourseTime { get; set; }

    }
}
