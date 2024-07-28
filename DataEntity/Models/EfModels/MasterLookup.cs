using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class MasterLookup
    {
        public MasterLookup()
        {
            DetailsLookups = new HashSet<DetailsLookup>();
            MasterLookupTranslations = new HashSet<MasterLookupTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<DetailsLookup> DetailsLookups { get; set; }
        public virtual ICollection<MasterLookupTranslation> MasterLookupTranslations { get; set; }
    }
}
