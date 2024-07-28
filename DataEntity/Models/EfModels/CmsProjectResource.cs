using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectResource
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int? Type { get; set; }
        public int? ProjectId { get; set; }

        public virtual CmsProject Project { get; set; }
    }
}
