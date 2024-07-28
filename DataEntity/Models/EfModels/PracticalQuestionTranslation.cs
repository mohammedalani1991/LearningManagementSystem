using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalQuestionTranslation
    {
        public int Id { get; set; }
        public int PracticalQuestionId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PracticalQuestion PracticalQuestion { get; set; }
    }
}
