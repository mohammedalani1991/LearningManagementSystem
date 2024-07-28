using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? ToId { get; set; }
        public int? TypeId { get; set; }
        public string Message1 { get; set; }
        public string Mobile { get; set; }
        public bool? IsExtraMobile { get; set; }
        public string ExtraMobile { get; set; }
        public int? BranchId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Source { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
