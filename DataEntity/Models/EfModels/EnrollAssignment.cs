using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollAssignment
    {
        public EnrollAssignment()
        {
            EnrollAssignmentTranslations = new HashSet<EnrollAssignmentTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int EnrollCourseId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
        public virtual ICollection<EnrollAssignmentTranslation> EnrollAssignmentTranslations { get; set; }
    }
}
