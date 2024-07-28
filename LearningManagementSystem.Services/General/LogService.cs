using System;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Services.General
{
    public class LogService: ILogService
    {
        private readonly LearningManagementSystemContext _db;

        public LogService(LearningManagementSystemContext context)
        {
            _db = context;
        }
        public void AddSystemLog(SystemLog log)
        {
           
                try
                {
                    _db.SystemLogs.Add(log);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //TODO: Send Email incase of the failure of adding appliaction log
                }
        }

        public void LogException(string username, Exception ex, string component)
        {
            SystemLog log = new SystemLog
            {
                Name = ex.Message,
                CreatedOn = DateTime.Now,
                CreatedBy = username ?? string.Empty,
                Component = component,
                Status = (int) GeneralEnums.StatusEnum.Active,
                StackTrace = $"InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}"
            };
            AddSystemLog(log);
        }
    }
}
