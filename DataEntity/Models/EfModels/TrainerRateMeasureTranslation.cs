using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class TrainerRateMeasureTranslation
    {
        public int Id { get; set; }
        public int TrainerRateMeasureId { get; set; }
        public string Measure { get; set; }
        public int LanguageId { get; set; }

        public virtual TrainerRateMeasure TrainerRateMeasure { get; set; }
    }
}
