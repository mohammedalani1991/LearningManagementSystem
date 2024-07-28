using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseExamQuestionViewModel
    {
        public EnrollCourseExamQuestionViewModel()
        {

        }

   

        public EnrollCourseExamQuestionViewModel(EnrollCourseExamQuestion enrollCourseExamQuestion)
        {
            Id = enrollCourseExamQuestion.Id;
            CreatedBy = enrollCourseExamQuestion.CreatedBy;
            CreatedOn = enrollCourseExamQuestion.CreatedOn;
            Status = enrollCourseExamQuestion.Status;
            QuestionId = enrollCourseExamQuestion.QuestionId;
            EnrollCourseExamId = enrollCourseExamQuestion.EnrollCourseExamId;
            Mark = enrollCourseExamQuestion.Mark;

        }


        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public double? Mark { get; set; }
        public int Status { get; set; }
        public int QuestionId { get; set; }
        public int EnrollCourseExamId { get; set; }
       
    }
}
