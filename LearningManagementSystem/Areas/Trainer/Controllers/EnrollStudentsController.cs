using System;
using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;
using System.Collections.Generic;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.ViewModels;
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollStudentsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IUserProfileService _userProfileService;
        public EnrollStudentsController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            IEnrollStudentCourseService enrollStudentCourseService,
            ITrainerService trainerService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            IUserProfileService userProfileService
            )
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _trainerService = trainerService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _userProfileService = userProfileService;
        }
        [CustomAuthentication(PageName = "EnrollStudents", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enroll Students List")]
        public async Task<IActionResult> GetData(int? page, int pagination, string table, int? CourseId)
        {

          
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TeacherId = -2000; //do not return any data
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);
            if (TrainerDetails != null)
                TeacherId = TrainerDetails.Id;
          

            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
            }
            else
            {
                ViewBag.CourseId = 0;
            }

            if (page == 0)
                page = 1;

            ViewBag.Page = page;


            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollStudentsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollStudentsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");



            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "StudentId", "CourseId", "Price", "IsPass", "Status", "CreatedOn"};

            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollStudentsTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollStudentsTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollStudentsTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.TrainerCourses = _enrollTeacherCourseService.GetCourseByTeacherId(TrainerDetails.Id, languageId);
            ViewBag.CountAttendance = _enrollStudentCourseService.GetAttendanceDays(CourseId??0);
            var result = _enrollStudentCourseService.GetEnrollStudentCourses(page, 0,0, TeacherId, CourseId, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "EnrollStudents", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enroll Students")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

      

    }
}
