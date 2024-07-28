using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CommunicationLogService : ICommunicationLogService
    {
        private readonly ISettingService _settingService;
        public CommunicationLogService(ISettingService settingService)
        {
            _settingService = settingService;
        }        

        public IPagedList<CommunicationLog> GetCommunicationLogs(string searchText, int? page, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationLogs = db.CommunicationLogs.Include(a=>a.Contact).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    communicationLogs = communicationLogs.Where(r => r.LogText.Contains(searchText) || r.Contact.FullName.Contains(searchText));
                }

                var result = communicationLogs;

                var pageSize = pagination;
                var pageNumber = (page ?? 1);


                return result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            }
        }

        public CommunicationLog GetCommunicationLogById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationLog = db.CommunicationLogs.Include(a=>a.Contact).Where(a=>a.Id==id).FirstOrDefault();
                return communicationLog;
            }
        }

        public CommunicationLogsViewModel GetCommunicationLogById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {                
                var aboutDic = db.CommunicationLogs.Find(id);
                return new CommunicationLogsViewModel(aboutDic);
            }
        }

        public void AddCommunicationLog(CommunicationLogsViewModel communicationLogViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationLog = new CommunicationLog()
                {
                    CreatedOn = DateTime.Now,
                    Status = communicationLogViewModel.Status,
                    TypeId = communicationLogViewModel.TypeId,
                    LogText = communicationLogViewModel.LogText,
                    //CreatedBy = communicationLogViewModel.CreatedBy,
                    IsForExtraType = communicationLogViewModel.IsForExtraType,
                    ContactId = communicationLogViewModel.ContactId,
                    ContactType = communicationLogViewModel.ContactType,
                    TypeText = communicationLogViewModel.TypeText,
                };
                db.CommunicationLogs.Add(communicationLog);
                db.SaveChanges();
                communicationLogViewModel.Id = communicationLog.Id;
            }
        }

        public void DeleteCommunicationLog(CommunicationLog communicationLog)
        {
            using (var db = new LearningManagementSystemContext())
            {
                communicationLog.Status = (int)GeneralEnums.StatusEnum.Deleted;
                communicationLog.DeletedOn = DateTime.Now;
                db.Entry(communicationLog).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void AddCommunicationLogs(List<CommunicationLogsViewModel> communicationLogs)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var logs = communicationLogs.Select(a => new CommunicationLog()
                {
                    LogText = a.LogText,
                    TypeId = a.TypeId,
                    CreatedOn = DateTime.Now,
                    //CreatedBy = a.CreatedBy,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    IsForExtraType = a.IsForExtraType,
                    ContactId = a.ContactId,
                    ContactType = a.ContactType,
                    TypeText = a.TypeText,
                });
                db.AddRange(logs);
                db.SaveChanges();
            }
        }
    }
}
