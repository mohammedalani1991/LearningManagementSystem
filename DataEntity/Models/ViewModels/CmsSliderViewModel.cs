using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CmsSliderViewModel
    {
        public CmsSliderViewModel()
        {

        }

        public CmsSliderViewModel(CmsSliderTranslation cmsSlider)
        {
            Id = cmsSlider.SliderId;
            Name = cmsSlider.Name;
            Description = cmsSlider.Description;
            LanguageId = cmsSlider.LanguageId;
            Status = cmsSlider.Slider.Status;
            ImageUrl = cmsSlider.Slider.ImageUrl;
            Image2Url = cmsSlider.Slider.Image2Url;
            ReadMoreLink = cmsSlider.Slider.ReadMoreLink;
            SortOrder = cmsSlider.Slider.SortOrder;
            CreatedBy = cmsSlider.Slider.CreatedBy;
            CreatedOn = cmsSlider.Slider.CreatedOn;
        }

        public CmsSliderViewModel(CmsSlider cmsSlider)
        {
            Id = cmsSlider.Id;
            Name = cmsSlider.Name;
            Description = cmsSlider.Description;
            ImageUrl = cmsSlider.ImageUrl;
            Image2Url = cmsSlider.Image2Url;
            ReadMoreLink = cmsSlider.ReadMoreLink;
            SortOrder = cmsSlider.SortOrder;
            CreatedBy = cmsSlider.CreatedBy;
            CreatedOn = cmsSlider.CreatedOn;
            Status = cmsSlider.Status;
        }



        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Image2Url { get; set; }
        public string ReadMoreLink { get; set; }
        public int? SortOrder { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }       
        public int Page { get; set; }

    }
}
