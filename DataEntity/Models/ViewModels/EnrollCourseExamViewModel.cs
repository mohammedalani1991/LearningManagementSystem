using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseExamViewModel
    {
        public EnrollCourseExamViewModel()
        {

        }

        public EnrollCourseExamViewModel(EnrollCourseExamTranslation enrollCourseExam)
        {
            Id = enrollCourseExam.EnrollCourseExamId;
            Name = enrollCourseExam.Name;
            Description = enrollCourseExam.Description;
            CreatedBy = enrollCourseExam.EnrollCourseExam.CreatedBy;
            CreatedOn = enrollCourseExam.EnrollCourseExam.CreatedOn;
            PublishDate = enrollCourseExam.EnrollCourseExam.PublishDate;
            PublishEndDate = enrollCourseExam.EnrollCourseExam.PublishEndDate;
            Status = enrollCourseExam.EnrollCourseExam.Status;
            LanguageId = enrollCourseExam.LanguageId;
            Duration = enrollCourseExam.EnrollCourseExam.Duration;
            TestTypeID = enrollCourseExam.EnrollCourseExam.TestTypeId.Value;
            ExamFinalMark = enrollCourseExam.EnrollCourseExam.ExamFinalMark;
            IsPrerequisite = enrollCourseExam.EnrollCourseExam.IsPrerequisite ?? false;
            EnrollTeacherCourseId = enrollCourseExam.EnrollCourseExam.EnrollTeacherCourseId;
            EnrollLectureId = enrollCourseExam.EnrollCourseExam.EnrollLectureId;
            ExamTemplateId = enrollCourseExam.EnrollCourseExam.ExamTemplateId;
            Shuffle = enrollCourseExam.EnrollCourseExam.Shuffle?? false;
            ExamTemplateName = (enrollCourseExam.EnrollCourseExam.ExamTemplate != null ? enrollCourseExam.EnrollCourseExam.ExamTemplate.Name : "--");
            EnrollLectureName = (enrollCourseExam.EnrollCourseExam.EnrollLecture != null ? enrollCourseExam.EnrollCourseExam.EnrollLecture.LectureName : "--");

        }

        public EnrollCourseExamViewModel(EnrollCourseExam enrollCourseExam)
        {
            Id = enrollCourseExam.Id;
            Name = enrollCourseExam.Name;
            Description = enrollCourseExam.Description;
            CreatedBy = enrollCourseExam.CreatedBy;
            CreatedOn = enrollCourseExam.CreatedOn;
            PublishDate = enrollCourseExam.PublishDate;
            PublishEndDate = enrollCourseExam.PublishEndDate;
            Status = enrollCourseExam.Status;
            Duration = enrollCourseExam.Duration;
            TestTypeID = enrollCourseExam.TestTypeId.Value;
            ExamFinalMark = enrollCourseExam.ExamFinalMark;
            IsPrerequisite = enrollCourseExam.IsPrerequisite ?? false;
            EnrollTeacherCourseId = enrollCourseExam.EnrollTeacherCourseId;
            EnrollLectureId = enrollCourseExam.EnrollLectureId;
            ExamTemplateId = enrollCourseExam.ExamTemplateId;
            Shuffle = enrollCourseExam.Shuffle?? false;
            ExamTemplateName = (enrollCourseExam.ExamTemplate != null ? enrollCourseExam.ExamTemplate.Name : "--");
            EnrollLectureName = (enrollCourseExam.EnrollLecture != null ? enrollCourseExam.EnrollLecture.LectureName : "--");
        }


        public int Id { get; set; }
        public int? Duration { get; set; }
        public double? ExamFinalMark { get; set; }
        public bool IsPrerequisite { get; set; }
        public string Name { get; set; }
        public string ExamTemplateName { get; set; }
        public string EnrollLectureName { get; set; }
        public string Description { get; set; }
        public string Description1 { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishEndDate { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public int? ExamTemplateId { get; set; }
        public int TestTypeID { get; set; }
        public int? EnrollLectureId { get; set; }
        public int Page { get; set; }
        public bool IsOnlineLearningMethod { get; set; }
        public bool Shuffle { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

    }
}
