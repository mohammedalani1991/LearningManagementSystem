using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemGroupTranslation
    {
        public int Id { get; set; }
        public int SystemGroupId { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual SystemGroup SystemGroup { get; set; }
    }
}
