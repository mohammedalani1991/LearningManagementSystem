using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class AuditLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string RequestDetails { get; set; }
    }
}
