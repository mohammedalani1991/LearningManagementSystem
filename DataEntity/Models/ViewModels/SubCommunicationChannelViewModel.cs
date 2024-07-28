using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
 public  class SubCommunicationChannelViewModel
    {
        public SubCommunicationChannelViewModel(SubCommunicationChannelTranslation communicationChannelTrans)
        {
            Id = communicationChannelTrans.SubCommunicationChannelId;
            CommunicationChannelId = communicationChannelTrans.SubCommunicationChannel.CommunicationChannelId;
            Name = communicationChannelTrans.Name;
            Note = communicationChannelTrans.Note;
            CreatedOn = communicationChannelTrans.SubCommunicationChannel.CreatedOn;
            Status = communicationChannelTrans.SubCommunicationChannel.Status;
            DeletedOn = communicationChannelTrans.SubCommunicationChannel.DeletedOn;
            CreatedBy = communicationChannelTrans.SubCommunicationChannel.CreatedBy;
            LanguageId = communicationChannelTrans.LanguageId;
        }

        public SubCommunicationChannelViewModel()
        {
        }

        public SubCommunicationChannelViewModel(SubCommunicationChannel subCommunicationChannel)
        {
            CommunicationChannelId = subCommunicationChannel.CommunicationChannelId;

            Id = subCommunicationChannel.Id;
            Name = subCommunicationChannel.Name;
            Note = subCommunicationChannel.Note;
            CreatedOn = subCommunicationChannel.CreatedOn;
            Status = subCommunicationChannel.Status;
            DeletedOn = subCommunicationChannel.DeletedOn;
            CreatedBy = subCommunicationChannel.CreatedBy;
        }

        public int Id { get; set; }
        public int Page { get; set; }
        public int CommunicationChannelId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? Type { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int LanguageId { get; set; }
        public int? ParentId { get; set; }
        public List<SubCommunicationChannelViewModel> SubCommunicationChannels { get; set; }
    }
}
