﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class EnrollSectionOfCourse
    {
        public EnrollSectionOfCourse()
        {
            EnrollLectures = new HashSet<EnrollLecture>();
            EnrollSectionOfCourseTranslations = new HashSet<EnrollSectionOfCourseTranslation>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int EnrollCourseId { get; set; }

        public virtual EnrollTeacherCourse EnrollCourse { get; set; }
        public virtual ICollection<EnrollLecture> EnrollLectures { get; set; }
        public virtual ICollection<EnrollSectionOfCourseTranslation> EnrollSectionOfCourseTranslations { get; set; }
    }
}