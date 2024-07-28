using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;

namespace DataEntity.Models.ViewModels
{
    public class CmsPageViewModel
    {
        public CmsPageViewModel()
        {

        }

        public CmsPageViewModel(CmsPageTranslation cmsPage)
        {
            Id = cmsPage.PageId;
            Name = cmsPage.Name;
            Description = cmsPage.Description;
            ShortDescription = cmsPage.ShortDescription;
            Keyword = cmsPage.Page.Keyword;
            LanguageId = cmsPage.LanguageId;
            Status = cmsPage.Page.Status;
            ImageUrl = cmsPage.Page.ImageUrl;
            AllowComment = cmsPage.Page.AllowComment.Value;
            IsFeatured = cmsPage.Page.IsFeatured.Value;
            ShowInHomePage = cmsPage.Page.ShowInHomePage.Value;
            PublishDate = cmsPage.Page.PublishDate;
            EndDate = cmsPage.Page.EndDate;           
            SortOrder = cmsPage.Page.SortOrder;
            CreatedBy = cmsPage.Page.CreatedBy;
            CreatedOn = cmsPage.Page.CreatedOn;
            CateryId = cmsPage.Page.CateryId??0;
            CateryName = (cmsPage.Page.CateryId == null || cmsPage.Page.Catery.Status == (int)GeneralEnums.StatusEnum.Deleted) ? "--" : cmsPage.Page.Catery.Name;
        }

        public CmsPageViewModel(CmsPage cmsPage)
        {
            Id = cmsPage.Id;
            Name = cmsPage.Name;
            Description = cmsPage.Description;
            ShortDescription = cmsPage.ShortDescription;
            Keyword = cmsPage.Keyword;
            Status = cmsPage.Status;
            ImageUrl = cmsPage.ImageUrl;
            AllowComment = cmsPage.AllowComment.Value;
            IsFeatured = cmsPage.IsFeatured.Value;
            ShowInHomePage = cmsPage.ShowInHomePage.Value;
            PublishDate = cmsPage.PublishDate;
            EndDate = cmsPage.EndDate;
            SortOrder = cmsPage.SortOrder;
            CreatedBy = cmsPage.CreatedBy;
            CreatedOn = cmsPage.CreatedOn;
            CateryId = cmsPage.CateryId??0;
            CateryName = (cmsPage.CateryId == null || cmsPage.Catery.Status == (int)GeneralEnums.StatusEnum.Deleted) ? "--" : cmsPage.Catery.Name;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Keyword { get; set; }
        public string CateryName { get; set; }

        public int? SortOrder { get; set; }
        public bool AllowComment { get; set; }
        public bool IsFeatured { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int? CateryId { get; set; }
        public int LanguageId { get; set; }       
        public int Page { get; set; }
        public List<CmsCatery> ListCmsCaterys { get; set; }

    }
}
