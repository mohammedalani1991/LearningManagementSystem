using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Semester
    {
        public Semester()
        {
            CertificateAdoptions = new HashSet<CertificateAdoption>();
            EnrollTeacherCourses = new HashSet<EnrollTeacherCourse>();
            SemesterTranslations = new HashSet<SemesterTranslation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? PublicationEndDate { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public int? BranchId { get; set; }
        public bool? Default { get; set; }

        public virtual ICollection<CertificateAdoption> CertificateAdoptions { get; set; }
        public virtual ICollection<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }
        public virtual ICollection<SemesterTranslation> SemesterTranslations { get; set; }
    }
}
