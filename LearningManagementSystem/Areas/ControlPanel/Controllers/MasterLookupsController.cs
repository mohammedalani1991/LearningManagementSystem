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
    public class MasterLookupsController : Controller
    {
        private readonly ILookupService _lookupService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;

        public MasterLookupsController(
            ICookieService cookieService
            ,ILookupService lookupService
            , ISettingService settingService
            , ILogService logService)
        {
            _lookupService = lookupService;
            _logService = logService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Lookups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Lookups List")]
        // GET: ControlPanel/MasterLookups        
        public async Task<IActionResult> Index(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.MasterLookupPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.MasterLookupPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _lookupService.GetMasterLookups(searchText, page, languageId, pagination);
            return View(result);
        }

        // GET: ControlPanel/MasterLookups/Details/5
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Lookups Details")]
        public async Task<IActionResult> Details(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var lookup = _lookupService.GetMasterLookupById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            lookup.LookupDetails = _lookupService.GetDetailsLookups(lookup.Id, languageId);
            lookup.Page = page;
            lookup.LanguageId = languageId;
            ViewBag.LangId = languageId;
            return View(lookup);
        }

        // POST: ControlPanel/MasterLookups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Lookups Create Post")]
        [HttpPost]
        public IActionResult Create(MasterLookupViewModel masterLookupViewModel)
        {
            try
            {
                masterLookupViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                _lookupService.AddMasterLookup(masterLookupViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add master lookup");
                return View(masterLookupViewModel);
            }
        }

        // POST: ControlPanel/MasterLookups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [HttpPost]        
        [AuditLogFilter(ActionDescription = "Lookups Edit Post")]
        public IActionResult Edit(MasterLookupViewModel masterLookupViewModel)
        {
            try
            {
                var masterLookup = _lookupService.GetMasterLookupById(masterLookupViewModel.Id);
                if (masterLookup != null && masterLookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {                    
                    _lookupService.EditMasterLookup(masterLookupViewModel, masterLookup);
                }
                return RedirectToAction(nameof(Index), new { page=masterLookupViewModel.Page});
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Master Lookup (Post)");
                return View(masterLookupViewModel);
            }
        }

        // POST: ControlPanel/MasterLookups/Delete/5
        [AuditLogFilter(ActionDescription = "Lookups Delete Post")]        
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Delete")]
        public IActionResult DeleteConfirmed(int id, int page)
        {
            try
            {
                var systemSettingDeleted = _lookupService.GetMasterLookupById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _lookupService.DeleteMasterLookup(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index), new { page=page});
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Master Lookup");
                return RedirectToAction(nameof(Index));
            }
        }


        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lookups Get Get")]
        public async Task<IActionResult> GetMasterLookupByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _lookupService.GetMasterLookupById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }

    }
}
