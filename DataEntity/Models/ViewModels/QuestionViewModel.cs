using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            Data = new List<Question>();
        }

        public QuestionViewModel(QuestionTranslation question)
        {
            Id = question.Question.Id;
            Name = question.Name;
            Status = question.Question.Status;
            CreatedBy = question.Question.CreatedBy;
            CreatedOn = question.Question.CreatedOn;
            LanguageId = question.LanguageId;
            CategoryId = question.Question.CategoryId;
            CourseId = question.Question.CourseId;
            Type = question.Question.Type;
            CertifiedBankQuestion = question.Question.CertifiedBankQuestion;
            OptionList = question.Question.QuestionOptions.Select(r => new QuestionOptionViewModel(r)).ToList();
            Mark = question.Question.Mark;
            TeacherId = question.Question.TeacherId;



        }

        public QuestionViewModel(Question question)
        {
            Id = question.Id;
            Name = question.Name;
            Status = question.Status;
            CreatedBy = question.CreatedBy;
            CreatedOn = question.CreatedOn;
            CategoryId = question.CategoryId;
            CourseId = question.CourseId;
            Type = question.Type;
            CertifiedBankQuestion = question.CertifiedBankQuestion;
            OptionList = question.QuestionOptions.Select(r => new QuestionOptionViewModel(r)).ToList();
            Mark = question.Mark;
            TeacherId = question.TeacherId;
        }


        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int Type { get; set; }
        public bool? CertifiedBankQuestion { get; set; }
        public int? Mark { get; set; }
        public string CategoryName { get; set; }
        public string CourseName { get; set; }
        public List<QuestionOptionViewModel> OptionList { get; set; }
        public List<Question> Data { get; set; }
    }
}
