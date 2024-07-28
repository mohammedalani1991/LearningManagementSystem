using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CalendarViewModel
    {
        public CalendarViewModel()
        {

        }

        public CalendarViewModel(CalendarTranslation calendar)
        {
            Id = calendar.CalendarId;
            Name = calendar.Name;
            CreatedBy = calendar.Calendar.CreatedBy;
            CreatedOn = calendar.Calendar.CreatedOn;
            StartDate = calendar.Calendar.StartDate;
            EndDate = calendar.Calendar.EndDate;
            Status = calendar.Calendar.Status;
            LanguageId = calendar.LanguageId;
            Type = calendar.Calendar.Type;
            Description = calendar.Description;
        }

        public CalendarViewModel(Calendar calendar)
        {
            Id = calendar.Id;
            Name = calendar.Name;
            CreatedBy = calendar.CreatedBy;
            CreatedOn = calendar.CreatedOn;
            Status = calendar.Status;
            Type = calendar.Type;
            StartDate = calendar.StartDate;
            EndDate = calendar.EndDate;
            Description = calendar.Description;
        }



        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }       
        public int Type { get; set; }
        public int Page { get; set; }
        public string Description { get; set; }
    }
}
