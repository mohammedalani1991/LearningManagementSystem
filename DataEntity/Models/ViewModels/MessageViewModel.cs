using System.Collections.Generic;

namespace DataEntity.Models.ViewModels
{
    public class MessageViewModel
    {

        public List<int> Ids { get; set; }
        public List<string> MobileNumbers { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string CreatedBy { get; set; }
        public List<string> Emails { get; set; }
        public bool SendToAdmin { get; set; }

    }
}
