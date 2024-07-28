using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsWhatPeopleSayTranslation
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public int LanguageId { get; set; }
        public string PersonName { get; set; }
        public string PersonDetails { get; set; }
        public string Description { get; set; }

        public virtual CmsWhatPeopleSay People { get; set; }
    }
}
