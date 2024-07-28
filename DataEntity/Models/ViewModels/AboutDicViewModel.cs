using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class AboutDicViewModel
    {
        public AboutDicViewModel()
        {

        }
        public AboutDicViewModel(AboutDicTranslation aboutDic)
        {
            Id = aboutDic.AboutDicId;
            Name = aboutDic.Name;
            Value = aboutDic.Value;
            CreatedBy = aboutDic.AboutDic.CreatedBy;
            CreatedOn = aboutDic.AboutDic.CreatedOn;
            Status = aboutDic.AboutDic.Status;
            LanguageId = aboutDic.LanguageId;
            GroupName = aboutDic.AboutDic.GroupName;
            SortOrder = aboutDic.AboutDic.SortOrder;
        }
        public AboutDicViewModel(AboutDic aboutDic)
        {
            Id = aboutDic.Id;
            Name = aboutDic.Name;
            Value = aboutDic.Value;
            CreatedBy = aboutDic.CreatedBy;
            CreatedOn = aboutDic.CreatedOn;
            Status = aboutDic.Status;
            GroupName = aboutDic.GroupName;
            SortOrder = aboutDic.SortOrder;
        }
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? SortOrder { get; set; }
    }
}
