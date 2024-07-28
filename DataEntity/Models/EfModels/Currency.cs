using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyTranslations = new HashSet<CurrencyTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public double Value { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsExchange { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<CurrencyTranslation> CurrencyTranslations { get; set; }
    }
}
