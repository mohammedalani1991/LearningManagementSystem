using ClosedXML.Excel;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.Reports.ISerives;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System;
using LearningManagementSystem.Core;
using System.Data;

namespace LearningManagementSystem.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class TrainerRateMeasureReportsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IStringLocalizer<TrainerRateMeasureReportsController> _localizer;
        private readonly ICookieService _cookieService;
        private readonly ITrainerReportService _trainerReportService;

        public TrainerRateMeasureReportsController(ICookieService cookieService, ITrainerReportService trainerReportService, ISettingService settingService
            , ILogService logService, IStringLocalizer<TrainerRateMeasureReportsController> localizer)
        {
            _settingService = settingService;
            _logService = logService;
            _localizer = localizer;
            _cookieService = cookieService;
            _trainerReportService = trainerReportService;
        }

        [CustomAuthentication(PageName = "TrainerRateMeasureReports", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Currencys
        [CustomAuthentication(PageName = "TrainerRateMeasureReports", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Trainer Rate Measure Reports")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, FilterViewModel filter)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.TrainerRateMeasureReportsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.TrainerRateMeasureReportsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            if (filter.FromDate == default && !filter.SecondOpen)
                filter.FromDate = DateTime.Now.AddYears(-1);
            if (filter.ToDate == default && !filter.SecondOpen)
                filter.ToDate = DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(filter.FromToDate))
            {
                var fromToDates = filter.FromToDate.Replace("-", "/").Split(" / ");
                string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                filter.FromDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                filter.ToDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
            }

            ViewBag.Courses = filter.Courses;
            ViewBag.SectionName = filter.SectionName;
            ViewBag.Teacher = filter.Teacher;
            ViewBag.Semester = filter.Semester;
            ViewBag.FromDate = filter.FromDate;
            ViewBag.ToDate = filter.ToDate;
            ViewBag.CourseCategory = filter.CourseCategory;

            var result = _trainerReportService.GetTrainerRateMeasureReports(searchText, page, languageId, pagination, filter);

            return PartialView("_Index", result);
        }

        [HttpGet]
        public ActionResult GetItems(int? page, int id, string user)
        {
            try
            {
                var result = _trainerReportService.GetAcademicItems(page ?? 1, id, user);

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;
                ViewBag.Id = id;
                ViewBag.User = user;

                return PartialView("_Items", result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [AuditLogFilter(ActionDescription = "DownLoad TrainerRateMeasure Report")]
        public ActionResult DownloadReport(FilterViewModel filter, string searchText)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                filter.LanguageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                filter.SearchText = searchText;

                if (!string.IsNullOrEmpty(filter.FromToDate))
                {
                    var fromToDates = filter.FromToDate.Replace("-", "/").Split(" / ");
                    string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                    filter.FromDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                    filter.ToDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = _trainerReportService.DownloadTrainerRateMeasureReports(filter, _localizer).Tables[0];
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["TrainerRateMeasureReports"] + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}
