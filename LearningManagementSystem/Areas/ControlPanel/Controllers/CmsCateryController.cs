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
    public class CmsCateryController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICmsCateryService _cmscateryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CmsCateryController(
            ICookieService cookieService,ILogService logService,
            ICmsCateryService cmscateryService, ISettingService settingService)
        {
            _logService = logService;
            _cmscateryService = cmscateryService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsCatery")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsCatery List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsCateryPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsCateryPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "CreatedOn", "CreatedBy", "Status" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsCateryTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsCateryTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsCateryTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _cmscateryService.GetCmsCaterys(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/CmsCatery/ShowImage
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsCatery Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CmsCateryViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CmsCatery/Details/5
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsCatery Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmscatery = _cmscateryService.GetCmsCateryById(id.Value, langId);
            
            if (cmscatery == null || cmscatery.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmscatery.Page = page;
            return PartialView("Details", cmscatery);
        }

        // GET: ControlPanel/CmsCatery/Create
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsCatery Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
           
            ViewBag.ListCmsCaterys = _cmscateryService.GetAllCmsCaterys(languageId);
            ViewBag.LanguageId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/CmsCatery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsCatery Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "Status","LanguageId")]
            CmsCateryViewModel CmsCateryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CmsCateryViewModel.LanguageId == 0)
                        CmsCateryViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    CmsCateryViewModel.CreatedBy = User.Identity?.Name;                    
                    _cmscateryService.AddCmsCatery(CmsCateryViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsCatery Dic");
                    return View(CmsCateryViewModel);
                }
            }
            return View(CmsCateryViewModel);
        }

        // GET: ControlPanel/CmsCatery/Edit/5
        [AuditLogFilter(ActionDescription = "CmsCatery Edit Get")]
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId=0)
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
         
            var cmscatery = _cmscateryService.GetCmsCateryById(id.Value, languageId);
            cmscatery.LanguageId = languageId;
            if (cmscatery == null || cmscatery.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            cmscatery.Page = page;
            cmscatery.ListCmsCaterys = _cmscateryService.GetAllCmsCaterys(languageId);
            cmscatery.LanguageId = languageId;
            ViewBag.LanguageId = languageId;

            return PartialView("Edit", cmscatery);
        }

        // POST: ControlPanel/CmsCatery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsCatery Edit Pot")]
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id", "Name", "Description", "ImageUrl", "ParentId", "ShowInHomePage", "Status","LanguageId")]
            CmsCateryViewModel cmscateryViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var cmscatery = _cmscateryService.GetCmsCateryById(cmscateryViewModel.Id);
                    if (cmscatery != null && cmscatery.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (cmscateryViewModel.LanguageId == 0)
                            cmscateryViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _cmscateryService.EditCmsCatery(cmscateryViewModel, cmscatery);
                    }

                    return RedirectToAction(nameof(Index), new { page = cmscateryViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsCatery Dic (Post)");
                    return View(cmscateryViewModel);
                }
            }
            return View(cmscateryViewModel);
        }
        [AuditLogFilter(ActionDescription = "CmsCatery Delete Get")]
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsCatery/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmscatery = _cmscateryService.GetCmsCateryById(id.Value, langId);
            if (cmscatery == null || cmscatery.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmscatery.Page = page;
            return PartialView("Delete", cmscatery);
        }

        // POST: ControlPanel/CmsCatery/Delete/5
        [AuditLogFilter(ActionDescription = "CmsCatery Delete Post")]
        [CustomAuthentication(PageName = "CmsCaterys", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsCatery(int id, int Page)
        {
            var cmscatery = _cmscateryService.GetCmsCateryById(id);
            if (cmscatery != null && cmscatery.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _cmscateryService.DeleteCmsCatery(cmscatery);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
