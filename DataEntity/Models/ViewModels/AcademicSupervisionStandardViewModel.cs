using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class AcademicSupervisionStandardViewModel
    {
        public AcademicSupervisionStandardViewModel()
        {

        }
        public AcademicSupervisionStandardViewModel(AcademicSupervisionStandard AcademicSupervisionStandard)
        {
            CreatedOn = AcademicSupervisionStandard.CreatedOn;
            CreatedBy = AcademicSupervisionStandard.CreatedBy;
            Standard = AcademicSupervisionStandard.Standard;
            Status = AcademicSupervisionStandard.Status;
            SortOrder = AcademicSupervisionStandard.SortOrder;
            Id = AcademicSupervisionStandard.Id;
        }

        public AcademicSupervisionStandardViewModel(AcademicSupervisionStandardTranslation AcademicSupervisionStandardTranslation)
        {
            Id = AcademicSupervisionStandardTranslation.AcademicSupervisionStandardId;
            Standard = AcademicSupervisionStandardTranslation.Standard;
            CreatedBy = AcademicSupervisionStandardTranslation.AcademicSupervisionStandard.CreatedBy;
            CreatedOn = AcademicSupervisionStandardTranslation.AcademicSupervisionStandard.CreatedOn;
            Status = AcademicSupervisionStandardTranslation.AcademicSupervisionStandard.Status;
            LanguageId = AcademicSupervisionStandardTranslation.LanguageId;
            SortOrder = AcademicSupervisionStandardTranslation.AcademicSupervisionStandard.SortOrder;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Standard { get; set; }
        public int? SortOrder { get; set; }
        public int LanguageId { get; set; }

    }
}
