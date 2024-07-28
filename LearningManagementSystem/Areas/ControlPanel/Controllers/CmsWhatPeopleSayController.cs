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
    public class CmsWhatPeopleSayController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICmsWhatPeopleSayService _cmsWhatPeopleSayService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CmsWhatPeopleSayController(
            ICookieService cookieService,ILogService logService,
            ICmsWhatPeopleSayService cmsWhatPeopleSayService, ISettingService settingService)
        {
            _logService = logService;
            _cmsWhatPeopleSayService = cmsWhatPeopleSayService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsWhatPeopleSay")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsWhatPeopleSayPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsWhatPeopleSayPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "PersonName", "PersonDetails", "Description", "ImageUrl", "ShowInHomePage", "CreatedOn", "CreatedBy", "Status" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsWhatPeopleSayTable);

            if (val1 == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsWhatPeopleSayTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsWhatPeopleSayTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _cmsWhatPeopleSayService.GetCmsWhatPeopleSays(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/CmsWhatPeopleSay/ShowImage
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CmsWhatPeopleSayViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CmsWhatPeopleSay/Details/5
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Details")]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Details")]
        public async Task<IActionResult> ShowDetails(int? id,  int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.LangId = languageId;

            var cmsWhatPeopleSay = _cmsWhatPeopleSayService.GetCmsWhatPeopleSayById(id.Value, languageId);
            
            if (cmsWhatPeopleSay == null || cmsWhatPeopleSay.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }


            return PartialView("Details", cmsWhatPeopleSay);
        }

        // GET: ControlPanel/CmsWhatPeopleSay/Create
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

    
            return PartialView("Create");
        }

        // POST: ControlPanel/CmsWhatPeopleSay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id", "PersonName","PersonDetails", "Description", "ImageUrl", "ShowInHomePage", "Status","LanguageId")]
            CmsWhatPeopleSayViewModel CmsWhatPeopleSayViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CmsWhatPeopleSayViewModel.LanguageId == 0)
                        CmsWhatPeopleSayViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    CmsWhatPeopleSayViewModel.CreatedBy = User.Identity?.Name;                    
                    _cmsWhatPeopleSayService.AddCmsWhatPeopleSay(CmsWhatPeopleSayViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsWhatPeopleSay Dic");
                    return View(CmsWhatPeopleSayViewModel);
                }
            }
            return View(CmsWhatPeopleSayViewModel);
        }

        // GET: ControlPanel/CmsWhatPeopleSay/Edit/5
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Edit Get")]
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId=0)
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
         
            var cmsWhatPeopleSay = _cmsWhatPeopleSayService.GetCmsWhatPeopleSayById(id.Value, languageId);
            cmsWhatPeopleSay.LanguageId = languageId;
            if (cmsWhatPeopleSay == null || cmsWhatPeopleSay.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
           
            cmsWhatPeopleSay.LanguageId = languageId;
            ViewBag.LangId = langId;

            return PartialView("Edit", cmsWhatPeopleSay);
        }

        // POST: ControlPanel/CmsWhatPeopleSay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Edit Pot")]
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id", "PersonName","PersonDetails", "Description", "ImageUrl", "ShowInHomePage", "Status","LanguageId")]
            CmsWhatPeopleSayViewModel cmsWhatPeopleSayViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var cmsWhatPeopleSay = _cmsWhatPeopleSayService.GetCmsWhatPeopleSayById(cmsWhatPeopleSayViewModel.Id);
                    if (cmsWhatPeopleSay != null && cmsWhatPeopleSay.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (cmsWhatPeopleSayViewModel.LanguageId == 0)
                            cmsWhatPeopleSayViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _cmsWhatPeopleSayService.EditCmsWhatPeopleSay(cmsWhatPeopleSayViewModel, cmsWhatPeopleSay);
                    }

                    return RedirectToAction(nameof(Index), new { page = cmsWhatPeopleSayViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsWhatPeopleSay Dic (Post)");
                    return View(cmsWhatPeopleSayViewModel);
                }
            }
            return View(cmsWhatPeopleSayViewModel);
        }
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Delete Get")]
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsWhatPeopleSay/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var cmsWhatPeopleSay = _cmsWhatPeopleSayService.GetCmsWhatPeopleSayById(id.Value, languageId);
            if (cmsWhatPeopleSay == null || cmsWhatPeopleSay.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;
            return PartialView("Delete", cmsWhatPeopleSay);
        }

        // POST: ControlPanel/CmsWhatPeopleSay/Delete/5
        [AuditLogFilter(ActionDescription = "CmsWhatPeopleSay Delete Post")]
        [CustomAuthentication(PageName = "CmsWhatPeopleSays", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsWhatPeopleSay(int id)
        {
            var cmsWhatPeopleSay = _cmsWhatPeopleSayService.GetCmsWhatPeopleSayById(id);
            if (cmsWhatPeopleSay != null && cmsWhatPeopleSay.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _cmsWhatPeopleSayService.DeleteCmsWhatPeopleSay(cmsWhatPeopleSay);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
