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
    public class ExpulsionController : Controller
    {
        private readonly IExpulsionService _expulsionService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public ExpulsionController(ICookieService cookieService, IExpulsionService expulsionService, ISettingService settingService, ILogService logService)
        {
            _expulsionService = expulsionService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Expulsions
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Expulsions")]
        public async Task<IActionResult> GetData(int? page, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.ExpulsionPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ExpulsionPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _expulsionService.GetExpulsions(page, pagination);
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/Expulsions/Details/5
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var expulsion = _expulsionService.GetExpulsionById(id.Value);
            if (expulsion == null || expulsion.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", expulsion);
        }

        // GET: ControlPanel/Expulsions/Create
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Expulsions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Create")]
        public async Task<IActionResult> Create(ExpulsionViewModel expulsion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    expulsion.CreatedOn = DateTime.Now;
                    expulsion.CreatedBy = User.Identity?.Name ?? string.Empty;
                    _expulsionService.AddExpulsion(expulsion);

                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Expulsion");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/Expulsions/Edit/5
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var expulsion = _expulsionService.GetExpulsionById(id.Value);
            if (expulsion == null || expulsion.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            return PartialView("Edit", new ExpulsionViewModel(expulsion));
        }

        // POST: ControlPanel/Expulsions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExpulsionViewModel expulsion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _expulsionService.GetExpulsionById(expulsion.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        _expulsionService.EditExpulsion(expulsion, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Expulsion");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/Expulsions/Delete/5
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _expulsionService.GetExpulsionById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _expulsionService.DeleteExpulsion(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Expulsion");
                return null;
            }
        }

        // GET: ControlPanel/Expulsions/Create
        [CustomAuthentication(PageName = "Expulsion", PermissionKey = "Create")]
        public IActionResult ShowStudents(int id , int? page)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.Page = page??1;
            ViewBag.ExpulsionId = id;

            var expulsion = _expulsionService.GetExpulsionById(id);
            var result = _expulsionService.GetExpelledStudents(expulsion.ExpelledFrom , expulsion.ExpelledTo ,page??1 , languageId);

            return PartialView("_Students", result );
        }
    }
}
