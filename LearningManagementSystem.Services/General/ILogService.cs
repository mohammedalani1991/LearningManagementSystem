using System;
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Services.General
{
    public interface ILogService
    {
        void AddSystemLog(SystemLog log);
        void LogException(string username, Exception ex, string component);
    }
}
