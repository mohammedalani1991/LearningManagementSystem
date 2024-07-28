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

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollCoursesController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly IEnrollCourseTimeService _enrollCourseTimeService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ISectionOfCourseService _sectionOfCourseService;
        private readonly ILectureService _lectureService;
        private readonly ICourseResourceService _courseResourceService;
        private readonly ITrainerService _trainerService;
        private readonly ISemesterService _semesterService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourse;
        private readonly IUserProfileService _userProfileService;
        private readonly ICoursePackagesService _CoursePackagesService;

        public EnrollCoursesController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            IEnrollCourseTimeService enrollCourseTimeService,
            ISectionOfCourseService sectionOfCourseService,
            ILectureService lectureService,
            ICourseResourceService courseResourceService,
            ITrainerService trainerService,
            ISemesterService semesterService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollSectionOfCourseService enrollSectionOfCourse,
            IUserProfileService userProfileService,
            ICoursePackagesService CoursePackagesService
            )
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollCourseTimeService = enrollCourseTimeService;
            _sectionOfCourseService = sectionOfCourseService;
            _lectureService = lectureService;
            _courseResourceService = courseResourceService;
            _trainerService = trainerService;
            _semesterService = semesterService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollSectionOfCourse = enrollSectionOfCourse;
            _userProfileService= userProfileService;
            _CoursePackagesService = CoursePackagesService;
        }
        
        [CustomAuthentication(PageName = "EnrollCourses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enroll Course List")]
        [CheckSuperAdmin(PageName = "Packages")]
        public async Task<IActionResult> GetCoursesPackagesData(int? page, int pagination, string table)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);
            var TeacherId = 0;
            if (TrainerDetails != null)
                TeacherId = TrainerDetails.Id;
            else
                TeacherId = -2000; //do not return any data


            if (page == 0)
                page = 1;

            ViewBag.Page = page;
            pagination = int.MaxValue;
            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "PackageName", "Courses", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CoursePackagesTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePackagesTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePackagesTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
     
            var result = await _CoursePackagesService.GetCoursePackages(false ,"", page, languageId, pagination, TeacherId,0);
            return PartialView("_IndexCoursePackages", result);
        }

        [CustomAuthentication(PageName = "EnrollCourses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enroll Course List")]
        public async Task<IActionResult> GetData(int? page, int? TeacherId, int pagination, string table, int? CourseId)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);
            if (TrainerDetails != null)
                TeacherId = TrainerDetails.Id;
            else
                TeacherId = -2000; //do not return any data

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


            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollmentCoursePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollmentCoursePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");


            pagination = 6;
            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "TeacherId", "CourseId", "SemesterId", "SectionName", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollmentCourseTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollmentCourseTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollmentCourseTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            //ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            ViewBag.TrainerCourses = _enrollTeacherCourseService.GetCourseByTeacherId(TrainerDetails.Id, languageId);
            var result = _enrollTeacherCourseService.GetEnrollTeacherCourses(page, languageId, pagination, CourseId, TeacherId);

            return PartialView("_Index", result);
        }

        public async Task<IActionResult> GetSupportData(int? page)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);

            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _enrollTeacherCourseService.GetEnrollTeacherSupportCourses(page, languageId, 10, TrainerDetails.Id);
            return PartialView("_IndexSupport", result);
        }

        [CustomAuthentication(PageName = "EnrollCourses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enroll Courses")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

      
        [CustomAuthentication(PageName = "EnrollCourses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enroll Course Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id.Value, langId);
            if (enrollTeacherCourse == null || enrollTeacherCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            enrollTeacherCourse.TeacherName = _trainerService.GetTrainerById(enrollTeacherCourse.TeacherId, langId).Contact.FullName;
            if (enrollTeacherCourse.SemesterId != null && enrollTeacherCourse.SemesterId != 0)
                enrollTeacherCourse.SemesterName = _semesterService.GetSemesterById(enrollTeacherCourse.SemesterId.Value, langId).Name;
            enrollTeacherCourse.ListEnrollCourseTime = _enrollCourseTimeService.GetEnrollCourseTimeByEnrollTeacherCourseId(enrollTeacherCourse.Id);


            var Course = _courseService.GetCourseById(enrollTeacherCourse.CourseId, langId);
            var EnrollmentCourseViewModel = new EnrollmentCourseViewModel();
            var Courses = new CourseViewModel();
            if (Course != null)
            {
                Courses.Id = Course.Id;
                Courses.CourseName = Course.CourseName;
            }
            Courses.LanguageId = enrollTeacherCourse.LanguageId;
            EnrollmentCourseViewModel.CourseViewModel = Courses;
            EnrollmentCourseViewModel.EnrollTeacherCourseViewModel = enrollTeacherCourse;


            ViewBag.LangId = langId;
            return PartialView("Details", EnrollmentCourseViewModel);
        }
    }
}
