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
using LearningManagementSystem.Services.BankOfQuestion;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class ExamTemplateController : Controller
    {
        private readonly ILogService _logService;
        private readonly IExamTemplateService _examTemplateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;

        public ExamTemplateController(
            ICookieService cookieService, ILogService logService,
            IExamTemplateService examTemplateService, ISettingService settingService, 
            ICourseCategoryService courseCategoryService,
            ICourseService courseService)
        {
            _logService = logService;
            _examTemplateService = examTemplateService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseCategoryService = courseCategoryService;
            _courseService = courseService;
         
        }

        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Exam Template")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Exam Template List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, int? CourseId, string hdCourseName, int? CategoryId)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            if (CategoryId > 0)
            {
                ViewBag.CategoryId = CategoryId;
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

            var val = _cookieService.GetCookie(Constants.Pagenation.ExamTemplatePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ExamTemplatePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "Description", "Duration", "CategoryId", "CourseId", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.ExamTemplateTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.ExamTemplateTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.ExamTemplateTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            var result = _examTemplateService.GetExamTemplates(searchText, page, languageId, pagination, CourseId, CategoryId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }



        // GET: ControlPanel/ExamTemplate/Details/5
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Exam Template Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var examTemplate = _examTemplateService.GetExamTemplateById(id.Value, langId);

            if (examTemplate == null || examTemplate.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            examTemplate.Page = page;
            return PartialView("Details", examTemplate);
        }

        // GET: ControlPanel/ExamTemplate/Create
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Template Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/ExamTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Template Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Description,Duration,CategoryId,CourseId","LanguageId","Shuffle","Status")]
            ExamTemplateViewModel ExamTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ExamTemplateViewModel.LanguageId == 0)
                        ExamTemplateViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    ExamTemplateViewModel.CreatedBy = User.Identity?.Name;
                    _examTemplateService.AddExamTemplate(ExamTemplateViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new ExamTemplate Dic");
                    return View(ExamTemplateViewModel);
                }
            }
            return View(ExamTemplateViewModel);
        }

        // GET: ControlPanel/ExamTemplate/Edit/5
        [AuditLogFilter(ActionDescription = "ExamTemplate Edit Get")]
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Edit")]
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

            var examTemplate = _examTemplateService.GetExamTemplateById(id.Value, languageId);
            examTemplate.LanguageId = languageId;
            if (examTemplate == null || examTemplate.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var Courses = _courseService.GetCourseById((examTemplate.CourseId != null) ? examTemplate.CourseId.Value : 0, languageId);
            List<Course> newList = new List<Course>();
            if (Courses != null)
                newList.Add(new Course() { CourseName = Courses.CourseName, Id = Courses.Id });
            examTemplate.ListCourse = newList;

            var CoursesCategory = _courseCategoryService.GetCourseCategoryById((examTemplate.CategoryId != null) ? examTemplate.CategoryId.Value : 0, languageId);
            if (CoursesCategory == null)
            {
                examTemplate.CategoryId = 0;
                examTemplate.CategoryName = "";
            }


            examTemplate.Page = page;
            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            return PartialView("Edit", examTemplate);
        }

        // POST: ControlPanel/ExamTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Exam Template Edit Pot")]
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id,Name,Description,Duration,CategoryId,CourseId","LanguageId","Shuffle","Status")]
            ExamTemplateViewModel examTemplateViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var examTemplate = _examTemplateService.GetExamTemplateById(examTemplateViewModel.Id);
                    if (examTemplate != null && examTemplate.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (examTemplateViewModel.LanguageId == 0)
                            examTemplateViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _examTemplateService.EditExamTemplate(examTemplateViewModel, examTemplate);
                    }

                    return RedirectToAction(nameof(Index), new { page = examTemplateViewModel.Page });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing ExamTemplate Dic (Post)");
                    return View(examTemplateViewModel);
                }
            }
            return View(examTemplateViewModel);
        }
        [AuditLogFilter(ActionDescription = "Exam Template Delete Get")]
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Delete")]

        // GET: ControlPanel/ExamTemplate/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var examTemplate = _examTemplateService.GetExamTemplateById(id.Value, langId);
            if (examTemplate == null || examTemplate.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            examTemplate.Page = page;
            return PartialView("Delete", examTemplate);
        }

        // POST: ControlPanel/ExamTemplate/Delete/5
        [AuditLogFilter(ActionDescription = "Exam Template Delete Post")]
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteExamTemplate(int id, int Page)
        {
            var examTemplate = _examTemplateService.GetExamTemplateById(id);
            if (examTemplate != null && examTemplate.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _examTemplateService.DeleteExamTemplate(examTemplate);
            }

            return RedirectToAction(nameof(Index), new { page = Page });
        }

        // GET: ControlPanel/ExamTemplate/CourseSearch
        [CustomAuthentication(PageName = "ExamTemplates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Exam Templates Courses Get")]
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
