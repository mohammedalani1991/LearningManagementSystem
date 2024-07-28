using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class SmsViewModel
    {
        public List<int> Ids { get; set; }
        public int? ToId { get; set; }
        public int? TypeId { get; set; }
        public string Message { get; set; }
        public string Mobile { get; set; }
        public bool? IsExtraMobile { get; set; }
        public string ExtraMobile { get; set; }
        public int? BranchId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Source { get; set; }
    }
}
