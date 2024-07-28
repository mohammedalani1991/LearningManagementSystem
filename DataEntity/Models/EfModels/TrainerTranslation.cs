using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class TrainerTranslation
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}
