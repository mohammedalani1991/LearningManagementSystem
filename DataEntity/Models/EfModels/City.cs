using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class City
    {
        public City()
        {
            CityTranslations = new HashSet<CityTranslation>();
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<CityTranslation> CityTranslations { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
