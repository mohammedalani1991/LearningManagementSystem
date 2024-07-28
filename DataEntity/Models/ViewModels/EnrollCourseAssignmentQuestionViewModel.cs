using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class EnrollCourseAssignmentQuestionViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseAssigmentId { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public string QuestionName { get; set; }
        public int QuestionType { get; set; }
    }
}
