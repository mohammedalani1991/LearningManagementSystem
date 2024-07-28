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
using Microsoft.AspNetCore.Mvc.Rendering;
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class AssignmentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IAssignmentService _assignmentService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        public AssignmentController(
            ICookieService cookieService, ILogService logService,
            IAssignmentService assignmentService, ISettingService settingService, ICourseService courseService)
        {
            _logService = logService;
            _assignmentService = assignmentService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseService = courseService;
        }

        [CustomAuthentication(PageName = "Assignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Assignment")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Assignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Assignment List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, int? CourseId,string hdCourseName)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

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

            var val = _cookieService.GetCookie(Constants.Pagenation.AssignmentPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.AssignmentPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "CourseId", "SubmissionDate", "ExpiryDate", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.AssignmentTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.AssignmentTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.AssignmentTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _assignmentService.GetAssignments(searchText, page, languageId, pagination, CourseId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Assignments", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Assignment/Details/5
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Assignment Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var assignment = _assignmentService.GetAssignmentById(id.Value, langId);

            if (assignment == null || assignment.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            assignment.Page = page;
            return PartialView("Details", assignment);
        }

        // GET: ControlPanel/Assignment/Create
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Assignment Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

          //  ViewBag.ListCourse = _courseService.GetAllCourses(languageId);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Assignment Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,SubmissionDate,ExpiryDate,CourseId,Name,Description,LanguageId,Status","Type")]
            AssignmentViewModel AssignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(AssignmentViewModel.ExpiryDate != null &&  AssignmentViewModel.SubmissionDate >= AssignmentViewModel.ExpiryDate)
                        return Content("AddMassegeErrorInvalidDates", "text/plain");
                   
                  
                    if (AssignmentViewModel.LanguageId == 0)
                        AssignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    AssignmentViewModel.CreatedBy = User.Identity?.Name;
                    _assignmentService.AddAssignment(AssignmentViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Assignment Dic");
                    return View(AssignmentViewModel);
                }
            }
            return View(AssignmentViewModel);
        }

        // GET: ControlPanel/Assignment/Edit/5
        [AuditLogFilter(ActionDescription = "Assignment Edit Get")]
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;
            var assignment = _assignmentService.GetAssignmentById(id.Value, languageId);
            if (assignment == null || assignment.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            assignment.LanguageId = languageId;
            var Courses = _courseService.GetCourseById(assignment.CourseId, languageId);
            List<Course> newList = new List<Course>();
            if (Courses != null)
                newList.Add(new Course() { CourseName = Courses.CourseName, Id = Courses.Id });
            assignment.ListCourse = newList;

            assignment.Page = page;
            return PartialView("Edit", assignment);
        }

        // POST: ControlPanel/Assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Assignment Edit Pot")]
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id,SubmissionDate,ExpiryDate,CourseId,Name,Description,LanguageId,Status","Type")]
            AssignmentViewModel assignmentViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentViewModel.ExpiryDate != null && assignmentViewModel.SubmissionDate >= assignmentViewModel.ExpiryDate)
                        return Content("EditMassegeErrorInvalidDates", "text/plain");

                    var assignment = _assignmentService.GetAssignmentById(assignmentViewModel.Id);
                    if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (assignmentViewModel.LanguageId == 0)
                            assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _assignmentService.EditAssignment(assignmentViewModel, assignment);
                    }

                    return RedirectToAction(nameof(Index), new { page = assignmentViewModel.Page });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Assignment Dic (Post)");
                    return View(assignmentViewModel);
                }
            }
            return View(assignmentViewModel);
        }
        [AuditLogFilter(ActionDescription = "Assignment Delete Get")]
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Delete")]

        // GET: ControlPanel/Assignment/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var assignment = _assignmentService.GetAssignmentById(id.Value, langId);
            if (assignment == null || assignment.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            assignment.Page = page;
            return PartialView("Delete", assignment);
        }

        // POST: ControlPanel/Assignment/Delete/5
        [AuditLogFilter(ActionDescription = "Assignment Delete Post")]
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteAssignment(int id, int Page)
        {
            var assignment = _assignmentService.GetAssignmentById(id);
            if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _assignmentService.DeleteAssignment(assignment);
            }

            return RedirectToAction(nameof(Index), new { page = Page });
        }



        // GET: ControlPanel/Assignment/CourseSearch
        [CustomAuthentication(PageName = "Assignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Assignments Courses Get")]
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


    }
}
