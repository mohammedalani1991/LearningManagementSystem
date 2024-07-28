using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PermissionTranslation
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string PageName { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
