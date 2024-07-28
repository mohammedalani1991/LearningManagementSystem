using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class ModuleController : Controller
    {
        private readonly ILogService _logService;
        private readonly IModuleService _moduleService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public ModuleController(ISettingService settingService,ICookieService cookieService,ILogService logService, IModuleService ModuleService)
        {
            _logService = logService;
            _moduleService = ModuleService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        // User with Role Administrator can access this.
        // [Authorize("Administrator")]
        // GET: ControlPanel/Modules
        [CustomAuthentication(PageName = "Modules", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Module List")]
        public async Task<IActionResult> Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.ModulePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ModulePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
          
            var result = _moduleService.GetModule(searchText, page, languageId, pagination);

            return View(result);
        }

        // GET: ControlPanel/Modules/Details/5
        [CustomAuthentication(PageName = "Modules", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Module Details")]
        public async Task<IActionResult> Details(int? id,int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var module = _moduleService.GetModuleById(id.Value, langId);
            if (module == null || module.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            module.Page = page;
            return View(module);
        }

        // GET: ControlPanel/Modules/Create
        [CustomAuthentication(PageName = "Modules", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Module Create Get")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            return View();
        }

        // POST: ControlPanel/Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Module Create Post")]
        [CustomAuthentication(PageName = "Modules", PermissionKey = "Create")]
        public async Task<IActionResult> Create(
            [Bind("Id,BaseUrl,Name,Code,Description,LanguageId,Status")]
            ModuleViewModel ModuleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModuleViewModel.LanguageId == 0)
                        ModuleViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    ModuleViewModel.CreatedBy = User.Identity?.Name;
                    _moduleService.AddModule(ModuleViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Module Dic");
                    return View(ModuleViewModel);
                }
            }
            return View(ModuleViewModel);
        }

        // GET: ControlPanel/Modules/Edit/5
        [AuditLogFilter(ActionDescription = "Module Edit Get")]
        [CustomAuthentication(PageName = "Modules", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id,int page, int languageId=0)
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

            var module = _moduleService.GetModuleById(id.Value, languageId);
            module.LanguageId = languageId;
            if (module == null || module.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            module.Page = page;
            return View(module);
        }

        // POST: ControlPanel/Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Module Edit Pot")]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,BaseUrl,Page,Name,Code,Description,LanguageId,Status")]
            ModuleViewModel moduleViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var Module = _moduleService.GetModuleById(moduleViewModel.Id);
                    if (Module != null && Module.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (moduleViewModel.LanguageId == 0)
                            moduleViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _moduleService.EditModule(moduleViewModel, Module);
                    }

                    return RedirectToAction(nameof(Index), new { page=moduleViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Module Dic (Post)");
                    return View(moduleViewModel);
                }
            }
            return View(moduleViewModel);
        }
        [AuditLogFilter(ActionDescription = "Module Delete Get")]
        [CustomAuthentication(PageName = "Modules", PermissionKey = "Delete")]
        // GET: ControlPanel/Modules/Delete/5
        public async Task<IActionResult> Delete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var module = _moduleService.GetModuleById(id.Value, langId);
            if (module == null || module.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            module.Page = page;
            return View(module);
        }

        // POST: ControlPanel/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Module Delete Post")]
        [CustomAuthentication(PageName = "Modules", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int Page)
        {
            var Module = _moduleService.GetModuleById(id);
            if (Module != null && Module.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _moduleService.DeleteModule(Module);
            }

            return RedirectToAction(nameof(Index), new { page =Page});
        }
    }
}
