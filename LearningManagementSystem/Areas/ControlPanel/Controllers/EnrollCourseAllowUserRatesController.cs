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
    public class EnrollCourseAllowUserRateController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollCourseAllowUserRateService _allowUserRateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;

        public EnrollCourseAllowUserRateController(
            ICookieService cookieService, ILogService logService, IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollCourseAllowUserRateService assignmentService, ISettingService settingService, ICourseService courseService, IUserProfileService userProfileService, ITrainerService trainerService)
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

        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, int CourseId)
        {
            ViewBag.Page = page ?? 1;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            ViewBag.CourseId = CourseId;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCourseAllowUserRatesPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCourseAllowUserRatesPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _allowUserRateService.GetAllowUserRates(searchText, page ?? 1, languageId, pagination, CourseId);
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/EnrollCourseAllowUserRate/Details/5
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Details")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Details")]
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

        // GET: ControlPanel/EnrollCourseAllowUserRate/Create
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Create Get")]
        public IActionResult ShowCreate(int enrollTeacherCoursId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            ViewBag.EnrollTeacherCoursId = enrollTeacherCoursId;
            return PartialView("Create");
        }

        // POST: ControlPanel/EnrollCourseAllowUserRate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Create Post")]
        public async Task<IActionResult> Create(EnrollCourseAllowUserRateViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentViewModel.LanguageId == 0)
                        assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    assignmentViewModel.CreatedBy = User.Identity?.Name;
                    _allowUserRateService.AddEnrollCourseAllowUserRate(assignmentViewModel);
                    return Content("Success");
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new EnrollCourseAllowUserRate Dic");
                    return null;
                }
            }
            return null;
        }

        // GET: ControlPanel/EnrollCourseAllowUserRate/Edit/5
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Edit Get")]
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Edit")]
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

            var enrollTeacherCoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(allowUserRate.EnrollTeacherCourseId ?? 0);
            if (enrollTeacherCoursDetails == null || enrollTeacherCoursDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.IsOnlineLearningMethod = enrollTeacherCoursDetails.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("Edit", new EnrollCourseAllowUserRateViewModel(allowUserRate));
        }

        // POST: ControlPanel/EnrollCourseAllowUserRate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Edit Pot")]
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(EnrollCourseAllowUserRateViewModel assignmentViewModel)
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

                        _allowUserRateService.EditEnrollCourseAllowUserRate(assignmentViewModel, assignment);
                        return Content("Success");
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing EnrollCourseAllowUserRate Dic (Post)");
                    return null;
                }
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Delete Get")]
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Delete")]

        // GET: ControlPanel/EnrollCourseAllowUserRate/Delete/5
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

        // POST: ControlPanel/EnrollCourseAllowUserRate/Delete/5
        [AuditLogFilter(ActionDescription = "EnrollCourseAllowUserRate Delete Post")]
        [CustomAuthentication(PageName = "EnrollCourseAllowUserRates", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteEnrollCourseAllowUserRate(int id)
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
