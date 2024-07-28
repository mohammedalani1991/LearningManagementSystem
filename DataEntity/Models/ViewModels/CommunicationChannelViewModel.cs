using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class CommunicationChannelViewModel
    {

        public CommunicationChannelViewModel(CommunicationChannelTranslation communicationChannelTrans)
        {
            Id = communicationChannelTrans.CommunicationChannelId;
            Name = communicationChannelTrans.Name;
            Note = communicationChannelTrans.Note;
            CreatedOn = communicationChannelTrans.CommunicationChannel.CreatedOn;
            Status = communicationChannelTrans.CommunicationChannel.Status;
            DeletedOn = communicationChannelTrans.CommunicationChannel.DeletedOn;
            CreatedBy = communicationChannelTrans.CommunicationChannel.CreatedBy;
            LanguageId = communicationChannelTrans.LanguageId;
        } 

        public CommunicationChannelViewModel()
        {
        }

        public CommunicationChannelViewModel(CommunicationChannel communicationChannel)
        {
            Id = communicationChannel.Id;
            Name = communicationChannel.Name;
            Note = communicationChannel.Note;
            CreatedOn = communicationChannel.CreatedOn;
            Status = communicationChannel.Status;
            DeletedOn = communicationChannel.DeletedOn;
            CreatedBy = communicationChannel.CreatedBy;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public int Page { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int LanguageId { get; set; }
        public List<SubCommunicationChannelViewModel> SubCommunicationChannels { get; set; }
    }
}
