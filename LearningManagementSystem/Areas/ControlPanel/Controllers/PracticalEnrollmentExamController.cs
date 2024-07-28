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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Printing;
using Microsoft.Extensions.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class PracticalEnrollmentExamController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly ILogService _logService;
        private readonly ISettingService _settingService;
        private readonly IPracticalEnrollmentExamService _practicalEnrollmentExamService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IPracticalEnrollmentExamStudentService _practicalEnrollmentExamStudentService;
        private readonly IStringLocalizer<PracticalEnrollmentExamController> _localizer;
        private readonly ITrainerService _trainerService;

        public PracticalEnrollmentExamController(ICookieService cookieService, ILogService logService, ISettingService settingService
            , IPracticalEnrollmentExamService practicalEnrollmentExamService, IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollStudentCourseService enrollStudentCourseService, IPracticalEnrollmentExamStudentService practicalEnrollmentExamStudentService
            , IStringLocalizer<PracticalEnrollmentExamController> localizer, ITrainerService trainerService)
        {
            _cookieService = cookieService;
            _logService = logService;
            _settingService = settingService;
            _practicalEnrollmentExamService = practicalEnrollmentExamService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _practicalEnrollmentExamStudentService = practicalEnrollmentExamStudentService;
            _localizer = localizer;
            _trainerService = trainerService;
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Practical Exams")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> GetPracticalExams(int? page, int pagination, int? TecherCourseId, string searchText, int? Type)
        {
            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalEnrollmentExamPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalEnrollmentExamPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.Page = page;
            ViewBag.CourseId = _enrollTeacherCourseService.GetEnrollById(TecherCourseId ?? 0, languageId)?.CourseId;
            ViewBag.TecherCourseId = TecherCourseId;
            ViewBag.Type = Type;

            ViewBag.Count = _enrollStudentCourseService.GetStudentCount(TecherCourseId ?? 0);
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;

            var result = _practicalEnrollmentExamService.GetPracticalEnrollmentExam(Type, TecherCourseId ?? 0, page ?? 1, searchText, languageId, pagination);
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/PracticalExams/Create
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddExam")]
        public IActionResult Create(int courseId, int techerCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.CourseId = courseId;
            ViewBag.TecherCourseId = techerCourseId;

            return PartialView("_Create");
        }

        // POST: ControlPanel/PracticalExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddExam")]
        public async Task<IActionResult> Create(PracticalEnrollmentExamViewModel practicalExam)
        {
            try
            {
                practicalExam.CreatedOn = DateTime.Now;
                practicalExam.CreatedBy = User.Identity?.Name ?? string.Empty;
                practicalExam.TypeId = _practicalEnrollmentExamService.GetTypeId(practicalExam.PracticalExamId ?? 0);
                if (practicalExam.TypeId == 0)
                    return null;

                _practicalEnrollmentExamService.AddPracticalEnrollmentExam(practicalExam);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalEnrollmentExam");
                return null;
            }
        }

        // GET: ControlPanel/PracticalExams/Create
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddExam")]
        public IActionResult CreateGroup(int? semester, int courseId, int? teacherId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.SemesterId = semester;
            ViewBag.CourseId = courseId;
            ViewBag.TeacherId = teacherId;

            return PartialView("_CreateGroup");
        }

        // POST: ControlPanel/PracticalExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddExam")]
        public async Task<IActionResult> CreateGroup(PracticalEnrollmentExamViewModel practicalExam)
        {
            try
            {

                practicalExam.CreatedOn = DateTime.Now;
                practicalExam.CreatedBy = User.Identity?.Name ?? string.Empty;
                practicalExam.TypeId = _practicalEnrollmentExamService.GetTypeId(practicalExam.PracticalExamId ?? 0);
                if (practicalExam.TypeId == 0)
                    return null;

                var course = _enrollTeacherCourseService.GetEnrollTeacherCoursesIds(practicalExam.CourseId, practicalExam.TeacherId, practicalExam.SemesterId);

                using (var context = new LearningManagementSystemContext())
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    try
                    {
                        foreach (var item in course)
                        {
                            practicalExam.EnrollTeacherCourseId = item.Id;
                            _practicalEnrollmentExamService.AddPracticalEnrollmentExam_WithoutUsing(practicalExam, context);
                        }
                        transaction.Commit();
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalEnrollmentExam");
                        return null;
                    }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalEnrollmentExam");
                return null;
            }
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "EditExam")]
        public IActionResult Edit(int id, int courseId, int techerCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.CourseId = courseId;
            ViewBag.TecherCourseId = techerCourseId;
            var result = _practicalEnrollmentExamService.GetPracticalEnrollmentExamById(id);
            return PartialView("_Edit", new PracticalEnrollmentExamViewModel(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "EditExam")]
        public async Task<IActionResult> Edit(PracticalEnrollmentExamViewModel practicalExam)
        {
            try
            {
                var exam = _practicalEnrollmentExamService.GetPracticalEnrollmentExamById(practicalExam.Id);
                if (exam == null)
                    return null;

                _practicalEnrollmentExamService.EditPracticalEnrollmentExam(practicalExam, exam);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalEnrollmentExam");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Show Students for Practical Enrollment Exam")]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddStudent")]
        public async Task<IActionResult> ShowEnrolledStudents(int PracticalEnrollmentExamId, int EnrollTeacherCourseId, string searchText, int? page, int pagination, int languageId = 0)
        {

            if (page == 0 || page == null)
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
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId, languageId);
            if (enrollTeacherCourse == null || enrollTeacherCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            enrollTeacherCourse.LanguageId = languageId;
            ViewBag.LangId = languageId;
            ViewBag.EnrollTeacherCourseId = EnrollTeacherCourseId;
            ViewBag.PracticalEnrollmentExamId = PracticalEnrollmentExamId;
            ViewBag.User = User?.Identity?.Name ?? string.Empty;
            ViewBag.SubjectsCount = _practicalEnrollmentExamStudentService.GetSubjectCount(PracticalEnrollmentExamId, enrollTeacherCourse.CourseId);
            var result = _enrollStudentCourseService.GetEnrollStudentCoursesForQuiz(page, enrollTeacherCourse.Id, languageId, pagination, searchText);

            return PartialView("_Students", result);
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddStudent")]
        [AuditLogFilter(ActionDescription = "List Subject for PracticalEnrollmentExam")]
        public async Task<IActionResult> ShowSubjects(int EnrollStudentCourseId, int TecherCourseId, int PracticalEnrollmentExamId, int? TypeId, int? page1, int pagination1, string searchText1)
        {
            try
            {
                var practicalEnrollmentExamStudent = _practicalEnrollmentExamStudentService.GetOrCreate(PracticalEnrollmentExamId, EnrollStudentCourseId, User?.Identity?.Name ?? string.Empty);

                ViewBag.Page = page1;
                ViewBag.TecherCourseId = TecherCourseId;
                ViewBag.EnrollStudentCourseId = EnrollStudentCourseId;
                ViewBag.PracticalEnrollmentExamId = PracticalEnrollmentExamId;
                ViewBag.PracticalEnrollmentExamStudentId = practicalEnrollmentExamStudent.Id;
                ViewBag.TypeId = TypeId;
                ViewBag.User = User?.Identity?.Name ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(searchText1))
                    ViewBag.searchText = searchText1;

                var val = _cookieService.GetCookie(Constants.Pagenation.SubjectPagination);

                if (val == null && pagination1 == 0)
                    pagination1 = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                else if (pagination1 != 0)
                    pagination1 = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SubjectPagination, pagination1.ToString(), 7));
                else
                    pagination1 = int.Parse(val != "" ? val : "10");

                ViewBag.PaginationValue1 = pagination1;


                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;
                var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(TecherCourseId, languageId);

                var result = _practicalEnrollmentExamStudentService.GetSubjectsForStudents(PracticalEnrollmentExamId, enrollTeacherCourse.CourseId, TypeId, searchText1, page1 ?? 1, languageId, pagination1);
                ViewBag.Subjects = _practicalEnrollmentExamStudentService.GetStudentSubjects(practicalEnrollmentExamStudent.Id, languageId);

                return PartialView("_Subjects", result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Add Subject to Student")]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddStudent")]
        public async Task<IActionResult> AddOrRemoveSubjects(int PracticalEnrollmentExamStudentId, int SubjectId, string checkedRow)
        {
            try
            {
                if (_practicalEnrollmentExamStudentService.DidExamStart(PracticalEnrollmentExamStudentId) && _practicalEnrollmentExamStudentService.DidExamNotEnd(PracticalEnrollmentExamStudentId))
                {
                    //if (!_practicalEnrollmentExamStudentService.IsExamSubmited(PracticalEnrollmentExamStudentId))
                    if (true)
                    {
                        var examQuestion = _practicalEnrollmentExamStudentService.GetStudentSubject(PracticalEnrollmentExamStudentId, SubjectId);
                        if (checkedRow.ToLower() == "true")
                        {
                            var eq = new PracticalEnrollmentExamStudentSubject()
                            {
                                PracticalEnrollmentExamStudentId = PracticalEnrollmentExamStudentId,
                                SubjectId = SubjectId,
                                CreatedBy = User?.Identity?.Name ?? string.Empty,
                                CreatedOn = DateTime.Now,

                            };
                            _practicalEnrollmentExamStudentService.AddPracticalEnrollmentExamStudentSubject(eq);
                        }
                        else
                            _practicalEnrollmentExamStudentService.RemovePracticalEnrollmentExamStudentSubject(examQuestion);

                        //_practicalEnrollmentExamStudentService.ReCalculateMark(PracticalEnrollmentExamStudentId);
                        return Json(new { success = true });
                    }
                    else
                        return Json(new { success = false, message = _localizer["The Exam is Already Submitted"].Value });
                }
                else
                    return Json(new { success = false, message = !_practicalEnrollmentExamStudentService.DidExamNotEnd(PracticalEnrollmentExamStudentId) && _practicalEnrollmentExamStudentService.DidExamStart(PracticalEnrollmentExamStudentId) ? _localizer["The Exam Is Finished"].Value : _localizer["The Exam Did not Start Yet"].Value });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Practical Question to Exam");
                return Json(new { success = false });
            }
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddPoints")]
        [AuditLogFilter(ActionDescription = "Start Practical Enrollment Exam")]
        public async Task<IActionResult> ShowAddPoints(int EnrollStudentCourseId, int PracticalEnrollmentExamId)
        {
            var practicalEnrollmentExamStudent = _practicalEnrollmentExamStudentService.GetOrCreate(PracticalEnrollmentExamId, EnrollStudentCourseId, User?.Identity?.Name ?? string.Empty);
            var exam = _practicalEnrollmentExamService.GetPracticalEnrollmentExamById(PracticalEnrollmentExamId);
            var mark = _practicalEnrollmentExamService.GetMark(exam.PracticalExamId ?? 0, exam.EnrollTeacherCourse.CourseId);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.EnrollStudentCourseId = EnrollStudentCourseId;
            ViewBag.PracticalEnrollmentExamId = PracticalEnrollmentExamId;
            ViewBag.Mark = exam.PracticalExam.Mark;
            ViewBag.Type = LookupHelper.GetLookupDetailsById(exam.TypeId ?? 0, languageId)?.Code == "QuranMemorization";
            ViewBag.MarkAfterConversion = exam.PracticalExam.MarkAfterConversion;
            ViewBag.SubjectMark = mark > 0 ? mark : int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Exam_Subject_Points, "20").Value);
            ViewBag.Subjects = _practicalEnrollmentExamStudentService.GetStudentSubjects(practicalEnrollmentExamStudent.Id, languageId);
            ViewBag.PracticalEnrollmentExamStudentId = practicalEnrollmentExamStudent.Id;

            var questions = _practicalEnrollmentExamStudentService.GetQuestions(exam?.PracticalExamId ?? 0, languageId);

            return PartialView("_Exam", questions);
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddPoints")]
        [AuditLogFilter(ActionDescription = "Submiting Practical Exam")]
        public async Task<IActionResult> SubmitExam(int practicalEnrollmentExamStudentId, List<ExamObjectViewModel> examObjects, decimal mark, decimal markAfterConversion, decimal subjectMark)
        {
            try
            {
                if (_practicalEnrollmentExamStudentService.DidExamStart(practicalEnrollmentExamStudentId) && _practicalEnrollmentExamStudentService.DidExamNotEnd(practicalEnrollmentExamStudentId))
                {

                    foreach (var item in examObjects) item.CreatedBy = User?.Identity?.Name ?? string.Empty;
                    _practicalEnrollmentExamStudentService.AddExam(practicalEnrollmentExamStudentId, examObjects, subjectMark);
                    _practicalEnrollmentExamStudentService.EditMark(practicalEnrollmentExamStudentId, mark, markAfterConversion);
                    return Json(new { success = true });
                }
                else
                    return Json(new { success = false, message = !_practicalEnrollmentExamStudentService.DidExamNotEnd(practicalEnrollmentExamStudentId) && _practicalEnrollmentExamStudentService.DidExamStart(practicalEnrollmentExamStudentId) ? _localizer["The Exam Is Finished"].Value : _localizer["The Exam Did not Start Yet"].Value });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Submiting Practical Exam");
                return null;
            }
        }


        // POST: ControlPanel/Permissions/Delete/5
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "DeleteExam")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exam = _practicalEnrollmentExamStudentService.GetPracticalEnrollmentExamById(id);
                _practicalEnrollmentExamStudentService.DeletePracticalEnrollmentExam(exam);
                return Json(true);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete PracticalEnrollmentExam");
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "MarkAdoption")]
        public async Task<IActionResult> MarkAdoption(int id, bool adopt)
        {
            try
            {
                var exam = _practicalEnrollmentExamStudentService.GetPracticalEnrollmentExamById(id);
                _practicalEnrollmentExamStudentService.EditMarkAdoption(exam, adopt);
                return Json(true);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editting MarkAdoption for PracticalEnrollmentExam");
                return null;
            }
        }

        public async Task<IActionResult> GetCount(int PracticalEnrollmentExamId, int EnrollStudentCourseId)
        {
            var count = _practicalEnrollmentExamStudentService.GetSubjectCoumtForStudent(PracticalEnrollmentExamId, EnrollStudentCourseId, User?.Identity.Name ?? string.Empty);
            return Json(count);
        }

        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddTrainer")]
        [AuditLogFilter(ActionDescription = "List of Trainer in PracticalEnrollmentExam")]
        public async Task<IActionResult> ShowTeachers(int PracticalEnrollmentExamId, int? page1, string searchText1)
        {
            try
            {
                ViewBag.Page = page1;
                ViewBag.PracticalEnrollmentExamId = PracticalEnrollmentExamId;

                if (!string.IsNullOrWhiteSpace(searchText1))
                    ViewBag.searchText = searchText1;


                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;

                var result = _trainerService.GetTrainers(searchText1, page1, languageId, 10);
                ViewBag.Trainers = _practicalEnrollmentExamService.GetPracticalEnrollmentExamTrainers(PracticalEnrollmentExamId);
                return PartialView("_Teachers", result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Add Trainer to PracticalEnrollmentExam")]
        [CustomAuthentication(PageName = "PracticalEnrollmentExam", PermissionKey = "AddTrainer")]
        public async Task<IActionResult> AddOrRemoveTeachers(int PracticalEnrollmentExamId, int TrainerId, string checkedRow)
        {
            try
            {
                var trainer = _practicalEnrollmentExamService.GetPracticalEnrollmentExamTrainers(PracticalEnrollmentExamId , TrainerId);
                if (checkedRow.ToLower() == "true")
                {
                    var eq = new PracticalEnrollmentExamTrainer()
                    {
                        PracticalEnrollmentExamId = PracticalEnrollmentExamId,
                        TrainerId = TrainerId,
                        CreatedBy = User?.Identity?.Name ?? string.Empty,
                        CreatedOn = DateTime.Now,

                    };
                    _practicalEnrollmentExamService.AddPracticalEnrollmentExamTrainer(eq);
                }
                else
                    _practicalEnrollmentExamService.RemovePracticalEnrollmentExamTrainer(trainer);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Trainer PracticalEnrollmentExam");
                return Json(new { success = false });
            }
        }
    }
}
