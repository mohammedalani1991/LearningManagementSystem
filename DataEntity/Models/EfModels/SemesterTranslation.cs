using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SemesterTranslation
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }

        public virtual Semester Semester { get; set; }
    }
}
