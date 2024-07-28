using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class NotificationViewModel
    {
        public NotificationViewModel()
        {

        }        

        public NotificationViewModel(Notification Notification)
        {
            Id = Notification.Id;
            CreatedBy = Notification.CreatedBy;
            CreatedOn = Notification.CreatedOn;
            Status = Notification.Status;
            IsRead = Notification.IsRead;
        }

        public int Id { get; set; }
        public bool? IsRead { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }       
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
