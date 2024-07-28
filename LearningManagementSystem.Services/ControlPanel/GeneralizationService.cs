using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class GeneralizationService : IGeneralizationService
    {
        private readonly ISettingService _settingService;
        public GeneralizationService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<Generalization> GetGeneralization(string searchText, int? page, int languageId, int pagination, int branchId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var generalizations = db.Generalizations.Include(r => r.GeneralizationTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (branchId > 0)
                {
                    generalizations = generalizations.Where(a => a.BranchId == branchId);
                }

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    generalizations = generalizations.Where(r => r.Title.Contains(searchText) || r.Description.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        generalizations = generalizations.Where(r => r.Title.Contains(searchText) || r.Description.Contains(searchText));
                    }
                    else
                    {
                   
                        generalizations = generalizations.Where(r => r.GeneralizationTranslations.Any(t => (t.Title.Contains(searchText) || t.Description.Contains(searchText)) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = generalizations;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.GeneralizationTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Title = trans.Title;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public Generalization GetGeneralizationById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var generalization = db.Generalizations.Include(a => a.GeneralizationEmployees).Where(a => a.Id == id).FirstOrDefault();
                return generalization;
            }
        }
        public Generalization GetGeneralizationById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var generalization = db.Generalizations.Include(a => a.GeneralizationTranslations).Include(a => a.GeneralizationEmployees).Where(a => a.Id == id).FirstOrDefault();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var generalizationTran = generalization.GeneralizationTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.GeneralizationId == id);
                    if (generalizationTran != null)
                    {
                        generalization.Title = generalizationTran.Title;
                        generalization.Description = generalizationTran.Description;
                    }
                }
                return generalization;
            }
        }

        public void AddGeneralization(GeneralizationViewModel generalization)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var about = new Generalization()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Title = generalization.Title,
                    Description = generalization.Description,
                    BranchId = generalization.BranchId,
                    JobId = generalization.JobId,
                    GeneralizationTypeId = generalization.GeneralizationTypeId,
                    CreatedBy = generalization.CreatedBy,
                };
                db.Generalizations.Add(about);
                db.SaveChanges();
                generalization.Id = about.Id;

                if (generalization.GeneralizationContacts != null)
                {
                    if (generalization.GeneralizationContacts.Count() > 0)
                    {
                        foreach (var emp in generalization.GeneralizationContacts)
                        {
                            var employee = new GeneralizationEmployee()
                            {
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                GeneralizationId = generalization.Id,
                                ContactId = Int32.Parse(emp),
                                CreatedBy = generalization.CreatedBy,
                            };
                            db.GeneralizationEmployees.Add(employee);
                        }
                        db.SaveChanges();
                    }
                }

                if (generalization.SelectEmployee==true)
                {
                    AddNotification(generalization.GeneralizationContacts, generalization.Id, generalization.CreatedBy);
                } else {

                    List<string> ids = GetEmployees(generalization.BranchId??0, generalization.JobId??0);
                    AddNotification(ids, generalization.Id, generalization.CreatedBy);
                }

                if (generalization.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var GeneralizationTran = new GeneralizationTranslation()
                    {
                        Title = generalization.Title,
                        Description = generalization.Description,
                        LanguageId = generalization.LanguageId,
                        GeneralizationId = generalization.Id
                    };
                    db.GeneralizationTranslations.Add(GeneralizationTran);
                    db.SaveChanges();
                }
            }
        }


        public void AddNotification(List<string> contacts, int id, string createdBy)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (contacts.Any())
                {
                    foreach (var emp in contacts)
                    {
                        var notification = new Notification()
                        {
                            CreatedOn = DateTime.Now,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            GeneralizationId = id,
                            ContactId = Int32.Parse(emp),
                            CreatedBy = createdBy,
                            IsRead = false,
                        };
                        db.Notifications.Add(notification);
                    }
                    db.SaveChanges();
                }                                
            }
        }


        public void EditGeneralization(GeneralizationViewModel generalizationViewModel, Generalization generalization)
        {
            using (var db = new LearningManagementSystemContext())
            {
                generalization.GeneralizationTypeId = generalizationViewModel.GeneralizationTypeId;
                generalization.JobId = generalizationViewModel.JobId;
                generalization.BranchId = generalizationViewModel.BranchId;
                if (generalizationViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    generalization.Title = generalizationViewModel.Title;
                    generalization.Description = generalizationViewModel.Description;
                }
                db.Entry(generalization).State = EntityState.Modified;
                var employees = db.GeneralizationEmployees.Where(a => a.GeneralizationId == generalization.Id).ToList();
                if (employees.Count() > 0)
                {
                    foreach (var emp in employees)
                    {
                        db.GeneralizationEmployees.Remove(emp);
                    }
                }
                if (generalizationViewModel.GeneralizationContacts != null)
                {
                    if (generalizationViewModel.GeneralizationContacts.Count() > 0)
                    {
                        foreach (var emp in generalizationViewModel.GeneralizationContacts)
                        {
                            var employee = new GeneralizationEmployee()
                            {
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                GeneralizationId = generalization.Id,
                                ContactId = Int32.Parse(emp),
                                CreatedBy = generalization.CreatedBy,
                            };
                            db.GeneralizationEmployees.Add(employee);
                        }
                    }
                }

                if (generalizationViewModel.SelectEmployee == true)
                {
                    AddNotification(generalizationViewModel.GeneralizationContacts, generalizationViewModel.Id, generalizationViewModel.CreatedBy);
                }
                else
                {
                    List<string> ids = GetEmployees(generalizationViewModel.BranchId ?? 0, generalizationViewModel.JobId ?? 0);
                    AddNotification(ids, generalizationViewModel.Id, generalizationViewModel.CreatedBy);
                }

                db.SaveChanges();
                if (generalizationViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var translation = db.GeneralizationTranslations.FirstOrDefault(r =>
                        r.LanguageId == generalizationViewModel.LanguageId &&
                        r.GeneralizationId == generalizationViewModel.Id);
                    if (translation != null)
                    {
                        translation.Title = generalizationViewModel.Title;
                        translation.Description = generalizationViewModel.Description;
                        db.Entry(translation).State = EntityState.Modified;
                    }
                    else
                    {
                        var generalizationTran = new GeneralizationTranslation()
                        {
                            Title = generalizationViewModel.Title,
                            Description = generalizationViewModel.Description,
                            LanguageId = generalizationViewModel.LanguageId,
                            GeneralizationId = generalizationViewModel.Id
                        };
                        db.GeneralizationTranslations.Add(generalizationTran);
                    }
                    db.SaveChanges();
                }
            }
        }

        public void DeleteGeneralization(Generalization generalization)
        {
            using (var db = new LearningManagementSystemContext())
            {
                generalization.Status = (int)GeneralEnums.StatusEnum.Deleted;
                generalization.DeletedOn = DateTime.Now;
                db.Entry(generalization).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IPagedList<GeneralizationEmployeeViewModel> GetGeneralizationEmployees(int languageId, int branchId, int jobId, int generalizationId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var users = db.Employees.Include(r => r.Contact.Branch.BranchTranslations).Include(r => r.Contact.Branch).Include(r => r.Contact)
                    .Include(r => r.Contact.ContactTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (branchId > 0)
                {
                    users = users.Where(a => a.Contact.BranchId == branchId);
                }
                if (jobId > 0)
                {
                    users = users.Where(a => a.JobId == jobId);
                }
                var employees = new List<int>();
                if (generalizationId > 0)
                {
                    employees = db.GeneralizationEmployees.Where(a => a.GeneralizationId == generalizationId).Select(a => a.ContactId.GetValueOrDefault(0)).ToList();
                }

                var pageSize = users.Count() + 1;
                var pageNumber = 1;
                var result = users;
                var output = result.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Contact.FullName = trans.FullName;
                        }
                        var transBranch = item.Contact?.Branch.BranchTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transBranch != null)
                        {
                            item.Contact.Branch.Name = transBranch.Name;
                        }
                    }
                }

                return output.Select(a => new GeneralizationEmployeeViewModel()
                {
                    UserBranch = a.Contact?.Branch?.Name,
                    UserBranchId = a.Contact?.BranchId ?? 0,
                    UserName = a.Contact?.FullName,
                    ContactId = a.ContactId,
                    UserJobId = a.JobId ?? 0,
                    Selected = employees.FirstOrDefault(b => b == a.ContactId) > 0,
                    UserJob = a.JobId == null ? "": LookupHelper.GetLookupDetailsById(a.JobId.GetValueOrDefault(0), languageId)?.Name,
                    UserGender = a.Contact?.GenderId == null ? "" : LookupHelper.GetLookupDetailsById(a.Contact.GenderId.GetValueOrDefault(0), languageId)?.Name
                }).OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
            }
        }

        public List<string> GetEmployees(int branchId, int jobId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var users = db.Employees.Include(r => r.Contact.Branch).Include(r => r.Contact)
                    .Include(r => r.Contact.ContactTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (branchId > 0)
                {
                    users = users.Where(a => a.Contact.BranchId == branchId);
                }

                if (jobId > 0)
                {
                    users = users.Where(a => a.JobId == jobId);
                }

                var employees = new List<string>();
                employees = users.Select(a => a.ContactId.ToString()).ToList();
                return employees;
            }
        }


        public List<GeneralizationEmployeeViewModel> GetGeneralizationEmployees(int generalizationId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var users = db.GeneralizationEmployees.Include(r => r.Contact.UserProfiles).Include(r => r.Generalization).Include(r => r.Contact.Branch).Include(r => r.Contact.Branch.BranchTranslations).Include(r => r.Contact).Include(r => r.Contact.ContactTranslations)
                    .Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.GeneralizationId == generalizationId);

                var result = users;
                var output = result.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Contact.FullName = trans.FullName;
                        }
                        var transBranch = item.Contact.Branch.BranchTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transBranch != null)
                        {
                            item.Contact.Branch.Name = transBranch.Name;
                        }
                    }
                }

                return output.Select(a => new GeneralizationEmployeeViewModel()
                {
                    UserBranch = a.Contact.Branch?.Name,
                    UserBranchId = a.Contact.BranchId ?? 0,
                    UserName = a.Contact.FullName,
                    ContactId = a.ContactId,
                    UserJob = LookupHelper.GetLookupDetailsById(a.Contact.UserProfiles.FirstOrDefault(b => b.ContactId == a.ContactId)?.JobId ?? 0, languageId)?.Name,
                    UserGender = LookupHelper.GetLookupDetailsById(a.Contact.GenderId.GetValueOrDefault(0), languageId)?.Name,
                }).OrderByDescending(r => r.Id).ToList();
            }
        }

        public bool ReadNotification(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var notification = db.Notifications.FirstOrDefault(a => a.Id == id);
                if (notification != null)
                {
                    notification.IsRead = true;
                    db.Entry(notification).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
