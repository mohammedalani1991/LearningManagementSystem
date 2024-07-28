using DataEntity.Models.ViewModels;
using LearningManagementSystem.Controllers;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{

    [Area("ControlPanel")]
    public class SemesterController : Controller
    {
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly ISemesterService _semesterService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IStringLocalizer<SemesterController> _localizer;

        public SemesterController(
            ICookieService cookieService, ILogService logService, IUserProfileService userProfileService,
            ISemesterService semesterService, ISettingService settingService, IStringLocalizer<SemesterController> localizer)
        {
            _logService = logService;
            _userProfileService = userProfileService;
            _semesterService = semesterService;
            _cookieService = cookieService;
            _settingService = settingService;
            _localizer = localizer;
        }

        [CustomAuthentication(PageName = "Semester", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Semester")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Semester", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Semester List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.SemesterPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SemesterPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Name", "Description", "DefaultSemester", "Status", "CreatedBy", "CreatedOn", "PublicationDate", "PublicationEndDate", "WorkStartDate", "WorkEndDate" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.SemesterTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.SemesterTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.SemesterTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;


            var result = _semesterService.GetSemesters(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Semester", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Semester/Details/5
        [CustomAuthentication(PageName = "Semester", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Semester Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;


            var semester = _semesterService.GetSemesterById(id.Value, languageId);
            if (semester == null || semester.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", semester);
        }


        // GET: ControlPanel/Semester/Create
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Semester Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;



            return PartialView("Create");
        }

        // POST: ControlPanel/Semester/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Semester Create Post")]
        public async Task<IActionResult> Create(SemesterViewModel semesterViewModel)
        {
            try
            {
                if(semesterViewModel.PublicationDate > semesterViewModel.PublicationEndDate)
                    return Json(new { success = false, message = _localizer["Make sure that Publication End Date is after Publication Date"].Value });
                if (semesterViewModel.WorkStartDate > semesterViewModel.WorkEndDate)
                    return Json(new { success = false, message = _localizer["Make sure that Work End Date is after Work Date"].Value });

                if (semesterViewModel.LanguageId == 0)
                    semesterViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                semesterViewModel.CreatedBy = User.Identity?.Name;
                _semesterService.AddSemester(semesterViewModel);
                return Json(new {success = true});
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Semester Dic");
                return null;
            }
        }

        // GET: ControlPanel/Semester/Edit/5
        [AuditLogFilter(ActionDescription = "Semester Edit Get")]
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId)
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
            var semester = _semesterService.GetSemesterById(id.Value, languageId);
            semester.LanguageId = languageId;
            if (semester == null || semester.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return PartialView("Edit", semester);
        }

        // POST: ControlPanel/Semester/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Semester Edit Pot")]
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(SemesterViewModel semesterViewModel)
        {
            try
            {
                if (semesterViewModel.PublicationDate > semesterViewModel.PublicationEndDate)
                    return Json(new { success = false, message = _localizer["Make sure that Publication End Date is after Publication Date"].Value });
                if (semesterViewModel.WorkStartDate > semesterViewModel.WorkEndDate)
                    return Json(new { success = false, message = _localizer["Make sure that Work End Date is after Work Date"].Value });

                var semester = _semesterService.GetSemesterById(semesterViewModel.Id);
                if (semester != null && semester.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (semesterViewModel.LanguageId == 0)
                        semesterViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    _semesterService.EditSemester(semesterViewModel, semester);
                    return Json(new { success = true });

                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Semester Dic (Post)");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Semester Delete Get")]
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Delete")]

        // GET: ControlPanel/Semester/Delete/5
        public async Task<IActionResult> ShowDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;

            var semester = _semesterService.GetSemesterById(id.Value, langId);
            if (semester == null || semester.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Delete", semester);
        }

        // POST: ControlPanel/Semester/Delete/5
        [AuditLogFilter(ActionDescription = "Semester Delete Post")]
        [CustomAuthentication(PageName = "Semester", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            try
            {
                var semester = _semesterService.GetSemesterById(id);
                if (semester != null && semester.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _semesterService.DeleteSemester(semester);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While Delete Semester");
                return null;
            }
        }
    }

}
