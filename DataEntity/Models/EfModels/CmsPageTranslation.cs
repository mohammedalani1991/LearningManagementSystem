using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsPageTranslation
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string ShortDescription { get; set; }

        public virtual CmsPage Page { get; set; }
    }
}
