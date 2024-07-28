using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class DonationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Item { get; set; }
        public double Cost { get; set; }
    }
}
