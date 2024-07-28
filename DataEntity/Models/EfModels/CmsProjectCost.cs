using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectCost
    {
        public CmsProjectCost()
        {
            CmsProjectCostTranslations = new HashSet<CmsProjectCostTranslation>();
            CmsProjectDonors = new HashSet<CmsProjectDonor>();
            SenangPays = new HashSet<SenangPay>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public bool? IsOther { get; set; }

        public virtual CmsProject Project { get; set; }
        public virtual ICollection<CmsProjectCostTranslation> CmsProjectCostTranslations { get; set; }
        public virtual ICollection<CmsProjectDonor> CmsProjectDonors { get; set; }
        public virtual ICollection<SenangPay> SenangPays { get; set; }
    }
}
