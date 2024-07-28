using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Branch
    {
        public Branch()
        {
            BranchTranslations = new HashSet<BranchTranslation>();
            Contacts = new HashSet<Contact>();
            Generalizations = new HashSet<Generalization>();
            Messages = new HashSet<Message>();
            SystemSettings = new HashSet<SystemSetting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ColorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<BranchTranslation> BranchTranslations { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Generalization> Generalizations { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<SystemSetting> SystemSettings { get; set; }
    }
}
