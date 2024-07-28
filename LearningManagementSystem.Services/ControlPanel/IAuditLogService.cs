using DataEntity.Models.EfModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IAuditLogService
    {
        IPagedList<AuditLog> GetAuditLogs(string searchString, int? page, int pagination);
        AuditLog GetAuditLogById(int? id);
        void DeleteAuditLog(AuditLog auditLog);
    }
}
