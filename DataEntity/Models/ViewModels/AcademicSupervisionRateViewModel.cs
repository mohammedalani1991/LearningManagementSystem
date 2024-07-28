using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class AcademicSupervisionRateViewModel
    {
        public AcademicSupervisionRateViewModel()
        {

        }
        
        public AcademicSupervisionRateViewModel(AcademicSupervisionRate AcademicSupervisionRate)
        {
            Id = AcademicSupervisionRate.Id;
            StandardId = AcademicSupervisionRate.StandardId;
            EnrollTeacherCourseId = AcademicSupervisionRate.EnrollTeacherCourseId;
            Note = AcademicSupervisionRate.Note;
            CreatedBy = AcademicSupervisionRate.CreatedBy;
            CreatedOn = AcademicSupervisionRate.CreatedOn;
            Status = AcademicSupervisionRate.Status;
            Rate = AcademicSupervisionRate.Rate;
        }
        public int Id { get; set; }
        public int? StandardId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public string Note { get; set; }
        public string Standard { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int? Rate { get; set; }
    }
}
