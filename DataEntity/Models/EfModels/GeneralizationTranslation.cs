using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class GeneralizationTranslation
    {
        public int Id { get; set; }
        public int GeneralizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }

        public virtual Generalization Generalization { get; set; }
    }
}
