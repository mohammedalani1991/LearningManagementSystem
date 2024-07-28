using System;
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Services.Helpers
{
    public class LogHelper
    {
        public static void AddSystemLog(SystemLog log)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    db.SystemLogs.Add(log);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //TODO: Send Email incase of the failure of adding appliaction log
                }

            }
        }

        public static void AddEmailLog(CommunicationLog log)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    db.CommunicationLogs.Add(log);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //TODO: Send Email incase of the failure of adding Email log
                }

            }
        }

        public static void LogException(string username, Exception ex, string component)
        {
            try
            {
                SystemLog log = new SystemLog
                {
                    Name = ex.Message,
                    CreatedOn = DateTime.Now,
                    CreatedBy = username,
                    Component = component,
                    StackTrace = $"InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}"
                };
                AddSystemLog(log);
            }
            catch (Exception exx)
            {

            }
        }
    }
}
