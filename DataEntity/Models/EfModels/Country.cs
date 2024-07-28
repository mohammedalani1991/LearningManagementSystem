using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Contacts = new HashSet<Contact>();
            CountryTranslations = new HashSet<CountryTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CountryTranslation> CountryTranslations { get; set; }
    }
}
