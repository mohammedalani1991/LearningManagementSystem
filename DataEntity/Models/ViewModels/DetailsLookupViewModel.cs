using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class DetailsLookupViewModel
    {
        public DetailsLookupViewModel()
        {

        }
        public DetailsLookupViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public DetailsLookupViewModel(DetailsLookupTranslation detailsLookup)
        {
            Id = detailsLookup.DetailsLookupId;
            Name = detailsLookup.Value;
            Code = detailsLookup.DetailsLookup.Code;
            CreatedBy = detailsLookup.DetailsLookup.CreatedBy;
            CreatedOn = detailsLookup.DetailsLookup.CreatedOn;
            Status = detailsLookup.DetailsLookup.Status;
            LanguageId = detailsLookup.LanguageId;
            MasterId = detailsLookup.DetailsLookup.MasterId;
            MasterCode = detailsLookup.DetailsLookup.Master?.Code;
        }

        public DetailsLookupViewModel(DetailsLookup detailsLookup)
        {
            Id = detailsLookup.Id;
            Name = detailsLookup.Value;
            Code = detailsLookup.Code;
            CreatedBy = detailsLookup.CreatedBy;
            CreatedOn = detailsLookup.CreatedOn;
            Status = detailsLookup.Status;
            MasterId = detailsLookup.MasterId;
            MasterCode = detailsLookup.Master?.Code;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MasterId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public string MasterCode { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int LanguageId { get; set; }

        public bool IsDefault { get; set; }
    }
}
