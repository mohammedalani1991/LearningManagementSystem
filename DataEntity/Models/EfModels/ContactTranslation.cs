using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class ContactTranslation
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int LanguageId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
