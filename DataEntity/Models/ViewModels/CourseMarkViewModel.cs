using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class CourseMarkViewModel
    {
        public CourseMarkViewModel()
        {
            
        }
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public decimal? Value { get; set; }
        public decimal? ValueTo { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
