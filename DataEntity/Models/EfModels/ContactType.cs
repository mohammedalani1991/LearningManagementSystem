using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ContactType
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int TypeId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
