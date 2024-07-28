using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class ExpulsionViewModel
    {
        public ExpulsionViewModel()
        {
            
        }
        public ExpulsionViewModel(Expulsion expulsion)
        {
            Id = expulsion.Id;
            ExpelledFrom = expulsion.ExpelledFrom;
            ExpelledTo = expulsion.ExpelledTo;
            ExpulsionStart = expulsion.ExpulsionStart;
            ExpulsionEnd = expulsion.ExpulsionEnd;
            Status = expulsion.Status;
            CreatedOn = expulsion.CreatedOn;
            CreatedBy = expulsion.CreatedBy;
        }

        public int Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpelledFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpelledTo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpulsionStart { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpulsionEnd { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
