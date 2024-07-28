using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class BranchViewModel
    {
        public BranchViewModel()
        {

        }

        public BranchViewModel(BranchTranslation branch)
        {
            Id = branch.BranchId;
            Name = branch.Name;
            CreatedBy = branch.Branch.CreatedBy;
            CreatedOn = branch.Branch.CreatedOn;
            Status = branch.Branch.Status;
            LanguageId = branch.LanguageId;
            Code = branch.Branch.Code;
            ColorId = branch.Branch.ColorId;
        }

        public BranchViewModel(Branch branch)
        {
            Id = branch.Id;
            Name = branch.Name;
            CreatedBy = branch.CreatedBy;
            CreatedOn = branch.CreatedOn;
            Status = branch.Status;
            Code = branch.Code;
            ColorId = branch.ColorId;
        }

        public int Id { get; set; }
        public int? ColorId { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }       
        public string Name { get; set; }
        public int Page { get; set; }
    }
}
