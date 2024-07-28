using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ManagementRateLine
    {
        public int Id { get; set; }
        public int? ManagementRateId { get; set; }
        public int? StandardId { get; set; }
        public string Value { get; set; }

        public virtual ManagementRate ManagementRate { get; set; }
        public virtual ManagementStandard Standard { get; set; }
    }
}
