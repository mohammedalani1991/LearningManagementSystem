using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class MasterLookupTranslation
    {
        public int Id { get; set; }
        public int MasterLookupId { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }

        public virtual MasterLookup MasterLookup { get; set; }
    }
}
