using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class DayViewModel
    {
        public DayViewModel()
        {

        }   
        
        public string Day { get; set; }
        public decimal Hours { get; set; }
        public decimal Minutes { get; set; }
    }
}
