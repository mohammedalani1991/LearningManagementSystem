using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public  class SubjectViewModel
    {
        public SubjectViewModel()
        {
            
        }
        public SubjectViewModel(Subject subject)
        {
            Title = subject.Title;
            TypeId = subject.TypeId;
            CreatedBy = subject.CreatedBy;
            CreatedOn = subject.CreatedOn;
            Status = subject.Status;
            ExamTypeId = subject.ExamTypeId;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int? TypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int LanguageId { get; set; }
        public int? ExamTypeId { get; set; }
    }
}
