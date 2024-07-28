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
    public class TrainerRateMeasureController : Controller
    {
        private readonly ITrainerRateMeasureService _TrainerRateMeasureService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public TrainerRateMeasureController(ICookieService cookieService, ITrainerRateMeasureService TrainerRateMeasureService, ISettingService settingService, ILogService logService)
        {
            _TrainerRateMeasureService = TrainerRateMeasureService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/TrainerRateMeasures
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List TrainerRateMeasures")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.TrainerRateMeasurePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.TrainerRateMeasurePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _TrainerRateMeasureService.GetTrainerRateMeasures(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/TrainerRateMeasures/Details/5
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var TrainerRateMeasure = _TrainerRateMeasureService.GetTrainerRateMeasureById(id.Value, languageId);
            if (TrainerRateMeasure == null || TrainerRateMeasure.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", TrainerRateMeasure);
        }

        // GET: ControlPanel/TrainerRateMeasures/Create
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/TrainerRateMeasures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "Create")]
        public async Task<IActionResult> Create(TrainerRateMeasureViewModel TrainerRateMeasure)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TrainerRateMeasure.CreatedOn = DateTime.Now;
                    TrainerRateMeasure.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (TrainerRateMeasure.LanguageId == 0)
                        TrainerRateMeasure.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = TrainerRateMeasure.LanguageId;

                    _TrainerRateMeasureService.AddTrainerRateMeasure(TrainerRateMeasure);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new TrainerRateMeasure");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/TrainerRateMeasures/Edit/5
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var TrainerRateMeasure = _TrainerRateMeasureService.GetTrainerRateMeasureById(id.Value, languageId);
            if (TrainerRateMeasure == null || TrainerRateMeasure.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new TrainerRateMeasureViewModel(TrainerRateMeasure));
        }

        // POST: ControlPanel/TrainerRateMeasures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TrainerRateMeasureViewModel TrainerRateMeasure)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _TrainerRateMeasureService.GetTrainerRateMeasureById(TrainerRateMeasure.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (TrainerRateMeasure.LanguageId == 0)
                            TrainerRateMeasure.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _TrainerRateMeasureService.EditTrainerRateMeasure(TrainerRateMeasure, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new TrainerRateMeasure");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/TrainerRateMeasures/Delete/5
        [CustomAuthentication(PageName = "TrainerRateMeasures", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _TrainerRateMeasureService.GetTrainerRateMeasureById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _TrainerRateMeasureService.DeleteTrainerRateMeasure(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete TrainerRateMeasure");
                return null;
            }
        }
    }
}
