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
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using System.IO;

namespace LearningManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class EnrollCoursesContentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        private readonly IUserProfileService _userProfileService;
        private readonly IEnrollStudentCourseService _enrollStudentCoursService;
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EnrollCoursesContentController(
            ICookieService cookieService,
            ILogService logService,
            IEnrollTeacherCourseService enrollTeacherCourseService, 
            ISettingService settingService,
            IEnrollSectionOfCourseService enrollSectionOfCourseService,
            IEnrollLectureService enrollLectureService,
            IEnrollCourseResourceService enrollCourseResourceService,
            IUserProfileService userProfileService,
            IEnrollStudentCourseService enrollStudentCoursService,
            IStudentService studentService,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _logService = logService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _enrollLectureService = enrollLectureService;
            _enrollCourseResourceService = enrollCourseResourceService;
            _userProfileService = userProfileService;
            _enrollStudentCoursService = enrollStudentCoursService;
            _studentService = studentService;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "Enroll Lectures Content Download")]
        public ActionResult DownloadDocument(string filePath)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                var fullPath = HttpUtility.UrlDecode(filePath);
                var fileName = Path.GetFileName(fullPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fullPath);
                return File(fileBytes, "application/force-download", fileName);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Download Enroll Lectures Documents");
                return NotFound();
            }
        }
        [Authorize]
        [AuditLogFilter(ActionDescription = "Courses Content List")]
        public async Task<IActionResult> GetData(int? page, int pagination, string table,int? CourseId)
        {

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var studentId = -2000; //do not return any data
            if (studenDetails != null)
                studentId = studenDetails.Id;

            if (page == 0)
                page = 1;

            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
            }
            else
            {
                ViewBag.CourseId = 0;
            }

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCoursesContentPagination);
            if (val == null && pagination == 0)
                pagination = 5;
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCoursesContentPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "5");

            ViewBag.PaginationValue = pagination;

        
            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollCoursesContentTable);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.StudentCourses = _enrollStudentCoursService.GetEnrollStudentCourseByStudentId(studentId, languageId);
            var result = _enrollSectionOfCourseService.GetEnrollSectionOfCourses(0,studentId, page, languageId, pagination, CourseId);

            return PartialView("_Index", result);
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "List Enroll Courses Content")]
        public async Task<IActionResult> Index()
        {
            return PartialView("_Index");
        }



       
        [Authorize]
        [AuditLogFilter(ActionDescription = "Courses Content Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var section = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById(id.Value, langId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var Course = _enrollTeacherCourseService.GetEnrollTeacherCourseById(section.EnrollCourseId, langId);
            section.EnrollLecture = _enrollLectureService.GetEnrollLectureBySectionId(section.Id, langId);
            var EnrollCoursesContentViewModel = new EnrollCoursesContentViewModel();
            var EnrollTeacherCourse = new EnrollTeacherCourseViewModel();
            EnrollTeacherCourse.Id = section.EnrollCourseId;
            EnrollTeacherCourse.LanguageId = section.LanguageId;
            EnrollTeacherCourse.CourseName = Course.CourseName;
            EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel= EnrollTeacherCourse;
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel = new List<EnrollSectionOfCourseViewModel>();
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Add(section);

            ViewBag.LangId = langId;
            return PartialView("Details", EnrollCoursesContentViewModel);
        }

    }
}
