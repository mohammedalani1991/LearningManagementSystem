using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using System.Threading.Tasks;
using System;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Core;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class EnrollStudentAlertController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollStudentAlertService _allowUserRateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;

        public EnrollStudentAlertController(
            ICookieService cookieService, ILogService logService, IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollStudentAlertService assignmentService, ISettingService settingService, ICourseService courseService, IUserProfileService userProfileService, ITrainerService trainerService)
        {
            _logService = logService;
            _allowUserRateService = assignmentService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseService = courseService;
            _userProfileService = userProfileService;
            _trainerService = trainerService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
        }

        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, int CourseId)
        {
            ViewBag.Page = page ?? 1;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            ViewBag.CourseId = CourseId;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollStudentAlertsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollStudentAlertsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _allowUserRateService.GetAllowUserRates(searchText, page ?? 1, languageId, pagination, CourseId, null);
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/EnrollStudentAlert/Details/5
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Detail")]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId > 0)
                langId = languageId;


            var assignment = _allowUserRateService.GetAllowUserRateById(id.Value, langId);

            ViewBag.LangId = langId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            return PartialView("Details", assignment);
        }

        // GET: ControlPanel/EnrollStudentAlert/Create
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Create Get")]
        public IActionResult ShowCreate(int enrollTeacherCoursId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            ViewBag.EnrollTeacherCourseId = enrollTeacherCoursId;
            return PartialView("Create");
        }

        // POST: ControlPanel/EnrollStudentAlert/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Create Post")]
        public async Task<IActionResult> Create(EnrollStudentAlertViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentViewModel.LanguageId == 0)
                        assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    assignmentViewModel.CreatedBy = User.Identity?.Name;
                    _allowUserRateService.AddEnrollStudentAlert(assignmentViewModel);
                    return Content("Success");
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new EnrollStudentAlert Dic");
                    return null;
                }
            }
            return null;
        }

        // GET: ControlPanel/EnrollStudentAlert/Edit/5
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Edit Get")]
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId == 0)
                languageId = langId;

            ViewBag.LangId = languageId;
            var allowUserRate = _allowUserRateService.GetAllowUserRateById(id.Value, languageId);
            if (allowUserRate == null || allowUserRate.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            return PartialView("Edit", new EnrollStudentAlertViewModel(allowUserRate));
        }

        // POST: ControlPanel/EnrollStudentAlert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Edit Pot")]
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(EnrollStudentAlertViewModel assignmentViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var assignment = _allowUserRateService.GetAllowUserRateById(assignmentViewModel.Id);
                    if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (assignmentViewModel.LanguageId == 0)
                            assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                        _allowUserRateService.EditEnrollStudentAlert(assignmentViewModel, assignment);
                        return Content("Success");
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing EnrollStudentAlert Dic (Post)");
                    return null;
                }
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Delete Get")]
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Delete")]

        // GET: ControlPanel/EnrollStudentAlert/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int languageId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId > 0)
                langId = languageId;

            var assignment = _allowUserRateService.GetAllowUserRateById(id.Value, langId);

            ViewBag.LangId = langId;
            return PartialView("Delete", assignment);
        }

        // POST: ControlPanel/EnrollStudentAlert/Delete/5
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert Delete Post")]
        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteEnrollStudentAlert(int id)
        {
            var assignment = _allowUserRateService.GetAllowUserRateById(id);
            if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _allowUserRateService.DeleteAllowUserRate(assignment);
                return Content("Success");
            }

            return Content("Fail");
        }
    }
}
