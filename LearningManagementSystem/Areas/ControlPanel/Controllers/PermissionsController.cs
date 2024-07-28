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
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public PermissionsController(ICookieService cookieService, IPermissionService permissionService, ISettingService settingService, ILogService logService)
        {
            _permissionService = permissionService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "Permissions", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Permissions
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Permissions")]
        public async Task<IActionResult> GetData(int? page, int pagination, string searchText, int resetTo = 0)
        {
            if (resetTo == 1 || page == null || page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.PermissionPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PermissionPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _permissionService.GetPermissions(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/Permissions/Details/5
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission = _permissionService.GetPermissionById(id.Value, languageId);
            if (permission == null || permission.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", permission);
        }

        // GET: ControlPanel/Permissions/Create
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "Create")]
        public async Task<IActionResult> Create(
            [Bind("Id,ModuleId,PageUrl,PageName,PermissionKey,Description,Status,LanguageId")]
            PermissionViewModel permission)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    #region Add Permission

                    permission.CreatedOn = DateTime.Now;
                    permission.CreatedBy = User.Identity?.Name ?? string.Empty;
                    permission.PageUrl ??= string.Empty;

                    if (permission.LanguageId == 0)
                    {
                        permission.LanguageId = CultureHelper.GetDefaultLanguageId();
                    }

                    ViewBag.LangId = permission.LanguageId;

                    _permissionService.AddPermission(permission);

                    #endregion

                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Permission");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/Permissions/Edit/5
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var permission = _permissionService.GetPermissionById(id.Value, languageId);
            if (permission == null || permission.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", permission);
        }

        // POST: ControlPanel/Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PermissionViewModel permission)
        {
            if (id != permission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _permissionService.GetPermissionById(permission.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (permission.LanguageId == 0)
                        {
                            permission.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _permissionService.EditPermission(permission, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Permission");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/Permissions/Delete/5
        [CustomAuthentication(PageName = "Permissions", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _permissionService.GetPermissionById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _permissionService.DeletePermission(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Permission");
                return null;
            }
        }

    }
}



