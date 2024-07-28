using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CoursePrerequisiteController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
       
        public CoursePrerequisiteController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            ICoursePrerequisiteService coursePrerequisiteService
          
            ) 
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _coursePrerequisiteService = coursePrerequisiteService;
        }
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Prerequisite List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table,int? CourseId,string hdCourseName)
        {

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ViewBag.searchText = searchText;
            }
           
            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
                ViewBag.hdCourseName = hdCourseName;
            }
            else
            {
                ViewBag.CourseId = 0;
                ViewBag.hdCourseName = "";
            }

            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.CoursePrerequisitePagination);
            if (val == null && pagination == 0)
                pagination = 5;
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CoursePrerequisitePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "5");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "CourseId", "PrerequisiteCourseId", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CoursePrerequisiteTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePrerequisiteTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePrerequisiteTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            
            var result =await _coursePrerequisiteService.GetCoursePrerequisites(searchText, page, languageId, pagination, CourseId);
            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Course Prerequisite")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/CoursePrerequisite/Create
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Prerequisite Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/CoursePrerequisite/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Prerequisite Create Post")]
        public async Task<IActionResult> Create(
            CoursePrerequisiteGeneralViewModel CoursePrerequisiteGeneralViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses == null))
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses.Count == 0 || s.CourseId == 0))
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses.Exists(r => r.PrerequisiteCourseId == s.CourseId)))
                        return Content("AddMassegeErrorInvalidData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Course in CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel)
                                {
                                    foreach (var CoursePrerequisite in Course.PrerequisiteCourses)
                                    {
                                        var CoursePrerequisiteViewModel = new CoursePrerequisiteViewModel()
                                        {

                                            CreatedBy = User.Identity?.Name,
                                            CourseId = Course.CourseId,
                                            PrerequisiteCourseId = CoursePrerequisite.PrerequisiteCourseId,
                                            Status = CoursePrerequisiteGeneralViewModel.Status,
                                        };
                                        _coursePrerequisiteService.AddCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel, context);
                                       
                                    }
                                }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Prerequisite Dic");
                                return Content("Fail");
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Prerequisite Dic");
                    return View(CoursePrerequisiteGeneralViewModel);
                }
            }
            return View(CoursePrerequisiteGeneralViewModel);
        }

       
        // GET: ControlPanel/CoursePrerequisite/Edit/5
        [AuditLogFilter(ActionDescription = "Courses Prerequisite Edit Get")]
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? CourseId, int page, int languageId = 0)
        {
            if (CourseId == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            
            var coursePrerequisites = _coursePrerequisiteService.GetCoursePrerequisiteByCourseId(CourseId.Value, languageId);
            if (coursePrerequisites == null )
            {
                return NotFound();
            }
            var CoursePrerequisiteList = new CoursePrerequisiteGeneralViewModel() { Status = coursePrerequisites.FirstOrDefault().Status};
            CoursePrerequisiteList.CoursePrerequisiteViewModel = new List<CoursePrerequisiteViewModel>();
            foreach (var course in coursePrerequisites)
                CoursePrerequisiteList.CoursePrerequisiteViewModel.Add(new CoursePrerequisiteViewModel(course));

            ViewBag.LangId = languageId;
            return PartialView("Edit", CoursePrerequisiteList);
        }
        
       // POST: ControlPanel/CoursePrerequisite/Edit
       // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
       // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "Edit")]
       [AuditLogFilter(ActionDescription = "Course Prerequisite Edit Post")]
       public async Task<IActionResult> Edit(
           CoursePrerequisiteGeneralViewModel CoursePrerequisiteGeneralViewModel)
       {
           if (ModelState.IsValid)
           {
               try
               {

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel == null)
                        return Content("AddMassegeErrorInvalidData");


                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses == null))
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses.Count == 0 || s.CourseId == 0))
                        return Content("AddMassegeErrorInvalidData");


                    if (CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel.Exists(s => s.PrerequisiteCourses.Exists(r=>r.PrerequisiteCourseId == s.CourseId)))
                        return Content("AddMassegeErrorInvalidData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                foreach (var Course in CoursePrerequisiteGeneralViewModel.CoursePrerequisiteViewModel)
                                {
                                    //Delete CoursePrerequisite
                                    var ExistsAllCoursePrerequisite = _coursePrerequisiteService.GetCoursePrerequisiteByCourseId_WithoutUsing(Course.CourseId, context);
                                    foreach (var CoursePrerequisite in ExistsAllCoursePrerequisite)
                                    {

                                        if (Course.PrerequisiteCourses != null)
                                        {

                                            if (!Course.PrerequisiteCourses.Exists(e => e.PrerequisiteCourses_Id == CoursePrerequisite.Id))
                                                _coursePrerequisiteService.DeleteCoursePrerequisite_WithoutUsing(CoursePrerequisite, context);
                                        }
                                        else  //Delete all CoursePrerequisite
                                        {
                                            _coursePrerequisiteService.DeleteCoursePrerequisiteByCourseId_WithoutUsing(Course.CourseId, context);
                                        }
                                    }
                                    if (Course.PrerequisiteCourses != null)
                                    {
                                        foreach (var CoursePrerequisite in Course.PrerequisiteCourses)
                                        {
                                            var CoursePrerequisiteViewModel = new CoursePrerequisiteViewModel()
                                            {

                                                CreatedBy = User.Identity?.Name,
                                                CourseId = Course.CourseId,
                                                PrerequisiteCourseId = CoursePrerequisite.PrerequisiteCourseId,
                                                Status = CoursePrerequisiteGeneralViewModel.Status,
                                            };

                                            if (CoursePrerequisite.PrerequisiteCourses_Id > 0)
                                            {
                                                CoursePrerequisiteViewModel.Id = CoursePrerequisite.PrerequisiteCourses_Id;
                                                var ExistsCoursePrerequisite = _coursePrerequisiteService.GetCoursePrerequisiteById_WithoutUsing(CoursePrerequisiteViewModel.Id, context);
                                                _coursePrerequisiteService.EditCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel, ExistsCoursePrerequisite, context);
                                            }
                                            else
                                            {
                                                _coursePrerequisiteService.AddCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel, context);
                                                
                                            }
                                        }
                                    }
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Courses Prerequisite Dic");
                                return Content("Fail");
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
               }
               catch (Exception ex)
               {
                   _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Courses Prerequisite Dic");
                   return View(CoursePrerequisiteGeneralViewModel);
               }
           }
           return View(CoursePrerequisiteGeneralViewModel);
       }

        // GET: ControlPanel/CoursePrerequisite/Details/5
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "View")]
       [AuditLogFilter(ActionDescription = "Courses Prerequisite Details")]
       public async Task<IActionResult> ShowDetails(int? CourseId, int page)
       {
           if (CourseId == null)
           {
               return NotFound();
           }
           var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
           var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var coursePrerequisites = _coursePrerequisiteService.GetCoursePrerequisiteByCourseId(CourseId.Value);
            if (coursePrerequisites == null)
            {
                return NotFound();
            }
            var CoursePrerequisiteList = new CoursePrerequisiteGeneralViewModel();
            CoursePrerequisiteList.CoursePrerequisiteViewModel = new List<CoursePrerequisiteViewModel>();
            foreach (var course in coursePrerequisites)
                CoursePrerequisiteList.CoursePrerequisiteViewModel.Add(new CoursePrerequisiteViewModel(course));

       
           return PartialView("Details", CoursePrerequisiteList);
       }
     
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
       
        // GET: ControlPanel/Assignment/CourseSearch
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CoursePrerequisite Courses Get")]
        public IActionResult CourseSearch(string param, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var Courses = _courseService.GetCoursesByName(param, languageId);
            return Json(Courses);
        }

        // POST: ControlPanel/Permissions/Delete/5
        [CustomAuthentication(PageName = "CoursePrerequisite", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coursePrerequisites = _coursePrerequisiteService.GetCoursePrerequisiteByCourseId(id);
                if (coursePrerequisites != null)
                {
                    _coursePrerequisiteService.DeleteCoursePrerequisites(coursePrerequisites);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete CoursePrerequisite");
                return null;
            }
        }
    }
}
