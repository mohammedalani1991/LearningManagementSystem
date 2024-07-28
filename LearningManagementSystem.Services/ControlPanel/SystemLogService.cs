using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SystemLogService: ISystemLogService
    {
        private readonly ISettingService _settingService;
        public SystemLogService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<SystemLog> GetSystemLogs(string searchString, int? page ,int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var systemLog = db.SystemLogs.Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (!String.IsNullOrEmpty(searchString))
                {
                    systemLog = systemLog.Where(x => x.Name.Contains(searchString) || x.Component.Contains(searchString) || x.StackTrace.Contains(searchString));
                }
                int pageNumber = (page ?? 1);
                systemLog = systemLog.OrderByDescending(x => x.Id);

                var pageSize = pagination;
                return systemLog.ToPagedList(pageNumber, pageSize);
            }
        }
        public SystemLog GetSystemLogById(int? id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var systemLog = db.SystemLogs.Find(id);
                return systemLog;
            }
        }
        public void DeleteSystemLog(SystemLog systemLog)
        {
            using (var db = new LearningManagementSystemContext())
            {
                systemLog.Status = (int)GeneralEnums.StatusEnum.Deleted;
                db.Entry(systemLog).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
