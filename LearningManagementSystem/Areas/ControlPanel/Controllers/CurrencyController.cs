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
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public CurrencyController(ICookieService cookieService, ICurrencyService currencyService, ISettingService settingService, ILogService logService)
        {
            _currencyService = currencyService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }

        [CustomAuthentication(PageName = "Currency", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Currencys
        [CustomAuthentication(PageName = "Currency", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Currencys")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CurrencyPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CurrencyPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _currencyService.GetCurrencies(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/Currencys/Details/5
        [CustomAuthentication(PageName = "Currency", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var currency = _currencyService.GetCurrencyById(id.Value, languageId);
            if (currency == null || currency.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", currency);
        }

        // GET: ControlPanel/Currencys/Create
        [CustomAuthentication(PageName = "Currency", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Currencys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Currency", PermissionKey = "Create")]
        public async Task<IActionResult> Create(CurrencyViewModel currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    currency.CreatedOn = DateTime.Now;
                    currency.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (currency.LanguageId == 0)
                        currency.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = currency.LanguageId;

                    _currencyService.AddCurrency(currency);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Currency");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/Currencys/Edit/5
        [CustomAuthentication(PageName = "Currency", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var currency = _currencyService.GetCurrencyById(id.Value, languageId);
            if (currency == null || currency.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new CurrencyViewModel(currency));
        }

        // POST: ControlPanel/Currencys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Currency", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CurrencyViewModel currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _currencyService.GetCurrencyById(currency.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (currency.LanguageId == 0)
                        {
                            currency.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _currencyService.EditCurrency(currency, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Currency");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/Currencys/Delete/5
        [CustomAuthentication(PageName = "Currency", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _currencyService.GetCurrencyById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _currencyService.DeleteCurrency(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Currency");
                return null;
            }
        }
    }
}
