using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ITrainerService
    {
        Trainer GetTrainerById(int id);
        Trainer GetTrainerByCourseExamId(int courseExamId);
        List<TrainerViewModel> GetTrainers(int languageId);
        TrainerViewModel GetTrainerById(int id, int languageId);

        Trainer GetActiveTrainerById(int id, int languageId);

        IPagedList<Trainer> GetTrainers(string searchText, int? page, int languageId, int pagination);
        IPagedList<Trainer> GetActiveTrainers(string searchText, int? specialty, int? page, int languageId, int pagination);
        List<Trainer> GetActiveTrainers(bool? EnableAndShowInpages,int languageId);
        Task<List<Trainer>> GetActiveTrainersForHonePage(bool? EnableAndShowInpages, int languageId);
        int AddTrainer(TrainerViewModel trainerViewModel);
        void EditTrainer(TrainerViewModel trainerViewModel, Trainer trainer);
        void UpdateTrainer(TrainerViewModel trainerViewModel);
        void DeleteTrainer(Trainer trainer);
        TrainerViewModel GetTrainerByContactId(int ContactId);
    }
}
