using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SubjectTranslation
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
