using System;

namespace DataEntity.Models.ViewModels
{
    public class SubscribersViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
