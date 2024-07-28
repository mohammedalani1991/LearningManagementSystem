using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class AttendanceViewModel
    {
        public AttendanceViewModel()
        {
        }
        public AttendanceViewModel(Attendance attendance, ContactViewModel contactViewModel)
        {
            Id = attendance.Id;
            ContactId = contactViewModel.Id;
            CreatedOn = attendance.CreatedOn;
            Status = attendance.Status;
            DeletedOn = attendance.DeletedOn;
            CreatedBy = attendance.CreatedBy;
            AbsenceNote = attendance.AbsenceNote;
            IsPresent = attendance.IsPresent;
            Date = attendance.Date;
            FromHour = attendance.FromHour;
            ToHour = attendance.ToHour;
            Contact = contactViewModel;
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string AbsenceNote { get; set; }
        public bool? IsPresent { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? FromHour { get; set; }
        public TimeSpan? ToHour { get; set; }

        public ContactViewModel Contact { get; }
    }
}
