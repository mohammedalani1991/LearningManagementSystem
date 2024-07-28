﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CommunicationChannel
    {
        public CommunicationChannel()
        {
            CommunicationChannelTranslations = new HashSet<CommunicationChannelTranslation>();
            SubCommunicationChannels = new HashSet<SubCommunicationChannel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<CommunicationChannelTranslation> CommunicationChannelTranslations { get; set; }
        public virtual ICollection<SubCommunicationChannel> SubCommunicationChannels { get; set; }
    }
}