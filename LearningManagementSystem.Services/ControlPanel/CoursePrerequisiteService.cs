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
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CoursePrerequisiteService : ICoursePrerequisiteService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        public CoursePrerequisiteService(ISettingService settingService, ICourseService courseService)
        {
            _settingService = settingService;
            _courseService = courseService;
        }
        public async Task<IPagedList<Course>> GetCoursePrerequisites(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? CourseId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var courses = db.Courses.Include(r => r.CourseTranslations).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                var CoursePrerequisites = db.CoursePrerequisites.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.CourseId);
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    courses = courses.Where(r => r.CourseName.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        courses = courses.Where(r => r.CourseName.Contains(searchText));
                    }
                    else
                    {
                        courses = courses.Where(r => r.CourseTranslations.Any(t => t.CourseName.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (CourseId > 0)
                {
                    courses = courses.Where(r => r.Id == CourseId);
                }

                courses = courses.Where(r => CoursePrerequisites.Contains(r.Id));
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = await courses.ToListAsync();

                foreach (var item in result)
                {
                    item.CoursePrerequisiteCourses = await db.CoursePrerequisites.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted & r.CourseId == item.Id).ToListAsync();
                }

                var output = result.OrderByDescending(r => r.CoursePrerequisiteCourses.LastOrDefault().CreatedOn).ToPagedList(pageNumber, pageSize);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.CourseName = trans.CourseName;
                        }

                    }
                }
                return output;
            }

        }
        public CoursePrerequisite GetCoursePrerequisiteById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursePrerequisite = db.CoursePrerequisites.Include(d => d.Course).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return coursePrerequisite;
            }
        }
        public CoursePrerequisite GetCoursePrerequisiteById_WithoutUsing(int id, LearningManagementSystemContext db)
        {

            var coursePrerequisite = db.CoursePrerequisites.Include(d => d.Course).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            return coursePrerequisite;

        }
        public List<CoursePrerequisite> GetCoursePrerequisiteByCourseId(int Courseid)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursePrerequisite = db.CoursePrerequisites.Where(r => r.CourseId == Courseid && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(d => d.Course).Include(d => d.PrerequisiteCourse).ToList();
                return coursePrerequisite;
            }
        }
        public List<CoursePrerequisite> GetCoursePrerequisiteByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db)
        {

            var coursePrerequisite = db.CoursePrerequisites.Include(d => d.Course).Include(d => d.PrerequisiteCourse).Where(r => r.CourseId == Courseid && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
            return coursePrerequisite;

        }
        public List<CoursePrerequisite> GetCoursePrerequisiteByCourseId(int Courseid, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursePrerequisite = db.CoursePrerequisites.Include(d => d.Course).Include(d => d.PrerequisiteCourse).Where(r => r.CourseId == Courseid && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in coursePrerequisite)
                    {
                        var PrerequisiteCourseName = db.CourseTranslations.Where(r => r.CourseId == item.PrerequisiteCourseId & r.LanguageId == languageId).Select(r => r.CourseName).FirstOrDefault();
                        var CourseName = db.CourseTranslations.Where(r => r.CourseId == item.CourseId & r.LanguageId == languageId).Select(r => r.CourseName).FirstOrDefault();
                        if (!string.IsNullOrEmpty(PrerequisiteCourseName))
                        {
                            item.PrerequisiteCourse.CourseName = PrerequisiteCourseName;
                        }
                        if (!string.IsNullOrEmpty(CourseName))
                        {
                            item.Course.CourseName = CourseName;
                        }
                    }
                }
                return coursePrerequisite;
            }
        }
        public List<CoursePrerequisiteViewModel> GetViewModeCoursePrerequisiteByCourseId(int Courseid, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursePrerequisite = db.CoursePrerequisites.Include(d => d.Course).Include(d => d.PrerequisiteCourse).Where(r => r.CourseId == Courseid && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in coursePrerequisite)
                    {
                        var PrerequisiteCourseName = db.CourseTranslations.Where(r => r.CourseId == item.PrerequisiteCourseId & r.LanguageId == languageId).Select(r => r.CourseName).FirstOrDefault();
                        var PrerequisiteCourseNotes = db.CourseTranslations.Where(r => r.CourseId == item.PrerequisiteCourseId & r.LanguageId == languageId).Select(r => r.Notes).FirstOrDefault();
                        var CourseName = db.CourseTranslations.Where(r => r.CourseId == item.CourseId & r.LanguageId == languageId).Select(r => r.CourseName).FirstOrDefault();
                        if (!string.IsNullOrEmpty(PrerequisiteCourseName))
                        {
                            item.PrerequisiteCourse.CourseName = PrerequisiteCourseName;
                        }
                        if (!string.IsNullOrEmpty(CourseName))
                        {
                            item.Course.CourseName = CourseName;
                        }
                        if (!string.IsNullOrEmpty(PrerequisiteCourseNotes))
                        {
                            item.PrerequisiteCourse.Notes = PrerequisiteCourseNotes;
                        }
                    }
                }
                return coursePrerequisite.Select(r => new CoursePrerequisiteViewModel(r)).ToList();
            }
        }
        public void AddCoursePrerequisite(CoursePrerequisiteViewModel coursePrerequisiteViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var GetcoursePrerequisite = db.CoursePrerequisites.FirstOrDefault(r => r.CourseId == coursePrerequisiteViewModel.CourseId && r.PrerequisiteCourseId == coursePrerequisiteViewModel.PrerequisiteCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (GetcoursePrerequisite == null)
                {
                    var coursePrerequisite = new CoursePrerequisite()
                    {
                        CreatedOn = DateTime.Now,
                        Status = coursePrerequisiteViewModel.Status,
                        PrerequisiteCourseId = coursePrerequisiteViewModel.PrerequisiteCourseId,
                        CourseId = coursePrerequisiteViewModel.CourseId,
                        CreatedBy = coursePrerequisiteViewModel.CreatedBy,
                    };

                    db.CoursePrerequisites.Add(coursePrerequisite);
                    db.SaveChanges();
                }
            }
        }
        public void AddCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel coursePrerequisiteViewModel, LearningManagementSystemContext db)
        {

            var GetcoursePrerequisite = db.CoursePrerequisites.FirstOrDefault(r => r.CourseId == coursePrerequisiteViewModel.CourseId && r.PrerequisiteCourseId == coursePrerequisiteViewModel.PrerequisiteCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (GetcoursePrerequisite == null)
            {
                var coursePrerequisite = new CoursePrerequisite()
                {
                    CreatedOn = DateTime.Now,
                    Status = coursePrerequisiteViewModel.Status,
                    PrerequisiteCourseId = coursePrerequisiteViewModel.PrerequisiteCourseId,
                    CourseId = coursePrerequisiteViewModel.CourseId,
                    CreatedBy = coursePrerequisiteViewModel.CreatedBy,
                };

                db.CoursePrerequisites.Add(coursePrerequisite);
                db.SaveChanges();
            }

        }

        public void EditCoursePrerequisite(CoursePrerequisiteViewModel coursePrerequisiteViewModel, CoursePrerequisite coursePrerequisite)
        {
            using (var db = new LearningManagementSystemContext())
            {

                coursePrerequisite.PrerequisiteCourseId = coursePrerequisiteViewModel.PrerequisiteCourseId;
                coursePrerequisite.CourseId = coursePrerequisiteViewModel.CourseId;
                coursePrerequisite.Status = coursePrerequisiteViewModel.Status;
                db.Entry(coursePrerequisite).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void EditCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel coursePrerequisiteViewModel, CoursePrerequisite coursePrerequisite, LearningManagementSystemContext db)
        {
            coursePrerequisite.PrerequisiteCourseId = coursePrerequisiteViewModel.PrerequisiteCourseId;
            coursePrerequisite.CourseId = coursePrerequisiteViewModel.CourseId;
            coursePrerequisite.Status = coursePrerequisiteViewModel.Status;
            db.Entry(coursePrerequisite).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCoursePrerequisite(CoursePrerequisite coursePrerequisite)
        {
            using (var db = new LearningManagementSystemContext())
            {
                coursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
                coursePrerequisite.DeletedOn = DateTime.Now;
                db.Entry(coursePrerequisite).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCoursePrerequisiteByCourseId(int CourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePrerequisites = db.CoursePrerequisites.Where(e => e.CourseId == CourseId);
                foreach (var CoursePrerequisite in CoursePrerequisites)
                {
                    CoursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CoursePrerequisite.DeletedOn = DateTime.Now;
                    CoursePrerequisite.CourseId = CourseId;
                    db.Entry(CoursePrerequisite).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public void DeleteCoursePrerequisiteByPrerequisiteCourseId(int PrerequisiteCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePrerequisites = db.CoursePrerequisites.Where(e => e.PrerequisiteCourseId == PrerequisiteCourseId);
                foreach (var CoursePrerequisite in CoursePrerequisites)
                {
                    CoursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CoursePrerequisite.DeletedOn = DateTime.Now;
                    CoursePrerequisite.PrerequisiteCourseId = PrerequisiteCourseId;
                    db.Entry(CoursePrerequisite).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        public void DeleteCoursePrerequisites(List<CoursePrerequisite> CoursePrerequisites)
        {
            using (var db = new LearningManagementSystemContext())
            {
                foreach (var CoursePrerequisite in CoursePrerequisites)
                {
                    CoursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CoursePrerequisite.DeletedOn = DateTime.Now;
                    db.Entry(CoursePrerequisite).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public void DeleteCoursePrerequisite_WithoutUsing(CoursePrerequisite coursePrerequisite, LearningManagementSystemContext db)
        {

            coursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
            coursePrerequisite.DeletedOn = DateTime.Now;
            db.Entry(coursePrerequisite).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void DeleteCoursePrerequisiteByCourseId_WithoutUsing(int CourseId, LearningManagementSystemContext db)
        {

            var CoursePrerequisites = db.CoursePrerequisites.Where(e => e.CourseId == CourseId);
            foreach (var CoursePrerequisite in CoursePrerequisites)
            {
                CoursePrerequisite.Status = (int)GeneralEnums.StatusEnum.Deleted;
                CoursePrerequisite.DeletedOn = DateTime.Now;
                CoursePrerequisite.CourseId = CourseId;
                db.Entry(CoursePrerequisite).State = EntityState.Modified;
            }
            db.SaveChanges();

        }
    }
}
