using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectCategory
    {
        public CmsProjectCategory()
        {
            CmsProjectCategoryTranslations = new HashSet<CmsProjectCategoryTranslation>();
            CmsProjects = new HashSet<CmsProject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<CmsProjectCategoryTranslation> CmsProjectCategoryTranslations { get; set; }
        public virtual ICollection<CmsProject> CmsProjects { get; set; }
    }
}
