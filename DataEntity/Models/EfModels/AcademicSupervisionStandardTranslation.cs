using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class AcademicSupervisionStandardTranslation
    {
        public int Id { get; set; }
        public int AcademicSupervisionStandardId { get; set; }
        public string Standard { get; set; }
        public int LanguageId { get; set; }

        public virtual AcademicSupervisionStandard AcademicSupervisionStandard { get; set; }
    }
}
