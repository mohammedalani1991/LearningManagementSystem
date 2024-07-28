using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class PracticalExamTranslation
    {
        public int Id { get; set; }
        public int PracticalExamId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual PracticalExam PracticalExam { get; set; }
    }
}
