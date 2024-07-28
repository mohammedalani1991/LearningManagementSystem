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
    public class SectionOfCourseService : ISectionOfCourseService
    {
        private readonly ISettingService _settingService;
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;
        public SectionOfCourseService(ISettingService settingService, ILectureService lectureService, ICourseService courseService)
        {
            _settingService = settingService;
            _lectureService = lectureService;
            _courseService = courseService;
        }
        public IPagedList<SectionOfCourse> GetSectionOfCourses(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25,int? CourseId=0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var sectionoffcourses = db.SectionOfCourses.Include(r => r.SectionOfCourseTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    sectionoffcourses = sectionoffcourses.Where(r => r.SectionName.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        sectionoffcourses = sectionoffcourses.Where(r => r.SectionName.Contains(searchText));
                    }
                    else
                    {
                        sectionoffcourses = sectionoffcourses.Where(r => r.SectionOfCourseTranslations.Any(t => t.SectionName.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (CourseId > 0)
                {
                    sectionoffcourses = sectionoffcourses.Where(r => r.CourseId == CourseId);
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = sectionoffcourses;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.SectionOfCourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.SectionName = trans.SectionName;
                            item.Description = trans.Description;
                        }
                    }
                }
               
                foreach (var item in output)
                {
                    
                    var trans = _courseService.GetCourseById(item.CourseId, languageId);
                    item.Course = new Course();
                    item.Lectures = _lectureService.GetLectureBySectionId(item.Id, languageId);
                    if (trans != null && trans.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        item.Course.CourseName = trans.CourseName;
                
                    }
                    else
                    {
                        item.Course.CourseName = "--";
                    }
                }

                return output;
            }
        }
        public SectionOfCourse GetSectionOfCourseById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var sectionoffcourse = db.SectionOfCourses.Include(d => d.Course).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return sectionoffcourse;
            }
        }
        public SectionOfCourse GetSectionOfCourseById_WithoutUsing(int id, LearningManagementSystemContext db)
        {
                var sectionoffcourse = db.SectionOfCourses.Include(d => d.Course).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return sectionoffcourse;
        }
        public SectionOfCourseViewModel GetSectionOfCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.SectionOfCourseTranslations.Include(r => r.Section).ThenInclude(d=>d.Course).FirstOrDefault(r => r.LanguageId == languageId && r.SectionId == id);
                    if (aboutTran != null)
                    {
                        return new SectionOfCourseViewModel(aboutTran);
                    }
                }
                var sectionoffcourse = db.SectionOfCourses.Include(r => r.Course).FirstOrDefault(d => d.Id == id);
                return new SectionOfCourseViewModel(sectionoffcourse);
            }
        }
        public List<SectionOfCourse> GetSectionOfCourseByCourseId(int Courseid)
        {
            using (var db = new LearningManagementSystemContext())
            {
               
                var sectionoffcourse = db.SectionOfCourses.Include(r => r.SectionOfCourseTranslations).Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return sectionoffcourse.ToList();
            }
        }
        public List<SectionOfCourse> GetSectionOfCourseByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db)
        {
                var sectionoffcourse = db.SectionOfCourses.Include(r => r.SectionOfCourseTranslations).Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return sectionoffcourse.ToList();
        }
        public List<SectionOfCourseViewModel> GetSectionOfCourseByCourseId(int Courseid, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.SectionOfCourseTranslations.Include(r => r.Section).ThenInclude(d => d.Course).Where(r => r.LanguageId == languageId && r.Section.CourseId == Courseid && r.Section.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r=> new SectionOfCourseViewModel(r));
                    if (aboutTran != null && aboutTran.Count() != 0)
                    {
                        return aboutTran.ToList();
                    }
                }
                var sectionoffcourse = db.SectionOfCourses.Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new SectionOfCourseViewModel(r));
                return sectionoffcourse.ToList();
            }
        }
        public SectionOfCourse AddSectionOfCourse(SectionOfCourseViewModel sectionoffcourseViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var sectionoffcourse = new SectionOfCourse()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    SectionName = sectionoffcourseViewModel.SectionName,
                    Description = sectionoffcourseViewModel.Description,
                    CourseId = sectionoffcourseViewModel.CourseId,
                    CreatedBy = sectionoffcourseViewModel.CreatedBy,
                };
                db.SectionOfCourses.Add(sectionoffcourse);
                db.SaveChanges();

                sectionoffcourse.Id = sectionoffcourse.Id;

                if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var sectionoffcourseTran = new SectionOfCourseTranslation()
                    {
                        SectionName = sectionoffcourseViewModel.SectionName,
                        Description =sectionoffcourseViewModel.Description,
                        LanguageId = sectionoffcourseViewModel.LanguageId,
                        SectionId = sectionoffcourse.Id
                    };
                    db.SectionOfCourseTranslations.Add(sectionoffcourseTran);
                    db.SaveChanges();
                }
                return sectionoffcourse;
            }
        }
        public SectionOfCourse AddSectionOfCourse_WithoutUsing(SectionOfCourseViewModel sectionoffcourseViewModel, LearningManagementSystemContext db)
        {
          
                var sectionoffcourse = new SectionOfCourse()
                {
                    CreatedOn = DateTime.Now,
                    Status = sectionoffcourseViewModel.Status,
                    SectionName = sectionoffcourseViewModel.SectionName,
                    Description = sectionoffcourseViewModel.Description,
                    CourseId = sectionoffcourseViewModel.CourseId,
                    CreatedBy = sectionoffcourseViewModel.CreatedBy,
                };
                db.SectionOfCourses.Add(sectionoffcourse);
                db.SaveChanges();

                sectionoffcourse.Id = sectionoffcourse.Id;

                if (sectionoffcourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var sectionoffcourseTran = new SectionOfCourseTranslation()
                    {
                        SectionName = sectionoffcourseViewModel.SectionName,
                        Description = sectionoffcourseViewModel.Description,
                        LanguageId = sectionoffcourseViewModel.LanguageId,
                        SectionId = sectionoffcourse.Id
                    };
                    db.SectionOfCourseTranslations.Add(sectionoffcourseTran);
                    db.SaveChanges();
                }
                return sectionoffcourse;
           
        }

        public void EditSectionOfCourse(SectionOfCourseViewModel sectionoffcourseViewModel, SectionOfCourse sectionoffcourse)
        {
            using (var db = new LearningManagementSystemContext())
            {
               
                sectionoffcourse.CourseId = sectionoffcourseViewModel.CourseId;
                sectionoffcourse.Status = sectionoffcourseViewModel.Status;

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
                        var sectionoffcourseTran = new SectionOfCourseTranslation()
                        {
                            SectionName = sectionoffcourseViewModel.SectionName,
                            Description = sectionoffcourseViewModel.Description,
                            LanguageId = sectionoffcourseViewModel.LanguageId,
                            SectionId = sectionoffcourseViewModel.Id
                        };
                        db.SectionOfCourseTranslations.Add(sectionoffcourseTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void EditSectionOfCourse_WithoutUsing(SectionOfCourseViewModel sectionoffcourseViewModel, SectionOfCourse sectionoffcourse, LearningManagementSystemContext db)
        {
           

                sectionoffcourse.CourseId = sectionoffcourseViewModel.CourseId;
                sectionoffcourse.Status = sectionoffcourseViewModel.Status;
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
                        var sectionoffcourseTran = new SectionOfCourseTranslation()
                        {
                            SectionName = sectionoffcourseViewModel.SectionName,
                            Description = sectionoffcourseViewModel.Description,
                            LanguageId = sectionoffcourseViewModel.LanguageId,
                            SectionId = sectionoffcourseViewModel.Id
                        };
                        db.SectionOfCourseTranslations.Add(sectionoffcourseTran);
                    }

                    db.SaveChanges();
                }

          
        }

        public void DeleteSectionOfCourse(SectionOfCourse sectionoffcourse, bool WithoutUsing = false, LearningManagementSystemContext context = null)
        {
            if (!WithoutUsing)
            {
                using (var db =  new LearningManagementSystemContext())
                {
                    Actions(db);
                }
            }
            else
            {
                Actions(context);
            }

            void Actions(LearningManagementSystemContext db)
            {
                sectionoffcourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
                sectionoffcourse.DeletedOn = DateTime.Now;
                db.Entry(sectionoffcourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
