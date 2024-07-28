using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class DetailsLookupTranslation
    {
        public int Id { get; set; }
        public int DetailsLookupId { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }

        public virtual DetailsLookup DetailsLookup { get; set; }
    }
}
