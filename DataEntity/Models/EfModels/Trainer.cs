using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class Trainer
    {
        public Trainer()
        {
            EnrollTeacherCourses = new HashSet<EnrollTeacherCourse>();
            PracticalEnrollmentExamTrainers = new HashSet<PracticalEnrollmentExamTrainer>();
            Questions = new HashSet<Question>();
            TrainerTranslations = new HashSet<TrainerTranslation>();
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime StartWorkDate { get; set; }
        public int? GeneralSpecialtyId { get; set; }
        public int? WorkHouers { get; set; }
        public int? UserProfileId { get; set; }
        public bool? IsUser { get; set; }
        public bool? IsFullTimeWorker { get; set; }
        public bool? ShowInPages { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Signature { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }
        public virtual ICollection<PracticalEnrollmentExamTrainer> PracticalEnrollmentExamTrainers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TrainerTranslation> TrainerTranslations { get; set; }
    }
}
