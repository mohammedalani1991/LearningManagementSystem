using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class TrainerRateMeasureViewModel
    {
        public TrainerRateMeasureViewModel()
        {

        }
        public TrainerRateMeasureViewModel(TrainerRateMeasure TrainerRateMeasure)
        {
            CreatedOn = TrainerRateMeasure.CreatedOn;
            CreatedBy = TrainerRateMeasure.CreatedBy;
            FromRange = TrainerRateMeasure.FromRange;
            ToRange = TrainerRateMeasure.ToRange;
            Measure = TrainerRateMeasure.Measure;
            Status = TrainerRateMeasure.Status;
            Type = TrainerRateMeasure.Type;
            Id = TrainerRateMeasure.Id;
        }

        public TrainerRateMeasureViewModel(TrainerRateMeasureTranslation trainerRateMeasureTranslation)
        {
            Id = trainerRateMeasureTranslation.TrainerRateMeasureId;
            Measure = trainerRateMeasureTranslation.Measure;
            FromRange = trainerRateMeasureTranslation.TrainerRateMeasure.FromRange;
            CreatedBy = trainerRateMeasureTranslation.TrainerRateMeasure.CreatedBy;
            CreatedOn = trainerRateMeasureTranslation.TrainerRateMeasure.CreatedOn;
            Status = trainerRateMeasureTranslation.TrainerRateMeasure.Status;
            LanguageId = trainerRateMeasureTranslation.LanguageId;
            Type = trainerRateMeasureTranslation.TrainerRateMeasure.Type;
            ToRange = trainerRateMeasureTranslation.TrainerRateMeasure.ToRange;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public decimal? ToRange { get; set; }
        public decimal? FromRange { get; set; }
        public string Measure { get; set; }
        public int Type { get; set; }
        public int LanguageId { get; set; }

    }
}
