using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsPage
    {
        public CmsPage()
        {
            CmsPageTranslations = new HashSet<CmsPageTranslation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public int? CateryId { get; set; }
        public string Keyword { get; set; }
        public bool? AllowComment { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsFeatured { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? SortOrder { get; set; }

        public virtual CmsCatery Catery { get; set; }
        public virtual ICollection<CmsPageTranslation> CmsPageTranslations { get; set; }
    }
}
