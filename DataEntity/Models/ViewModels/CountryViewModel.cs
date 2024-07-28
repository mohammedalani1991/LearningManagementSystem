using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class CountryViewModel
    {
        public CountryViewModel() { }

        public CountryViewModel(Country country) {
            Id = country.Id;
            CreatedOn = country.CreatedOn;
            CreatedBy = country.CreatedBy;
            Status = country.Status;
            DeletedOn = country.DeletedOn;
            Name = country.Name;
        }

        public CountryViewModel(CountryTranslation countryTranslation) {
            Id = countryTranslation.CountryId;
            CreatedOn = countryTranslation.Country.CreatedOn;
            CreatedBy = countryTranslation.Country.CreatedBy;
            Status = countryTranslation.Country.Status;
            DeletedOn = countryTranslation.Country.DeletedOn;
            Name = countryTranslation.Name;
        }

        public int Id { get; set; }
        public int Page { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public List<CityViewModel> Cities { get; set; }
    }
}
