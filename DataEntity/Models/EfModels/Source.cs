using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Source
    {
        public string Gender { get; set; }
        public string Name { get; set; }
        public double? DateYear { get; set; }
        public double? DateMonth { get; set; }
        public double? Birthday { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int NewId { get; set; }
    }
}
