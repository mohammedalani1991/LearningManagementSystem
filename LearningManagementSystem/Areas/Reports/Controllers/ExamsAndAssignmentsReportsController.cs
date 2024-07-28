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

namespace LearningManagementSystem.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class ExamsAndAssignmentsReportsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IStringLocalizer<ExamsAndAssignmentsReportsController> _localizer;
        private readonly ICookieService _cookieService;
        private readonly IReportsServies _reportsServies;

        public ExamsAndAssignmentsReportsController(ICookieService cookieService, IReportsServies reportsServies, ISettingService settingService
            , ILogService logService, IStringLocalizer<ExamsAndAssignmentsReportsController> localizer)
        {
            _settingService = settingService;
            _logService = logService;
            _localizer = localizer;
            _cookieService = cookieService;
            _reportsServies = reportsServies;
        }
        [CustomAuthentication(PageName = "ExamsAndAssignmentsReports", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Currencys
        [CustomAuthentication(PageName = "ExamsAndAssignmentsReports", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Exams And Assignments Reports")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, FilterViewModel filter)
        {
            try
            {
                if (page == 0)
                    page = 1;

                ViewBag.Page = page;

                if (!string.IsNullOrWhiteSpace(searchText))
                    ViewBag.searchText = searchText;

                var val = _cookieService.GetCookie(Constants.Pagenation.ExamsAndAssignmentsReportsPagination);

                if (val == null && pagination == 0)
                    pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                else if (pagination != 0)
                    pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ExamsAndAssignmentsReportsPagination, pagination.ToString(), 7));
                else
                    pagination = int.Parse(val != "" ? val : "10");

                ViewBag.PaginationValue = pagination;

                List<string> tables = new List<string> { "Type", "Name", "CourseName", "TeacherName", "SemesterName", "SectionName", "PublicationDate", "PublicationEndDate", "Status", "CreatedOn", "NumberOfStudents", "NumberOfSubmitted", "NumberOfSuccess" };

                var val1 = _cookieService.GetCookie(Constants.TableFields.ExamsAndAssignmentsReportsTable);

                if (val1 == null && table == null)
                    val1 = _cookieService.CreateCookie(Constants.TableFields.ExamsAndAssignmentsReportsTable, tables, 7);
                else if (table != null)
                    val1 = _cookieService.CreateCookie(Constants.TableFields.ExamsAndAssignmentsReportsTable, table, 7);

                ViewBag.Table = val1;

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;


                if (filter.FromPublisheDate == default && !filter.SecondOpen)
                    filter.FromPublisheDate = DateTime.Now.AddYears(-1);
                if (filter.ToPublisheDate == default && !filter.SecondOpen)
                    filter.ToPublisheDate = DateTime.Now.AddDays(1);

                if (!string.IsNullOrEmpty(filter.FromToPublisheDate))
                {
                    var fromToDates = filter.FromToPublisheDate.Replace("-", "/").Split(" / ");
                    string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                    filter.FromPublisheDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                    filter.ToPublisheDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
                }

                if (filter.ReportType == null)
                    filter.ReportType = (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam;

                ViewBag.ReportType = filter.ReportType;
                ViewBag.Courses = filter.Courses;
                ViewBag.Teacher = filter.Teacher;
                ViewBag.Semester = filter.Semester;
                ViewBag.FromPublisheDate = filter.FromPublisheDate;
                ViewBag.ToPublisheDate = filter.ToPublisheDate;
                ViewBag.Status = filter.Status;
                ViewBag.MarkFrom = filter.MarkFrom;
                ViewBag.MarkTo = filter.MarkTo;
                ViewBag.SectionName = filter.SectionName;

                var result = _reportsServies.GetExamsAndAssignmentsReports(searchText, page, languageId, pagination, filter);

                return PartialView("_Index", result);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [CustomAuthentication(PageName = "CoursesReports", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        [HttpGet]
        [AuditLogFilter(ActionDescription = "Download Exams And Assignments Reports")]
        public ActionResult DownloadReport(FilterViewModel filter, string searchText)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                filter.LanguageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                filter.SearchText = searchText;

                if (!string.IsNullOrEmpty(filter.FromToPublisheDate))
                {
                    var fromToDates = filter.FromToPublisheDate.Replace("-", "/").Split(" / ");
                    string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy" };
                    filter.FromPublisheDate = DateTime.ParseExact(fromToDates[0], formats, CultureInfo.InvariantCulture);
                    filter.ToPublisheDate = DateTime.ParseExact(fromToDates[1], formats, CultureInfo.InvariantCulture);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = _reportsServies.DownloadExamsAndAssignmentsReports(filter, _localizer).Tables[0];
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Exams_And_Assignments_Reports"] + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
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
