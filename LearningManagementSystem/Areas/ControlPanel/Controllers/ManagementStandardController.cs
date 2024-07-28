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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class ManagementStandardController : Controller
    {
        private readonly IManagementStandardService _ManagementStandardService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public ManagementStandardController(ICookieService cookieService, IManagementStandardService ManagementStandardService, ISettingService settingService, ILogService logService)
        {
            _ManagementStandardService = ManagementStandardService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/ManagementStandards
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List ManagementStandards")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.ManagementStandardPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ManagementStandardPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _ManagementStandardService.GetManagementStandards(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/ManagementStandards/Details/5
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var ManagementStandard = _ManagementStandardService.GetManagementStandardById(id.Value, languageId);
            if (ManagementStandard == null || ManagementStandard.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", ManagementStandard);
        }

        // GET: ControlPanel/ManagementStandards/Create
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/ManagementStandards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "Create")]
        public async Task<IActionResult> Create(ManagementStandardViewModel ManagementStandard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ManagementStandard.CreatedOn = DateTime.Now;
                    ManagementStandard.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (ManagementStandard.LanguageId == 0)
                        ManagementStandard.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = ManagementStandard.LanguageId;

                    _ManagementStandardService.AddManagementStandard(ManagementStandard);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new ManagementStandard");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/ManagementStandards/Edit/5
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var ManagementStandard = _ManagementStandardService.GetManagementStandardById(id.Value, languageId);
            if (ManagementStandard == null || ManagementStandard.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new ManagementStandardViewModel(ManagementStandard));
        }

        // POST: ControlPanel/ManagementStandards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "ManagementStandard", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ManagementStandardViewModel ManagementStandard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _ManagementStandardService.GetManagementStandardById(ManagementStandard.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (ManagementStandard.LanguageId == 0)
                        {
                            ManagementStandard.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _ManagementStandardService.EditManagementStandard(ManagementStandard, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new ManagementStandard");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/ManagementStandards/Delete/5
        [CustomAuthentication(PageName = "ManagementStandards", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _ManagementStandardService.GetManagementStandardById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _ManagementStandardService.DeleteManagementStandard(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete ManagementStandard");
                return null;
            }
        }
    }
}
