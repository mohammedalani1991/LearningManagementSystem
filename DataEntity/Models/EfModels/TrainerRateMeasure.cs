using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class TrainerRateMeasure
    {
        public TrainerRateMeasure()
        {
            TrainerRateMeasureTranslations = new HashSet<TrainerRateMeasureTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public DateTime? DeletedOn { get; set; }
        public decimal? FromRange { get; set; }
        public decimal? ToRange { get; set; }
        public string Measure { get; set; }

        public virtual ICollection<TrainerRateMeasureTranslation> TrainerRateMeasureTranslations { get; set; }
    }
}
