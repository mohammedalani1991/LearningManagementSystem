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
using Microsoft.AspNetCore.Mvc.Rendering;
using DataEntity.Models.EfModels;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class AttendancesController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IAttendancesService _attendancesService;

        public AttendancesController(
            ICookieService cookieService, ILogService logService, ISettingService settingService, IAttendancesService attendancesService)
        {
            _logService = logService;
            _cookieService = cookieService;
            _settingService = settingService;
            _attendancesService = attendancesService;
        }

        [CustomAuthentication(PageName = "CourseAttendances", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Attendances List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int CourseId)
        {
            ViewBag.Page = page ?? 1;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            ViewBag.CourseId = CourseId;
            DateTime value = DateTime.Now;
            if (DateTime.TryParse(_cookieService.GetCookie("DateValue"), out value))
                ViewBag.DateTime = value;
            else
                ViewBag.DateTime = DateTime.Now;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _attendancesService.GetAttendances(CourseId, searchText, page ?? 1, 10, languageId);
            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CourseAttendances", PermissionKey = "View")]
        public async Task<IActionResult> GetAttendances(int CourseId, DateTime dateTime)
        {
            var result = _attendancesService.GetAttendances(CourseId, dateTime);
            _cookieService.CreateCookie("DateValue", dateTime.ToString(), .0034);

            return Json(result);
        }

        [CustomAuthentication(PageName = "CourseAttendances", PermissionKey = "View")]
        public async Task<IActionResult> GetStudentAttendances(int CourseId, int id)
        {
            var result = _attendancesService.GetStudentAttendances(CourseId, id);
            return PartialView("_StudentAttendances", result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Add Student Attendance")]
        [CustomAuthentication(PageName = "CourseAttendances", PermissionKey = "Edit")]
        public IActionResult AddStudentAttendance(StudentAttendanceViewModel studentAttendance)
        {
            try
            {
                var checkIfDayIsValid = _attendancesService.checkIfDayIsValid(studentAttendance.CourseId, studentAttendance.Date.Value);
                var checkIfThereIsAClass = Boolean.Parse(_settingService.GetOrCreate("Check_If_There_Is_A_Class", "False").Value);

                if (checkIfDayIsValid || !checkIfThereIsAClass)
                {
                    if (studentAttendance.EnrollStudentCourseIds.Count == 0)
                        studentAttendance.EnrollStudentCourseIds.Add(0);
                    studentAttendance.CreatedBy = User.Identity?.Name;
                    studentAttendance.Status = (int)GeneralEnums.StatusEnum.Active;
                    for (int i = 0; i < studentAttendance.EnrollStudentCourseIds1.Count; i++)
                    {
                        var s = _attendancesService.GetStudentAttendanceById(studentAttendance.EnrollStudentCourseIds1[i], studentAttendance.Date.Value);
                        if (s == null)
                        {
                            studentAttendance.EnrollStudentCourseId = studentAttendance.EnrollStudentCourseIds1[i];
                            foreach (var item in studentAttendance.EnrollStudentCourseIds)
                            {
                                if (studentAttendance.EnrollStudentCourseIds1[i] == item)
                                {
                                    studentAttendance.IsPresent = true;
                                    studentAttendance.AbsenceNote = "";
                                    break;
                                }
                                else
                                {
                                    studentAttendance.IsPresent = false;
                                    studentAttendance.AbsenceNote = studentAttendance.AbsenceNotes.Count > i && studentAttendance.AbsenceNotes[i] != null ? studentAttendance.AbsenceNotes[i] : "";
                                }
                            }
                            _attendancesService.AddStudentAttendance(studentAttendance);
                        }
                        else
                        {
                            foreach (var item in studentAttendance.EnrollStudentCourseIds)
                            {
                                if (s.EnrollStudentCourseId == item)
                                {
                                    studentAttendance.IsPresent = true;
                                    studentAttendance.AbsenceNote = "";
                                    break;
                                }
                                else
                                {
                                    studentAttendance.IsPresent = false;
                                    studentAttendance.AbsenceNote = studentAttendance.AbsenceNotes.Count > i && studentAttendance.AbsenceNotes[i] != null ? studentAttendance.AbsenceNotes[i] : "";
                                }
                            }
                            _attendancesService.EditStudentAttendance(studentAttendance, s);
                        }
                    }

                    return Json(new { success = true, responseText = "success." });
                }
                return Json(new { success = false, responseText = "There is No Class Today." });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new About Dic");
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CourseAttendances", PermissionKey = "Edit")]
        public IActionResult DeleteStudentAttendance(int CourseId , DateTime Date)
        {
            try
            {
                _attendancesService.DeleteStudentAttendance(CourseId , Date);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Deleteing Attendance");
                return null;
            }
        }



            [AuditLogFilter(ActionDescription = "Teacher Attendances List")]
        public async Task<IActionResult> GetTeacherData(int? page, int CourseId)
        {
            ViewBag.Page = page ?? 1;

            ViewBag.CourseId = CourseId;
            DateTime value = DateTime.Now;
            if (DateTime.TryParse(_cookieService.GetCookie("DateValue"), out value))
                ViewBag.DateTime = value;
            else
                ViewBag.DateTime = DateTime.Now;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _attendancesService.GetTeacherAttendances(CourseId, page ?? 1, 10);
            return PartialView("_IndexTeacher", result);
        }

        [AuditLogFilter(ActionDescription = "Get Teacher Attendance")]
        public async Task<IActionResult> GetTeacherAttendance(int CourseId, DateTime date)
        {
            var result = _attendancesService.GetTeacherAttendance(CourseId, date);
            return Json(result);
        }

        [AuditLogFilter(ActionDescription = "Add Teacher Attendance")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAttendance(int CourseId, DateTime date, string note, bool attended)
        {
            try
            {
                var checkIfDayIsValid = _attendancesService.checkIfDayIsValid(CourseId, date);
                var checkIfThereIsAClass = Boolean.Parse(_settingService.GetOrCreate("Check_If_There_Is_A_Class", "False").Value);

                if (checkIfDayIsValid || !checkIfThereIsAClass)
                {
                    _attendancesService.AddAttendance(CourseId, date, note, attended);
                    return Json(new { success = true });
                }
                return Json(new { success = false, responseText = "There is No Class Today." });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Delete Teacher Attendance")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var addendence = _attendancesService.GetAttendanceById(id);
                if (addendence != null)
                {
                    _attendancesService.DeleteAttendance(addendence);
                    return Ok();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
