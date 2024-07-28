using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class StudentTranslation
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string BirthPlace { get; set; }
        public string Work { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string WorkPlace { get; set; }
        public int LanguageId { get; set; }

        public virtual Student Student { get; set; }
    }
}
