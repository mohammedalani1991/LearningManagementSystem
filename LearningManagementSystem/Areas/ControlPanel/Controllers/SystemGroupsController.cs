using System;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]

    public class SystemGroupsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ISystemGroupService _groupService;
        private readonly ISettingService _settingService;
        private readonly ICookieService _cookieService;

        public SystemGroupsController(ICookieService cookieService,
           ILogService logService,
           ISystemGroupService systemGroupService, ISettingService settingService
           )
        {
            _logService = logService;
            _groupService = systemGroupService;
            _settingService = settingService;
            _cookieService = cookieService;
        }

        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Group List")]
        public IActionResult Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (resetTo == 1 || page == null || page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;
            else
                ViewBag.AddMore = false;

            var val = _cookieService.GetCookie(Constants.Pagenation.SystemGroupPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SystemGroupPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;


            var result = _groupService.GetSystemGroups(searchText, page, languageId, pagination);

            return View(result);
        }
        
        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Group Details")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _groupService.GetSystemGroupWithLanguageById(id.Value);
            if (model == null || model.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(model);
        }

        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Group Create Get")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;


            return View(new SystemGroupViewModel() {LanguageId= langId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "System Create Post")]
        public IActionResult Create(SystemGroupViewModel model)
        {
            model.CreatedBy = User.Identity?.Name;
            model.CreatedOn = DateTime.Now;
            model.LanguageId = model.LanguageId == 0 ? CultureHelper.GetDefaultLanguageId() : model.LanguageId;

            if (ModelState.IsValid)
            {
                _groupService.AddSystemGroup(model);
                  return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Systems Edit Get")]
        public IActionResult Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.Arabic)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var model = _groupService.GetSystemGroupWithLanguageById(id.Value, languageId);
            if (model == null || model.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "System Edit Post")]
        public IActionResult Edit(SystemGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var systemGroup = _groupService.GetSystemGroupById(model.Id);

                    if (systemGroup != null && systemGroup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (model.LanguageId == 0)
                        {
                            model.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _groupService.EditSystemGroup(model, systemGroup);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity.Name, ex, "Error While Editing Master Lookup (Post)");
                    return View(model);
                }
            }
            return View(model);
        }

        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "SystemG Delete Get")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _groupService.GetSystemGroupWithLanguageById(id.Value);
            if (model == null || model.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "SystemGroups", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "System Delete Post")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _groupService.DeleteSystemGroupById(id);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While deleting group");
                return RedirectToAction(nameof(Index));
            }
        }

    public IActionResult GetUsers(int id)
        {
            var result = _groupService.GetSystemGroupUsers(id);
            return Ok(result);           
        }
    }
}
