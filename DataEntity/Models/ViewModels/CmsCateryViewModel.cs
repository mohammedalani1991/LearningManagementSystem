using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class CmsCateryViewModel
    {
        public CmsCateryViewModel()
        {

        }

        public CmsCateryViewModel(CmsCateryTranslation cmscatery)
        {
            Id = cmscatery.CateryId;
            Name = cmscatery.Name;
            Description = cmscatery.Description;
            LanguageId = cmscatery.LanguageId;
            Status = cmscatery.Catery.Status;
            ImageUrl = cmscatery.Catery.ImageUrl;
            ParentId = (cmscatery.Catery.ParentId == null) ? 0 : cmscatery.Catery.ParentId.Value;
            ShowInHomePage = cmscatery.Catery.ShowInHomePage.Value;
            ParentName = (cmscatery.Catery.ParentId == null) ? "--" : cmscatery.Catery.Parent.Name;
            CreatedBy = cmscatery.Catery.CreatedBy;
            CreatedOn = cmscatery.Catery.CreatedOn;

        }

        public CmsCateryViewModel(CmsCatery cmscatery)
        {
            Id = cmscatery.Id;
            Name = cmscatery.Name;
            Description = cmscatery.Description;
            ImageUrl = cmscatery.ImageUrl;
            ParentId = (cmscatery.ParentId == null) ? 0 : cmscatery.ParentId.Value;
            ShowInHomePage = cmscatery.ShowInHomePage.Value;
            CreatedBy = cmscatery.CreatedBy;
            CreatedOn = cmscatery.CreatedOn;
            Status = cmscatery.Status;
            ParentName = (cmscatery.ParentId == null) ? "--" : cmscatery.Parent.Name;
        }



        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public int Page { get; set; }
        public string ParentName { get; set; }
        public List<CmsCatery> ListCmsCaterys { get; set; }


    }
}
