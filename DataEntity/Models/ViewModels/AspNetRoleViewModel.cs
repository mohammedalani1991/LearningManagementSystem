using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class AspNetRoleViewModel
    {
        public AspNetRoleViewModel()
        {

        }

        public AspNetRoleViewModel(AspNetRole role)
        {
            Id = role.Id;
            NewId = role.Id;
            Name = role.Name;
            NormalizedName = role.NormalizedName;
            ConcurrencyStamp = role.ConcurrencyStamp;
        }

        public string Id { get; set; }
        public string NewId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
