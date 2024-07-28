using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class SystemGroupViewModel
    {
        public SystemGroupViewModel()
        {

        }
        public SystemGroupViewModel(SystemGroup systemGroup)
        {
            Id = systemGroup.Id;
            Name = systemGroup.Name;
            Description = systemGroup.Description;
            CreatedOn = systemGroup.CreatedOn;
            Status = systemGroup.Status;
            CreatedBy = systemGroup.CreatedBy;
            DeletedOn = systemGroup.DeletedOn;
            Status = systemGroup.Status;
        }

        public SystemGroupViewModel(SystemGroupTranslation systemGroup)
        {
            Id = systemGroup.SystemGroupId;
            Name = systemGroup.Name;
            Description = systemGroup.Description;
            CreatedOn = systemGroup.SystemGroup.CreatedOn;
            Status = systemGroup.SystemGroup.Status;
            CreatedBy = systemGroup.SystemGroup.CreatedBy;
            DeletedOn = systemGroup.SystemGroup.DeletedOn;
            LanguageId = systemGroup.LanguageId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int LanguageId { get; set; }
        public List<int> Users { get; set; }
    }
}
