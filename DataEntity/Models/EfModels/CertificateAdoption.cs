using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CertificateAdoption
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? SemesterId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
