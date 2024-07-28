using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Generalization
    {
        public Generalization()
        {
            GeneralizationEmployees = new HashSet<GeneralizationEmployee>();
            GeneralizationTranslations = new HashSet<GeneralizationTranslation>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? BranchId { get; set; }
        public int? GeneralizationTypeId { get; set; }
        public int? JobId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ICollection<GeneralizationEmployee> GeneralizationEmployees { get; set; }
        public virtual ICollection<GeneralizationTranslation> GeneralizationTranslations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
