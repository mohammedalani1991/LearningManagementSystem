using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsWhatPeopleSay
    {
        public CmsWhatPeopleSay()
        {
            CmsWhatPeopleSayTranslations = new HashSet<CmsWhatPeopleSayTranslation>();
        }

        public int Id { get; set; }
        public string PersonName { get; set; }
        public string PersonDetails { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool? ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<CmsWhatPeopleSayTranslation> CmsWhatPeopleSayTranslations { get; set; }
    }
}
