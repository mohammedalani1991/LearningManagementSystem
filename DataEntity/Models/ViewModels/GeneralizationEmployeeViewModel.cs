using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class GeneralizationEmployeeViewModel
    {
        public GeneralizationEmployeeViewModel()
        {

        }
        public GeneralizationEmployeeViewModel(GeneralizationEmployee generalizationEmployee)
        {
            ContactId = generalizationEmployee.ContactId;
            GeneralizationId = generalizationEmployee.GeneralizationId;
            CreatedOn = generalizationEmployee.CreatedOn;
            CreatedBy = generalizationEmployee.CreatedBy;
            Status = generalizationEmployee.Status;
        }
        public int Id { get; set; }
        public int? GeneralizationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? ContactId { get; set; }
        public string UserName { get; set; }
        public string UserGender { get; set; }
        public string UserJob { get; set; }
        public int UserJobId { get; set; }
        public string UserBranch { get; set; }
        public int UserBranchId { get; set; }
        public int Page { get; set; }
        public bool Selected { get; set; }
    }
}
