using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Attendance
    {
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

        public virtual Contact Contact { get; set; }
    }
}
