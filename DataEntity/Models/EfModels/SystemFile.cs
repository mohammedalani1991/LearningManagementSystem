using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SystemFile
    {
        public SystemFile()
        {
            ItemFiles = new HashSet<ItemFile>();
            SystemFileTranslations = new HashSet<SystemFileTranslation>();
        }

        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AltText { get; set; }

        public virtual ICollection<ItemFile> ItemFiles { get; set; }
        public virtual ICollection<SystemFileTranslation> SystemFileTranslations { get; set; }
    }
}
