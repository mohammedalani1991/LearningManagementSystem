using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class QuestionOptionViewModel
    {
        public QuestionOptionViewModel()
        {

        }

        public QuestionOptionViewModel(QuestionOptionTranslation questionOptionTranslation)
        {
            Id = questionOptionTranslation.Id;
            Status = questionOptionTranslation.Option.Status;
            CreatedBy = questionOptionTranslation.Option.CreatedBy;
            CreatedOn = questionOptionTranslation.Option.CreatedOn;
            LanguageId = questionOptionTranslation.LanguageId;
            Name = questionOptionTranslation.Name;
            IsCorrect = questionOptionTranslation.Option.IsCorrect;
            QuestionId = questionOptionTranslation.Option.QuestionId.Value;
        }

        public QuestionOptionViewModel(QuestionOption questionOption)
        {
            Id = questionOption.Id;
            Status = questionOption.Status;
            CreatedBy = questionOption.CreatedBy;
            CreatedOn = questionOption.CreatedOn;
            Name = questionOption.Name;
            IsCorrect = questionOption.IsCorrect;
            QuestionId = questionOption.QuestionId.Value;
        }




        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }


    }
}
