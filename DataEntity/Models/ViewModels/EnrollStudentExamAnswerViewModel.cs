using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class EnrollStudentExamAnswerViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseExamQuestionId { get; set; }
        public string Answer { get; set; }
        public double Mark { get; set; }
        public bool? IsCorrect { get; set; }
        public int EnrollStudentExamId { get; set; }
        public List<EnrollStudentExamAnswerOption> EnrollStudentExamAnswerOptions { get; set; }
    }
}
