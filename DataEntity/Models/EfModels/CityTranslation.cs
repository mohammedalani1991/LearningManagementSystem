using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CityTranslation
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
    }
}
