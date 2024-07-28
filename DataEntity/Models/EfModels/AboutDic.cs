using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class AboutDic
    {
        public AboutDic()
        {
            AboutDicTranslations = new HashSet<AboutDicTranslation>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<AboutDicTranslation> AboutDicTranslations { get; set; }
    }
}
