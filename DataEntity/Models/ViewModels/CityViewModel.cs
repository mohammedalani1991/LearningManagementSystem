using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
   public class CityViewModel
    {
        public CityViewModel() { }

        public CityViewModel(City city) {
            Id = city.Id;
            CreatedOn = city.CreatedOn;
            CreatedBy = city.CreatedBy;
            Status = city.Status;
            DeletedOn = city.DeletedOn;
            Name = city.Name;
            CountryId = city.CountryId;
        } 

        public CityViewModel(CityTranslation city) {
            Id = city.CityId;
            CreatedOn = city.City.CreatedOn;
            CreatedBy = city.City.CreatedBy;
            Status = city.City.Status;
            DeletedOn = city.City.DeletedOn;
            Name = city.Name;
            CountryId = city.City.CountryId;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
    }
}
