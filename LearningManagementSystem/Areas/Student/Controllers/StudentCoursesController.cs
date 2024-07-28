using LearningManagementSystem.Core;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LearningManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentCoursesController : Controller
    {
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStudentService _studentService;
        public StudentCoursesController(
            IEnrollStudentCourseService enrollStudentCourseService, 
            ICookieService cookieService, 
            ISettingService settingService, 
            IUserProfileService userProfileService,
            IStudentService studentService)
        {
            _enrollStudentCourseService = enrollStudentCourseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _userProfileService = userProfileService;
            _studentService = studentService;
        }

        [Authorize]
        public IActionResult GetData(int? page, int CourseID, int pagination = 10)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var studentId = -2000; //do not return any data
            if (studenDetails != null)
                studentId = studenDetails.Id;

            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (CourseID > 0)
                ViewBag.CourseID = CourseID;

            var val = _cookieService.GetCookie(Constants.Pagenation.StudentCoursesPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.StudentCoursesPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            ViewBag.StudentId = studentId;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _enrollStudentCourseService.GetEnrollStudentCourses(page,CourseID,studentId,0,0, languageId, pagination);
            ViewBag.StudentCourses = _enrollStudentCourseService.GetEnrollStudentCourseByStudentId(studentId, languageId);
            return PartialView("_Index", result);
        }
    }
}
