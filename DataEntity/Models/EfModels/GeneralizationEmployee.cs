﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class GeneralizationEmployee
    {
        public int Id { get; set; }
        public int? GeneralizationId { get; set; }
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Generalization Generalization { get; set; }
    }
}