using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CommunicationLogsViewModel
    {
        public CommunicationLogsViewModel()
        {

        }
      
        public CommunicationLogsViewModel(CommunicationLog communicationLog)
        {
            Id = communicationLog.Id;
            LogText = communicationLog.LogText;
            TypeId = communicationLog.TypeId;
            //CreatedBy = communicationLog.CreatedBy;
            CreatedOn = communicationLog.CreatedOn;
            Status = communicationLog.Status;
            IsForExtraType = communicationLog.IsForExtraType??false;
            ContactId = communicationLog.ContactId;
            ContactType = communicationLog.ContactType;
            TypeText = communicationLog.TypeText;
        }
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public int? ContactType { get; set; }
        public string LogText { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int TypeId { get; set; }
        public string TypeText { get; set; }
        public bool IsForExtraType { get; set; }
    }
}
