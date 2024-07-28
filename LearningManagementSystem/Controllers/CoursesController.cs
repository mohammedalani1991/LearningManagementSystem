using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using LearningManagementSystem.Areas.ControlPanel.Controllers;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Controllers;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ICourseService _courseService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly IEnrollCourseResourceService _erollCourseResourceService;
        private readonly IEnrollCourseExamService _enrollCourseExamervice;
        private readonly IEnrollAssignmentService _enrollAssignmentService;
        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
        private readonly IEnrollCourseTimeService _enrollCourseTimeService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IUserProfileService _userProfileService;
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollStudentCourseAttachmentService _enrollStudentCourseAttachmentService;
        private readonly IInvoicesPayService _invoicesPayService;
        private readonly ILogService _logService;
        private readonly ISmsService _smsService;
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly ICurrencyService _currencyService;
        private readonly IEnrollCourseQuizService _enrollCourseQuizService;
        private readonly IBalanceHistoryService _balanceHistoryService;
        private readonly IStringLocalizer<CoursesController> _localizer;
        private readonly IAttendancesService _attendancesService;

        public CoursesController(IEnrollStudentCourseAttachmentService enrollStudentCourseAttachmentService,
            IEnrollLectureService enrollLectureService,
            IUserProfileService userProfileService,
            IEnrollStudentCourseService enrollStudentCourseService,
            ICoursePrerequisiteService coursePrerequisiteService,
            IEnrollAssignmentService enrollAssignmentService,
            IEnrollCourseExamService enrollCourseExamervice,
            IEnrollCourseResourceService erollCourseResourceService,
            IEnrollSectionOfCourseService enrollSectionOfCourseService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            ILogger<CoursesController> logger,
            IEnrollCourseTimeService enrollCourseTimeService,
            ICookieService cookieService,
            ICourseService courseService,
            ICourseCategoryService courseCategoryService,
            ITrainerService trainerService,
            ISettingService settingService,
            IInvoicesPayService invoicesPayService,
            ILogService logService,
            ISmsService smsService,
            IStudentService studentService,
            IWebHostEnvironment hostingEnvironment,
             IEnrollStudentExamService enrollStudentExamService, IAttendancesService attendancesService, ICurrencyService currencyService,
             IEnrollCourseQuizService enrollCourseQuizService, IBalanceHistoryService balanceHistoryService, IStringLocalizer<CoursesController> localizer
            )
        {
            _logger = logger;
            _cookieService = cookieService;
            _courseService = courseService;
            _courseCategoryService = courseCategoryService;
            _trainerService = trainerService;
            _settingService = settingService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _erollCourseResourceService = erollCourseResourceService;
            _enrollCourseExamervice = enrollCourseExamervice;
            _enrollAssignmentService = enrollAssignmentService;
            _coursePrerequisiteService = coursePrerequisiteService;
            _enrollCourseTimeService = enrollCourseTimeService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _userProfileService = userProfileService;
            _enrollLectureService = enrollLectureService;
            _enrollStudentCourseAttachmentService = enrollStudentCourseAttachmentService;
            _invoicesPayService = invoicesPayService;
            _logService = logService;
            _smsService = smsService;
            _studentService = studentService;
            _hostingEnvironment = hostingEnvironment;
            _enrollStudentExamService = enrollStudentExamService;
            _attendancesService = attendancesService;
            _currencyService = currencyService;
            _enrollCourseQuizService = enrollCourseQuizService;
            _balanceHistoryService = balanceHistoryService;
            _localizer = localizer;
        }
        public IActionResult Index(string search, int? categoryId, int? trainer, int? page, int pagination)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);


            if (page == 0 || page == null)
                page = 1;

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.CoursePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CoursePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            if (!string.IsNullOrWhiteSpace(search))
                ViewBag.Search = search;

            if (categoryId != null && categoryId != 0)
                ViewBag.CategoryId = categoryId;


            if (trainer != null && trainer != 0)
                ViewBag.Trainer = trainer;


            ViewBag.ListCourseCategorys = _courseCategoryService.GetActiveCourseCategorysForGuest(false, null, null, languageId);
            ViewBag.ListTrainer = _trainerService.GetActiveTrainers(null, languageId).Select(r => r.Contact);

            var result = _courseService.GetActiveCoursesForGuest(null, search, categoryId, trainer, page, languageId, pagination);

            return View(result);
        }

        public IActionResult Details(int id, int? enrollTeacherCourseId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _courseService.GetCourseById(id, languageId);

            if (enrollTeacherCourseId == 0)
                if (result == null || result.Status == (int)GeneralEnums.StatusEnum.Deleted || result.Status == (int)GeneralEnums.StatusEnum.Deactive)
                    return RedirectToAction("NotFound", "Home");

            if (User.Identity.IsAuthenticated)
            {
                var profile = _userProfileService.GetUserProfileByUsername(User.Identity.Name);

                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(profile.Contact.Students.FirstOrDefault(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted)?.Id ?? 0, id);
                ViewBag.EnrollStudentCourseId = enrollStudentCourse.Id;
                ViewBag.StudentId = profile.Contact.Students.FirstOrDefault(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted)?.Id;

                if (enrollStudentCourse.Id != 0)
                    ViewBag.EnrollStudentCourseAttachment = _enrollStudentCourseAttachmentService.GetEnrollStudentCourseAttachmentByEnrollStudentCourseId(enrollStudentCourse.Id).Id != 0;
                else
                    ViewBag.EnrollStudentCourseAttachment = false;

                if (enrollTeacherCourseId > 0)
                {
                    ViewBag.EnrollTeacherCourseId = enrollTeacherCourseId;
                    var enrollTeacherCourseData = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId ?? 0, languageId);
                    ViewBag.enrollTeacherCourseData = enrollTeacherCourseData;
                    ViewBag.Enroll = enrollStudentCourse.Id != 0;
                }
                else
                {
                    if (enrollStudentCourse.CourseId > 0)
                    {
                        var enrollTeacherCourseData = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollStudentCourse.CourseId, languageId);
                        ViewBag.enrollTeacherCourseData = enrollTeacherCourseData;
                        if ((enrollTeacherCourseData.WorkEndDate != null && enrollTeacherCourseData.WorkEndDate > DateTime.Now) || (enrollStudentCourse.IsPass == true && !Boolean.Parse(_settingService.GetOrCreate("Allow_To_Register_In_Course_If_Passed", "false").Value)))
                        {
                            ViewBag.Enroll = enrollStudentCourse.Id != 0;
                            ViewBag.EnrollTeacherCourseId = enrollStudentCourse.CourseId;
                        }
                        else
                        {
                            ViewBag.Enroll = false;
                            ViewBag.EnrollTeacherCourseId = null;
                        }
                    }
                    else
                    {
                        ViewBag.enrollTeacherCourseData = null;
                        ViewBag.Enroll = false;
                    }
                }
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.EnrollStudentCourseAttachment = false;
            }

            return View(result);
        }

        [HttpPost]
        public ActionResult AddAttachment(StudentCourseAttachmentViewModel studentCourseAttachmentViewModel)
        {

            if (!string.IsNullOrEmpty(studentCourseAttachmentViewModel.FileUrl))
            {
                _enrollStudentCourseAttachmentService.AddEnrollStudentCourseAttachment(new EnrollStudentCourseAttachmentViewModel()
                {
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    FileAttached = studentCourseAttachmentViewModel.FileUrl,
                    EnrollStudentCourseId = studentCourseAttachmentViewModel.enrollStudentCourseId,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Notes = studentCourseAttachmentViewModel.notes
                });
            }
            else
            {
                return Json(new { result = "Fail" });

            }
            return Json(new { result = "Success" });
        }

        public IActionResult ShowAddAttachment(int id, int enrollStudentCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.EnrollStudentCourseId = enrollStudentCourseId;

            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);

            var result = _courseService.GetCourseById(enrollTeacherCours.CourseId, languageId);

            return PartialView("_ShowAddAttachment", result);
        }

        public IActionResult ShowCourseTrainer(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);
            var trainer = _trainerService.GetTrainerById(enrollTeacherCours.TeacherId, languageId);
            return PartialView("_ShowCourseTrainer", trainer);
        }


        public IActionResult ShowCourseTrainerEnrollDetails(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, languageId);


            if (User.Identity.IsAuthenticated)
            {
                var profile = _userProfileService.GetUserProfileByUsername(User.Identity.Name);
                if (profile.Contact.Students.Any() && !profile.Contact.Trainers.Any())
                {
                    var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(profile.Contact.Students.FirstOrDefault(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted).Id, enrollTeacherCours.CourseId);
                    ViewBag.Enroll = enrollStudentCourse.Id != 0;
                }
                else if (profile.Contact.Trainers.Any())
                {
                    ViewBag.Enroll = true;
                }
                else
                    ViewBag.Enroll = 0;
            }
            else
            {
                ViewBag.Enroll = false;
            }

            return PartialView("_ShowCourseTrainerEnrollDetails", enrollTeacherCours);
        }


        public IActionResult ShowCourseTimes(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var trainer = _enrollCourseTimeService.GetEnrollCourseTimeByEnrollTeacherCourseId(id);
            return PartialView("_ShowCourseTimes", trainer);
        }

        public IActionResult ShowLessons(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.studentId = null;
            ViewBag.CourseLessonQuiz = new EnrollCourseExam();
            var enrollSectionOfCourse = _enrollSectionOfCourseService.GetEnrollSectionByEnrollTeacherCourseId(id, languageId);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, languageId);

            if (User.Identity.IsAuthenticated)
            {
                var profile = _userProfileService.GetUserProfileByUsername(User.Identity.Name);

                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(profile.Contact.Students.FirstOrDefault(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted)?.Id ?? 0, enrollTeacherCours.CourseId);
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
                var QuizLessons = _enrollCourseExamervice.GetEnrollCourseQuizByEnrollTeacherCourseId(enrollTeacherCours.Id, enrollSectionOfCourse.FirstOrDefault()?.EnrollLectures.FirstOrDefault()?.Id, languageId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                if (studenDetails != null)
                {
                    ViewBag.studentId = studenDetails.Id;
                    foreach (var Exams in QuizLessons)
                    {
                        var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, enrollTeacherCours.Id);
                        var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                        if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                            Exams.EnrollStudentExams.Add(EnrollStudentExam);
                    }
                }
                ViewBag.CourseLessonQuiz = QuizLessons;
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }
            return PartialView("_ShowLessons", enrollSectionOfCourse);
        }
        public IActionResult ShowExams(int EnrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.studentId = null;
            var enrollCourseExams = _enrollCourseExamervice.GetEnrollCourseExamByEnrollTeacherCourseId(EnrollTeacherCourseId, languageId);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId, languageId);
            if (User.Identity.IsAuthenticated)
            {
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                if (studenDetails != null)
                    ViewBag.studentId = studenDetails.Id;

                foreach (var Exams in enrollCourseExams)
                {
                    var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, EnrollTeacherCourseId);
                    var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                    if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                        Exams.EnrollStudentExams.Add(EnrollStudentExam);
                }

                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(studenDetails?.Id ?? 0, enrollTeacherCours.CourseId);
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }

            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("_ShowExams", enrollCourseExams);
        }

        public IActionResult ShowAssignments(int enrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            if (User.Identity.Name != null)
            {
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId, languageId);
                var enrollCourseExams = _enrollAssignmentService.GetEnrollAssignmentByEnrollTeacherCourseId(enrollTeacherCourseId, studenDetails?.Id ?? 0, languageId);
                ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
                ViewBag.IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(studenDetails?.Id ?? 0, enrollTeacherCours.CourseId);
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                return PartialView("_ShowAssignments", enrollCourseExams);
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }
            return PartialView("_ShowAssignments", new List<EnrollStudentAssigment>());
        }

        public IActionResult ShowMarks(int enrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (User.Identity.Name != null)
            {
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                var id = _enrollTeacherCourseService.GetEnrollmentCourseIdFormStudentId(enrollTeacherCourseId, studenDetails.Id);
                var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId, languageId);
                var result = _enrollCourseQuizService.GetAllStudentQuizMarks(id, enrollTeacherCourseId , languageId);
                ViewBag.QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(studenDetails.Id, enrollTeacherCours.CourseId);
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                return PartialView("_StudentsMarksDetails", result);
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }
            return PartialView("_StudentsMarksDetails", new List<EnrollCourseQuiz>());
        }

        public IActionResult ShowAttendances(int enrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (User.Identity.Name != null)
            {
                var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId, languageId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                var id = _enrollTeacherCourseService.GetEnrollmentCourseIdFormStudentId(enrollTeacherCourseId, studenDetails.Id);
                var result = _attendancesService.GetStudentAttendances(enrollTeacherCourseId, id);
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(studenDetails.Id, enrollTeacherCours.CourseId);
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                return PartialView("_ShowAttendances", result);
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }
            return PartialView("_ShowAttendances", new List<CourseAttendance>());
        }

        public IActionResult ShowPreRequests(int EnrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.studentId = null;
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId);
            var coursePrerequisiteServices = _coursePrerequisiteService.GetViewModeCoursePrerequisiteByCourseId(enrollTeacherCours.CourseId, languageId);
            if (User.Identity.IsAuthenticated)
            {
                var enrollCoursePrerequisiteExams = _enrollCourseExamervice.GetPrerequisiteEnrollCourseExam(EnrollTeacherCourseId, languageId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                if (studenDetails != null)
                {
                    ViewBag.studentId = studenDetails.Id;
                    foreach (var Exams in enrollCoursePrerequisiteExams)
                    {
                        var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, EnrollTeacherCourseId);
                        var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                        if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                            Exams.EnrollStudentExams.Add(EnrollStudentExam);
                    }

                }

                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentId(studenDetails?.Id ?? 0, languageId);
                var CheckenrollStudentCourse = enrollStudentCourse.FirstOrDefault(e => e.Course.CourseId == enrollTeacherCours.CourseId);
                ViewBag.Enroll = false;
                if (CheckenrollStudentCourse != null && CheckenrollStudentCourse.Id > 0)
                    ViewBag.Enroll = true;

                ViewBag.EnrollStudentCourse = enrollStudentCourse;
                ViewBag.PrerequisiteExam = enrollCoursePrerequisiteExams;
            }
            else
            {
                ViewBag.Enroll = false;
            }

            return PartialView("_ShowPreRequests", coursePrerequisiteServices);
        }

        public IActionResult ShowLessonDetails(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var enrollSectionOfCourse = _erollCourseResourceService.GetEnrollCourseResource(id, languageId);
            ViewBag.studentId = null;
            ViewBag.CourseLessonQuiz = new EnrollCourseExam();
            var enrollLecture = _enrollLectureService.GetEnrollLectureById(id);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollLecture.EnrollCourseId);

            if (User.Identity.IsAuthenticated)
            {
                var profile = _userProfileService.GetUserProfileByUsername(User.Identity.Name);

                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(profile.Contact.Students.First(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted).Id, enrollTeacherCours.CourseId);
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                var QuizLessons = _enrollCourseExamervice.GetEnrollCourseQuizByEnrollTeacherCourseId(enrollTeacherCours.Id, id, languageId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                if (studenDetails != null)
                {
                    ViewBag.studentId = studenDetails.Id;
                    foreach (var Exams in QuizLessons)
                    {
                        var StudentCourseByStudentIdAndCourseId = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, enrollTeacherCours.Id);
                        var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(StudentCourseByStudentIdAndCourseId.Id, Exams.Id);
                        if (EnrollStudentExam != null && EnrollStudentExam.Id > 0)
                            Exams.EnrollStudentExams.Add(EnrollStudentExam);
                    }
                }
                ViewBag.CourseLessonQuiz = QuizLessons;
            }
            else
            {
                ViewBag.Enroll = false;
            }

            return PartialView("_ShowLessonDetails", enrollSectionOfCourse);
        }

        [Authorize]
        public IActionResult ShowSelectPayMethod(int selectedEnrollTeacherCourseId)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            if (studenDetails != null)
            {
                var hasBeenExpilled = _enrollStudentCourseService.CheckIfHasBeenExpilled(studenDetails.Id);
                if (hasBeenExpilled != null)
                    return Json(new { result = "FailExpilled", toDate = hasBeenExpilled.Value.ToShortDateString() });

                var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(selectedEnrollTeacherCourseId, studenDetails.Id);
                if (CheckAbilityToEnrollStudentInCourse != "done")
                    return Json(new { result = CheckAbilityToEnrollStudentInCourse });
            }
            var id = _enrollTeacherCourseService.GetEnrollTeacherCourseById(selectedEnrollTeacherCourseId).CourseId;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.selectedEnrollTeacherCourseId = selectedEnrollTeacherCourseId;
            ViewBag.Balance = studenDetails.Balance ?? 0;
            ViewBag.Course = _courseService.GetCourseById(id, languageId);
            return PartialView("_ShowSelectPayMethod");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInvoicesPay(InvoicesPayViewModel InvoicesPayViewModel)
        {
            var result = -1;
            if (ModelState.IsValid)
            {
                var cooke = _cookieService.GetCookie(CookieNames.SelectedCurrencyId);

                var currancy = _currencyService.GetCurrencyById(Int32.Parse(cooke));
                var exchangeCurrency = _currencyService.GetExchangeCurrency();
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var CourseName = _enrollTeacherCourseService.GetEnrollById(InvoicesPayViewModel.EnrollTeacherCourseId, langId).CourseName;
                InvoicesPayViewModel.ContactId = ContactID;
                InvoicesPayViewModel.CreatedBy = User.Identity?.Name;
                InvoicesPayViewModel.CurrencyRate = decimal.Round((decimal)(currancy.Value / exchangeCurrency.Value), 2);
                InvoicesPayViewModel.CustomerCurrencyCode = currancy.Code;

                var Student = _studentService.GetStudentByContactId(ContactID);
                if (Student != null && Student.Id > 0)
                {
                    var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(InvoicesPayViewModel.EnrollTeacherCourseId, Student.Id);
                    if (CheckAbilityToEnrollStudentInCourse != "done")
                        return Json(new { result = CheckAbilityToEnrollStudentInCourse });
                }

                result = await _invoicesPayService.AddInvoicesPay(InvoicesPayViewModel);
                if (result > 0)
                {
                    try
                    {
                        await _smsService.SendEmail(new MessageViewModel
                        {
                            CreatedBy = User.Identity.Name,
                            Ids = new List<int>() { ContactID },
                            Message = $"{_localizer["Please note that the user with the name"]} {User.Identity.Name} {_localizer["has sent an invoice attachment for the course name:"]} {CourseName} {_localizer["please check the Invoices Pay in the admin control panel"]}",
                            Subject = "Al-Safa Invoices Pay",
                            Emails = new List<string>() { },
                            SendToAdmin = true,
                        });
                        await _smsService.SendEmail(new MessageViewModel
                        {
                            CreatedBy = User.Identity.Name,
                            Ids = new List<int>() { ContactID },
                            Message = $"{_localizer["The subscription request has been sent for"]} ({CourseName}), {_localizer["Your request is under review by the site administration"]}",
                            Subject = $"{_localizer["Request is Under Review"]}",
                            Emails = new List<string>() { },
                        });
                    }
                    catch (Exception ex)
                    {
                        _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send email for Invoices Pay");
                    }
                }
            }
            return Json(new { result = result.ToString() });

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollNow(int id)
        {
            try
            {
                var enrollTeacherCourseBy = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);
                var course = _courseService.GetCourseById(enrollTeacherCourseBy.CourseId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                if (studenDetails.Balance - course.CoursePrice >= 0)
                {
                    var cooke = _cookieService.GetCookie(CookieNames.SelectedCurrencyId);

                    var currancy = _currencyService.GetCurrencyById(Int32.Parse(cooke));
                    var exchangeCurrency = _currencyService.GetExchangeCurrency();

                    var NeedApproval = false;
                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(enrollTeacherCourseBy.Id, studenDetails.Id);
                    if (CheckHasPreRequestsCourse != "done")
                        NeedApproval = true;

                    int enrollStudentCourseId = _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                    {
                        CreatedOn = DateTime.Now,
                        Price = course.CoursePrice,
                        CourseId = id,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        StudentId = studenDetails.Id,
                        NeedApproval = NeedApproval,
                        IsPass = false,
                        CurrencyRate = decimal.Round((decimal)(currancy.Value / exchangeCurrency.Value), 2),
                        CustomerCurrencyCode = currancy.Code,
                    });

                    _studentService.ChangeBalance(studenDetails, -(course.CoursePrice ?? 0));
                    _balanceHistoryService.AddBalanceHistory(new StudentBalanceHistory()
                    {
                        CreatedOn = DateTime.Now,
                        Amount = course.CoursePrice,
                        Balance = studenDetails.Balance - course.CoursePrice,
                        EnrollStudentCourseId = enrollStudentCourseId,
                        StudentId = studenDetails.Id,
                        Title = "Enrolled To",
                    });

                    try
                    {
                        await _smsService.SendEmail(new MessageViewModel
                        {
                            CreatedBy = User.Identity.Name,
                            Ids = new List<int>() { ContactID },
                            Message = $"You paid ${course.CoursePrice} using your Balance.",
                            Subject = "Al-Safa Payment",
                            Emails = new List<string>() { }
                        });
                    }
                    catch (Exception ex)
                    {
                        _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Sending Email For Enrolling using Balance");
                    }
                    return Ok();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Enrolling using Balance");
                return null;
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(int enrollTeacherCourseId)
        {
            try
            {
                var result = -1;
                var cooke = _cookieService.GetCookie(CookieNames.SelectedCurrencyId);

                var currancy = _currencyService.GetCurrencyById(Int32.Parse(cooke));
                var exchangeCurrency = _currencyService.GetExchangeCurrency();

                InvoicesPayViewModel InvoicesPayViewModel = new InvoicesPayViewModel();

                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var CoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId);
                InvoicesPayViewModel.ContactId = ContactID;
                InvoicesPayViewModel.EnrollTeacherCourseId = enrollTeacherCourseId;
                InvoicesPayViewModel.CreatedBy = User.Identity?.Name;
                InvoicesPayViewModel.CurrencyRate = decimal.Round((decimal)(currancy.Value / exchangeCurrency.Value), 2);
                InvoicesPayViewModel.CustomerCurrencyCode = currancy.Code;
                InvoicesPayViewModel.AttachmentUrl = "Free Course";
                InvoicesPayViewModel.ReceiptNo = "0000000";
                InvoicesPayViewModel.Notes = "Free Course";

                var Student = _studentService.GetStudentByContactId(ContactID);
                if (Student != null && Student.Id > 0)
                {
                    var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(enrollTeacherCourseId, Student.Id);
                    if (CheckAbilityToEnrollStudentInCourse != "done")
                        return Json(new { result = CheckAbilityToEnrollStudentInCourse });
                }

                if (bool.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Auto_Accept_Free_Courses, "true").Value))
                {
                    result = await _invoicesPayService.AddInvoicesPay(InvoicesPayViewModel, true);
                    var Price = 0m;
                    if (CoursDetails.Course.CoursePrice != null)
                        Price = CoursDetails.Course.CoursePrice.Value;

                    var NeedApproval = false;

                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(InvoicesPayViewModel.EnrollTeacherCourseId, Student.Id);
                    if (CheckHasPreRequestsCourse != "done")
                    {
                        NeedApproval = true;

                    }

                    _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                    {
                        CreatedOn = DateTime.Now,
                        Price = Price,
                        CourseId = InvoicesPayViewModel.EnrollTeacherCourseId,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        StudentId = Student.Id,
                        NeedApproval = NeedApproval,

                        CurrencyRate = InvoicesPayViewModel.CurrencyRate,
                        CustomerCurrencyCode = InvoicesPayViewModel.CustomerCurrencyCode,
                    });

                }
                else
                    result = await _invoicesPayService.AddInvoicesPay(InvoicesPayViewModel, false);

                if (result > 0)
                {
                    try
                    {
                        await _smsService.SendEmail(new MessageViewModel
                        {
                            CreatedBy = User.Identity.Name,
                            Ids = new List<int>() { ContactID },
                            Message = $"Please note that the user with the name {User.Identity.Name} has sent an invoice attachment for the course name: {CoursDetails.CourseName}, please check the Invoices Pay in the admin control panel",
                            Subject = "Al-Safa Invoices Pay",
                            Emails = new List<string>() { }
                        });
                    }
                    catch (Exception ex)
                    {
                        _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send email for Invoices Pay");
                    }

                }
                return Json(new { result = result.ToString() });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Adding Invoices To Free Course");
                return null;
            }
        }
    }
}
