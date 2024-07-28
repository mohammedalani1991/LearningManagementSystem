using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CommunicationLog
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int? ContactType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int TypeId { get; set; }
        public string TypeText { get; set; }
        public string LogText { get; set; }
        public bool? IsForExtraType { get; set; }
        public string CreatedBy { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
