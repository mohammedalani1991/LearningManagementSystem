using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsProjectDonor
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public double Cost { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectCostId { get; set; }

        public virtual CmsProject Project { get; set; }
        public virtual CmsProjectCost ProjectCost { get; set; }
    }
}
