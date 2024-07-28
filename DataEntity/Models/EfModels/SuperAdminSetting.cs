using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SuperAdminSetting
    {
        public int Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlAr { get; set; }
        public string SiteColor { get; set; }
        public string SecondarySiteColor { get; set; }
    }
}
