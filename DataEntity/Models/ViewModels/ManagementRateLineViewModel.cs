using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class ManagementRateLineViewModel
    {
        public ManagementRateLineViewModel()
        {

        }
        
        public ManagementRateLineViewModel(ManagementRateLine AcademicSupervisionRate)
        {
            Id = AcademicSupervisionRate.Id;
            StandardId = AcademicSupervisionRate.StandardId;
            ManagementRateId = AcademicSupervisionRate.ManagementRateId;
            EnrollTeacherCourseId = AcademicSupervisionRate.ManagementRate.EnrollTeacherCourseId;
            Value = AcademicSupervisionRate.Value;
            CreatedBy = AcademicSupervisionRate.ManagementRate.CreatedBy;
            CreatedOn = AcademicSupervisionRate.ManagementRate.CreatedOn;
            Status = AcademicSupervisionRate.ManagementRate.Status;
            Percent = AcademicSupervisionRate.ManagementRate.Percent;
        }
        public int Id { get; set; }
        public int? StandardId { get; set; }
        public int? ManagementRateId { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public string Value { get; set; }
        public string Standard { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public decimal? Percent { get; set; }
        public string StandardType { get; set; }
    }
}
