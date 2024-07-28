using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ITrainerRateMeasureService
    {
        IPagedList<TrainerRateMeasure> GetTrainerRateMeasures(string searchText, int? page, int languageId, int pagination);
        TrainerRateMeasure GetTrainerRateMeasureById(int id);
        TrainerRateMeasure GetTrainerRateMeasureById(int id, int languageId);
        void AddTrainerRateMeasure(TrainerRateMeasureViewModel TrainerRateMeasureViewModel);
        void EditTrainerRateMeasure(TrainerRateMeasureViewModel TrainerRateMeasureViewModel, TrainerRateMeasure TrainerRateMeasure);
        void DeleteTrainerRateMeasure(TrainerRateMeasure TrainerRateMeasure);
    }
}
