using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICommunicationLogService
    {
        IPagedList<CommunicationLog> GetCommunicationLogs(string searchText, int? page, int pagination);
        CommunicationLog GetCommunicationLogById(int id);
        CommunicationLogsViewModel GetCommunicationLogById(int id, int languageId);
        void AddCommunicationLog(CommunicationLogsViewModel communicationLog);
        void AddCommunicationLogs(List<CommunicationLogsViewModel> communicationLogs);
        void DeleteCommunicationLog(CommunicationLog communicationLog);
    }
}
