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
    public class CalendarController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICalendarService _calendarService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CalendarController(
            ICookieService cookieService,ILogService logService,
            ICalendarService calendarService, ISettingService settingService)
        {
            _logService = logService;
            _calendarService = calendarService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Calendars", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Calendar")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Calendars", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Calendar List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CalendarPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CalendarPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "Name","Type", "StartDate", "EndDate", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CalendarTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CalendarTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CalendarTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _calendarService.GetCalendars(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Calendars", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Calendar/Details/5
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Calendar Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var calendar = _calendarService.GetCalendarById(id.Value, langId);
            
            if (calendar == null || calendar.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            calendar.Page = page;
            return PartialView("Details", calendar);
        }

        // GET: ControlPanel/Calendar/Create
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Calendar Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Calendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Calendar Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,StartDate,EndDate,Name,LanguageId,Status","Type","Description","Status")]
            CalendarViewModel CalendarViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CalendarViewModel.StartDate > CalendarViewModel.EndDate)
                        return Content("AddMassegeErrorInvalidDates", "text/plain");

                    if (CalendarViewModel.LanguageId == 0)
                        CalendarViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    CalendarViewModel.CreatedBy = User.Identity?.Name;                    
                    _calendarService.AddCalendar(CalendarViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Calendar Dic");
                    return View(CalendarViewModel);
                }
            }
            return View(CalendarViewModel);
        }

        // GET: ControlPanel/Calendar/Edit/5
        [AuditLogFilter(ActionDescription = "Calendar Edit Get")]
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Edit")]
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
            ViewBag.LangId = languageId;
            var calendar = _calendarService.GetCalendarById(id.Value, languageId);
            calendar.LanguageId = languageId;
            if (calendar == null || calendar.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            calendar.Page = page;
            return PartialView("Edit", calendar);
        }

        // POST: ControlPanel/Calendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Calendar Edit Pot")]
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id,StartDate,EndDate,Name,LanguageId,Status","Type","Description","Status")]
            CalendarViewModel calendarViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (calendarViewModel.StartDate > calendarViewModel.EndDate)
                        return Content("EditMassegeErrorInvalidDates", "text/plain");

                    var calendar = _calendarService.GetCalendarById(calendarViewModel.Id);
                    if (calendar != null && calendar.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (calendarViewModel.LanguageId == 0)
                            calendarViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _calendarService.EditCalendar(calendarViewModel, calendar);
                    }

                    return RedirectToAction(nameof(Index), new { page = calendarViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Calendar Dic (Post)");
                    return View(calendarViewModel);
                }
            }
            return View(calendarViewModel);
        }
        [AuditLogFilter(ActionDescription = "Calendar Delete Get")]
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Delete")]

        // GET: ControlPanel/Calendar/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var calendar = _calendarService.GetCalendarById(id.Value, langId);
            if (calendar == null || calendar.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            calendar.Page = page;
            return PartialView("Delete", calendar);
        }

        // POST: ControlPanel/Calendar/Delete/5
        [AuditLogFilter(ActionDescription = "Calendar Delete Post")]
        [CustomAuthentication(PageName = "Calendars", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCalendar(int id, int Page)
        {
            var calendar = _calendarService.GetCalendarById(id);
            if (calendar != null && calendar.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _calendarService.DeleteCalendar(calendar);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
