using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectTranslation
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string ShortDescription { get; set; }
        public string SecondDescription { get; set; }

        public virtual CmsProject Project { get; set; }
    }
}
