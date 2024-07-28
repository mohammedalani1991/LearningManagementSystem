using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class CmsProjectResourcesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Link { get; set; }
    }
}
