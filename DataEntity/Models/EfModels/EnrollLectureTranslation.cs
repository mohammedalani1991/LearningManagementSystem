using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollLectureTranslation
    {
        public int Id { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }
        public int EnrollLectureId { get; set; }
        public int LanguageId { get; set; }

        public virtual EnrollLecture EnrollLecture { get; set; }
    }
}
