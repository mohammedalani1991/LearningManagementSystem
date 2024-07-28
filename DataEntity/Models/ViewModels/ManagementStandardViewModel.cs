using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class ManagementStandardViewModel
    {
        public ManagementStandardViewModel()
        {

        }
        public ManagementStandardViewModel(ManagementStandard ManagementStandard)
        {
            CreatedOn = ManagementStandard.CreatedOn;
            CreatedBy = ManagementStandard.CreatedBy;
            Standard = ManagementStandard.Standard;
            Status = ManagementStandard.Status;
            SortOrder = ManagementStandard.SortOrder;
            Id = ManagementStandard.Id;
            Type = ManagementStandard.Type;
        }

        public ManagementStandardViewModel(ManagementStandardTranslation ManagementStandardTranslation)
        {
            Id = ManagementStandardTranslation.ManagementStandardId;
            Standard = ManagementStandardTranslation.Standard;
            CreatedBy = ManagementStandardTranslation.ManagementStandard.CreatedBy;
            CreatedOn = ManagementStandardTranslation.ManagementStandard.CreatedOn;
            Status = ManagementStandardTranslation.ManagementStandard.Status;
            LanguageId = ManagementStandardTranslation.LanguageId;
            SortOrder = ManagementStandardTranslation.ManagementStandard.SortOrder;
            Type = ManagementStandardTranslation.ManagementStandard.Type;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? Type { get; set; }
        public string Standard { get; set; }
        public int? SortOrder { get; set; }
        public int LanguageId { get; set; }

    }
}
