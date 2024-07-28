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

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CourseCategoryController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseCategoryService _coursecategoryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CourseCategoryController(
            ICookieService cookieService,ILogService logService,
            ICourseCategoryService coursecategoryService, ISettingService settingService)
        {
            _logService = logService;
            _coursecategoryService = coursecategoryService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Course Category")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Category List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CourseCategoryPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CourseCategoryPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "CreatedOn", "CreatedBy", "Status" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CourseCategoryTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseCategoryTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseCategoryTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _coursecategoryService.GetCourseCategorys(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/CourseCategory/ShowImage
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Category Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CourseCategoryViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CourseCategory/Details/5
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Category Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var coursecategory = _coursecategoryService.GetCourseCategoryById(id.Value, langId);
            
            if (coursecategory == null || coursecategory.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            coursecategory.Page = page;
            return PartialView("Details", coursecategory);
        }

        // GET: ControlPanel/CourseCategory/Create
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Category Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
           
            ViewBag.ListCourseCategorys = _coursecategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LanguageId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/CourseCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Category Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "Status","LanguageId")]
            CourseCategoryViewModel CourseCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CourseCategoryViewModel.LanguageId == 0)
                        CourseCategoryViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    CourseCategoryViewModel.CreatedBy = User.Identity?.Name;                    
                    _coursecategoryService.AddCourseCategory(CourseCategoryViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CourseCategory Dic");
                    return View(CourseCategoryViewModel);
                }
            }
            return View(CourseCategoryViewModel);
        }

        // GET: ControlPanel/CourseCategory/Edit/5
        [AuditLogFilter(ActionDescription = "Course Category Edit Get")]
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId=0)
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
            ViewBag.LanguageId = languageId;

            var coursecategory = _coursecategoryService.GetCourseCategoryById(id.Value, languageId);
       
            if (coursecategory == null || coursecategory.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            coursecategory.Page = page;
            coursecategory.ListCourseCategorys = _coursecategoryService.GetAllCourseCategorys(languageId);
            return PartialView("Edit", coursecategory);
        }

        // POST: ControlPanel/CourseCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Course Category Edit Pot")]
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "Status","LanguageId")]
            CourseCategoryViewModel coursecategoryViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var coursecategory = _coursecategoryService.GetCourseCategoryById(coursecategoryViewModel.Id);
                    if (coursecategory != null && coursecategory.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (coursecategoryViewModel.LanguageId == 0)
                            coursecategoryViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _coursecategoryService.EditCourseCategory(coursecategoryViewModel, coursecategory);
                    }

                    return RedirectToAction(nameof(Index), new { page = coursecategoryViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CourseCategory Dic (Post)");
                    return View(coursecategoryViewModel);
                }
            }
            return View(coursecategoryViewModel);
        }
        [AuditLogFilter(ActionDescription = "Course Category Delete Get")]
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Delete")]

        // GET: ControlPanel/CourseCategory/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var coursecategory = _coursecategoryService.GetCourseCategoryById(id.Value, langId);
            if (coursecategory == null || coursecategory.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            coursecategory.Page = page;
            return PartialView("Delete", coursecategory);
        }

        // POST: ControlPanel/CourseCategory/Delete/5
        [AuditLogFilter(ActionDescription = "Course Category Delete Post")]
        [CustomAuthentication(PageName = "CourseCategorys", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCourseCategory(int id, int Page)
        {
            var coursecategory = _coursecategoryService.GetCourseCategoryById(id);
            if (coursecategory != null && coursecategory.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _coursecategoryService.DeleteCourseCategory(coursecategory);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
