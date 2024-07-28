using System;

namespace DataEntity.Models.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int ServiceId { get; set; }
        public string GoogleReCapture { get; set; }

    }
}
