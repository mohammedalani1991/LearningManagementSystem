using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class EmployeeAttendanceReportViewModel
    {
        public EmployeeAttendanceReportViewModel()
        {

        }
        public int? ContactId { get; set; }
        public string FullName { get; set; }
        public int PresentNo { get; set; }
        public int AllDayNo { get; set; }
        public decimal percent { get; set; }
        public int LanguageId { get; set; }
        public int JobTypeId { get; set; }
    }
}
