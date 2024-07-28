using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class DetailsLookup
    {
        public DetailsLookup()
        {
            DetailsLookupTranslations = new HashSet<DetailsLookupTranslation>();
            TemplateHtmls = new HashSet<TemplateHtml>();
        }

        public int Id { get; set; }
        public int MasterId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }

        public virtual MasterLookup Master { get; set; }
        public virtual ICollection<DetailsLookupTranslation> DetailsLookupTranslations { get; set; }
        public virtual ICollection<TemplateHtml> TemplateHtmls { get; set; }
    }
}
