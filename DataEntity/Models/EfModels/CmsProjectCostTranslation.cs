using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectCostTranslation
    {
        public int Id { get; set; }
        public int ProjectCostId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual CmsProjectCost ProjectCost { get; set; }
    }
}
