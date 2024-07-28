using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class BranchTranslation
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
