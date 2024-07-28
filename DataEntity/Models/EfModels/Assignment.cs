using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Assignment
    {
        public Assignment()
        {
            AssignmentTranslations = new HashSet<AssignmentTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int CourseId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<AssignmentTranslation> AssignmentTranslations { get; set; }
    }
}
