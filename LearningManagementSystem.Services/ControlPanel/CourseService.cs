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
using System.Net;
using static LearningManagementSystem.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CourseService : ICourseService
    {
        private readonly ISettingService _settingService;
        private readonly ICookieService _cookieService;
        private readonly ICurrencyService _currencyService;

        public CourseService(ISettingService settingService, ICurrencyService currencyService, ICookieService cookieService)
        {
            _settingService = settingService;
            _cookieService = cookieService;
            _currencyService = currencyService;
        }

        public List<Course> GetCoursesList(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Include(r => r.CourseTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in courses)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.CourseName = trans.CourseName;
                    }

                return courses.ToList();
            }
        }

        public List<Course> GetCoursesList(int semesterId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.SemesterId == semesterId).Include(r => r.Course.CourseTranslations).AsQueryable();
       
                var distinctCourses = courses.Select(r => r.Course).ToList().GroupBy(r => r.Id).Select(g => g.FirstOrDefault()).ToList();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in distinctCourses)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.CourseName = trans.CourseName;
                    }

                return distinctCourses;
            }
        }

        public IPagedList<Course> GetCourses(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Include(r => r.CourseTranslations).Include(d => d.Category).ThenInclude(d => d.CourseCategoryTranslations).Where(r =>
                        r.Status != (int)GeneralEnums.StatusEnum.Deleted);

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
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = courses;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.CourseName = trans.CourseName;
                            item.AcquiredSkills = trans.AcquiredSkills;
                            item.TargetGroup = trans.TargetGroup;
                            item.Notes = trans.Notes;
                            item.Requirements = trans.Requirements;


                        }

                        if (item.Category != null && item.Category.CourseCategoryTranslations != null)
                        {
                            var transCategory = item.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCategory != null)
                            {
                                item.Category.Name = transCategory.Name;
                            }
                        }
                    }
                }
                return output;
            }
        }


        public IPagedList<CourseViewModel> GetCourses(bool showInHome, int? page, int languageId, int pagination = 25)
        {
            var pageSize = pagination;
            var pageNumber = (page ?? 1);

            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var courses =
                        db.CourseTranslations.Include(r => r.Course).ThenInclude(d => d.Category).ThenInclude(r => r.CourseCategoryTranslations).Where(r => r.Course.Status == (int)GeneralEnums.StatusEnum.Active
                         && r.LanguageId == languageId && (showInHome ? r.Course.ShowInHomePage == true : 1 == 1)).Select(r => new CourseViewModel(r)).ToPagedList(pageNumber, pageSize);
                    return courses;


                }
                else
                {
                    var courses =
                             db.Courses.Include(d => d.Category).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && (showInHome ? r.ShowInHomePage == true : 1 == 1)
                             ).Select(r => new CourseViewModel(r)).ToPagedList(pageNumber, pageSize);

                    return courses;

                }
            }
        }
        public async Task<IPagedList<CourseViewModel>> GetCoursesForHomePgae(bool showInHome, int? page, int languageId, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Include(r => r.CourseTranslations).Include(d => d.Category).AsQueryable();

                if (showInHome)
                    courses = courses.Where(r => r.ShowInHomePage == true);

                foreach (var item in courses)
                    item.CoursePrice = _currencyService.GetValue(item.CoursePrice ?? 0);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in courses)
                    {
                        var tran = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (tran != null)
                        {
                            item.CourseName = tran.CourseName;
                            item.QuestionDescription = tran.QuestionDescription;
                        }
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);

                return await courses.Select(r => new CourseViewModel(r)).ToPagedListAsync(pageNumber, pageSize);
            }
        }

        public Course GetCourseById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var course = db.Courses.Include(r => r.CourseTranslations).Include(r => r.Category).Include(r => r.CourseCertifications).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
                return course;
            }
        }
        public Course GetCourseById_WithoutUsing(int id, LearningManagementSystemContext db)
        {

            var course = db.Courses.Include(r => r.CourseTranslations).Include(r => r.Category).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
            return course;

        }
        public CourseViewModel GetCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var courseTranslation = db.CourseTranslations.Include(r => r.Course.Category.CourseCategoryTranslations).Include(d => d.Course.CourseCertifications)
                        .FirstOrDefault(r => r.Course.Status != (int)GeneralEnums.StatusEnum.Deleted && r.LanguageId == languageId && r.CourseId == id);
                    if (courseTranslation != null)
                    {
                        var courseTrans = new CourseViewModel(courseTranslation);
                        var CategoryName = courseTranslation.Course?.Category?.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId)?.Name ?? "";
                        if (string.IsNullOrEmpty(CategoryName))
                        {
                            CategoryName = courseTranslation.Course?.Category?.Name ?? "";
                        }
                        courseTrans.CategoryName = CategoryName;
                        courseTrans.CourseExchangePrice = _currencyService.GetValue(courseTrans.CoursePrice ?? 0);

                        return courseTrans;

                    }
                }
                var course = db.Courses.Include(r => r.Category).Include(r => r.CourseCertifications).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
                if (course != null)
                {
                    var CourseExchangePrice = _currencyService.GetValue(course.CoursePrice ?? 0);
                    return new CourseViewModel(course, CourseExchangePrice);
                }
                else
                    return null;
            }

        }

        public List<Course> GetCourseByIdAsList(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Include(r => r.CourseTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted & r.Id == id).ToList();

                var output = courses;
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


        public void AddCourse(CourseViewModel courseViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var course = new Course()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = courseViewModel.CreatedBy,
                    Status = courseViewModel.Status,
                    CourseName = courseViewModel.CourseName,
                    CourseDuration = courseViewModel.CourseDuration,
                    CoursePrice = courseViewModel.CoursePrice,
                    AcquiredSkills = courseViewModel.AcquiredSkills,
                    TargetGroup = courseViewModel.TargetGroup,
                    Notes = courseViewModel.Notes,
                    Requirements = courseViewModel.Requirements,
                    CategoryId = (courseViewModel.CategoryId == 0) ? null : courseViewModel.CategoryId,
                    LearningMethodId = courseViewModel.LearningMethodId,
                    ImageUrl = courseViewModel.ImageUrl,
                    ShowInHomePage = courseViewModel.ShowInHomePage,
                    QuestionDescription = courseViewModel.QuestionDescription,
                    NeedQuestion = courseViewModel.NeedQuestion,
                    SuccessMark = courseViewModel.SuccessMark,
                    AssignmentMark = courseViewModel.AssignmentMark,
                };
                db.Courses.Add(course);

                foreach (var item in courseViewModel.TemplateIds)
                    course.CourseCertifications.Add(new CourseCertification()
                    {
                        CreatedOn = DateTime.Now,
                        CreatedBy = courseViewModel.CreatedBy,
                        Status = courseViewModel.Status,
                        TemplateHtmlId = item,
                    });

                db.SaveChanges();

                course.Id = course.Id;

                if (courseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var courseTran = new CourseTranslation()
                    {
                        CourseName = courseViewModel.CourseName,
                        AcquiredSkills = courseViewModel.AcquiredSkills,
                        TargetGroup = courseViewModel.TargetGroup,
                        Notes = courseViewModel.Notes,
                        Requirements = courseViewModel.Requirements,
                        LanguageId = courseViewModel.LanguageId,
                        CourseId = course.Id,
                        QuestionDescription = courseViewModel.QuestionDescription
                    };
                    db.CourseTranslations.Add(courseTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCourse(CourseViewModel courseViewModel, Course course)
        {
            using (var db = new LearningManagementSystemContext())
            {
                course.CourseDuration = courseViewModel.CourseDuration;
                course.CoursePrice = courseViewModel.CoursePrice;
                course.ImageUrl = courseViewModel.ImageUrl;
                course.ShowInHomePage = courseViewModel.ShowInHomePage;
                course.CategoryId = (courseViewModel.CategoryId == 0) ? null : courseViewModel.CategoryId;
                course.LearningMethodId = courseViewModel.LearningMethodId;
                course.NeedQuestion = courseViewModel.NeedQuestion;
                course.Status = courseViewModel.Status;
                course.SuccessMark = courseViewModel.SuccessMark;
                course.AssignmentMark = courseViewModel.AssignmentMark;

                if (courseViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    course.QuestionDescription = courseViewModel.QuestionDescription;
                    course.CourseName = courseViewModel.CourseName;
                    course.AcquiredSkills = courseViewModel.AcquiredSkills;
                    course.TargetGroup = courseViewModel.TargetGroup;
                    course.Notes = courseViewModel.Notes;
                    course.Requirements = courseViewModel.Requirements;
                }

                db.Entry(course).State = EntityState.Modified;
                db.CourseCertifications.RemoveRange(course.CourseCertifications);
                foreach (var item in courseViewModel.TemplateIds)
                    db.CourseCertifications.Add(new CourseCertification()
                    {
                        CreatedOn = DateTime.Now,
                        CreatedBy = course.CreatedBy,
                        Status = courseViewModel.Status,
                        TemplateHtmlId = item,
                        CourseId = course.Id,
                    });

                db.SaveChanges();
                if (courseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var courseTranslation = db.CourseTranslations.FirstOrDefault(r =>
                        r.LanguageId == courseViewModel.LanguageId &&
                        r.CourseId == courseViewModel.Id);
                    if (courseTranslation != null)
                    {
                        courseTranslation.QuestionDescription = courseViewModel.QuestionDescription;

                        courseTranslation.CourseName = courseViewModel.CourseName;
                        courseTranslation.AcquiredSkills = courseViewModel.AcquiredSkills;
                        courseTranslation.TargetGroup = courseViewModel.TargetGroup;
                        courseTranslation.Notes = courseViewModel.Notes;
                        courseTranslation.Requirements = courseViewModel.Requirements;
                        db.Entry(courseTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var courseTran = new CourseTranslation()
                        {
                            CourseName = courseViewModel.CourseName,
                            AcquiredSkills = courseViewModel.AcquiredSkills,
                            TargetGroup = courseViewModel.TargetGroup,
                            Notes = courseViewModel.Notes,
                            Requirements = courseViewModel.Requirements,
                            LanguageId = courseViewModel.LanguageId,
                            CourseId = courseViewModel.Id,
                            QuestionDescription = courseViewModel.QuestionDescription
                        };
                        db.CourseTranslations.Add(courseTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCourse(Course course)
        {
            using (var db = new LearningManagementSystemContext())
            {
                course.Status = (int)GeneralEnums.StatusEnum.Deleted;
                course.DeletedOn = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public List<Course> GetCoursesByName(string CourseName, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Include(r => r.CourseTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);


                if (languageId == CultureHelper.GetDefaultLanguageId())
                {
                    courses = courses.Where(r => r.CourseName.Contains(CourseName) && !string.IsNullOrEmpty(CourseName)).Take(20);
                }
                else
                {
                    courses = courses.Where(r => r.CourseTranslations.Any(t => t.CourseName.Contains(CourseName) & t.LanguageId == languageId && !string.IsNullOrEmpty(CourseName))).Take(20);
                }

                var result = courses;
                var output = result.OrderByDescending(r => r.Id).ToList();
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


        public IPagedList<CourseViewModel> GetActiveCoursesForGuest(bool? showInHome, string search, int? categoryId, int? trainer, int? page, int languageId, int pagination = 25)
        {
            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            using (var db = new LearningManagementSystemContext())
            {

                var courses = db.Courses.Include(r => r.CourseTranslations).Include(d => d.Category.CourseCategoryTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);

                if (showInHome.HasValue)
                {
                    courses = courses.Where(r => r.ShowInHomePage == true);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        courses = courses.Where(r => r.CourseTranslations.Any(g => g.CourseName.Contains(search)));
                    }
                    else
                    {

                        courses = courses.Where(r => r.CourseName.Contains(search));
                    }
                }


                if (categoryId != null && categoryId != 0)
                {
                    courses = courses.Where(r => r.CategoryId == categoryId);
                }


                var result = courses.ToPagedList(pageNumber, pageSize);

                foreach (var item in courses)
                    item.CoursePrice = _currencyService.GetValue(item.CoursePrice ?? 0);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in result)
                    {
                        var transCat = item.Category?.CourseCategoryTranslations?.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transCat != null)
                        {
                            item.Category.Name = transCat.Name;
                        }

                        var transCourse = item.CourseTranslations?.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transCourse != null)
                        {
                            item.CourseName = transCourse.CourseName;
                        }
                    }
                }

                return result.Select(r => new CourseViewModel(r));

            }

        }

        public Course GetCourseByEnrollTeacherCourseId(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var s = db.EnrollTeacherCourses.Include(r => r.Course).FirstOrDefault(r => r.Id == id);
                return s.Course;
            }
        }

        public CourseViewModel GetCourseByEnrollTeacherCourseId(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var course = db.EnrollTeacherCourses.Include(r => r.Course.CourseTranslations).FirstOrDefault(r => r.Id == id);
                var courseViewModel = new CourseViewModel(course.Course);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var transCorse = course.Course?.CourseTranslations?.FirstOrDefault(r => r.LanguageId == languageId);
                    if (transCorse != null)
                        courseViewModel = new CourseViewModel(transCorse);
                }
                courseViewModel.CourseExchangePrice = _currencyService.GetValue(courseViewModel.CoursePrice ?? 0);
                return courseViewModel;
            }
        }
    }
}