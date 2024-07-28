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
    public class CmsSliderController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICmsSliderService _cmssliderService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CmsSliderController(
            ICookieService cookieService,ILogService logService,
            ICmsSliderService cmssliderService, ISettingService settingService)
        {
            _logService = logService;
            _cmssliderService = cmssliderService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsSlider")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsSlider List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsSliderPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsSliderPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name", "Description", "ImageUrl", "Image2Url", "ReadMoreLink", "SortOrder", "CreatedOn", "CreatedBy", "Status" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsSliderTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsSliderTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsSliderTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _cmssliderService.GetCmsSliders(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/CmsSlider/ShowImage
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsSlider Image Details")]
        public IActionResult ShowImage(string ImageUrl,string Image2Url)
        {
            var result = new CmsSliderViewModel();
            if(!string.IsNullOrEmpty(ImageUrl))
            result.ImageUrl = ImageUrl;
            else
            result.Image2Url = Image2Url;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CmsSlider/Details/5
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsSlider Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmsslider = _cmssliderService.GetCmsSliderById(id.Value, langId);
            
            if (cmsslider == null || cmsslider.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmsslider.Page = page;
            return PartialView("Details", cmsslider);
        }

        // GET: ControlPanel/CmsSlider/Create
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsSlider Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            return PartialView("Create");
        }

        // POST: ControlPanel/CmsSlider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsSlider Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id", "Name", "Description", "ImageUrl","Image2Url", "ReadMoreLink", "SortOrder", "Status","LanguageId")]
            CmsSliderViewModel CmsSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CmsSliderViewModel.LanguageId == 0)
                        CmsSliderViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    CmsSliderViewModel.CreatedBy = User.Identity?.Name;                    
                    _cmssliderService.AddCmsSlider(CmsSliderViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsSlider Dic");
                    return View(CmsSliderViewModel);
                }
            }
            return View(CmsSliderViewModel);
        }

        // GET: ControlPanel/CmsSlider/Edit/5
        [AuditLogFilter(ActionDescription = "CmsSlider Edit Get")]
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Edit")]
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
         
            var cmsslider = _cmssliderService.GetCmsSliderById(id.Value, languageId);
            cmsslider.LanguageId = languageId;
            if (cmsslider == null || cmsslider.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            cmsslider.Page = page;
            ViewBag.LangId = langId;
            return PartialView("Edit", cmsslider);
        }

        // POST: ControlPanel/CmsSlider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsSlider Edit Pot")]
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id", "Name", "Description", "ImageUrl","Image2Url", "ReadMoreLink", "SortOrder", "Status","LanguageId")]
            CmsSliderViewModel cmssliderViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var cmsslider = _cmssliderService.GetCmsSliderById(cmssliderViewModel.Id);
                    if (cmsslider != null && cmsslider.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (cmssliderViewModel.LanguageId == 0)
                            cmssliderViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _cmssliderService.EditCmsSlider(cmssliderViewModel, cmsslider);
                    }

                    return RedirectToAction(nameof(Index), new { page = cmssliderViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsSlider Dic (Post)");
                    return View(cmssliderViewModel);
                }
            }
            return View(cmssliderViewModel);
        }
        [AuditLogFilter(ActionDescription = "CmsSlider Delete Get")]
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsSlider/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var cmsslider = _cmssliderService.GetCmsSliderById(id.Value, langId);
            if (cmsslider == null || cmsslider.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            cmsslider.Page = page;
            return PartialView("Delete", cmsslider);
        }

        // POST: ControlPanel/CmsSlider/Delete/5
        [AuditLogFilter(ActionDescription = "CmsSlider Delete Post")]
        [CustomAuthentication(PageName = "CmsSliders", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsSlider(int id, int Page)
        {
            var cmsslider = _cmssliderService.GetCmsSliderById(id);
            if (cmsslider != null && cmsslider.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _cmssliderService.DeleteCmsSlider(cmsslider);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
