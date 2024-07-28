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
    public class SemesterService : ISemesterService
    {
        private readonly ISettingService _settingService;

        public SemesterService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<Semester> GetSemesters(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Semesters = db.Semesters.Include(r => r.SemesterTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                        Semesters = Semesters.Where(r => r.Name.Contains(searchText));
                    else
                        Semesters = Semesters.Where(r => r.SemesterTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = Semesters;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var trans = item.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }


                return output;
            }
        }
        public Semester GetSemesterById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var semester = db.Semesters.Find(id);
                return semester;
            }
        }

        public List<SemesterViewModel> GetSemesters(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var semesterTrans = db.SemesterTranslations.Include(r => r.Semester).Where(r => r.LanguageId == languageId && r.Semester.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new SemesterViewModel(r));
                    if (semesterTrans != null && semesterTrans.Count() != 0)
                    {
                        return semesterTrans.ToList();
                    }
                }
                var semester = db.Semesters.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new SemesterViewModel(r));
                return semester.ToList();
            }
        }
        public SemesterViewModel GetSemesterById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var semesterTrans = db.SemesterTranslations.Include(r => r.Semester).FirstOrDefault(r => r.LanguageId == languageId && r.SemesterId == id);
                    if (semesterTrans != null)
                    {
                        return new SemesterViewModel(semesterTrans);
                    }
                }
                var semester = db.Semesters.Find(id);
                return new SemesterViewModel(semester);
            }
        }
        public void AddSemester(SemesterViewModel semesterViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (semesterViewModel.Default == true)
                {
                    var semesters = db.Semesters.ToList();
                    foreach (var item in semesters)
                    {
                        item.Default = false;
                        db.Entry(item).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }

            using (var db = new LearningManagementSystemContext())
            {
                var semester = new Semester()
                {
                    CreatedOn = DateTime.Now,
                    Status = semesterViewModel.Status,
                    Name = semesterViewModel.Name,
                    Description = semesterViewModel.Description,
                    CreatedBy = semesterViewModel.CreatedBy,
                    PublicationDate = semesterViewModel.PublicationDate,
                    PublicationEndDate = semesterViewModel.PublicationEndDate,
                    WorkStartDate = semesterViewModel.WorkStartDate,
                    WorkEndDate = semesterViewModel.WorkEndDate,
                    Default = semesterViewModel.Default,

                };
                db.Semesters.Add(semester);
                db.SaveChanges();

                semester.Id = semester.Id;

                if (semesterViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var semesterTran = new SemesterTranslation()
                    {
                        Name = semesterViewModel.Name,
                        Description = semesterViewModel.Description,
                        LanguageId = semesterViewModel.LanguageId,
                        SemesterId = semester.Id,

                    };
                    db.SemesterTranslations.Add(semesterTran);
                    db.SaveChanges();
                }

            }
        }

        public void EditSemester(SemesterViewModel semesterViewModel, Semester semester)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (semesterViewModel.Default == true)
                {
                    var semesters = db.Semesters.ToList();
                    foreach (var item in semesters)
                    {
                        item.Default = false;
                        db.Entry(item).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }

            using (var db = new LearningManagementSystemContext())
            {
                semester.Status = semesterViewModel.Status;
                semester.PublicationDate = semesterViewModel.PublicationDate;
                semester.PublicationEndDate = semesterViewModel.PublicationEndDate;
                semester.WorkStartDate = semesterViewModel.WorkStartDate;
                semester.WorkEndDate = semesterViewModel.WorkEndDate;
                semester.Default = semesterViewModel.Default;

                if (semesterViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    semester.Name = semesterViewModel.Name;
                    semester.Description = semesterViewModel.Description;
                }

                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();

                if (semesterViewModel.Update)
                {
                    var courses = db.EnrollTeacherCourses.Where(r => r.SemesterId == semester.Id && r.Status == (int)GeneralEnums.StatusEnum.Active);
                    foreach (var course in courses)
                    {
                        course.PublicationDate = semesterViewModel.PublicationDate;
                        course.PublicationEndDate = semesterViewModel.PublicationEndDate;
                        course.WorkStartDate = semesterViewModel.WorkStartDate;
                        course.WorkEndDate = semesterViewModel.WorkEndDate;
                        db.Entry(course).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }

                if (semesterViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var semesterTranslation = db.SemesterTranslations.FirstOrDefault(r =>
                        r.LanguageId == semesterViewModel.LanguageId &&
                        r.SemesterId == semesterViewModel.Id);
                    if (semesterTranslation != null)
                    {
                        semesterTranslation.Name = semesterViewModel.Name;
                        semesterTranslation.Description = semesterViewModel.Description;

                        db.Entry(semesterTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var semesterTran = new SemesterTranslation()
                        {
                            Name = semesterViewModel.Name,
                            Description = semesterViewModel.Description,
                            LanguageId = semesterViewModel.LanguageId,
                            SemesterId = semesterViewModel.Id
                        };
                        db.SemesterTranslations.Add(semesterTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteSemester(Semester semester)
        {
            using (var db = new LearningManagementSystemContext())
            {
                semester.Status = (int)GeneralEnums.StatusEnum.Deleted;
                semester.DeletedOn = DateTime.Now;
                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Semester> GetSemestersList(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var semesters = db.Semesters.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.SemesterTranslations);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in semesters)
                    {
                        var trans = item.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Name = trans.Name;
                    }

                return semesters.ToList();
            }
        }

        public int GetDefaultSemester()
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.Semesters.FirstOrDefault(r => r.Default == true)?.Id ?? 0;
            }
        }
    }
}