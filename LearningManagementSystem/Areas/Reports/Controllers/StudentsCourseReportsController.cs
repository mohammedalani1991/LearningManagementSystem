﻿using System;
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
using LearningManagementSystem.Services.Reports.Service;
using System.IO;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Globalization;

namespace LearningManagementSystem.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class StudentsCourseReportsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IStringLocalizer<StudentsCourseReportsController> _localizer;
        private readonly ICookieService _cookieService;
        private readonly IReportsServies _reportsService;

        public StudentsCourseReportsController(ICookieService cookieService, IReportsServies reportsService, ISettingService settingService
            , ILogService logService, IStringLocalizer<StudentsCourseReportsController> localizer)
        {
            _settingService = settingService;
            _logService = logService;
            _localizer = localizer;
            _cookieService = cookieService;
            _reportsService = reportsService;
        }
        [CustomAuthentication(PageName = "StudentsCourseReports", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Currencys
        [CustomAuthentication(PageName = "StudentsCourseReports", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Students Reports")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table, FilterViewModel filter)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.StudentsCourseReportsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.StudentsCourseReportsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "CourseName", "CourseCategory", "TeacherName", "SemesterName", "FullName", "CourseMark", "SuccessMark", "Mark", "Evaluation", "Pass", "Attendance", "Warning", "Status", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.StudentsCourseReportsTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.StudentsCourseReportsTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.StudentsCourseReportsTable, table, 7);

            ViewBag.Table = val1;

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
            ViewBag.Teacher = filter.Teacher;
            ViewBag.SectionName = filter.SectionName;
            ViewBag.FromDate = filter.FromDate;
            ViewBag.ToDate = filter.ToDate;
            ViewBag.Gender = filter.Gender;
            ViewBag.Country = filter.Country;
            ViewBag.City = filter.City;
            ViewBag.AgeFrom = filter.AgeFrom;
            ViewBag.AgeTo = filter.AgeTo;
            ViewBag.Status = filter.Status;
            ViewBag.SuccessStatus = filter.SuccessStatus;
            ViewBag.AttendanceFrom = filter.AttendanceFrom;
            ViewBag.AttendanceTo = filter.AttendanceTo;
            ViewBag.WarningNumber = filter.WarningNumber;
            ViewBag.CourseCategory = filter.CourseCategory;

            var result = _reportsService.GetStudentsCourses(searchText, page, languageId, pagination, filter);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "StudentsCourseReports", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        [HttpGet]
        [AuditLogFilter(ActionDescription = "Download Students Course Reports")]
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
                    DataTable dt = _reportsService.DownloadStudentsCoursesReport(filter, _localizer).Tables[0];
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Students_Course_Reports"] + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
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
