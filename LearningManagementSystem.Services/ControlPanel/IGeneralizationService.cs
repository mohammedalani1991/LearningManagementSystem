using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using LearningManagementSystem.Core.SystemEnums;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IGeneralizationService
    {
        IPagedList<Generalization> GetGeneralization(string searchText, int? page, int languageId, int pagination, int branchId);
        Generalization GetGeneralizationById(int id);
        Generalization GetGeneralizationById(int id, int languageId);

        void AddGeneralization(GeneralizationViewModel generalization);
        void EditGeneralization(GeneralizationViewModel generalizationViewModel, Generalization generalization);
        void DeleteGeneralization(Generalization generalization);
        IPagedList<GeneralizationEmployeeViewModel> GetGeneralizationEmployees(int languageId, int branchId,int jobId,int generalizationId);
        List<GeneralizationEmployeeViewModel> GetGeneralizationEmployees(int generalizationId, int languageId);
        bool ReadNotification(int id);
    }
}
