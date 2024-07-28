using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class GeneralizationViewModel
    {
        public GeneralizationViewModel()
        {

        }
        public GeneralizationViewModel(GeneralizationTranslation generalization)
        {
            Id = generalization.GeneralizationId;
            Title = generalization.Title;
            Description = generalization.Description;
            CreatedBy = generalization.Generalization.CreatedBy;
            CreatedOn = generalization.Generalization.CreatedOn;
            Status = generalization.Generalization.Status;
            LanguageId = generalization.LanguageId;
            GeneralizationTypeId = generalization.Generalization.GeneralizationTypeId;
            JobId = generalization.Generalization.JobId;
            BranchId = generalization.Generalization.BranchId;
        }
        public GeneralizationViewModel(Generalization generalization)
        {
            Id = generalization.Id;
            Title = generalization.Title;
            Description = generalization.Description;
            CreatedBy = generalization.CreatedBy;
            CreatedOn = generalization.CreatedOn;
            Status = generalization.Status;
            GeneralizationTypeId = generalization.GeneralizationTypeId;
            JobId = generalization.JobId;
            BranchId = generalization.BranchId;
        }
        public int Id { get; set; }
        public string Description { get; set; }        
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int? BranchId { get; set; }
        public int? BranchName { get; set; }
        public string Title { get; set; }
        public int? JobId { get; set; }
        public int? GeneralizationTypeId { get; set; }
        public int Page { get; set; }
        public bool SelectEmployee  { get; set; }
        public string GeneralizationContactsText { get; set; }
        public List<string> GeneralizationContacts { get; set; }
        public List<GeneralizationEmployeeViewModel> GeneralizationEmployees { get; set; }
    }
}
