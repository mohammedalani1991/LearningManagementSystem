using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollCourseAssigment
    {
        public EnrollCourseAssigment()
        {
            EnrollCourseAssigmentQuestions = new HashSet<EnrollCourseAssigmentQuestion>();
            EnrollCourseAssigmentTranslations = new HashSet<EnrollCourseAssigmentTranslation>();
            EnrollStudentAssigments = new HashSet<EnrollStudentAssigment>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishEndDate { get; set; }

        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual ICollection<EnrollCourseAssigmentQuestion> EnrollCourseAssigmentQuestions { get; set; }
        public virtual ICollection<EnrollCourseAssigmentTranslation> EnrollCourseAssigmentTranslations { get; set; }
        public virtual ICollection<EnrollStudentAssigment> EnrollStudentAssigments { get; set; }
    }
}
