using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class LectureTranslation
    {
        public int Id { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public int LectureId { get; set; }
        public int LanguageId { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
