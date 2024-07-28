using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class LookupService: ILookupService
    {
        private readonly ISettingService _settingService;
        public LookupService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        #region Master lookup
        public IPagedList<MasterLookup> GetMasterLookups(string searchText, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var masterLookup = db.MasterLookups.Include(a=>a.MasterLookupTranslations)
                    .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    masterLookup = masterLookup.Where(r => r.Name.Contains(searchText)).ToList();
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = masterLookup;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.MasterLookupTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }
                return output;
            }
        }
        public void AddMasterLookup(MasterLookupViewModel masterLookupViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var masterLookup = new MasterLookup
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = masterLookupViewModel.CreatedBy,
                    Status = masterLookupViewModel.Status,
                    Name = masterLookupViewModel.Name,
                    Code = masterLookupViewModel.Code,
                };
                db.MasterLookups.Add(masterLookup);
                db.SaveChanges();

                if (masterLookupViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterLookupTran = new MasterLookupTranslation()
                    {
                        Name = masterLookupViewModel.Name,
                        LanguageId = masterLookupViewModel.LanguageId,
                        MasterLookupId = masterLookup.Id
                    };
                    db.MasterLookupTranslations.Add(masterLookupTran);
                    db.SaveChanges();
                }
            }
        }
        public MasterLookup GetMasterLookupById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var masterLookup = db.MasterLookups.Find(id);
                return masterLookup;
            }
        }

        public MasterLookupViewModel GetMasterLookupById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterLookupTrans =
                        db.MasterLookupTranslations.Include(r=> r.MasterLookup).FirstOrDefault(r => r.LanguageId == languageId && r.MasterLookupId == id);
                    if (masterLookupTrans != null)
                    {
                        return new MasterLookupViewModel(masterLookupTrans);
                    }
                }
                var masterLookup = db.MasterLookups.Find(id);
                return new MasterLookupViewModel(masterLookup);
            }
        }
        public void EditMasterLookup(MasterLookupViewModel masterLookupViewModel, MasterLookup masterLookup)
        {
            using (var db = new LearningManagementSystemContext())
            {
                masterLookup.Status = masterLookupViewModel.Status;
                masterLookup.Code = masterLookupViewModel.Code;
                if (masterLookupViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    masterLookup.Name = masterLookupViewModel.Name;
                }

                db.Entry(masterLookup).State = EntityState.Modified;
                db.SaveChanges();
                if (masterLookupViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterTran = db.MasterLookupTranslations.FirstOrDefault(r =>
                        r.LanguageId == masterLookupViewModel.LanguageId &&
                        r.MasterLookupId == masterLookupViewModel.Id);
                    if (masterTran != null)
                    {
                        masterTran.Name = masterLookupViewModel.Name;
                        db.Entry(masterTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var masterLookupTran = new MasterLookupTranslation()
                        {
                            Name = masterLookupViewModel.Name,
                            LanguageId = masterLookupViewModel.LanguageId,
                            MasterLookupId = masterLookup.Id
                        };
                        db.MasterLookupTranslations.Add(masterLookupTran);
                    }

                    db.SaveChanges();
                }
            }
        }
        public void DeleteMasterLookup(MasterLookup master)
        {
            using (var db = new LearningManagementSystemContext())
            {
                master.Status = (int)GeneralEnums.StatusEnum.Deleted;
                master.DeletedOn = DateTime.Now;
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion

        #region Details lookup
        public List<DetailsLookupViewModel> GetDetailsLookups(int masterId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var detailsLookup = db.DetailsLookups.Include(a=>a.DetailsLookupTranslations)
                    .Where(r => r.MasterId == masterId &&
                                r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                
                var output = detailsLookup.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.DetailsLookupTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Value = trans.Value;
                        }
                    }
                }                
                var result = output.ToList().Select(r =>
                    new DetailsLookupViewModel
                    {
                        Id = r.Id,
                        Status = r.Status,
                        Name = r.Value,
                        Code = r.Code,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn,
                        DeletedOn = r.DeletedOn,
                        MasterId = r.MasterId
                    }
                ).OrderByDescending(r => r.Id);

                return result.ToList();
            }
        }
        public DetailsLookupViewModel AddDetailsLookup(DetailsLookupViewModel detailsLookupViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var detailsLookup = new DetailsLookup
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = detailsLookupViewModel.CreatedBy,
                    Status = detailsLookupViewModel.Status,
                    Code = detailsLookupViewModel.Code,
                    MasterId = detailsLookupViewModel.MasterId,
                    Value = detailsLookupViewModel.Name
                };
                db.DetailsLookups.Add(detailsLookup);
                db.SaveChanges();

                if (detailsLookupViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var detailsLookupTran = new DetailsLookupTranslation()
                    {
                        Value = detailsLookupViewModel.Name,
                        LanguageId = detailsLookupViewModel.LanguageId,
                        DetailsLookupId = detailsLookup.Id
                    };
                    db.DetailsLookupTranslations.Add(detailsLookupTran);
                    db.SaveChanges();
                }

                detailsLookupViewModel.Id = detailsLookup.Id;
                return detailsLookupViewModel;
            }
        }
        public DetailsLookup GetDetailsLookupById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var detailsLookup = db.DetailsLookups.Find(id);
                return detailsLookup;
            }
        }

        public DetailsLookupViewModel GetDetailsLookupById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterLookupTrans =
                        db.DetailsLookupTranslations.Include(r => r.DetailsLookup).FirstOrDefault(r => r.LanguageId == languageId && r.DetailsLookupId == id);
                    if (masterLookupTrans != null)
                    {
                        return new DetailsLookupViewModel(masterLookupTrans);
                    }
                }
                var detailsLookup = db.DetailsLookups.Find(id);
                return new DetailsLookupViewModel(detailsLookup);
            }
        }

        public void EditDetailLookup(DetailsLookupViewModel detailsLookupViewModel, DetailsLookup detailsLookup)
        {
            using (var db = new LearningManagementSystemContext())
            {
                detailsLookup.Status = detailsLookupViewModel.Status;
                detailsLookup.Code = detailsLookupViewModel.Code;

                if (detailsLookupViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    detailsLookup.Value = detailsLookupViewModel.Name;
                }

                db.Entry(detailsLookup).State = EntityState.Modified;
                db.SaveChanges();

                if (detailsLookupViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var detailsTran = db.DetailsLookupTranslations.FirstOrDefault(r =>
                        r.LanguageId == detailsLookupViewModel.LanguageId &&
                        r.DetailsLookupId == detailsLookupViewModel.Id);
                    if (detailsTran != null)
                    {
                        detailsTran.Value = detailsLookupViewModel.Name;
                        db.Entry(detailsTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var detailsLookupTran = new DetailsLookupTranslation()
                        {
                            Value = detailsLookupViewModel.Name,
                            LanguageId = detailsLookupViewModel.LanguageId,
                            DetailsLookupId = detailsLookup.Id
                        };
                        db.DetailsLookupTranslations.Add(detailsLookupTran);
                    }

                    db.SaveChanges();
                }
            }
        }
        public void DeleteDetailLookup(DetailsLookup details)
        {
            using (var db = new LearningManagementSystemContext())
            {
                details.Status = (int)GeneralEnums.StatusEnum.Deleted;
                details.DeletedOn = DateTime.Now;
                db.Entry(details).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion
    }
}
