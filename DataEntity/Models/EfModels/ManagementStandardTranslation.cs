using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ManagementStandardTranslation
    {
        public int Id { get; set; }
        public int ManagementStandardId { get; set; }
        public string Standard { get; set; }
        public int LanguageId { get; set; }

        public virtual ManagementStandard ManagementStandard { get; set; }
    }
}
