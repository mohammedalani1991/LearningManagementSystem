using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class GeneralContactViewModel
    {
        public GeneralContactViewModel()
        {
            UserProfileViewModel = new UserProfileViewModel();
        }
        public virtual ContactViewModel ContactViewModel { get; set; }
        public virtual EmployeeViewModel EmployeeViewModel { get; set; }
        public virtual UserProfileViewModel UserProfileViewModel { get; set; }
        public virtual StudentViewModel StudentViewModel { get; set; }
        public virtual TrainerViewModel TrainerViewModel { get; set; }
        public int IsUser { get; set; }
    }
}
