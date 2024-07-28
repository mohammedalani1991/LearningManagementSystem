using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
