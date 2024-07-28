using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class PracticalQuestionViewModel
    {
        public PracticalQuestionViewModel()
        {

        }
        public PracticalQuestionViewModel(PracticalQuestion practicalQuestion)
        {
            Id = practicalQuestion.Id;
            CreatedOn = practicalQuestion.CreatedOn;
            CreatedBy = practicalQuestion.CreatedBy;
            Status = practicalQuestion.Status;
            Name = practicalQuestion.Name;
            Mark = practicalQuestion.Mark;
            IsDiscountFromTotal = practicalQuestion.IsDiscountFromTotal ?? false;
            Type = practicalQuestion.Type;
            Main = practicalQuestion.Main ?? false;
            Description = practicalQuestion.Description;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public decimal? Mark { get; set; }
        public bool IsDiscountFromTotal { get; set; }
        public int? Type { get; set; }
        public int LanguageId { get; set; }
        public bool Main { get; set; }
        public string Description { get; set; }
    }
}
