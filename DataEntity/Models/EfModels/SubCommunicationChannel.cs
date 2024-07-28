using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SubCommunicationChannel
    {
        public SubCommunicationChannel()
        {
            InverseParent = new HashSet<SubCommunicationChannel>();
            SubCommunicationChannelTranslations = new HashSet<SubCommunicationChannelTranslation>();
        }

        public int Id { get; set; }
        public int CommunicationChannelId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? ParentId { get; set; }

        public virtual CommunicationChannel CommunicationChannel { get; set; }
        public virtual SubCommunicationChannel Parent { get; set; }
        public virtual ICollection<SubCommunicationChannel> InverseParent { get; set; }
        public virtual ICollection<SubCommunicationChannelTranslation> SubCommunicationChannelTranslations { get; set; }
    }
}
