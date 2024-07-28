using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemFileTranslation
    {
        public int Id { get; set; }
        public int SystemFileId { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AltText { get; set; }

        public virtual SystemFile SystemFile { get; set; }
    }
}
