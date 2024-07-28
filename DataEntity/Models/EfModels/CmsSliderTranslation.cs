using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class CmsSliderTranslation
    {
        public int Id { get; set; }
        public int SliderId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual CmsSlider Slider { get; set; }
    }
}
