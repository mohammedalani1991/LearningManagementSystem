using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class TemplateHtml
    {
        public TemplateHtml()
        {
            CourseCertifications = new HashSet<CourseCertification>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string RanderHtml { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }

        public virtual DetailsLookup Type { get; set; }
        public virtual ICollection<CourseCertification> CourseCertifications { get; set; }
    }
}
