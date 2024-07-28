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
using LearningManagementSystem.Services.Reports.ISerives;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Globalization;
using MailKit.Search;

namespace LearningManagementSystem.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class CoursesReportsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IStringLocalizer<CoursesReportsController> _localizer;
        private readonly ICookieService _cookieService;
        private readonly IReportsServies _reportsServies;
        private readonly IStudentReportService _studentReportService;

        public CoursesReportsController(ICookieService cookieService, IReportsServies reportsServies, ISettingService settingService
            , ILogService logService, IStringLocalizer<CoursesReportsController> localizer)
        {
            _settingService = settingService;
            _logService = logService;
            _localizer = localizer;
            _cookieService = cookieService;
            _reportsServies = reportsServies;
        }
        [CustomAuthentication(PageName = "CoursesReports", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Currencys
        [CustomAuthentication(PageName = "CoursesReports", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Students Reports")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, FilterViewModel filter)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CourseReportPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CourseReportPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "CourseName", "TeacherName", "AgeAllowedForRegistration", "CourseCategory", "SectionName", "GenderGroup", "CountOfAllowedStudent", "CountOfStudent", "Passed", "Attendance", "Warning", "EnrollLectures", "PublicationDate", "PublicationEndDate", "WorkStartDate", "WorkEndDate", "SemesterName", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CourseReportTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseReportTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseReportTable, table, 7);

            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            if (filter.FromDate == default && !filter.SecondOpen)
                filter.FromDate = DateTime.Now.AddYears(-1);
            if (filter.ToDate == default && !filter.SecondOpen)
                filter.ToDate = DateTime.Now.AddDays(1);
            if (filter.FromPublisheDate == default && !filter.SecondOpen)
                filter.FromPublisheDate = DateTime.Now.AddYears(-1);
            if (filter.ToPublisheDate == default && !filter.SecondOpen)
                filter.ToPublisheDate = DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(filter.FromToDate))
            {
                var fromToDates = filter.FromToDate.Replace("-", "/").Split(" / ");
                string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                filter.FromDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                filter.ToDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(filter.FromToPublisheDate))
            {
                var fromToDates = filter.FromToPublisheDate.Replace("-", "/").Split(" / ");
                string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                filter.FromPublisheDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                filter.ToPublisheDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
            }

            ViewBag.Courses = filter.Courses;
            ViewBag.Teacher = filter.Teacher;
            ViewBag.Semester = filter.Semester;
            ViewBag.SectionName = filter.SectionName;
            ViewBag.FromDate = filter.FromDate;
            ViewBag.ToDate = filter.ToDate;
            ViewBag.FromPublisheDate = filter.FromPublisheDate;
            ViewBag.ToPublisheDate = filter.ToPublisheDate;
            ViewBag.Gender = filter.Gender;
            ViewBag.AgeFrom = filter.AgeFrom;
            ViewBag.Status = filter.Status;
            ViewBag.CourseCategory = filter.CourseCategory;
            ViewBag.MarkAdoption = filter.MarkAdoption;
            ViewBag.CertificateAdoption = filter.CertificateAdoption;
           
            var result = _reportsServies.GetCourses(searchText, page, languageId, pagination, filter);
            ViewBag.StudentCount = result.StudentCount;
            ViewBag.SeccessCount = result.SeccessCount;
            ViewBag.FaildCount = result.FaildCount;
            ViewBag.WarningCount = result.WarningCount;
            ViewBag.Capacity = result.Capacity;

            return PartialView("_Index", result.EnrollTeacherCourses);
        }

        [CustomAuthentication(PageName = "CoursesReports", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        [HttpGet]
        [AuditLogFilter(ActionDescription = "Download Courses Report")]
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

                if (!string.IsNullOrEmpty(filter.FromToPublisheDate))
                {
                    var fromToDates = filter.FromToPublisheDate.Replace("-", "/").Split(" / ");
                    string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                    filter.FromPublisheDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                    filter.ToPublisheDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = _reportsServies.DownloadCoursesReport(filter, _localizer).Tables[0];
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Courses_Report"] + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
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
