using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class AcademicSupervisionStandard
    {
        public AcademicSupervisionStandard()
        {
            AcademicSupervisionRates = new HashSet<AcademicSupervisionRate>();
            AcademicSupervisionStandardTranslations = new HashSet<AcademicSupervisionStandardTranslation>();
        }

        public int Id { get; set; }
        public string Standard { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<AcademicSupervisionRate> AcademicSupervisionRates { get; set; }
        public virtual ICollection<AcademicSupervisionStandardTranslation> AcademicSupervisionStandardTranslations { get; set; }
    }
}
