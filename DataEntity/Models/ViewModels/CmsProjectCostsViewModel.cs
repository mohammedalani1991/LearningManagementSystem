using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class CmsProjectCostsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<CmsProjectCost> CmsProjectCosts { get; set; }
    }
}
