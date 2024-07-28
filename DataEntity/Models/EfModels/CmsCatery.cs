using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsCatery
    {
        public CmsCatery()
        {
            CmsCateryTranslations = new HashSet<CmsCateryTranslation>();
            CmsPages = new HashSet<CmsPage>();
            InverseParent = new HashSet<CmsCatery>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentId { get; set; }
        public bool? ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual CmsCatery Parent { get; set; }
        public virtual ICollection<CmsCateryTranslation> CmsCateryTranslations { get; set; }
        public virtual ICollection<CmsPage> CmsPages { get; set; }
        public virtual ICollection<CmsCatery> InverseParent { get; set; }
    }
}
