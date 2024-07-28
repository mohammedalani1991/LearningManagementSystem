using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
   public class CmsCategoryProjectViewModel
    {
        public CmsCategoryProjectViewModel()
        {

        }
        public CmsCategoryProjectViewModel(CmsProjectCategory cmsProject)
        {
            Id = cmsProject.Id;
            Name = cmsProject.Name;
            Description = cmsProject.Description;
            CreatedOn = cmsProject.CreatedOn;
            Status = cmsProject.Status;
            CreatedBy = cmsProject.CreatedBy;
            DeletedOn = cmsProject.DeletedOn;
        }

        public CmsCategoryProjectViewModel(CmsProjectCategoryTranslation cmsProject)
        {
            Id = cmsProject.ProjectCategoryId;
            Name = cmsProject.Name;
            Description = cmsProject.Description;
            CreatedOn = cmsProject.ProjectCategory.CreatedOn;
            Status = cmsProject.ProjectCategory.Status;
            CreatedBy = cmsProject.ProjectCategory.CreatedBy;
            DeletedOn = cmsProject.ProjectCategory.DeletedOn;
            LanguageId = cmsProject.LanguageId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }

        public int LanguageId { get; set; }
    }
}
