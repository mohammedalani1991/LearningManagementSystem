using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISystemSettingService
    {
        IPagedList<SystemSetting> GetSystemSettings(string searchText, int? page,int languageId, int pagination);
        void AddSystemSetting(SettingViewModel setting);
        SystemSetting GetSystemSetting(int id);
        SettingViewModel GetSystemSetting(int id, int languageId);
        void EditSystemSetting(SettingViewModel systemSettingViewModel, SystemSetting systemSetting);
        void DeleteSystemSetting(SystemSetting systemSettingDeleted);
    }
}
