using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemGroupUser
    {
        public int Id { get; set; }
        public int SystemGroupId { get; set; }
        public int? UserProfileId { get; set; }

        public virtual SystemGroup SystemGroup { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
