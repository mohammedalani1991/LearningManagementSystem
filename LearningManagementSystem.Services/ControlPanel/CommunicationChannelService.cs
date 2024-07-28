using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CommunicationChannelService : ICommunicationChannelService
    {
        public void AddCommunicationChannel(CommunicationChannelViewModel communicationChannelViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationChannel = new CommunicationChannel
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = communicationChannelViewModel.CreatedBy,
                    Status = communicationChannelViewModel.Status,
                    Name = communicationChannelViewModel.Name,
                    Note = communicationChannelViewModel.Note,
                };
                db.CommunicationChannels.Add(communicationChannel);
                db.SaveChanges();

                if (communicationChannelViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var communicationChannelTran = new CommunicationChannelTranslation()
                    {
                        Name = communicationChannel.Name,
                        Note = communicationChannel.Note,
                        LanguageId = communicationChannelViewModel.LanguageId,
                        CommunicationChannelId = communicationChannel.Id
                    };
                    db.CommunicationChannelTranslations.Add(communicationChannelTran);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteCommunicationChannel(CommunicationChannel communicationChannel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                communicationChannel.Status = (int)GeneralEnums.StatusEnum.Deleted;
                communicationChannel.DeletedOn = DateTime.Now;
                db.Entry(communicationChannel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditCommunicationChannel(CommunicationChannelViewModel communicationChannelViewModel, CommunicationChannel communicationChannel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                communicationChannel.Status = communicationChannelViewModel.Status;
                if (communicationChannelViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    communicationChannel.Name = communicationChannelViewModel.Name;
                }

                db.Entry(communicationChannel).State = EntityState.Modified;
                db.SaveChanges();
                if (communicationChannelViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterTran = db.CommunicationChannelTranslations.FirstOrDefault(r =>
                        r.LanguageId == communicationChannelViewModel.LanguageId &&
                        r.CommunicationChannelId == communicationChannelViewModel.Id);
                    if (masterTran != null)
                    {
                        masterTran.Name = communicationChannelViewModel.Name;
                        db.Entry(masterTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var communicationChannelTran = new CommunicationChannelTranslation()
                        {
                            Name = communicationChannelViewModel.Name,
                            LanguageId = communicationChannelViewModel.LanguageId,
                            CommunicationChannelId = communicationChannel.Id
                        };
                        db.CommunicationChannelTranslations.Add(communicationChannelTran);
                    }

                    db.SaveChanges();
                }
            }
        }

        public CommunicationChannel GetCommunicationChannelById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationChannel = db.CommunicationChannels.Find(id);
                return communicationChannel;
            }
        }

        public CommunicationChannelViewModel GetCommunicationChannelById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var communicationChannelTrans =
                        db.CommunicationChannelTranslations.Include(r => r.CommunicationChannel).FirstOrDefault(r => r.LanguageId == languageId && r.CommunicationChannelId == id);
                    if (communicationChannelTrans != null)
                    {
                        return new CommunicationChannelViewModel(communicationChannelTrans);
                    }
                }
                var communicationChannel = db.CommunicationChannels.Find(id);
                return new CommunicationChannelViewModel(communicationChannel);
            }
        }

        public IPagedList<CommunicationChannel> GetCommunicationChannels(string searchText, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationChannel = db.CommunicationChannels.Include(a => a.CommunicationChannelTranslations)
                    .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    communicationChannel = communicationChannel.Where(r => r.Name.Contains(searchText)).ToList();
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        communicationChannel = communicationChannel.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
            
                        communicationChannel = communicationChannel.Where(r => r.CommunicationChannelTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = communicationChannel;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CommunicationChannelTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Note = trans.Note;
                        }
                    }
                }
                return output;
            }
        }
    }
}
