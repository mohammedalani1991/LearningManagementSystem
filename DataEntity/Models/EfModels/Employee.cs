using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeTranslations = new HashSet<EmployeeTranslation>();
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime StartWorkDate { get; set; }
        public string Address { get; set; }
        public int? JobId { get; set; }
        public int? JobTypeId { get; set; }
        public string CreatedBy { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<EmployeeTranslation> EmployeeTranslations { get; set; }
    }
}
