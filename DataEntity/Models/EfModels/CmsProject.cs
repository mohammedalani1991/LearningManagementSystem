using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProject
    {
        public CmsProject()
        {
            CmsProjectCosts = new HashSet<CmsProjectCost>();
            CmsProjectDonors = new HashSet<CmsProjectDonor>();
            CmsProjectResources = new HashSet<CmsProjectResource>();
            CmsProjectTranslations = new HashSet<CmsProjectTranslation>();
            SenangPays = new HashSet<SenangPay>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Keyword { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SortOrder { get; set; }
        public bool? ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsFeatured { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? PaymentType { get; set; }
        public double? ProjectCost { get; set; }
        public double? OneObjectFees { get; set; }
        public int? ProjectStatus { get; set; }
        public int? ProjectCategoryId { get; set; }
        public int? TargetQty { get; set; }
        public string SecondDescription { get; set; }

        public virtual CmsProjectCategory ProjectCategory { get; set; }
        public virtual ICollection<CmsProjectCost> CmsProjectCosts { get; set; }
        public virtual ICollection<CmsProjectDonor> CmsProjectDonors { get; set; }
        public virtual ICollection<CmsProjectResource> CmsProjectResources { get; set; }
        public virtual ICollection<CmsProjectTranslation> CmsProjectTranslations { get; set; }
        public virtual ICollection<SenangPay> SenangPays { get; set; }
    }
}
