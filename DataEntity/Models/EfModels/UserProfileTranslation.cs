using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class UserProfileTranslation
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public int UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
