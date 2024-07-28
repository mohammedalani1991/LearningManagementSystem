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
    public class CountryController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICountryService _countryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICityService _Cityervice;

        public CountryController(
            ICookieService cookieService, ILogService logService, ICityService cityervice,
            ICountryService countryService, ISettingService settingService)
        {
            _logService = logService;
            _countryService = countryService;
            _cookieService = cookieService;
            _settingService = settingService;
            _Cityervice = cityervice;
        }

        [CustomAuthentication(PageName = "Countries", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Countryes")]
        public async Task<IActionResult> Index(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CountryPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CountryPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _countryService.GetCountryes(searchText, page, languageId, pagination);
            return View(result);
        }

        // GET: ControlPanel/MasterCountries/Details/5
        [CustomAuthentication(PageName = "Countries", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Countries Details")]
        public async Task<IActionResult> Details(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var lookup = _countryService.GetCountryById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            lookup.Cities = _Cityervice.GetCities(lookup.Id, languageId);
            lookup.Page = page;
            lookup.LanguageId = languageId;
            ViewBag.LangId = languageId;
            return View(lookup);
        }

        // POST: ControlPanel/MasterCountries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Countries", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Countries Create Post")]
        [HttpPost]
        public IActionResult Create(CountryViewModel countryViewModel)
        {
            try
            {
                countryViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                _countryService.AddCountry(countryViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add Countries");
                return View(countryViewModel);
            }
        }

        // POST: ControlPanel/MasterCountries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Countries", PermissionKey = "Edit")]
        [HttpPost]
        [AuditLogFilter(ActionDescription = "Countries Edit Post")]
        public IActionResult Edit(CountryViewModel countryViewModel)
        {
            try
            {
                var country = _countryService.GetCountryById(countryViewModel.Id);
                if (country != null && country.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _countryService.EditCountry(countryViewModel, country);
                }
                return RedirectToAction(nameof(Index), new { page = countryViewModel.Page });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Countries (Post)");
                return View(countryViewModel);
            }
        }

        // POST: ControlPanel/MasterCountries/Delete/5
        [AuditLogFilter(ActionDescription = "Countries Delete Post")]
        [CustomAuthentication(PageName = "Countries", PermissionKey = "Delete")]
        public IActionResult DeleteConfirmed(int id, int page)
        {
            try
            {
                var systemSettingDeleted = _countryService.GetCountryById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _countryService.DeleteCountry(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index), new { page = page });
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Countries");
                return RedirectToAction(nameof(Index));
            }
        }


        [CustomAuthentication(PageName = "Countries", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Countries Get Get")]
        public async Task<IActionResult> GetCountryByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _countryService.GetCountryById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }

        [HttpPost]
        public async Task<IActionResult> GetCountryCities(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var citiesHtml = "<option value=''> </option>";
            var cities = LookupHelper.GetCities(languageId, id);

            foreach (var citie in cities)
            {
                citiesHtml += "<option value='" + citie.Id + "'>" + citie.Name + "</option>";
            }
            return Json(new { result = citiesHtml });
        }
    }
}
