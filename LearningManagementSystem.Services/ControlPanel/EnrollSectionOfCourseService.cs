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
    public class EnrollSectionOfCourseService : IEnrollSectionOfCourseService
    {
        private readonly ISettingService _settingService;
        private readonly IEnrollLectureService _enrollLectureService;
        public EnrollSectionOfCourseService(ISettingService settingService, IEnrollLectureService enrollLectureService)
        {
            _settingService = settingService;
            _enrollLectureService = enrollLectureService;
        }

        public IPagedList<EnrollSectionOfCourse> GetEnrollSectionOfCourses(int? TeacherId = 0, int? studentId = 0, int? page = 0, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? EnrollTeacherCourseId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollSectionoffcourses = db.EnrollSectionOfCourses.Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollSectionOfCourseTranslations).Include(t => t.EnrollCourse.EnrollTeacherCourseTranlations).AsQueryable();

                if (studentId > 0)
                {
                    List<int> EnrollTeacherCourseIds = db.EnrollStudentCourses.Where(r => r.StudentId == studentId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.CourseId).ToList();
                    if (EnrollTeacherCourseIds.Count > 0)
                        EnrollSectionoffcourses = EnrollSectionoffcourses.Where(r => EnrollTeacherCourseIds.Contains(r.EnrollCourseId));
                    else
                        EnrollSectionoffcourses = EnrollSectionoffcourses.Where(r => r.EnrollCourseId == -2000);

                }

                if (TeacherId > 0)
                    EnrollSectionoffcourses = EnrollSectionoffcourses.Where(r => r.EnrollCourse.TeacherId == TeacherId);


                if (EnrollTeacherCourseId > 0)
                    EnrollSectionoffcourses = EnrollSectionoffcourses.Where(r => r.EnrollCourse.Id == EnrollTeacherCourseId);

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollSectionoffcourses;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.EnrollSectionOfCourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.SectionName = trans.SectionName;
                            item.Description = trans.Description;
                        }
                        var TransEnrollCourse = item.EnrollCourse.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (TransEnrollCourse != null)
                        {
                            item.EnrollCourse.CourseName = TransEnrollCourse.CourseName;
                        }
                    }
                }

                foreach (var item in output)
                {
                    item.EnrollLectures = _enrollLectureService.GetEnrollLectureBySectionId(item.Id, languageId);
                }

                return output;
            }
        }
        public EnrollSectionOfCourse GetEnrollSectionOfCourseById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var sectionoffcourse = db.EnrollSectionOfCourses.Include(r => r.EnrollCourse).FirstOrDefault(d => d.Id == id);
                return sectionoffcourse;
            }
        }
        public EnrollSectionOfCourse GetEnrollSectionOfCourseById_WithoutUsing(int id, LearningManagementSystemContext db)
        {

            var sectionoffcourse = db.EnrollSectionOfCourses.Include(r => r.EnrollCourse).FirstOrDefault(d => d.Id == id);
            return sectionoffcourse;

        }

        public EnrollSectionOfCourseViewModel GetEnrollSectionOfCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.EnrollSectionOfCourseTranslations.Include(r => r.EnrollSection).ThenInclude(d => d.EnrollCourse).FirstOrDefault(r => r.LanguageId == languageId && r.EnrollSectionId == id);
                    if (aboutTran != null)
                    {
                        return new EnrollSectionOfCourseViewModel(aboutTran);
                    }
                }
                var sectionoffcourse = db.EnrollSectionOfCourses.Include(r => r.EnrollCourse).FirstOrDefault(d => d.Id == id);
                return new EnrollSectionOfCourseViewModel(sectionoffcourse);
            }
        }
        public EnrollSectionOfCourse AddEnrollSectionOfCourseForAdmin(SectionOfCourse sectionoffcourse, int EnrollCourseId, LearningManagementSystemContext db)
        {

            var AddedEnrollSectionOffCourse = new EnrollSectionOfCourse()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                SectionName = sectionoffcourse.SectionName,
                Description = sectionoffcourse.Description,
                EnrollCourseId = EnrollCourseId,
                CreatedBy = sectionoffcourse.CreatedBy,
            };
            db.EnrollSectionOfCourses.Add(AddedEnrollSectionOffCourse);
            db.SaveChanges();

            AddedEnrollSectionOffCourse.Id = AddedEnrollSectionOffCourse.Id;


            if (sectionoffcourse.SectionOfCourseTranslations != null)
            {
                foreach (var SectionOfCourseTranslations in sectionoffcourse.SectionOfCourseTranslations)
                {
                    var sectionoffcourseTran = new EnrollSectionOfCourseTranslation()
                    {
                        SectionName = SectionOfCourseTranslations.SectionName,
                        Description = SectionOfCourseTranslations.Description,
                        LanguageId = SectionOfCourseTranslations.LanguageId,
                        EnrollSectionId = AddedEnrollSectionOffCourse.Id
                    };
                    db.EnrollSectionOfCourseTranslations.Add(sectionoffcourseTran);
                    db.SaveChanges();
                }
            }

            return AddedEnrollSectionOffCourse;

        }

        public EnrollSectionOfCourse AddEnrollSectionOfCourse(EnrollSectionOfCourseViewModel sectionoffcourseViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var sectionoffcourse = new EnrollSectionOfCourse()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    SectionName = sectionoffcourseViewModel.SectionName,
                    Description = sectionoffcourseViewModel.Description,
                    EnrollCourseId = sectionoffcourseViewModel.EnrollCourseId,
                    CreatedBy = sectionoffcourseViewModel.CreatedBy,
                };
                db.EnrollSectionOfCourses.Add(sectionoffcourse);
                db.SaveChanges();

                sectionoffcourse.Id = sectionoffcourse.Id;

                if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var sectionoffcourseTran = new EnrollSectionOfCourseTranslation()
                    {
                        SectionName = sectionoffcourseViewModel.SectionName,
                        Description = sectionoffcourseViewModel.Description,
                        LanguageId = sectionoffcourseViewModel.LanguageId,
                        EnrollSectionId = sectionoffcourse.Id
                    };
                    db.EnrollSectionOfCourseTranslations.Add(sectionoffcourseTran);
                    db.SaveChanges();
                }
                return sectionoffcourse;
            }
        }
        public EnrollSectionOfCourse AddEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, LearningManagementSystemContext db)
        {

            var sectionoffcourse = new EnrollSectionOfCourse()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                SectionName = sectionoffcourseViewModel.SectionName,
                Description = sectionoffcourseViewModel.Description,
                EnrollCourseId = sectionoffcourseViewModel.EnrollCourseId,
                CreatedBy = sectionoffcourseViewModel.CreatedBy,
            };
            db.EnrollSectionOfCourses.Add(sectionoffcourse);
            db.SaveChanges();

            sectionoffcourse.Id = sectionoffcourse.Id;

            if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var sectionoffcourseTran = new EnrollSectionOfCourseTranslation()
                {
                    SectionName = sectionoffcourseViewModel.SectionName,
                    Description = sectionoffcourseViewModel.Description,
                    LanguageId = sectionoffcourseViewModel.LanguageId,
                    EnrollSectionId = sectionoffcourse.Id
                };
                db.EnrollSectionOfCourseTranslations.Add(sectionoffcourseTran);
                db.SaveChanges();
            }
            return sectionoffcourse;

        }
        public void EditEnrollSectionOfCourse(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, EnrollSectionOfCourse sectionoffcourse)
        {
            using (var db = new LearningManagementSystemContext())
            {

                sectionoffcourse.EnrollCourseId = sectionoffcourseViewModel.EnrollCourseId;
                if (sectionoffcourseViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    sectionoffcourse.SectionName = sectionoffcourseViewModel.SectionName;
                    sectionoffcourse.Description = sectionoffcourseViewModel.Description;
                }

                db.Entry(sectionoffcourse).State = EntityState.Modified;
                db.SaveChanges();
                if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var sectionoffcourseTranslation = db.SectionOfCourseTranslations.FirstOrDefault(r =>
                        r.LanguageId == sectionoffcourseViewModel.LanguageId &&
                        r.SectionId == sectionoffcourseViewModel.Id);
                    if (sectionoffcourseTranslation != null)
                    {
                        sectionoffcourseTranslation.SectionName = sectionoffcourseViewModel.SectionName;
                        sectionoffcourseTranslation.Description = sectionoffcourseViewModel.Description;
                        db.Entry(sectionoffcourseTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var sectionoffcourseTran = new EnrollSectionOfCourseTranslation()
                        {
                            SectionName = sectionoffcourseViewModel.SectionName,
                            Description = sectionoffcourseViewModel.Description,
                            LanguageId = sectionoffcourseViewModel.LanguageId,
                            EnrollSectionId = sectionoffcourseViewModel.Id
                        };
                        db.EnrollSectionOfCourseTranslations.Add(sectionoffcourseTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void EditEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, EnrollSectionOfCourse sectionoffcourse, LearningManagementSystemContext db)
        {


            sectionoffcourse.EnrollCourseId = sectionoffcourseViewModel.EnrollCourseId;
            if (sectionoffcourseViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                sectionoffcourse.SectionName = sectionoffcourseViewModel.SectionName;
                sectionoffcourse.Description = sectionoffcourseViewModel.Description;
            }

            db.Entry(sectionoffcourse).State = EntityState.Modified;
            db.SaveChanges();
            if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var sectionoffcourseTranslation = db.SectionOfCourseTranslations.FirstOrDefault(r =>
                    r.LanguageId == sectionoffcourseViewModel.LanguageId &&
                    r.SectionId == sectionoffcourseViewModel.Id);
                if (sectionoffcourseTranslation != null)
                {
                    sectionoffcourseTranslation.SectionName = sectionoffcourseViewModel.SectionName;
                    sectionoffcourseTranslation.Description = sectionoffcourseViewModel.Description;
                    db.Entry(sectionoffcourseTranslation).State = EntityState.Modified;
                }
                else
                {
                    var sectionoffcourseTran = new EnrollSectionOfCourseTranslation()
                    {
                        SectionName = sectionoffcourseViewModel.SectionName,
                        Description = sectionoffcourseViewModel.Description,
                        LanguageId = sectionoffcourseViewModel.LanguageId,
                        EnrollSectionId = sectionoffcourseViewModel.Id
                    };
                    db.EnrollSectionOfCourseTranslations.Add(sectionoffcourseTran);
                }

                db.SaveChanges();
            }


        }
        public List<EnrollSectionOfCourse> GetEnrollSectionByEnrollTeacherCourseId(int EnrollTeacherCourseId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollSections = db.EnrollSectionOfCourses.Where(r => r.EnrollCourseId == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var EnrollSection in EnrollSections)
                    {
                        var trans = db.EnrollSectionOfCourseTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollSectionId == EnrollSection.Id);
                        if (trans != null)
                        {
                            EnrollSection.SectionName = trans.SectionName;
                            EnrollSection.Description = trans.Description;
                        }
                    }

                foreach (var item in EnrollSections)
                    item.EnrollLectures = _enrollLectureService.GetEnrollLectureBySectionId(item.Id, languageId);

                return EnrollSections;
            }
        }

        public void DeleteEnrollSectionOfCourse(EnrollSectionOfCourse sectionoffcourse)
        {
            using (var db = new LearningManagementSystemContext())
            {
                sectionoffcourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
                sectionoffcourse.DeletedOn = DateTime.Now;
                db.Entry(sectionoffcourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourse sectionoffcourse, LearningManagementSystemContext db)
        {

            sectionoffcourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
            sectionoffcourse.DeletedOn = DateTime.Now;
            db.Entry(sectionoffcourse).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void DeleteEnrollSectionByEnrollTeacherCourseID(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollSections = db.EnrollSectionOfCourses.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                foreach (var EnrollSection in EnrollSections)
                {
                    var EnrollSectionOfCourseTranslation = db.EnrollSectionOfCourseTranslations.Where(e => e.EnrollSectionId == EnrollSection.Id).ToList();
                    db.EnrollSectionOfCourseTranslations.RemoveRange(EnrollSectionOfCourseTranslation);
                }
                db.EnrollSectionOfCourses.RemoveRange(EnrollSections);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollSectionByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db)
        {

            var EnrollSections = db.EnrollSectionOfCourses.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
            foreach (var EnrollSection in EnrollSections)
            {
                var EnrollSectionOfCourseTranslation = db.EnrollSectionOfCourseTranslations.Where(e => e.EnrollSectionId == EnrollSection.Id).ToList();
                db.EnrollSectionOfCourseTranslations.RemoveRange(EnrollSectionOfCourseTranslation);
            }
            db.EnrollSectionOfCourses.RemoveRange(EnrollSections);
            db.SaveChanges();


        }

        public void FetchCoursesContent(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var course = db.EnrollTeacherCourses.Include(r => r.Course.SectionOfCourses).FirstOrDefault(r => r.Id == id && r.Status == (int)GeneralEnums.StatusEnum.Active && r.Course.Status == (int)GeneralEnums.StatusEnum.Active);

                foreach (var item in course.Course.SectionOfCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active))
                {
                    var enrol = db.EnrollSectionOfCourses.FirstOrDefault(r => r.EnrollCourseId == id && r.SectionName == item.SectionName && r.Status == (int)GeneralEnums.StatusEnum.Active);
                    if (enrol == null)
                    {
                        var tran = db.EnrollSectionOfCourseTranslations.Where(r => r.EnrollSectionId == item.Id);
                        var lectuers = db.Lectures.Where(r => r.SectionId == item.Id).Include(r => r.CourseResources.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active)).Include(r => r.LectureTranslations).Select(r =>
                            new EnrollLecture()
                            {
                                LectureName = r.LectureName,
                                Description = r.Description,
                                CreatedBy = r.CreatedBy,
                                CreatedOn = r.CreatedOn,
                                EnrollCourseId = id,
                                Status = r.Status,
                                EnrollCourseResources = r.CourseResources.Select(s => new EnrollCourseResource()
                                {
                                    CreatedBy = s.CreatedBy,
                                    CreatedOn = s.CreatedOn,
                                    Description = s.Description,
                                    Link = s.Link,
                                    Name = s.Name,
                                    Status = s.Status,
                                    Type = s.Type,
                                    DeletedOn = s.DeletedOn
                                ,
                                    EnrollCourseResourceTranslations = db.CourseResourceTranslations.Where(d => d.CourseResourceId == s.Id).Select(y => new EnrollCourseResourceTranslation() { LanguageId = y.LanguageId, Name = y.Name, Description = y.Description }).ToList()
                                }).ToList(),
                                EnrollLectureTranslations = r.LectureTranslations.Select(s => new EnrollLectureTranslation() { LectureName = s.LectureName, Description = s.Description, LanguageId = s.LanguageId }).ToList()
                            }).ToList();

                        db.EnrollSectionOfCourses.Add(new EnrollSectionOfCourse()
                        {
                            SectionName = item.SectionName,
                            EnrollCourseId = id,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            Description = item.Description,
                            EnrollSectionOfCourseTranslations = tran.Select(r => new EnrollSectionOfCourseTranslation() { LanguageId = r.LanguageId, Description = r.Description, SectionName = r.SectionName }).ToList(),
                            EnrollLectures = lectuers,
                            Status = item.Status,
                        });
                    }
                }
                db.SaveChanges();
            }
        }

    }
}
