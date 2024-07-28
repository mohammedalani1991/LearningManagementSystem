using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class AboutDicTranslation
    {
        public int Id { get; set; }
        public int AboutDicId { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual AboutDic AboutDic { get; set; }
    }
}
