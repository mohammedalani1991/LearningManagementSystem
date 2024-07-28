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
    public class CmsPageController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICmsPageService _cmspageService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICmsCateryService _cmscateryService;
        public CmsPageController(
            ICookieService cookieService,ILogService logService,
            ICmsPageService cmspageService, ISettingService settingService,ICmsCateryService cmscateryService)
        {
            _logService = logService;
            _cmspageService = cmspageService;
            _cookieService = cookieService;
            _settingService = settingService;
            _cmscateryService = cmscateryService;
        }

        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsPage")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsPage List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsPagePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsPagePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "CateryId", "ImageUrl", "AllowComment", "IsFeatured", "ShowInHomePage", "PublishDate", "EndDate", "SortOrder", "Status" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsPageTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsPageTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsPageTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _cmspageService.GetCmsPages(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/CmsPage/ShowImage
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsPage Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CmsPageViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CmsPage/Details/5
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsPage Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmspage = _cmspageService.GetCmsPageById(id.Value, langId);
            
            if (cmspage == null || cmspage.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmspage.Page = page;
            return PartialView("Details", cmspage);
        }

        // GET: ControlPanel/CmsPage/Create
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsPage Create Get")]
        public IActionResult ShowCreate(int languageId=0)
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

        // POST: ControlPanel/CmsPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsPage Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id", "Name", "Description", "ImageUrl", "CateryId","ShortDescription", "Keyword", "AllowComment", "IsFeatured","ShowInHomePage", "PublishDate", "EndDate", "SortOrder", "Status","LanguageId")]
            CmsPageViewModel CmsPageViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CmsPageViewModel.LanguageId == 0)
                        CmsPageViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    CmsPageViewModel.CreatedBy = User.Identity?.Name;                    
                    _cmspageService.AddCmsPage(CmsPageViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsPage Dic");
                    return View(CmsPageViewModel);
                }
            }
            return View(CmsPageViewModel);
        }

        // GET: ControlPanel/CmsPage/Edit/5
        [AuditLogFilter(ActionDescription = "CmsPage Edit Get")]
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Edit")]
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
         
            var cmspage = _cmspageService.GetCmsPageById(id.Value, languageId);
            cmspage.LanguageId = languageId;
            if (cmspage == null || cmspage.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            cmspage.Page = page;
            cmspage.LanguageId = languageId;
            cmspage.ListCmsCaterys = _cmscateryService.GetAllCmsCaterys(languageId);
            return PartialView("Edit", cmspage);
        }

        // POST: ControlPanel/CmsPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsPage Edit Pot")]
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id", "Name", "Description", "ImageUrl","CateryId", "ShortDescription", "Keyword", "AllowComment", "IsFeatured","ShowInHomePage", "PublishDate", "EndDate", "SortOrder", "Status","LanguageId")]
            CmsPageViewModel cmspageViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var cmspage = _cmspageService.GetCmsPageById(cmspageViewModel.Id);
                    if (cmspage != null && cmspage.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (cmspageViewModel.LanguageId == 0)
                            cmspageViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _cmspageService.EditCmsPage(cmspageViewModel, cmspage);
                    }

                    return RedirectToAction(nameof(Index), new { page = cmspageViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsPage Dic (Post)");
                    return View(cmspageViewModel);
                }
            }
            return View(cmspageViewModel);
        }
        [AuditLogFilter(ActionDescription = "CmsPage Delete Get")]
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsPage/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmspage = _cmspageService.GetCmsPageById(id.Value, langId);
            if (cmspage == null || cmspage.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmspage.Page = page;
            return PartialView("Delete", cmspage);
        }

        // POST: ControlPanel/CmsPage/Delete/5
        [AuditLogFilter(ActionDescription = "CmsPage Delete Post")]
        [CustomAuthentication(PageName = "CmsPages", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsPage(int id, int Page)
        {
            var cmspage = _cmspageService.GetCmsPageById(id);
            if (cmspage != null && cmspage.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _cmspageService.DeleteCmsPage(cmspage);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
