using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class MasterLookupViewModel
    {
        public MasterLookupViewModel()
        {

        }
        public MasterLookupViewModel(MasterLookupTranslation masterLookup)
        {
            Id = masterLookup.MasterLookupId;
            Name = masterLookup.Name;
            CreatedBy = masterLookup.MasterLookup.CreatedBy;
            CreatedOn = masterLookup.MasterLookup.CreatedOn;
            Status = masterLookup.MasterLookup.Status;
            LanguageId = masterLookup.LanguageId;
            Code = masterLookup.MasterLookup.Code;
        }

        public MasterLookupViewModel(MasterLookup masterLookup)
        {
            Id = masterLookup.Id;
            Name = masterLookup.Name;
            CreatedBy = masterLookup.CreatedBy;
            CreatedOn = masterLookup.CreatedOn;
            Status = masterLookup.Status;
            Code = masterLookup.Code;
        }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public int Page { get; set; }
        public string Code { get; set; }

        public List<DetailsLookupViewModel> LookupDetails { get; set; }  = new List<DetailsLookupViewModel>();
    }
}
