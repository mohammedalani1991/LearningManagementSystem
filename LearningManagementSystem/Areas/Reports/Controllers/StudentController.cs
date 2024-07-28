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
using System.Linq;
using System.Globalization;

namespace LearningManagementSystem.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class StudentController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IStudentService _studentService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly IBalanceHistoryService _balanceHistoryService;
        private readonly ICourseService _courseService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollCourseTimeService _enrollCourseTimeService;
        private readonly ISemesterService _semesterService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly IEnrollCourseExamService _enrollCourseExamService;
        private readonly IEnrollAssignmentService _enrollAssignmentService;
        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
        private readonly IAttendancesService _attendancesService;
        private readonly IEnrollCourseQuizService _enrollCourseQuizService;
        private readonly ICalendarService _calendarService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollStudentAlertService _enrollStudentAlertService;
        private readonly ICookieService _cookieService;
        private readonly IStudentReportService _studentReportService;

        public StudentController(ICookieService cookieService, IStudentReportService studentReportService, ISettingService settingService
            , ILogService logService, IStudentService studentService, IEnrollStudentCourseService enrollStudentCourseService
            , IEnrollSectionOfCourseService enrollSectionOfCourseService, IBalanceHistoryService balanceHistoryService, ICourseService courseService,
            IEnrollTeacherCourseService enrollTeacherCourseService, IEnrollCourseTimeService enrollCourseTimeService, ISemesterService semesterService
            , ITrainerService trainerService, IEnrollStudentExamService enrollStudentExamService, IEnrollCourseExamService enrollCourseExamService
            , IEnrollAssignmentService enrollAssignmentService, ICoursePrerequisiteService coursePrerequisiteService, IAttendancesService attendancesService,
            IEnrollCourseQuizService enrollCourseQuizService, ICalendarService calendarService, IEnrollCourseResourceService enrollCourseResourceService
            , IEnrollLectureService enrollLectureService, IEnrollStudentAlertService enrollStudentAlertService)
        {
            _settingService = settingService;
            _logService = logService;
            _studentService = studentService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _balanceHistoryService = balanceHistoryService;
            _courseService = courseService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollCourseTimeService = enrollCourseTimeService;
            _semesterService = semesterService;
            _trainerService = trainerService;
            _enrollStudentExamService = enrollStudentExamService;
            _enrollCourseExamService = enrollCourseExamService;
            _enrollAssignmentService = enrollAssignmentService;
            _coursePrerequisiteService = coursePrerequisiteService;
            _attendancesService = attendancesService;
            _enrollCourseQuizService = enrollCourseQuizService;
            _calendarService = calendarService;
            _enrollCourseResourceService = enrollCourseResourceService;
            _enrollLectureService = enrollLectureService;
            _enrollStudentAlertService = enrollStudentAlertService;
            _cookieService = cookieService;
            _studentReportService = studentReportService;
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "View")]
        public IActionResult Index(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var student = _studentService.GetStudentModalById(id, languageId);
            return View(student);
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "Courses")]
        public IActionResult GetEnrollStudentCourses(int? page, int studentId, int pagination = 10)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

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

            var result = _enrollStudentCourseService.GetEnrollStudentCourses(page, null, studentId, 0, 0, languageId, pagination);
            return PartialView("_EnrollStudentCourses", result);
        }

        [AuditLogFilter(ActionDescription = "Courses Content List")]
        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "View")]
        public async Task<IActionResult> GetEnrollSectionOfCourses(int? page, int pagination, string table, int studentId)
        {
            if (page == 0)
                page = 1;

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
            ViewBag.StudentCourses = _enrollStudentCourseService.GetEnrollStudentCourseByStudentId(studentId, languageId);
            var result = _enrollSectionOfCourseService.GetEnrollSectionOfCourses(0, studentId, page, languageId, pagination, null);
            return PartialView("_EnrollSectionOfCourses", result);
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "PaymentHistory")]
        public IActionResult GetStudentBalanceHistory(int? page, int studentId, FilterViewModel filter)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.StudentId = studentId;

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

            ViewBag.Course = filter.Course;
            ViewBag.Teacher = filter.Teacher;
            ViewBag.FromDate = filter.FromDate;
            ViewBag.ToDate = filter.ToDate;
            ViewBag.Status = filter.Status;
            ViewBag.Type = filter.Type;

            var result = _balanceHistoryService.GetStudentPayments(studentId, page ?? 1, languageId, 10, filter);
            ViewBag.Amount = result.Amount;
            return PartialView("_BalanceHistory", result.SenangPayViewModels);
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "Certificates")]
        public async Task<IActionResult> GetStudentCertificates(int studentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.StudentId = studentId;

            var result = _studentService.GetStudentCertificates(studentId, languageId);
            return PartialView("_StudentCertificates", result);
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "Certificates")]
        public IActionResult DownloadCertificates(int courseId, int templatetId, int studentId)
        {
            var course = _enrollStudentCourseService.GetEnrollStudentCourseById(courseId);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (course.StudentId == studentId)
            {
                var baseUri = $"{Request.Scheme}://{Request.Host}/";
                var stream = _enrollStudentCourseService.GetCertificate(course, templatetId, languageId, baseUri);
                return File(stream, "application/pdf", course.Course.CourseName + ".pdf");
            }
            return null;
        }

        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "Calendar")]
        public async Task<IActionResult> GetCalendar(int studentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.StudentId = studentId;

            return PartialView("_Calendar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCalendarDataForProfile(int studentId, string Name, int? TypeID, DateTime startDate, DateTime endDate)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = await _calendarService.GetCalendarsForStudent(null, studentId, Name, startDate, endDate, languageId, TypeID);
            return Json(new { data = result, lang = languageId });
        }


        public IActionResult ShowCourseDetiles(int enrollStudentCourseId)
        {
            ViewBag.EnrollStudentCourseId = enrollStudentCourseId;
            return PartialView("_CourseDetails");
        }

        public IActionResult GetDetails(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId, langId);
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
            return PartialView("Details/Index", EnrollmentCourseViewModel);
        }

        public IActionResult ShowLessons(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.studentId = null;
            ViewBag.CourseLessonQuiz = new EnrollCourseExam();
            ViewBag.EnrollStudentCourseId = enrollStudentCourseId;

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var enrollSectionOfCourse = _enrollSectionOfCourseService.GetEnrollSectionByEnrollTeacherCourseId(enrollStudentCourse.CourseId, languageId);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId, languageId);

            ViewBag.Enroll = enrollStudentCourse.Id != 0;
            ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
            var QuizLessons = _enrollCourseExamService.GetEnrollCourseQuizByEnrollTeacherCourseId(enrollTeacherCours.Id, enrollSectionOfCourse.FirstOrDefault()?.EnrollLectures.FirstOrDefault()?.Id, languageId);

            foreach (var Exams in QuizLessons)
            {
                var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(enrollStudentCourse.StudentId, enrollTeacherCours.Id);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                    Exams.EnrollStudentExams.Add(EnrollStudentExam);
            }
            ViewBag.CourseLessonQuiz = QuizLessons;
            return PartialView("Details/_ShowLessons", enrollSectionOfCourse);
        }

        public IActionResult ShowLessonDetails(int id, int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var enrollSectionOfCourse = _enrollCourseResourceService.GetEnrollCourseResource(id, languageId);
            ViewBag.CourseLessonQuiz = new List<EnrollCourseExam>();
            var enrollLecture = _enrollLectureService.GetEnrollLectureById(id);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollLecture.EnrollCourseId);
            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);
            ViewBag.studentId = enrollStudentCourse.StudentId;

            ViewBag.Enroll = enrollStudentCourse.Id != 0;

            var QuizLessons = _enrollCourseExamService.GetEnrollCourseQuizByEnrollTeacherCourseId(enrollTeacherCours.Id, id, languageId);

            foreach (var Exams in QuizLessons)
            {
                var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(enrollStudentCourse.StudentId, enrollTeacherCours.Id);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                    Exams.EnrollStudentExams.Add(EnrollStudentExam);
            }
            return PartialView("Details/_ShowLessonDetails", enrollSectionOfCourse);
        }

        public IActionResult ShowExams(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);
            ViewBag.studentId = enrollStudentCourse.StudentId;

            var enrollCourseExams = _enrollCourseExamService.GetEnrollCourseExamByEnrollTeacherCourseId(enrollStudentCourse.CourseId, languageId);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId, languageId);

            foreach (var Exams in enrollCourseExams)
            {
                var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(enrollStudentCourse.StudentId, enrollStudentCourse.CourseId);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                    Exams.EnrollStudentExams.Add(EnrollStudentExam);
            }

            ViewBag.Enroll = enrollStudentCourse.Id != 0;
            ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;

            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.Online;
            return PartialView("Details/_ShowExams", enrollCourseExams);
        }

        public IActionResult ShowAssignments(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId, languageId);
            var enrollCourseExams = _enrollAssignmentService.GetEnrollAssignmentByEnrollTeacherCourseId(enrollStudentCourse.CourseId, enrollStudentCourse.StudentId, languageId);
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.Online;
            return PartialView("Details/_ShowAssignments", enrollCourseExams);
        }

        public IActionResult ShowAttendances(int enrollStudentCourseId)
        {
            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var id = _enrollTeacherCourseService.GetEnrollmentCourseIdFormStudentId(enrollStudentCourse.CourseId, enrollStudentCourse.StudentId);
            var result = _attendancesService.GetStudentAttendances(enrollStudentCourse.CourseId, id);
            return PartialView("Details/_ShowAttendances", result);
        }

        public IActionResult ShowPreRequests(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId);
            var coursePrerequisiteServices = _coursePrerequisiteService.GetViewModeCoursePrerequisiteByCourseId(enrollTeacherCours.CourseId, languageId);

            var enrollCoursePrerequisiteExams = _enrollCourseExamService.GetPrerequisiteEnrollCourseExam(enrollStudentCourse.CourseId, languageId);

            ViewBag.studentId = enrollStudentCourse.StudentId;
            foreach (var Exams in enrollCoursePrerequisiteExams)
            {
                var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(enrollStudentCourse.StudentId, enrollStudentCourse.CourseId);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                    Exams.EnrollStudentExams.Add(EnrollStudentExam);
            }

            ViewBag.Enroll = true;
            ViewBag.EnrollStudentCourse = enrollStudentCourse;
            ViewBag.PrerequisiteExam = enrollCoursePrerequisiteExams;

            return PartialView("Details/_ShowPreRequests", coursePrerequisiteServices);
        }

        public IActionResult ShowMarks(int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);
            var id = _enrollTeacherCourseService.GetEnrollmentCourseIdFormStudentId(enrollStudentCourse.CourseId, enrollStudentCourse.StudentId);
            var result = _enrollCourseQuizService.GetStudentQuizMarks(id, enrollStudentCourse.CourseId, languageId);
            ViewBag.QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);
            return PartialView("Details/_StudentsMarksDetails", result);
        }

        [CustomAuthentication(PageName = "EnrollStudentAlerts", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "EnrollStudentAlert List For Student Page")]
        public async Task<IActionResult> ShowAlerts(int? page, int enrollStudentCourseId)
        {
            ViewBag.Page = page ?? 1;

            var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(enrollStudentCourseId);

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _enrollStudentAlertService.GetAllowUserRates("", page ?? 1, languageId, 10, enrollStudentCourse.CourseId, enrollStudentCourseId);
            return PartialView("Details/_ShowAlerts", result);
        }

        [AuditLogFilter(ActionDescription = "ShowExpulsions List For Student Page")]
        [CustomAuthentication(PageName = "StudentsInfo", PermissionKey = "ExpulsionHistory")]
        public async Task<IActionResult> GetExpulsionsHistory(int? page, int studentId)
        {
            ViewBag.Page = page ?? 1;
            ViewBag.StudentId = studentId;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _studentReportService.GetStudentExpulsionHistory(page ?? 1, 10, languageId, studentId);
            return PartialView("_ExpulsionsHistory", result);
        }
    }
}
