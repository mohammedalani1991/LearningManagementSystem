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
    public class SubCommunicationChannelService : ISubCommunicationChannelService
    {
        public void AddSubCommunicationChannel(SubCommunicationChannelViewModel subCommunicationChannelViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var subCommunicationChannel = new SubCommunicationChannel
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = subCommunicationChannelViewModel.CreatedBy,
                    Status = subCommunicationChannelViewModel.Status,
                    Name = subCommunicationChannelViewModel.Name,
                    Note = subCommunicationChannelViewModel.Note,
                    CommunicationChannelId = subCommunicationChannelViewModel.CommunicationChannelId,
                    ParentId = subCommunicationChannelViewModel.ParentId,
                };
                db.SubCommunicationChannels.Add(subCommunicationChannel);
                db.SaveChanges();

                if (subCommunicationChannelViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var communicationChannelTran = new SubCommunicationChannelTranslation()
                    {
                        Name = subCommunicationChannelViewModel.Name,
                        Note = subCommunicationChannelViewModel.Note,
                        LanguageId = subCommunicationChannelViewModel.LanguageId,
                        SubCommunicationChannelId = subCommunicationChannel.Id
                    };
                    db.SubCommunicationChannelTranslations.Add(communicationChannelTran);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteSubCommunicationChannel(SubCommunicationChannel subCommunicationChannel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                subCommunicationChannel.Status = (int)GeneralEnums.StatusEnum.Deleted;
                subCommunicationChannel.DeletedOn = DateTime.Now;
                db.Entry(subCommunicationChannel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditSubCommunicationChannel(SubCommunicationChannelViewModel subCommunicationChannelViewModel, SubCommunicationChannel subCommunicationChannel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                subCommunicationChannel.Status = subCommunicationChannelViewModel.Status;
                if (subCommunicationChannelViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    subCommunicationChannel.Name = subCommunicationChannelViewModel.Name;
                    subCommunicationChannel.Note = subCommunicationChannelViewModel.Note;
                }

                db.Entry(subCommunicationChannel).State = EntityState.Modified;
                db.SaveChanges();
                if (subCommunicationChannelViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterTran = db.SubCommunicationChannelTranslations.FirstOrDefault(r =>
                        r.LanguageId == subCommunicationChannelViewModel.LanguageId &&
                        r.SubCommunicationChannelId == subCommunicationChannelViewModel.Id);
                    if (masterTran != null)
                    {
                        masterTran.Name = subCommunicationChannelViewModel.Name;
                        masterTran.Note = subCommunicationChannelViewModel.Note;
                        db.Entry(masterTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var communicationChannelTran = new SubCommunicationChannelTranslation()
                        {
                            Name = subCommunicationChannelViewModel.Name,
                            Note = subCommunicationChannelViewModel.Note,
                            LanguageId = subCommunicationChannelViewModel.LanguageId,
                            SubCommunicationChannelId = subCommunicationChannel.Id
                        };
                        db.SubCommunicationChannelTranslations.Add(communicationChannelTran);
                    }

                    db.SaveChanges();
                }
            }
        }

        public SubCommunicationChannel GetSubCommunicationChannelById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var subCommunicationChannels = db.SubCommunicationChannels.Find(id);
                return subCommunicationChannels;
            }
        }

        public SubCommunicationChannelViewModel GetSubCommunicationChannelById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var communicationChannelTrans =
                        db.SubCommunicationChannelTranslations.Include(r => r.SubCommunicationChannel).FirstOrDefault(r => r.LanguageId == languageId && r.SubCommunicationChannelId == id);
                    if (communicationChannelTrans != null)
                    {
                        return new SubCommunicationChannelViewModel(communicationChannelTrans);
                    }
                }
                var communicationChannel = db.SubCommunicationChannels.Find(id);
                return new SubCommunicationChannelViewModel(communicationChannel);
            }
        }

        public List<SubCommunicationChannelViewModel> GetSubCommunicationChannels(int communicationChannelId, int languageId, int? parentId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var subCommunicationChannels = db.SubCommunicationChannels.Include(a => a.SubCommunicationChannelTranslations)
                    .Where(r => r.ParentId == parentId && r.CommunicationChannelId == communicationChannelId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();


                var output = subCommunicationChannels.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.SubCommunicationChannelTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Note = trans.Note;
                        }
                    }
                }
                var result = output.ToList().Select(r =>
                    new SubCommunicationChannelViewModel
                    {
                        Id = r.Id,
                        Status = r.Status,
                        Name = r.Name,
                        Note = r.Note,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn,
                        DeletedOn = r.DeletedOn,
                        CommunicationChannelId = r.CommunicationChannelId
                    }
                ).OrderByDescending(r => r.Id);

                return result.ToList();
            }
        }

        public IPagedList<SubCommunicationChannel> GetSubCommunicationChannels(int communicationChannelId, int languageId, string searchText, int? page, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var communicationChannel = db.SubCommunicationChannels.Include(a => a.SubCommunicationChannelTranslations)
                    .Where(r => r.ParentId == null && r.CommunicationChannelId == communicationChannelId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

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
                        communicationChannel = communicationChannel.Where(r => r.SubCommunicationChannelTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
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
                        var trans = item.SubCommunicationChannelTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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
