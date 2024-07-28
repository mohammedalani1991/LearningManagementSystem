using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollAssignmentTranslation
    {
        public int Id { get; set; }
        public int EnrollAssignmentId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual EnrollAssignment EnrollAssignment { get; set; }
    }
}
