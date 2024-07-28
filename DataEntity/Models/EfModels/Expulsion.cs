using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Expulsion
    {
        public int Id { get; set; }
        public DateTime ExpelledFrom { get; set; }
        public DateTime ExpelledTo { get; set; }
        public DateTime ExpulsionStart { get; set; }
        public DateTime ExpulsionEnd { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
