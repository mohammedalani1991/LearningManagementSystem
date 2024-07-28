﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarTranslations = new HashSet<CalendarTranslation>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CalendarTranslation> CalendarTranslations { get; set; }
    }
}
