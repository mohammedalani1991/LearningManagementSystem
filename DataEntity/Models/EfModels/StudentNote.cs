using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class StudentNote
    {
        public StudentNote()
        {
            StudentNotesTranslations = new HashSet<StudentNotesTranslation>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<StudentNotesTranslation> StudentNotesTranslations { get; set; }
    }
}
