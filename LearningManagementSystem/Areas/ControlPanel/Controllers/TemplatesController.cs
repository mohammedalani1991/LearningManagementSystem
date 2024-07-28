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
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class TemplatesController : Controller
    {
        private readonly ILogService _logService;
        private readonly ITemplateService _templateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public TemplatesController(
            ICookieService cookieService, ILogService logService,
            ITemplateService templateService, ISettingService settingService)
        {
            _logService = logService;
            _templateService = templateService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Templates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Templates")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Templates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Template List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.TemplatePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.TemplatePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Id", "Name", "Code", "RanderHtml", "Type", "Status", "CreatedBy", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.TemplateTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.TemplateTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.TemplateTable, table, 7);
            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _templateService.GetTemplates(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Templates", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Templates/Details/5
        [CustomAuthentication(PageName = "Templates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Template Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var template = _templateService.GetTemplateById(id.Value);

            if (template == null || template.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.LangId = langId;
            ViewBag.Page = page;

            return PartialView("Details", template);
        }

        [CustomAuthentication(PageName = "Templates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Template Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

            return PartialView("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Templates", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Template Create Post")]
        public async Task<IActionResult> Create(TemplateViewModel TemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TemplateViewModel.CreatedBy = User.Identity?.Name;
                    _templateService.AddTemplate(TemplateViewModel);
                    return Ok();
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Template");
                    return null;
                }
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "Template Edit Get")]
        [CustomAuthentication(PageName = "Templates", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
                languageId = langId;

            var template = _templateService.GetTemplateById(id.Value);

            if (template == null || template.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.Page = page;
            ViewBag.LangId = languageId;

            return PartialView("Edit", new TemplateViewModel(template));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Template Edit Pot")]
        [CustomAuthentication(PageName = "Templates", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(TemplateViewModel templateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var template = _templateService.GetTemplateById(templateViewModel.Id);
                    if (template != null && template.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        _templateService.EditTemplate(templateViewModel, template);
                        return Ok();
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Template (Post)");
                    return null;
                }
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "Template Delete Get")]
        [CustomAuthentication(PageName = "Templates", PermissionKey = "Delete")]
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
                return NotFound();

            var template = _templateService.GetTemplateById(id.Value);
            if (template == null || template.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            return PartialView("Delete", template);
        }

        [AuditLogFilter(ActionDescription = "Template Delete Post")]
        [CustomAuthentication(PageName = "Templates", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteTemplate(int id, int Page)
        {
            var template = _templateService.GetTemplateById(id);
            if (template != null && template.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _templateService.DeleteTemplate(template);
                return Ok();
            }

            return null;
        }
    }
}
