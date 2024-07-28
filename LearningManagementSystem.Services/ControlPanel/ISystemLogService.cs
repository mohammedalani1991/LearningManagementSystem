using DataEntity.Models.EfModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISystemLogService
    {
        IPagedList<SystemLog> GetSystemLogs(string searchString, int? page, int pagination);
        SystemLog GetSystemLogById(int? id);
        void DeleteSystemLog(SystemLog systemLog);
    }
}
