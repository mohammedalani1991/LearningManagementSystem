using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class ModuleViewModel
    {
        public ModuleViewModel()
        {

        }
        public ModuleViewModel(ModuleTranslation module)
        {
            Id = module.ModuleId;
            Name = module.Name;
            Description = module.Description;
            CreatedBy = module.Module.CreatedBy;
            CreatedOn = module.Module.CreatedOn;
            Status = module.Module.Status;
            LanguageId = module.LanguageId;
            BaseUrl = module.Module.BaseUrl;
            SortOrder = module.Module.SortOrder;
            Code = module.Module.Code;
        }
        public ModuleViewModel(Module module)
        {
            Id = module.Id;
            Name = module.Name;
            Description = module.Description;
            CreatedBy = module.CreatedBy;
            CreatedOn = module.CreatedOn;
            Status = module.Status;
            BaseUrl = module.BaseUrl;
            SortOrder = module.SortOrder;
            Code = module.Code;
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }       
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public int? SortOrder { get; set; }
        public int Page { get; set; }
    }
}
