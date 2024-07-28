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
    public class AcademicSupervisionStandardController : Controller
    {
        private readonly IAcademicSupervisionStandardService _AcademicSupervisionStandardService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public AcademicSupervisionStandardController(ICookieService cookieService, IAcademicSupervisionStandardService AcademicSupervisionStandardService, ISettingService settingService, ILogService logService)
        {
            _AcademicSupervisionStandardService = AcademicSupervisionStandardService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/AcademicSupervisionStandards
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List AcademicSupervisionStandards")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.AcademicSupervisionStandardPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.AcademicSupervisionStandardPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _AcademicSupervisionStandardService.GetAcademicSupervisionStandards(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/AcademicSupervisionStandards/Details/5
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var AcademicSupervisionStandard = _AcademicSupervisionStandardService.GetAcademicSupervisionStandardById(id.Value, languageId);
            if (AcademicSupervisionStandard == null || AcademicSupervisionStandard.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", AcademicSupervisionStandard);
        }

        // GET: ControlPanel/AcademicSupervisionStandards/Create
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/AcademicSupervisionStandards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "Create")]
        public async Task<IActionResult> Create(AcademicSupervisionStandardViewModel AcademicSupervisionStandard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AcademicSupervisionStandard.CreatedOn = DateTime.Now;
                    AcademicSupervisionStandard.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (AcademicSupervisionStandard.LanguageId == 0)
                        AcademicSupervisionStandard.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = AcademicSupervisionStandard.LanguageId;

                    _AcademicSupervisionStandardService.AddAcademicSupervisionStandard(AcademicSupervisionStandard);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new AcademicSupervisionStandard");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/AcademicSupervisionStandards/Edit/5
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var AcademicSupervisionStandard = _AcademicSupervisionStandardService.GetAcademicSupervisionStandardById(id.Value, languageId);
            if (AcademicSupervisionStandard == null || AcademicSupervisionStandard.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new AcademicSupervisionStandardViewModel(AcademicSupervisionStandard));
        }

        // POST: ControlPanel/AcademicSupervisionStandards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "Edit")]
        public IActionResult Edit(AcademicSupervisionStandardViewModel AcademicSupervisionStandard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _AcademicSupervisionStandardService.GetAcademicSupervisionStandardById(AcademicSupervisionStandard.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (AcademicSupervisionStandard.LanguageId == 0)
                        {
                            AcademicSupervisionStandard.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _AcademicSupervisionStandardService.EditAcademicSupervisionStandard(AcademicSupervisionStandard, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new AcademicSupervisionStandard");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/AcademicSupervisionStandards/Delete/5
        [CustomAuthentication(PageName = "AcademicSupervisionStandards", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _AcademicSupervisionStandardService.GetAcademicSupervisionStandardById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _AcademicSupervisionStandardService.DeleteAcademicSupervisionStandard(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete AcademicSupervisionStandard");
                return null;
            }
        }
    }
}
