using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class PracticalExamViewModel
    {
        public PracticalExamViewModel()
        {
            
        }
        public PracticalExamViewModel(PracticalExam practicalExams)
        {
            Id = practicalExams.Id;
            Name = practicalExams.Name;
            Mark= practicalExams.Mark;
            MarkAfterConversion= practicalExams.MarkAfterConversion;
            Status = practicalExams.Status;
            CreatedBy = practicalExams.CreatedBy;
            CreatedOn = practicalExams.CreatedOn;
            TypeId = practicalExams.TypeId;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public decimal? Mark { get; set; }
        public decimal? MarkAfterConversion { get; set; }
        public int LanguageId { get; set; }
        public int? TypeId { get; set; }
    }
}
