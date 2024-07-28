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
using System.Transactions;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Microsoft.Graph.Auth;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Constants = LearningManagementSystem.Core.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystem.Services.ControlPanel.IServices;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class EnrollmentCourseController : Controller
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
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        private readonly IEnrollAssignmentService _enrollAssignmentService;
        private readonly IAssignmentService _assignmentService;
        private readonly IExamTemplateService _examTemplateService;
        private readonly IEnrollCourseExamService _enrollCourseExamService;
        private readonly IExamQuestionService _examQuestionService;
        private readonly IEnrollCourseExamQuestionService _enrollCourseExamQuestionService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IStudentService _studentServiceService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly IAttendancesService _attendancesService;
        private readonly IEnrollCourseAssignmentService _enrollCourseAssignmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentService _studentService;
        private readonly IBalanceHistoryService _balanceHistoryService;
        private readonly ISmsService _smsService;
        private readonly ICertificateAdoptionService _certificateAdoptionService;
        private readonly IEnrollCourseQuizService _enrollCourseQuizService;
        private readonly IEnrollCourseAllowUserRateService _enrollCourseAllowUserRateService;

        public EnrollmentCourseController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            IEnrollCourseTimeService enrollCourseTimeService,
            ISectionOfCourseService sectionOfCourseService,
            ILectureService lectureService,
            IEnrollCourseAllowUserRateService enrollCourseAllowUserRateService,
            ICourseResourceService courseResourceService,
            ITrainerService trainerService,
            ISemesterService semesterService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollSectionOfCourseService enrollSectionOfCourse,
            IEnrollLectureService enrollLectureService,
            IEnrollCourseResourceService enrollCourseResourceService,
            IEnrollAssignmentService enrollAssignmentService,
            IAssignmentService assignmentService,
            IExamTemplateService examTemplateService,
            IEnrollCourseExamService enrollCourseExamService,
            IExamQuestionService examQuestionService,
            IEnrollCourseExamQuestionService enrollCourseExamQuestionService,
            IEnrollStudentCourseService enrollStudentCourseService,
            IStudentService studentServiceService,
            IEnrollStudentExamService enrollStudentExamService, IAttendancesService attendancesService, IEnrollCourseAssignmentService enrollCourseAssignmentService
            , IHttpContextAccessor httpContextAccessor, IStudentService studentService, IBalanceHistoryService balanceHistoryService, ISmsService smsService,
            ICertificateAdoptionService certificateAdoptionService, IEnrollCourseQuizService enrollCourseQuizService
            )
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollCourseTimeService = enrollCourseTimeService;
            _sectionOfCourseService = sectionOfCourseService;
            _enrollCourseAllowUserRateService = enrollCourseAllowUserRateService;
            _lectureService = lectureService;
            _courseResourceService = courseResourceService;
            _trainerService = trainerService;
            _semesterService = semesterService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollSectionOfCourse = enrollSectionOfCourse;
            _enrollLectureService = enrollLectureService;
            _enrollCourseResourceService = enrollCourseResourceService;
            _enrollAssignmentService = enrollAssignmentService;
            _assignmentService = assignmentService;
            _examTemplateService = examTemplateService;
            _enrollCourseExamService = enrollCourseExamService;
            _examQuestionService = examQuestionService;
            _enrollCourseExamQuestionService = enrollCourseExamQuestionService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _studentServiceService = studentServiceService;
            _enrollStudentExamService = enrollStudentExamService;
            _attendancesService = attendancesService;
            _enrollCourseAssignmentService = enrollCourseAssignmentService;
            _httpContextAccessor = httpContextAccessor;
            _studentService = studentService;
            _balanceHistoryService = balanceHistoryService;
            _smsService = smsService;
            _certificateAdoptionService = certificateAdoptionService;
            _enrollCourseQuizService = enrollCourseQuizService;
        }
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enrollment Course List")]
        public async Task<IActionResult> GetData(int? page, int? TeacherId, int pagination, string table, int? CourseId, string hdCourseName, int? LearningMethodId, int? SemesterId = -1)
        {

            if (TeacherId > 0)
                ViewBag.TeacherId = TeacherId;

            if (LearningMethodId > 0)
                ViewBag.LearningMethodId = LearningMethodId;

            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
                ViewBag.hdCourseName = hdCourseName;
            }
            else
            {
                ViewBag.CourseId = 0;
                ViewBag.hdCourseName = "";
            }
            if (SemesterId == -1)
                SemesterId = _semesterService.GetDefaultSemester();
            ViewBag.SemesterId = SemesterId;

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



            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "TeacherId", "CourseId", "SemesterId", "SectionName", "Status", "CreatedOn", "CountOfStudent" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollmentCourseTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollmentCourseTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollmentCourseTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            var result = _enrollTeacherCourseService.GetEnrollTeacherCourses(page, languageId, pagination, CourseId, TeacherId, LearningMethodId, SemesterId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enrollment Course List")]
        public async Task<IActionResult> GetExamsAndAssignmentsData(int? page, int? TeacherId1, int? CourseId1, int? SemesterId1)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.TeacherId1 = TeacherId1;
            ViewBag.CourseId1 = CourseId1;
            ViewBag.SemesterId1 = SemesterId1;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);

            var result = _enrollTeacherCourseService.GetEnrollTeacherCourses(page, languageId, 10, CourseId1, TeacherId1, 0, SemesterId1);

            return PartialView("_CreateExamsAndAssignments", result);
        }

        public async Task<IActionResult> GetCoursesList(int? semester)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                var data = _enrollTeacherCourseService.GetEnrollCourseListForSemester(semester , languageId);
                return Json(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IActionResult> GetTeacherList(int? semester ,int? courseId)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                var data = _enrollTeacherCourseService.GetTeacherListByCourseId(semester ,courseId, languageId);
                return Json(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enrollment Course")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/EnrollmentCourse/Create
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.LangId = languageId;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            ViewBag.ListSemesters = _semesterService.GetSemesters(languageId);
            return PartialView("Create");
        }

        // GET: ControlPanel/EnrollmentCourse/CourseSearch
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enrollment Course  Get")]
        public IActionResult CourseSearch(string param, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var Courses = _courseService.GetCoursesByName(param, languageId);
            return Json(Courses);
        }

        // GET: ControlPanel/EnrollmentCourse/GetCourseSections
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Get Course Sections")]
        public IActionResult GetCourseSections(int CourseId, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var Sections = _sectionOfCourseService.GetSectionOfCourseByCourseId(CourseId, languageId);
            return Json(Sections);
        }

        // GET: ControlPanel/EnrollmentCourse/GetSemesterByID
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Get Semester By ID")]
        public IActionResult GetSemesterByID(int SemesterId, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var semesters = _semesterService.GetSemesterById(SemesterId, languageId);
            return Json(semesters);
        }

        // POST: ControlPanel/EnrollmentCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Create Post")]
        public async Task<IActionResult> Create(
            EnrollmentCourseViewModel EnrollmentCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.CourseViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.CourseViewModel.Id == 0)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to add Semester or Section if the added learning method is "Online"
                    {
                        EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester = "0";
                        EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection = "0";
                    }

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester == "1")
                    {
                        if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SemesterId == 0)
                            return Content("AddMassegeErrorInvalidData");

                    }

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection == "1")
                    {
                        if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName == null)
                            return Content("AddMassegeErrorInvalidData");
                    }

                    if (EnrollmentCourseViewModel.TrainerViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.TrainerViewModel.Exists(s => s.Id == 0))
                        return Content("AddMassegeErrorInvalidData");


                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId != (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to check the added days if the added learning method is "Online"
                    {

                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel == null)
                            return Content("AddMassegeErrorInvalidData");

                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Exists(s => s.DayId == 0 || s.FromTime == null || s.ToTime == null))
                            return Content("AddMassegeErrorInvalidData");

                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Select(e => e.DayId).Count() != EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Select(e => e.DayId).Distinct().Count())
                            return Content("AddMassegeErrorInvalidCourseTime");
                    }

                    var CheckEnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseByTeacherIdAndCourseId(EnrollmentCourseViewModel.TrainerViewModel[0].Id, EnrollmentCourseViewModel.CourseViewModel.Id, EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName);
                    if (CheckEnrollTeacherCourse != null && CheckEnrollTeacherCourse.Id != 0)
                        return Content("AddMassegeErrorSameEnrollCourse");


                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                #region //Add Enroll Teacher Course

                                EnrollTeacherCourseViewModel AddEnrollTeacherCourseViewModel = new EnrollTeacherCourseViewModel();
                                AddEnrollTeacherCourseViewModel.LanguageId = (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                AddEnrollTeacherCourseViewModel.CreatedBy = User.Identity?.Name;
                                AddEnrollTeacherCourseViewModel.Status = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.Status;

                                AddEnrollTeacherCourseViewModel.CourseId = EnrollmentCourseViewModel.CourseViewModel.Id;
                                AddEnrollTeacherCourseViewModel.CourseName = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CourseName;
                                AddEnrollTeacherCourseViewModel.TeacherId = EnrollmentCourseViewModel.TrainerViewModel[0].Id;

                                if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester == "1")
                                    AddEnrollTeacherCourseViewModel.SemesterId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SemesterId;

                                if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection == "1")
                                    AddEnrollTeacherCourseViewModel.SectionName = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName;

                                AddEnrollTeacherCourseViewModel.PublicationDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.PublicationDate;
                                AddEnrollTeacherCourseViewModel.PublicationEndDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.PublicationEndDate;
                                AddEnrollTeacherCourseViewModel.WorkStartDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.WorkStartDate;
                                AddEnrollTeacherCourseViewModel.WorkEndDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.WorkEndDate;

                                AddEnrollTeacherCourseViewModel.CountOfStudent = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CountOfStudent;
                                AddEnrollTeacherCourseViewModel.LearningMethodId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId;

                                AddEnrollTeacherCourseViewModel.AgeAllowedForRegistration = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeAllowedForRegistration;
                                AddEnrollTeacherCourseViewModel.AgeGroup = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeGroup;
                                AddEnrollTeacherCourseViewModel.AgeGroupTo = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeGroupTo;

                                AddEnrollTeacherCourseViewModel.CalculationTypeId = (int)GeneralEnums.CourseType.Admin;
                                //AddEnrollTeacherCourseViewModel.CalculationTypeId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CalculationTypeId;
                                AddEnrollTeacherCourseViewModel.NotesForEnrolled = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.NotesForEnrolled;
                                var Course = _courseService.GetCourseById_WithoutUsing(EnrollmentCourseViewModel.CourseViewModel.Id, context);
                                var AddedEnrollTeacherCourse = _enrollTeacherCourseService.AddEnrollTeacherCourse_WithoutUsing(AddEnrollTeacherCourseViewModel, Course, context);
                                #endregion

                                #region //Add Enroll Section Of Course &  Lectures & Resources

                                foreach (var Section in _sectionOfCourseService.GetSectionOfCourseByCourseId_WithoutUsing(AddedEnrollTeacherCourse.CourseId, context))
                                {


                                    var AddedEnrollSectionOfCourse = _enrollSectionOfCourse.AddEnrollSectionOfCourseForAdmin(Section, AddedEnrollTeacherCourse.Id, context);
                                    #region //Add Enroll Lectures & Resources

                                    foreach (var Lecture in _lectureService.GetLectureBySectionId_WithoutUsing(Section.Id, context))
                                    {

                                        var AddedEnrollLecture = _enrollLectureService.AddEnrollLectureForAdmin(Lecture, AddedEnrollTeacherCourse.Id, AddedEnrollSectionOfCourse.Id, context);
                                        #region //Add Enroll Resources 

                                        foreach (var Resource in _courseResourceService.GetCourseResourceByLectureId_WithoutUsing(Lecture.Id, context))
                                        {
                                            _enrollCourseResourceService.AddEnrollCourseResourceForAdmin(Resource, AddedEnrollTeacherCourse.Id, AddedEnrollLecture.Id, context);
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }


                                #endregion

                                #region //Add Enroll Assignments

                                foreach (var Assignment in _assignmentService.GetAssignmentByCourseId_WithoutUsing(AddedEnrollTeacherCourse.CourseId, context))
                                {

                                    _enrollAssignmentService.AddEnrollAssignmentForAdmin(Assignment, AddedEnrollTeacherCourse.Id, context);

                                }

                                #endregion

                                #region //Add Enroll Course Exam & Questions
                                //Removed by Mahmoud Abo Abdo, because the trainer is responsible to enroll the Exam and questions
                                //foreach (var ExamTemplate in _examTemplateService.GetdExamTemplatetByCourseId_WithoutUsing(AddedEnrollTeacherCourse.CourseId, context))
                                //{

                                //    var AddedEnrollCourseExam = _enrollCourseExamService.AddEnrollCourseExamForAdmin(ExamTemplate, AddedEnrollTeacherCourse.Id, context);

                                //    #region //Add Enroll Exam Questions
                                //    foreach (var ExamQuestions in _examQuestionService.GetQuestionByTemplateId_WithoutUsing(ExamTemplate.Id, context))
                                //    {
                                //        var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                                //        {
                                //            QuestionId = ExamQuestions.Id,
                                //            EnrollCourseExamId = AddedEnrollCourseExam.Id,
                                //            CreatedBy = User.Identity?.Name
                                //        };
                                //        _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion_WithoutUsing(enrollCourseExamQuestionViewModel, context);
                                //    }
                                //    #endregion
                                //}
                                #endregion

                                _enrollCourseQuizService.RefetchQuizzes_WithoutUsing(AddedEnrollTeacherCourse.Id, User?.Identity?.Name ?? string.Empty , context);

                                if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId != (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to check the added days if the added learning method is "Online"
                                {
                                    #region //Add Enroll Course Time
                                    foreach (var EnrollCourseTime in EnrollmentCourseViewModel.EnrollCourseTimeViewModel)
                                    {
                                        var enrollCourseTime = new EnrollCourseTimeViewModel()
                                        {
                                            DayId = EnrollCourseTime.DayId,
                                            FromTime = EnrollCourseTime.FromTime,
                                            ToTime = EnrollCourseTime.ToTime,
                                            EnrollCourseId = AddedEnrollTeacherCourse.Id,
                                            LearningMethodId = AddedEnrollTeacherCourse.LearningMethodId,
                                            CreatedBy = User.Identity?.Name
                                        };
                                        _enrollCourseTimeService.AddEnrollCourseTime_WithoutUsing(enrollCourseTime, context);
                                    }
                                    #endregion
                                }


                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Enrollment Course Dic");
                                return View(EnrollmentCourseViewModel);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Enrollment Course Dic");
                    return View(EnrollmentCourseViewModel);
                }
            }
            return View(EnrollmentCourseViewModel);
        }

        // GET: ControlPanel/EnrollmentCourse/Edit/5
        [AuditLogFilter(ActionDescription = "Enrollment Course Edit Get")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id.Value, languageId);
            if (enrollTeacherCourse == null || enrollTeacherCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            enrollTeacherCourse.LanguageId = languageId;
            ViewBag.LangId = languageId;

            var Course = _courseService.GetCourseById(enrollTeacherCourse.CourseId, languageId);
            if (Course != null)
            {
                SelectListItem selListItem = new SelectListItem() { Value = Course.Id.ToString(), Text = Course.CourseName };
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(selListItem);
                ViewBag.SelectedCourse = newList;
            }
            else
            {
                SelectListItem selListItem = new SelectListItem() { Value = "", Text = "" };
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(selListItem);
                ViewBag.SelectedCourse = newList;
            }


            enrollTeacherCourse.ListEnrollCourseTime = _enrollCourseTimeService.GetEnrollCourseTimeByEnrollTeacherCourseId(enrollTeacherCourse.Id);
            var EnrollmentCourseViewModel = new EnrollmentCourseViewModel();
            var Courses = new CourseViewModel();
            Courses.Id = enrollTeacherCourse.CourseId;
            Courses.LanguageId = enrollTeacherCourse.LanguageId;
            if (Course != null)
                Courses.CourseName = Course.CourseName;
            EnrollmentCourseViewModel.CourseViewModel = Courses;
            enrollTeacherCourse.CourseName = Courses.CourseName;
            EnrollmentCourseViewModel.EnrollTeacherCourseViewModel = enrollTeacherCourse;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            ViewBag.ListSemesters = _semesterService.GetSemesters(languageId);
            var lookup = LookupHelper.GetLookupDetailsById(enrollTeacherCourse.LearningMethodId ?? 0);
            ViewBag.LearningMethodCode = lookup?.Code;

            return PartialView("Edit", EnrollmentCourseViewModel);
        }


        [AuditLogFilter(ActionDescription = "CheckStudentNumber")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Edit")]
        public async Task<IActionResult> CheckStudentNumber(int id)
        {
            var count = _enrollStudentCourseService.GetCountEnrollStudentCourses(id);
            return Json(count);
        }

        // GET: ControlPanel/EnrollmentCourse/ShowCountOfEnrolledStudents/5
        [AuditLogFilter(ActionDescription = "Enrollment Course Edit Get")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ShowCountOfEnrolledStudents")]
        public async Task<IActionResult> ShowCountOfEnrolledStudents(int id, string searchText, int? page, int pagination, int languageId = 0)
        {

            if (page == 0 || page == null)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrEmpty(searchText))
                ViewBag.SearchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollStudentsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollStudentsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, languageId);
            if (enrollTeacherCourse == null || enrollTeacherCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            enrollTeacherCourse.LanguageId = languageId;
            ViewBag.LangId = languageId;
            ViewBag.enrollTeacherCourseId = id;
            ViewBag.CountEnrollStudent = _enrollStudentCourseService.GetCountEnrollStudentCourses(enrollTeacherCourse.Id);
            ViewBag.CountAttendance = _enrollStudentCourseService.GetAttendanceDays(enrollTeacherCourse.Id);
            var result = _enrollStudentCourseService.GetEnrollStudentCourses(page, 0, 0, 0, enrollTeacherCourse.Id, languageId, pagination, searchText);
            return PartialView("_CountOfEnrolledStudents", result);
        }

        public async Task<IActionResult> GetStudentAttendances(int CourseId, int id)
        {
            var result = _attendancesService.GetStudentAttendances(CourseId, id);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            ViewBag.enrollTeacherCourseId = CourseId;

            return PartialView("_StudentAttendances", result);
        }

        // GET: ControlPanel/EnrollmentCourse/ShowCountOfEnrolledStudents/5
        [AuditLogFilter(ActionDescription = "Enrollment Course Edit Get")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ShowAddNewStudent")]
        public async Task<IActionResult> ShowAddNewStudent(int id, string searchText, int? page, int pagination, int languageId = 0)
        {

            if (page == 0 || page == null)
                page = 1;


            if (!string.IsNullOrEmpty(searchText))
                ViewBag.SearchText = searchText;

            ViewBag.Page = page;


            var val = _cookieService.GetCookie(Constants.Pagenation.StudentPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.StudentPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }


            ViewBag.LangId = languageId;
            ViewBag.enrollTeacherCourseId = id;
            var result = _studentServiceService.GetStudentsForAssign(searchText, id, page, languageId, pagination);
            return PartialView("_AddNewStudent", result);
        }


        [AuditLogFilter(ActionDescription = "Enrollment Course Add New Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ShowAddNewStudent")]
        public async Task<IActionResult> AddNewStudent(int StudentId = 0, int enrollTeacherCourseId = 0)
        {
            try
            {
                if (StudentId > 0 && enrollTeacherCourseId > 0)
                {
                    var hasBeenExpilled = _enrollStudentCourseService.CheckIfHasBeenExpilled(StudentId);
                    if (hasBeenExpilled != null)
                        return Json(new { result = "FailExpilled", toDate = hasBeenExpilled.Value.ToShortDateString() });

                    var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(enrollTeacherCourseId, StudentId);
                    if (CheckAbilityToEnrollStudentInCourse != "done")
                        return Json(new { result = CheckAbilityToEnrollStudentInCourse });


                    var CoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId);
                    var Price = 0m;
                    if (CoursDetails.Course.CoursePrice != null)
                        Price = CoursDetails.Course.CoursePrice.Value;

                    var NeedApproval = false;
                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(enrollTeacherCourseId, StudentId);
                    if (CheckHasPreRequestsCourse != "done")
                        NeedApproval = true;


                    _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                    {
                        CreatedOn = DateTime.Now,
                        Price = Price,
                        CourseId = enrollTeacherCourseId,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        NeedApproval = NeedApproval,
                        StudentId = StudentId,
                        IsPass = null,
                    });

                    return Json(new { result = "Success" });
                }
                else
                {
                    return Json(new { result = "Fail" });
                }

            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while  Add New Student");

                return Json(new { result = "Fail" });
            }
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Add New Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ShowAddNewStudent")]
        public async Task<IActionResult> ForceAddNewStudent(int StudentId = 0, int enrollTeacherCourseId = 0)
        {
            try
            {
                var forceAddNewStudent = Boolean.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Allow_Force_Add_New_Student, "true")?.Value);
                if (forceAddNewStudent && StudentId > 0 && enrollTeacherCourseId > 0)
                {
                    var CoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId);
                    var Price = 0m;
                    if (CoursDetails.Course.CoursePrice != null)
                        Price = CoursDetails.Course.CoursePrice.Value;

                    var NeedApproval = false;

                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(enrollTeacherCourseId, StudentId);
                    if (CheckHasPreRequestsCourse != "done")
                        NeedApproval = true;


                    _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                    {
                        CreatedOn = DateTime.Now,
                        Price = Price,
                        CourseId = enrollTeacherCourseId,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        NeedApproval = NeedApproval,
                        StudentId = StudentId,
                        IsPass = null,
                    });

                    return Json(new { result = "Success" });
                }
                else
                {
                    return Json(new { result = "Fail" });
                }

            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while  Add New Student");

                return Json(new { result = "Fail" });
            }
        }


        // GET: ControlPanel/EnrollmentCourse/Edit/5
        [AuditLogFilter(ActionDescription = "Enrollment Course Edit Get")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ShowEnrollStudent")]
        public async Task<IActionResult> ShowEnrollStudent(int id, int? page, int pagination, int languageId = 0)
        {

            if (page == 0 || page == null)
                page = 1;

            ViewBag.Page = page;


            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollmentCoursePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollmentCoursePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, languageId);
            if (enrollTeacherCourse == null || enrollTeacherCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            enrollTeacherCourse.LanguageId = languageId;
            ViewBag.LangId = languageId;
            ViewBag.Id = id;
            ViewBag.CourseId = enrollTeacherCourse.CourseId;
            var result = _enrollTeacherCourseService.GetEnrollmentCourse(id, page, pagination, languageId);
            return PartialView("_EnrollAttachmentStudent", result);
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Delete Post")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "PassStudent")]
        public async Task<IActionResult> PassStudent(int id)
        {
            try
            {
                _enrollTeacherCourseService.PassStudent(id);

                return Json(new { result = "Success" });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Pass Student");

                return Json(new { result = "Fail" });
            }
        }
        [AuditLogFilter(ActionDescription = "Enrollment Course Delete Post")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ChangeEnrollStudent")]
        public async Task<IActionResult> ChangeEnrollStudent(int ChangeEnrollStudentId, int EnrollTeacherCourseId)
        {
            try
            {
                _enrollTeacherCourseService.ChangeEnrollStudent(ChangeEnrollStudentId, EnrollTeacherCourseId);

                return Json(new { result = "Success" });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Pass Student");

                return Json(new { result = "Fail" });
            }
        }

        // POST: ControlPanel/EnrollmentCourse/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Edit Post")]
        public async Task<IActionResult> Edit(
            EnrollmentCourseViewModel EnrollmentCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.CourseViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.CourseViewModel.Id == 0)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to add Semester or Section if the added learning method is "Online"
                    {
                        EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester = "0";
                        EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection = "0";
                    }


                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester == "1")
                    {
                        if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SemesterId == 0)
                            return Content("AddMassegeErrorInvalidData");

                    }

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection == "1")
                    {
                        if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName == null)
                            return Content("AddMassegeErrorInvalidData");
                    }

                    if (EnrollmentCourseViewModel.TrainerViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.TrainerViewModel.Exists(s => s.Id == 0))
                        return Content("AddMassegeErrorInvalidData");

                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId != (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to check the added days if the added learning method is "Online"
                    {

                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel == null)
                            return Content("AddMassegeErrorInvalidData");

                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Exists(s => s.DayId == 0 || s.FromTime == null || s.ToTime == null))
                            return Content("AddMassegeErrorInvalidData");


                        if (EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Select(e => e.DayId).Count() != EnrollmentCourseViewModel.EnrollCourseTimeViewModel.Select(e => e.DayId).Distinct().Count())
                            return Content("AddMassegeErrorInvalidCourseTime");
                    }
                    var CurrentEnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.Id);
                    var CheckEnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseByTeacherIdAndCourseId(EnrollmentCourseViewModel.TrainerViewModel[0].Id, EnrollmentCourseViewModel.CourseViewModel.Id, EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName);
                    if (CheckEnrollTeacherCourse != null && CheckEnrollTeacherCourse.Id != 0 && CurrentEnrollTeacherCourse.Id != CheckEnrollTeacherCourse.Id)
                        return Content("AddMassegeErrorSameEnrollCourse");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                #region //Edit Enroll Teacher Course
                                EnrollTeacherCourseViewModel EditEnrollTeacherCourseViewModel = new EnrollTeacherCourseViewModel();
                                EditEnrollTeacherCourseViewModel.Id = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.Id;
                                EditEnrollTeacherCourseViewModel.LanguageId = (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                EditEnrollTeacherCourseViewModel.CreatedBy = User.Identity?.Name;
                                EditEnrollTeacherCourseViewModel.Status = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.Status;

                                EditEnrollTeacherCourseViewModel.CourseId = EnrollmentCourseViewModel.CourseViewModel.Id;
                                EditEnrollTeacherCourseViewModel.CourseName = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CourseName;
                                EditEnrollTeacherCourseViewModel.TeacherId = EnrollmentCourseViewModel.TrainerViewModel[0].Id;

                                if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSemester == "1")
                                    EditEnrollTeacherCourseViewModel.SemesterId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SemesterId;

                                if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.IslinkedSection == "1")
                                    EditEnrollTeacherCourseViewModel.SectionName = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.SectionName;

                                EditEnrollTeacherCourseViewModel.PublicationDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.PublicationDate;
                                EditEnrollTeacherCourseViewModel.PublicationEndDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.PublicationEndDate;
                                EditEnrollTeacherCourseViewModel.WorkStartDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.WorkStartDate;
                                EditEnrollTeacherCourseViewModel.WorkEndDate = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.WorkEndDate;

                                EditEnrollTeacherCourseViewModel.CountOfStudent = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CountOfStudent;
                                EditEnrollTeacherCourseViewModel.LearningMethodId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId;

                                EditEnrollTeacherCourseViewModel.AgeAllowedForRegistration = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeAllowedForRegistration;
                                EditEnrollTeacherCourseViewModel.AgeGroup = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeGroup;
                                EditEnrollTeacherCourseViewModel.AgeGroupTo = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.AgeGroupTo;

                                EditEnrollTeacherCourseViewModel.CalculationTypeId = (int)GeneralEnums.CourseType.Admin;
                                //EditEnrollTeacherCourseViewModel.CalculationTypeId = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.CalculationTypeId;
                                EditEnrollTeacherCourseViewModel.NotesForEnrolled = EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.NotesForEnrolled;

                                #endregion


                                if (CurrentEnrollTeacherCourse != null && CurrentEnrollTeacherCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
                                {
                                    var Course = _courseService.GetCourseById_WithoutUsing(EnrollmentCourseViewModel.CourseViewModel.Id, context);
                                    var EditedEnrollTeacherCourse = _enrollTeacherCourseService.EditEnrollTeacherCourse_WithoutUsing(EditEnrollTeacherCourseViewModel, CurrentEnrollTeacherCourse, Course, context);
                                    #region //Delete CourseTime Enrolled Data
                                    _enrollCourseTimeService.DeleteEnrollCourseTimeByEnrollTeacherCourseID_WithoutUsing(EditedEnrollTeacherCourse.Id, context);
                                    #endregion

                                    if (EnrollmentCourseViewModel.EnrollTeacherCourseViewModel.LearningMethodId != (int)GeneralEnums.LearningMethodEnum.ElectronicOnce) //No need to check the added days if the added learning method is "Online"
                                    {

                                        #region //Add Enroll Course Time
                                        foreach (var EnrollCourseTime in EnrollmentCourseViewModel.EnrollCourseTimeViewModel)
                                        {
                                            var enrollCourseTime = new EnrollCourseTimeViewModel()
                                            {
                                                DayId = EnrollCourseTime.DayId,
                                                FromTime = EnrollCourseTime.FromTime,
                                                ToTime = EnrollCourseTime.ToTime,
                                                EnrollCourseId = EditedEnrollTeacherCourse.Id,
                                                LearningMethodId = EditedEnrollTeacherCourse.LearningMethodId,
                                                CreatedBy = User.Identity?.Name
                                            };
                                            _enrollCourseTimeService.AddEnrollCourseTime_WithoutUsing(enrollCourseTime, context);
                                        }
                                        #endregion
                                    }

                                }

                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit  Enrollment Course Dic");
                                return View(EnrollmentCourseViewModel);
                            }
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit  Enrollment Course Dic");
                    return View(EnrollmentCourseViewModel);
                }
            }
            return View(EnrollmentCourseViewModel);
        }


        [AuditLogFilter(ActionDescription = "Enrollment Course Delete Get")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Delete")]
        // GET: ControlPanel/EnrollmentCourse/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
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
            return PartialView("Delete", EnrollmentCourseViewModel);
        }

        // POST: ControlPanel/EnrollmentCourse/Delete/5
        [AuditLogFilter(ActionDescription = "Enrollment Course Delete Post")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteEnrollmentCourse(int id, int Page)
        {
            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);
            if (enrollTeacherCourse != null && enrollTeacherCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            _enrollTeacherCourseService.DeleteEnrollTeacherCourse_WithoutUsing(enrollTeacherCourse, context);
                            #region //Delete All Enrolled Data
                            _enrollCourseResourceService.DeleteEnrollCourseResourceByEnrollTeacherCourseID_WithoutUsing(id, context);
                            _enrollLectureService.DeleteEnrollLectureByEnrollTeacherCourseID_WithoutUsing(id, context);
                            _enrollSectionOfCourse.DeleteEnrollSectionByEnrollTeacherCourseID_WithoutUsing(id, context);
                            _enrollAssignmentService.DeleteEnrollAssignmentByEnrollTeacherCourseID_WithoutUsing(id, context);
                            _enrollCourseTimeService.DeleteEnrollCourseTimeByEnrollTeacherCourseID_WithoutUsing(id, context);
                            var enrollCourseExams = _enrollCourseExamService.GetEnrollCourseExamByEnrollTeacherCourseId_WithoutUsing(id, context);
                            foreach (var enrollCourseExam in enrollCourseExams)
                                _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(enrollCourseExam.Id, context);
                            _enrollCourseExamService.DeleteEnrollCourseExamByEnrollTeacherCourseID_WithoutUsing(id, context);
                            #endregion
                            transaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delete Enrollment Course");
                            return Content("Fail");
                        }
                    }
                }

            }
            return RedirectToAction(nameof(Index), new { page = Page });
        }


        // GET: ControlPanel/EnrollmentCourse/Details/5
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Details")]
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
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/EnrollmentCourse/RegenerateCourseData/5
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "RegenerateCourseData")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Details")]
        public async Task<IActionResult> ShowRegenerateEnrollmentCourse(int? id, int page)
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
            return PartialView("Regenerate", EnrollmentCourseViewModel);
        }
        // POST: ControlPanel/EnrollmentCourse/RegenerateCourseData/5
        [AuditLogFilter(ActionDescription = "Regenerate Course data Post")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "RegenerateCourseData")]
        public async Task<IActionResult> RegenerateCourseData(int id, int Page)
        {


            var CurrentEnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var LanguageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (CurrentEnrollTeacherCourse != null && CurrentEnrollTeacherCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {

                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            #region //Delete All Enrolled Data
                            _enrollCourseResourceService.DeleteEnrollCourseResourceByEnrollTeacherCourseID_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            _enrollLectureService.DeleteEnrollLectureByEnrollTeacherCourseID_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            _enrollSectionOfCourse.DeleteEnrollSectionByEnrollTeacherCourseID_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            _enrollAssignmentService.DeleteEnrollAssignmentByEnrollTeacherCourseID_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            //var enrollCourseExams = _enrollCourseExamService.GetEnrollCourseExamByEnrollTeacherCourseId_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            //foreach (var enrollCourseExam in enrollCourseExams)
                            //    _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(enrollCourseExam.Id, context);
                            //_enrollCourseExamService.DeleteEnrollCourseExamByEnrollTeacherCourseID_WithoutUsing(CurrentEnrollTeacherCourse.Id, context);
                            #endregion

                            #region //Regenerate Enroll Section Of Course &  Lectures & Resources

                            foreach (var Section in _sectionOfCourseService.GetSectionOfCourseByCourseId_WithoutUsing(CurrentEnrollTeacherCourse.CourseId, context))
                            {

                                var AddedEnrollSectionOfCourse = _enrollSectionOfCourse.AddEnrollSectionOfCourseForAdmin(Section, CurrentEnrollTeacherCourse.Id, context);
                                #region //Regenerate Enroll Lectures & Resources

                                foreach (var Lecture in _lectureService.GetLectureBySectionId_WithoutUsing(Section.Id, context))
                                {

                                    var AddedEnrollLecture = _enrollLectureService.AddEnrollLectureForAdmin(Lecture, CurrentEnrollTeacherCourse.Id, AddedEnrollSectionOfCourse.Id, context);
                                    #region //Regenerate Enroll Resources 

                                    foreach (var Resource in _courseResourceService.GetCourseResourceByLectureId_WithoutUsing(Lecture.Id, context))
                                    {
                                        _enrollCourseResourceService.AddEnrollCourseResourceForAdmin(Resource, CurrentEnrollTeacherCourse.Id, AddedEnrollLecture.Id, context);
                                    }
                                    #endregion
                                }
                                #endregion
                            }


                            #endregion

                            #region //Regenerate Enroll Assignments

                            foreach (var Assignment in _assignmentService.GetAssignmentByCourseId_WithoutUsing(CurrentEnrollTeacherCourse.CourseId, context))
                            {

                                _enrollAssignmentService.AddEnrollAssignmentForAdmin(Assignment, CurrentEnrollTeacherCourse.Id, context);

                            }

                            #endregion

                            #region //Regenerate Enroll Course Exam & Questions

                            //foreach (var ExamTemplate in _examTemplateService.GetdExamTemplatetByCourseId_WithoutUsing(CurrentEnrollTeacherCourse.CourseId, context))
                            //{

                            //    var AddedEnrollCourseExam = _enrollCourseExamService.AddEnrollCourseExamForAdmin(ExamTemplate, CurrentEnrollTeacherCourse.Id, context);

                            //    #region //Regenerate Enroll Exam Questions
                            //    foreach (var ExamQuestions in _examQuestionService.GetQuestionByTemplateId_WithoutUsing(ExamTemplate.Id, context))
                            //    {
                            //        var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                            //        {
                            //            QuestionId = ExamQuestions.Id,
                            //            EnrollCourseExamId = AddedEnrollCourseExam.Id,
                            //            CreatedBy = User.Identity?.Name
                            //        };
                            //        _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion_WithoutUsing(enrollCourseExamQuestionViewModel, context);
                            //    }
                            //    #endregion
                            //}
                            #endregion

                            _enrollCourseQuizService.RefetchQuizzes_WithoutUsing(id, User?.Identity?.Name ?? string.Empty, context);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit  Regenerate Course Data");
                            return Content("Fail");
                        }
                    }
                }

            }

            return RedirectToAction(nameof(Index), new { page = Page });
        }

        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "View")]
        public async Task<IActionResult> EditPage(int? id)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            var lookups = LookupHelper.GetLookupDetailsByMasterCode(GeneralEnums.MasterLookupCodeEnums.TrainerRateMeasureType.ToString(), langId);


            ViewBag.CheckManagmentRate = _enrollCourseAllowUserRateService.CheckAllowUserToRate(User.Identity.Name, id.Value,
                lookups.FirstOrDefault(a => a.Code.Equals("Management_Rate"))?.Id ?? 0);
            ViewBag.CheckAcademicRate = _enrollCourseAllowUserRateService.CheckAllowUserToRate(User.Identity.Name, id.Value,
                lookups.FirstOrDefault(a => a.Code.Equals("Academic_Rate"))?.Id ?? 0);
            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollById(id.Value, langId);
            return View(enrollTeacherCourse);
        }

        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "RemoveStudent")]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            try
            {
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(id);
                if (enrollStudentCourse != null)
                {
                    _enrollStudentCourseService.DeleteEnrollStudentCourse(enrollStudentCourse);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while removing Student");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Transfer Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "TransferStudent")]
        public async Task<IActionResult> TransferStudent(int StudentId = 0, int oldEnrollTeacherCourseId = 0, int enrollTeacherCourseId = 0)
        {
            try
            {
                if (StudentId > 0 && enrollTeacherCourseId > 0)
                {

                    var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(enrollTeacherCourseId, StudentId);
                    if (CheckAbilityToEnrollStudentInCourse != "done")
                        return Json(new { result = CheckAbilityToEnrollStudentInCourse });


                    var CoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollTeacherCourseId);
                    var Price = 0m;
                    if (CoursDetails.Course.CoursePrice != null)
                        Price = CoursDetails.Course.CoursePrice.Value;

                    var NeedApproval = false;
                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(enrollTeacherCourseId, StudentId);
                    if (CheckHasPreRequestsCourse != "done")
                        NeedApproval = true;

                    var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                    var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

                    var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourse(StudentId, oldEnrollTeacherCourseId);
                    if (enrollStudentCourse != null)
                        _enrollStudentCourseService.DeleteEnrollStudentCourse(enrollStudentCourse);

                    _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                    {
                        CreatedOn = DateTime.Now,
                        Price = Price,
                        CourseId = enrollTeacherCourseId,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        NeedApproval = NeedApproval,
                        StudentId = StudentId,
                        IsPass = null,
                    });

                    return Json(new { result = "Success" });
                }
                else
                {
                    return Json(new { result = "Fail" });
                }

            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while  Add New Student");
                return Json(new { result = "Fail" });
            }
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Expel Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ExpelStudent")]
        public async Task<IActionResult> ExpelStudent(int EnrollStudentCourseId)
        {
            try
            {
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(EnrollStudentCourseId);
                if (enrollStudentCourse != null)
                {
                    _enrollStudentCourseService.ExpelEnrollStudentCourse(enrollStudentCourse, User?.Identity?.Name ?? string.Empty);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Expeling Student");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Cancel Expelled Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "ExpelStudent")]
        public async Task<IActionResult> CancelExpulsion(int EnrollStudentCourseId)
        {
            try
            {
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(EnrollStudentCourseId);
                if (enrollStudentCourse != null)
                {
                    _enrollStudentCourseService.CancelExpulsionEnrollStudentCourse(enrollStudentCourse, User?.Identity?.Name ?? string.Empty);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Cancelling Expelled Student");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Enrollment Course Delay Student")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "DelayStudent")]
        public async Task<IActionResult> DelayStudent(int EnrollStudentCourseId)
        {
            try
            {
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(EnrollStudentCourseId);
                if (enrollStudentCourse != null)
                {
                    _enrollStudentCourseService.DeleteEnrollStudentCourse(enrollStudentCourse);
                    _studentService.ChangeBalance(enrollStudentCourse.Student, enrollStudentCourse.Course.Course.CoursePrice ?? 0);
                    _balanceHistoryService.AddBalanceHistory(new StudentBalanceHistory()
                    {
                        CreatedOn = DateTime.Now,
                        Amount = enrollStudentCourse.Course.Course.CoursePrice,
                        Balance = enrollStudentCourse.Student.Balance,
                        StudentId = enrollStudentCourse.StudentId,
                        EnrollStudentCourseId = enrollStudentCourse.Id,
                        Title = "You are Delayed For",
                    });

                    try
                    {
                        await _smsService.SendEmail(new MessageViewModel
                        {
                            CreatedBy = User.Identity.Name,
                            Ids = new List<int>() { enrollStudentCourse.Student.ContactId },
                            Message = $"You are Delayed From the Course ${enrollStudentCourse.Course.Course.CourseName} and the Fee for the Course Has been added to your Account the added Fee Equal ${enrollStudentCourse.Course.Course.CoursePrice}",
                            Subject = "Al-Safa Payment",
                            Emails = new List<string>() { }
                        });
                    }
                    catch (Exception ex)
                    {
                        _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Sending Email For Delaying");
                    }
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delaying a Student");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "CreateTeamMeeting")]
        public async Task<IActionResult> ShowCreateTeamMeeting(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollTeacherCourseById = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id, langId);
            return PartialView("_CreateTeamMeeting", enrollTeacherCourseById);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> CreateMeeting(int id, DateTime start, DateTime end)
        {
            try
            {
                //var course = _enrollTeacherCourseService.GetEnrollById(id, (int)GeneralEnums.LanguageEnum.Arabic);

                //var teamId = _settingService.GetOrCreate("Team_Id", "").Value;

                //var clientApplication = ConfidentialClientApplicationBuilder
                //    .Create(_settingService.GetOrCreate("Team_Client_Id", "").Value)
                //    .WithTenantId(_settingService.GetOrCreate("Team_Tenant_Id", "").Value)
                //    .WithClientSecret(_settingService.GetOrCreate("Team_Client-Secret", "").Value)
                //    .Build();

                //var authProvider = new ClientCredentialProvider(clientApplication);
                //var graphClient = new GraphServiceClient(authProvider);
                //string channelName = course.CourseName + "_" + course.Teacher?.Contact?.FullName + "_" + course.WorkStartDate.Year;
                //string channelId = null;

                //try
                //{
                //    var channels = await graphClient.Teams[teamId].Channels
                //        .Request()
                //        .GetAsync();

                //    var matchingChannel = channels.FirstOrDefault(c => c.DisplayName.Equals(channelName, StringComparison.OrdinalIgnoreCase));

                //    if (matchingChannel != null)
                //        channelId = matchingChannel.Id;
                //    else
                //    {
                //        var channel = new Channel
                //        {
                //            DisplayName = channelName,
                //            Description = channelName,
                //            MembershipType = ChannelMembershipType.Standard,
                //        };

                //        var createdChannel = await graphClient.Teams[teamId].Channels
                //            .Request()
                //            .AddAsync(channel);

                //        channelId = createdChannel.Id;
                //    }
                //}
                //catch (ServiceException ex)
                //{

                //}

                //// Create a new online meeting
                //var onlineMeeting = new OnlineMeeting
                //{
                //    Subject = channelName,
                //    StartDateTime = DateTimeOffset.Parse(start.ToString()),
                //    EndDateTime = DateTimeOffset.Parse(end.ToString()),
                //    AllowAttendeeToEnableCamera = false,
                //    AllowAttendeeToEnableMic = true,
                //    IsBroadcast = false,

                //};

                //var createdMeeting = await graphClient.Me.OnlineMeetings
                //    .Request()
                //    .AddAsync(onlineMeeting);

                //var chatMessage = new ChatMessage
                //{
                //    Body = new ItemBody
                //    {
                //        ContentType = BodyType.Text,
                //        Content = "Join our meeting: " + createdMeeting.JoinWebUrl
                //    }
                //};

                //await graphClient.Teams[teamId].Channels[channelId].Messages
                //    .Request()
                //    .AddAsync(chatMessage);

                //// Get the URL for the online meeting
                //string meetingUrl = createdMeeting.JoinWebUrl;

                //_enrollTeacherCourseService.EditEnrollTeacherCourseNote(meetingUrl, course, start, end);
                //// Return the meeting URL as the response
                //return Json(true);
                var course = _enrollTeacherCourseService.GetEnrollTeacherCourseById(id);

                var userId = _settingService.GetOrCreate("Team_User_Id", "").Value;
                // Initialize the Microsoft Graph API client
                var clientApplication = ConfidentialClientApplicationBuilder
                    .Create(_settingService.GetOrCreate("Team_Client_Id", "").Value)
                    .WithTenantId(_settingService.GetOrCreate("Team_Tenant_Id", "").Value)
                    .WithClientSecret(_settingService.GetOrCreate("Team_Client-Secret", "").Value)
                    .Build();

                var authProvider = new ClientCredentialProvider(clientApplication);

                var graphClient = new GraphServiceClient(authProvider);

                // Create a new online meeting
                var meeting = new OnlineMeeting
                {
                    Subject = course.CourseName + " Meeting",
                    StartDateTime = DateTimeOffset.Parse(start.ToString()),
                    EndDateTime = DateTimeOffset.Parse(end.ToString()),
                    AllowAttendeeToEnableCamera = false,
                    AllowAttendeeToEnableMic = true,
                    IsBroadcast = false,
                };

                var createdMeeting = await graphClient.Users[userId].OnlineMeetings
                    .Request()
                    .AddAsync(meeting);

                // Get the URL for the online meeting
                string meetingUrl = createdMeeting.JoinWebUrl;

                _enrollTeacherCourseService.EditEnrollTeacherCourseNote(meetingUrl, course, start, end);
                // Return the meeting URL as the response
                return Json(true);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Creating Team Meeting");
                return null;
            }
        }

        public async Task<IActionResult> GetLearningMethod(int id)
        {
            var course = _courseService.GetCourseById(id);
            return Json(course?.LearningMethodId);
        }

        public async Task<IActionResult> GetLearningMethodCode(int id)
        {
            var lookup = LookupHelper.GetLookupDetailsById(id);
            return Json(lookup?.Code);
        }

        [AuditLogFilter(ActionDescription = "Teacher Attendances List")]
        [CustomAuthentication(PageName = "TeacherAttendances", PermissionKey = "View")]
        public async Task<IActionResult> GetTeacherAttendancesData(int? page, int CourseId)
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

        [AuditLogFilter(ActionDescription = "Calculate Students Marks")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "CalculateStudentsMark")]
        public async Task<IActionResult> CalculateMarks(int CourseId)
        {
            try
            {
                var course = _enrollTeacherCourseService.GetEnrollTeacherCourseById(CourseId);
                _enrollStudentCourseService.CalculateMarks(course);
                var closeCourse = Boolean.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Close_Course_After_Calculation, "false")?.Value);
                if (closeCourse)
                    _enrollTeacherCourseService.CloseCourse(CourseId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Calculate Students Marks");
                return null;
            }

        }

        [AuditLogFilter(ActionDescription = "EditCertificateAdoption")]
        [CustomAuthentication(PageName = "EnrollmentCourse", PermissionKey = "EditCertificateAdoption")]
        public async Task<IActionResult> ShowCertificates(int id, bool show)
        {
            try
            {
                _certificateAdoptionService.EditCertificateAdoption(id, show);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Editting CertificateAdoption");
                return null;
            }

        }
    }
}

