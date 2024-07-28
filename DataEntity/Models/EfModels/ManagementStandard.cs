using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ManagementStandard
    {
        public ManagementStandard()
        {
            ManagementRateLines = new HashSet<ManagementRateLine>();
            ManagementStandardTranslations = new HashSet<ManagementStandardTranslation>();
        }

        public int Id { get; set; }
        public string Standard { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? SortOrder { get; set; }
        public int? Type { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<ManagementRateLine> ManagementRateLines { get; set; }
        public virtual ICollection<ManagementStandardTranslation> ManagementStandardTranslations { get; set; }
    }
}
