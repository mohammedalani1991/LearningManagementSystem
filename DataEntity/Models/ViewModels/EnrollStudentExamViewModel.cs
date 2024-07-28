using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class EnrollStudentExamViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public double? FinalMark { get; set; }
        public double? MarkHeGot { get; set; }
        public double? MarkAfterConversion { get; set; }
        public int EnrollCourseExamId { get; set; }
        public int EnrollStudentCourseId { get; set; }

    }
}
