using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CommunicationChannelTranslation
    {
        public int Id { get; set; }
        public int CommunicationChannelId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int LanguageId { get; set; }

        public virtual CommunicationChannel CommunicationChannel { get; set; }
    }
}
