
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;


namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class CourseHomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICoursePackagesService _CoursePackagesService;
        private readonly ITrainerService _trainerService;
        private readonly ICourseService _courseService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;

        public CourseHomeController(
            ICookieService cookieService,
            ILogService logService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            IUserProfileService userProfileService,
            ICoursePackagesService CoursePackagesService,
            ITrainerService trainerService, ICourseService courseService,IEnrollStudentCourseService enrollStudentCourseService
            )
        {
            _logService = logService;
            _cookieService = cookieService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _userProfileService = userProfileService;
            _CoursePackagesService = CoursePackagesService;
            _trainerService = trainerService;
            _courseService = courseService;
            _enrollStudentCourseService = enrollStudentCourseService;
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "Trainer Home Page")]
        public async Task<IActionResult> Index(int EnrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var EnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId, langId);
            ViewBag.CourseName = EnrollTeacherCourse.CourseName;
            ViewBag.CourseId = EnrollTeacherCourse.CourseId;
            ViewBag.EnrollTeacherCourseById = EnrollTeacherCourse.Id;
            return PartialView("_Index" , EnrollTeacherCourse);
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "Trainer Home Page")]
        [CheckSuperAdmin(PageName = "Packages")]
        public async Task<IActionResult> IndexCoursesPackages(int CoursesPackagesID, int EnrollTeacherCourseId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);
            var TeacherId = 0;
            if (TrainerDetails != null)
                TeacherId = TrainerDetails.Id;
            else
                TeacherId = -2000; //do not return any data

            var CoursePackages = _CoursePackagesService.GetCoursePackageById(CoursesPackagesID, langId);
            if (CoursePackages == null)
                return NotFound();

            var EnrollTeacherCourseData = _CoursePackagesService.GetCoursePackagesRelationByPackageIdAndTeacherId(CoursesPackagesID, TeacherId, langId);
            if (EnrollTeacherCourseData.Count > 0)
            {
                if (EnrollTeacherCourseId == 0)
                {

                    ViewBag.CourseName = EnrollTeacherCourseData[0].CourseName;
                    ViewBag.CourseId = EnrollTeacherCourseData[0].CourseId;
                    ViewBag.EnrollTeacherCourseId = EnrollTeacherCourseData[0].Id;
                }
                else
                {
                    var EnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId);
                    ViewBag.CourseName = EnrollTeacherCourse.CourseName;
                    ViewBag.CourseId = EnrollTeacherCourse.CourseId;
                    ViewBag.EnrollTeacherCourseId = EnrollTeacherCourse.Id;

                }


                ViewBag.PackageName = CoursePackages.PackageName;
                ViewBag.PackagesID = CoursePackages.Id;
                ViewBag.CoursesPackagesList = EnrollTeacherCourseData;
            }
            else
            {
                return NotFound();
            }
            return PartialView("_IndexCoursesPackages");
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "Trainer Home Page")]
        public async Task<IActionResult> GetSupportCourse(int EnrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var EnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId, langId);
            ViewBag.CourseName = EnrollTeacherCourse.CourseName;
            ViewBag.CourseId = EnrollTeacherCourse.CourseId;
            ViewBag.EnrollTeacherCourseById = EnrollTeacherCourse.Id;
            return PartialView("_SupportCourse", EnrollTeacherCourse);
        }

        public IActionResult Details(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _courseService.GetCourseByEnrollTeacherCourseId(id, languageId);
            ViewBag.EnrollTeacherCourseId = id;
            ViewBag.enrollTeacherCourseData = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, languageId);
            return PartialView("_Details", result);
        }
    }
}
