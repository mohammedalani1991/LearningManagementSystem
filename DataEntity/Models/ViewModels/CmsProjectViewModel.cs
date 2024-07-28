using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
   public class CmsProjectViewModel
    {
        public CmsProjectViewModel()
        {

        }
        public CmsProjectViewModel(CmsProject cmsProject)
        {
            Id = cmsProject.Id;
            Name = cmsProject.Name;
            Description = cmsProject.Description;
            CreatedOn = cmsProject.CreatedOn;
            Status = cmsProject.Status;
            CreatedBy = cmsProject.CreatedBy;
            DeletedOn = cmsProject.DeletedOn;
            ShortDescription = cmsProject.ShortDescription;
            ImageUrl = cmsProject.ImageUrl;
            Keyword = cmsProject.Keyword;
            PublishDate = cmsProject.PublishDate;
            EndDate = cmsProject.EndDate;
            SortOrder = cmsProject.SortOrder;
            ShowInHomePage = cmsProject.ShowInHomePage.Value;
            IsFeatured = cmsProject.IsFeatured.Value;
            PaymentType = cmsProject.PaymentType;
            ProjectCost = cmsProject.ProjectCost;
            OneObjectFees = cmsProject.OneObjectFees;
            ProjectStatus = cmsProject.ProjectStatus;
            ProjectCategoryId = cmsProject.ProjectCategoryId;
            SecondDescription = cmsProject.SecondDescription;
            CmsProjectCosts = cmsProject.CmsProjectCosts;
            CmsProjectDonors = cmsProject.CmsProjectDonors;
            CmsProjectResources = cmsProject.CmsProjectResources;
        }

        public CmsProjectViewModel(CmsProjectTranslation cmsProject)
        {
            Id = cmsProject.ProjectId;
            Name = cmsProject.Name;
            Description = cmsProject.Description;
            CreatedOn = cmsProject.Project.CreatedOn;
            Status = cmsProject.Project.Status;
            CreatedBy = cmsProject.Project.CreatedBy;
            DeletedOn = cmsProject.Project.DeletedOn;
            LanguageId = cmsProject.LanguageId;
            ShortDescription = cmsProject.ShortDescription;
            ImageUrl = cmsProject.Project.ImageUrl;
            Keyword = cmsProject.Keyword;
            PublishDate = cmsProject.Project.PublishDate;
            EndDate = cmsProject.Project.EndDate;
            SortOrder = cmsProject.Project.SortOrder;
            ShowInHomePage = cmsProject.Project.ShowInHomePage.Value;
            IsFeatured = cmsProject.Project.IsFeatured.Value;
            PaymentType = cmsProject.Project.PaymentType;
            ProjectCost = cmsProject.Project.ProjectCost;
            OneObjectFees = cmsProject.Project.OneObjectFees;
            ProjectStatus = cmsProject.Project.ProjectStatus;
            ProjectCategoryId = cmsProject.Project.ProjectCategoryId;
            SecondDescription = cmsProject.SecondDescription;
            CmsProjectCosts = cmsProject.Project.CmsProjectCosts;
            CmsProjectDonors = cmsProject.Project.CmsProjectDonors;
            CmsProjectResources = cmsProject.Project.CmsProjectResources;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SecondDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Keyword { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SortOrder { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsFeatured { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? PaymentType { get; set; }
        public double? ProjectCost { get; set; }
        public double? OneObjectFees { get; set; }
        public int? ProjectStatus { get; set; }
        public int? ProjectCategoryId { get; set; }
        public string ProjectCategoryName { get; set; }
        public int LanguageId { get; set; }
        public int? BranchId { get; set; }

        public virtual ICollection<CmsProjectCost> CmsProjectCosts { get; set; }
        public virtual ICollection<CmsProjectDonor> CmsProjectDonors { get; set; }
        public virtual ICollection<CmsProjectResource> CmsProjectResources { get; set; }
    }
}
