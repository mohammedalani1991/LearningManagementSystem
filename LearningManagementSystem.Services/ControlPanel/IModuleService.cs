using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IModuleService
    {
        IPagedList<Module> GetModule(string searchText, int? page, int languageId, int pagination);
        Module GetModuleById(int id);
        ModuleViewModel GetModuleById(int id, int languageId);

        void AddModule(ModuleViewModel module);
        void EditModule(ModuleViewModel moduleViewModel, Module module);
        void DeleteModule(Module module);
    }
}
