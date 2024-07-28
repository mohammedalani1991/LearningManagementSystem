using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class ManagementRateViewModel
    {
        public ManagementRateViewModel()
        {
            ManagementRateLines = new List<ManagementRateLineViewModel>();
        }
        
        public ManagementRateViewModel(ManagementRate managementRate)
        {
            Id = managementRate.Id;
            EnrollTeacherCourseId = managementRate.EnrollTeacherCourseId;
            CreatedBy = managementRate.CreatedBy;
            CreatedOn = managementRate.CreatedOn;
            Status = managementRate.Status;
            Percent = managementRate.Percent;
        }
        public int Id { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public decimal? Percent { get; set; }
        public List<ManagementRateLineViewModel> ManagementRateLines { get; set; }
    }
}
