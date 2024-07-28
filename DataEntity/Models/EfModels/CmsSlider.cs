using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsSlider
    {
        public CmsSlider()
        {
            CmsSliderTranslations = new HashSet<CmsSliderTranslation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ReadMoreLink { get; set; }
        public int? SortOrder { get; set; }
        public string Image2Url { get; set; }

        public virtual ICollection<CmsSliderTranslation> CmsSliderTranslations { get; set; }
    }
}
