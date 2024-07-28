using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CurrencyTranslation
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
