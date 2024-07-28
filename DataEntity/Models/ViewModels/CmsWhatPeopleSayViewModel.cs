using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CmsWhatPeopleSayViewModel
    {
        public CmsWhatPeopleSayViewModel()
        {

        }

        public CmsWhatPeopleSayViewModel(CmsWhatPeopleSayTranslation cmsWhatPeopleSay)
        {
            Id = cmsWhatPeopleSay.PeopleId;
            PersonName = cmsWhatPeopleSay.PersonName;
            PersonDetails = cmsWhatPeopleSay.PersonDetails;
            Description = cmsWhatPeopleSay.Description;
            LanguageId = cmsWhatPeopleSay.LanguageId;
            Status = cmsWhatPeopleSay.People.Status;
            ImageUrl = cmsWhatPeopleSay.People.ImageUrl;
            ShowInHomePage = cmsWhatPeopleSay.People.ShowInHomePage.Value;
            CreatedBy = cmsWhatPeopleSay.People.CreatedBy;
            CreatedOn = cmsWhatPeopleSay.People.CreatedOn;

        }

        public CmsWhatPeopleSayViewModel(CmsWhatPeopleSay cmsWhatPeopleSay)
        {
            Id = cmsWhatPeopleSay.Id;
            PersonName = cmsWhatPeopleSay.PersonName;
            PersonDetails = cmsWhatPeopleSay.PersonDetails;
            Description = cmsWhatPeopleSay.Description;
            Status = cmsWhatPeopleSay.Status;
            ImageUrl = cmsWhatPeopleSay.ImageUrl;
            ShowInHomePage = cmsWhatPeopleSay.ShowInHomePage.Value;
            CreatedBy = cmsWhatPeopleSay.CreatedBy;
            CreatedOn = cmsWhatPeopleSay.CreatedOn;
        }



        public int Id { get; set; }
        public string PersonName { get; set; }
        public string PersonDetails { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int Page { get; set; }



    }
}
