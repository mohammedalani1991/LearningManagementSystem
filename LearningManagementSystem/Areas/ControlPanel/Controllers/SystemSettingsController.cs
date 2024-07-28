using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;
using Microsoft.AspNetCore.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class SystemSettingsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IUserProfileService _userProfileService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        public SystemSettingsController(
            ISettingService settingService, IUserProfileService userProfileService,
            ICookieService cookieService,
            ILogService logService,
            ISystemSettingService systemSettingService)
        {
            _settingService = settingService;
            _userProfileService = userProfileService;
            _systemSettingService = systemSettingService;
            _logService = logService;
            _cookieService = cookieService;
        }

        // GET: ControlPanel/SystemSettings
        [CustomAuthentication(PageName = "Settings", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Settings List")]
        public async Task<IActionResult> Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.SettingPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SettingPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;



            var settings = _systemSettingService.GetSystemSettings(searchText, page, languageId, pagination);
            return View(settings);
        }

        // POST: ControlPanel/SystemSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuditLogFilter(ActionDescription = "Settings Create Post")]
        [CustomAuthentication(PageName = "Settings", PermissionKey = "Create")]
        public ActionResult Create(SettingViewModel systemSetting)
        {
            try
            {
                var massterId = LookupHelper.GetMasterLookupsByCode(GeneralEnums.MasterLookupCodeEnums.SettingTypes.ToString());
                systemSetting.TypeId = LookupHelper.GetLookupDetailsByCode(systemSetting.Type, massterId).Id;
                systemSetting.CreatedBy = User.Identity.Name;
                _systemSettingService.AddSystemSetting(systemSetting);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Add System Settings (Post)");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ControlPanel/SystemSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "Settings", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Settings Edit Post")]
        public IActionResult Edit(SettingViewModel systemSettingViewModel)
        {
            try
            {
                var systemSetting = _systemSettingService.GetSystemSetting(systemSettingViewModel.Id);
                if (systemSetting != null && systemSetting.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    var massterId = LookupHelper.GetMasterLookupsByCode(GeneralEnums.MasterLookupCodeEnums.SettingTypes.ToString());
                    systemSettingViewModel.TypeId = LookupHelper.GetLookupDetailsByCode(systemSettingViewModel.Type, massterId)?.Id;
                    _systemSettingService.EditSystemSetting(systemSettingViewModel, systemSetting);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing System Settings (Post)");
                return View(systemSettingViewModel);
            }
        }

        // POST: ControlPanel/SystemSettings/Delete/5        
        [CustomAuthentication(PageName = "Settings", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Settings Delete Post")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var systemSettingDeleted = _systemSettingService.GetSystemSetting(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _systemSettingService.DeleteSystemSetting(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete SystemSetting");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ControlPanel/SystemSettings/Edit/5
        [CustomAuthentication(PageName = "Settings", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Settings Edit Get")]
        public async Task<IActionResult> GetSettingByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetting = _systemSettingService.GetSystemSetting(id.Value, languageId);
            if (systemSetting == null || systemSetting.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return Json(systemSetting);
        }

        [AuditLogFilter(ActionDescription = "Settings Edit Get")]
        [CustomAuthentication(PageName = "Settings", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowTimeZone(int id)
        {
            ViewBag.TimeZoneId = id;
            return PartialView("ShowTimeZone");
        }



    }
}
