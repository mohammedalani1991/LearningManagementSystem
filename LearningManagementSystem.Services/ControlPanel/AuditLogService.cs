using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ISettingService _settingService;
        public AuditLogService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<AuditLog> GetAuditLogs(string searchString, int? page, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var auditLog = db.AuditLogs.Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (!String.IsNullOrEmpty(searchString))
                {
                    auditLog = auditLog.Where(x => x.Controller.Contains(searchString) || x.Action.Contains(searchString) || x.Description.Contains(searchString));
                }
                int pageNumber = (page ?? 1);
                auditLog = auditLog.OrderByDescending(x => x.Id);

                var pageSize = pagination;
                return auditLog.ToPagedList(pageNumber, pageSize);
            }
        }
        public AuditLog GetAuditLogById(int? id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var auditLog = db.AuditLogs.Find(id);
                return auditLog;
            }
        }
        public void DeleteAuditLog(AuditLog auditLog)
        {
            using (var db = new LearningManagementSystemContext())
            {
                auditLog.Status = (int)GeneralEnums.StatusEnum.Deleted;
                db.Entry(auditLog).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
