using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EmployeeTranslation
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Address { get; set; }
        public int LanguageId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
