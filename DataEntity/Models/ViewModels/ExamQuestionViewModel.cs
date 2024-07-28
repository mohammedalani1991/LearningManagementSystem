using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class ExamQuestionViewModel
    {
        public ExamQuestionViewModel()
        {

        }



        public ExamQuestionViewModel(ExamQuestion ExamQuestion)
        {
            Id = ExamQuestion.Id;
            CreatedBy = ExamQuestion.CreatedBy;
            CreatedOn = ExamQuestion.CreatedOn;
            Status = ExamQuestion.Status;
            TemplateId = ExamQuestion.TemplateId;
            QuestionId = ExamQuestion.QuestionId;
            Mark = ExamQuestion.Question.Mark;

        }


        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? TemplateId { get; set; }
        public int QuestionId { get; set; }
        public double? Mark { get; set; }

    }
}
