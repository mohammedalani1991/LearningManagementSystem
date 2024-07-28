﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Email
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int? ContactType { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int TypeId { get; set; }
        public string Email1 { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ImageLink { get; set; }
        public string CycleLink { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
