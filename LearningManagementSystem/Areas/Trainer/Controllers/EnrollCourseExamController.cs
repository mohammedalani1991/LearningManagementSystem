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

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollCourseExamController : Controller
    {
        private readonly ILogService _logService;
        private readonly IExamTemplateService _examTemplateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollCourseExamService _enrollCourseExamService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly IExamQuestionService _examQuestionService;
        private readonly IEnrollCourseExamQuestionService _enrollCourseExamQuestionService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly IEnrollStudentExamAnswerService _enrollStudentExamAnswerService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IEnrollLectureService _enrollLectureService;
        public EnrollCourseExamController(
            ICookieService cookieService, ILogService logService,
            IExamTemplateService examTemplateService, ISettingService settingService, 
            ICourseCategoryService courseCategoryService,
            ICourseService courseService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollCourseExamService enrollCourseExamService,
            ITrainerService trainerService,
            IUserProfileService userProfileService,
            IExamQuestionService examQuestionService,
            IEnrollCourseExamQuestionService enrollCourseExamQuestionService,
            IEnrollStudentExamService enrollStudentExamService, IEnrollStudentExamAnswerService enrollStudentExamAnswerService, IEnrollStudentCourseService enrollStudentCourseService, IEnrollLectureService enrollLectureService)
        {
            _logService = logService;
            _examTemplateService = examTemplateService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseCategoryService = courseCategoryService;
            _courseService = courseService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollCourseExamService = enrollCourseExamService;
            _trainerService = trainerService;
            _userProfileService = userProfileService;
            _examQuestionService = examQuestionService;
            _enrollCourseExamQuestionService = enrollCourseExamQuestionService;
            _enrollStudentExamService = enrollStudentExamService;
            _enrollStudentExamAnswerService = enrollStudentExamAnswerService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _enrollLectureService = enrollLectureService;
        }



        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enroll Course Exam")]
        public async Task<IActionResult> GetData(int? page,int? TeacherId, int pagination, int? CourseId)
        {

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var TrainerDetails = _trainerService.GetTrainerByContactId(ContactID);
            if (TrainerDetails != null)
                TeacherId = TrainerDetails.Id;
            else
                TeacherId = -2000; //do not return any data

            if (page == 0)
                page = 1;

            ViewBag.Page = page;


            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
            }
            else
            {
                ViewBag.CourseId = 0;
            }

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCourseExamSPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCourseExamSPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollCourseExamSTable);
            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;
            var result = _enrollCourseExamService.GetEnrollCourseExams(page, languageId, pagination, CourseId, TeacherId);

            return PartialView("_Index", result);
        }



        // GET: Trainer/EnrollCourseExam/GetSemesterByID
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Get Exam Template By ID")]
        public IActionResult GetExamTemplateByID(int ExamTemplateId, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var ExamTemplate = _examTemplateService.GetExamTemplateById(ExamTemplateId, languageId);
            return Json(ExamTemplate);
        }

        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enroll Course Exam")]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // GET: Trainer/EnrollCoursesContent/Create
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Create Get")]
        public IActionResult ShowCreate(int EnrollTeacherCoursId, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.LangId = languageId;
            var enrollTeacherCoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCoursId);
            if (enrollTeacherCoursDetails == null || enrollTeacherCoursDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var CategoryId = 0;
            if (enrollTeacherCoursDetails.Course.Category != null)
                CategoryId = enrollTeacherCoursDetails.Course.Category.Id;

            ViewBag.ListExamTemplates = _examTemplateService.GetAllExamTemplatet(languageId,enrollTeacherCoursDetails.CourseId, CategoryId);
            ViewBag.ListLessonsCourse = _enrollLectureService.GetEnrollLectureByEnrollTeacherCoursId(EnrollTeacherCoursId, languageId);
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCoursDetails.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("Create",new EnrollCourseExamViewModel{EnrollTeacherCourseId= EnrollTeacherCoursId });
        }


        // POST: Trainer/EnrollCourseExam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Create Post")]
        public async Task<IActionResult> Create(
            EnrollCourseExamViewModel EnrollCourseExamViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (EnrollCourseExamViewModel.IsOnlineLearningMethod == false && EnrollCourseExamViewModel.TestTypeID == (int)GeneralEnums.TestType.Exam)
                    {
                        if (EnrollCourseExamViewModel.PublishDate > EnrollCourseExamViewModel.PublishEndDate)
                            return Content("Fail");
                    }
                    if (EnrollCourseExamViewModel.TestTypeID == (int)GeneralEnums.TestType.Quiz)
                    {
                       
                        if (EnrollCourseExamViewModel.EnrollLectureId <=0)
                            return Content("FailEnrollLectureId");
                    }
                       

                    if (EnrollCourseExamViewModel.LanguageId == 0)
                        EnrollCourseExamViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    EnrollCourseExamViewModel.CreatedBy = User.Identity?.Name;

                    var AddedEnrollCourseExam = _enrollCourseExamService.AddEnrollCourseExam(EnrollCourseExamViewModel);
                    if (EnrollCourseExamViewModel.ExamTemplateId != null && EnrollCourseExamViewModel.ExamTemplateId > 0 && EnrollCourseExamViewModel.TestTypeID == (int)GeneralEnums.TestType.Exam)
                    {
                        using (var context = new LearningManagementSystemContext())
                        {
                            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                      

                                    #region //Add Enroll Exam Questions
                                    foreach (var ExamQuestions in _examQuestionService.GetQuestionByTemplateId_WithoutUsing(EnrollCourseExamViewModel.ExamTemplateId.Value, context))
                                    {
                                        var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                                        {
                                            QuestionId = ExamQuestions.Id,
                                            Mark= ((ExamQuestions.Mark.HasValue) ? ExamQuestions.Mark.Value : 0) ,
                                            EnrollCourseExamId = AddedEnrollCourseExam.Id,
                                            CreatedBy = User.Identity?.Name
                                        };
                                        _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion_WithoutUsing(enrollCourseExamQuestionViewModel, context);
                                    }
                                    #endregion
                                    transaction.Commit();

                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Enroll Course Exam Dic");
                                    return Content("Fail");
                                }
                            }
                        }
                    }

                    return Content("Success");
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Enroll Course Exam Dic");
                    return Content("Fail");
                }
            }
            return Content("Fail");
        }

        // POST: Trainer/EnrollCourseExam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "EditMarkQuestion")]
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Create Post")]
        public async Task<IActionResult> EditMarkQuestion(
            int ExamQuestionid,int Mark)
        {
            if (ExamQuestionid > 0 && Mark > 0)
            {

              
                var EnrollCourseExamQuestion = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByID(ExamQuestionid);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollCourseExamId(EnrollCourseExamQuestion.EnrollCourseExamId);
                if (EnrollStudentExam == null)
                {
                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                _enrollCourseExamQuestionService.EditMarkEnrollCourseExamQuestion(ExamQuestionid, Mark, EnrollCourseExamQuestion, context);
                                transaction.Commit();
                                return Content("Success");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Enroll Course Exam Dic");
                                return Content("Fail");
                            }
                        }
                    }
                }
                else
                {
                    return Content("Fail");
                }

            }
            return Content("Fail");
        }
        

        // GET: Trainer/EnrollCourseExam/Edit/5
        [AuditLogFilter(ActionDescription = "EnrollCourseExam Edit Get")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
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

            var enrollCourseExamS = _enrollCourseExamService.GetEnrollCourseExamById(id.Value, languageId);
            enrollCourseExamS.LanguageId = languageId;
            if (enrollCourseExamS == null || enrollCourseExamS.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var enrollTeacherCoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollCourseExamS.EnrollTeacherCourseId);
            if (enrollTeacherCoursDetails == null || enrollTeacherCoursDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var CategoryId = 0;
            if (enrollTeacherCoursDetails.Course.Category != null)
                CategoryId = enrollTeacherCoursDetails.Course.Category.Id;

            ViewBag.ListExamTemplates = _examTemplateService.GetAllExamTemplatet(languageId,enrollTeacherCoursDetails.CourseId, CategoryId);
            ViewBag.ListLessonsCourse = _enrollLectureService.GetEnrollLectureByEnrollTeacherCoursId(enrollTeacherCoursDetails.Id, languageId);
            ViewBag.LangId = languageId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCoursDetails.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("Edit", enrollCourseExamS);
        }

        // POST: Trainer/EnrollCourseExam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Enroll CourseExam Edit Pot")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(
            EnrollCourseExamViewModel EnrollCourseExamViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollCourseExamId(EnrollCourseExamViewModel.Id);
                    if (EnrollStudentExam == null)
                    {
                        if (EnrollCourseExamViewModel.IsOnlineLearningMethod == false && EnrollCourseExamViewModel.TestTypeID == (int)GeneralEnums.TestType.Exam)
                        {
                            if (EnrollCourseExamViewModel.PublishDate > EnrollCourseExamViewModel.PublishEndDate)
                                return Content("Fail");
                        }

                        if (EnrollCourseExamViewModel.TestTypeID == (int)GeneralEnums.TestType.Quiz)
                        {

                            if (EnrollCourseExamViewModel.EnrollLectureId <= 0)
                                return Content("FailEnrollLectureId");
                        }

                   
                        var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamByCourseExamId(EnrollCourseExamViewModel.Id);
                        if (enrollCourseExam != null && enrollCourseExam.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            if (EnrollCourseExamViewModel.LanguageId == 0)
                                EnrollCourseExamViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                            var EditedEnrollCourseExam = _enrollCourseExamService.EditEnrollCourseExam(EnrollCourseExamViewModel, enrollCourseExam);

                        }

                        return Content("Success");
                    }
                    else
                    {
                        return Content("Fail");
                    }
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing EnrollCourseExam Dic (Post)");
                    return Content("Fail");
                }
            }
            return Content("Fail");
        }

        // GET: Trainer/EnrollCourseExam/Details/5
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollCourseExamS = _enrollCourseExamService.GetEnrollCourseExamById(id.Value, langId);

            if (enrollCourseExamS == null || enrollCourseExamS.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            ViewBag.TimeZone =  TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;  
            return PartialView("Details", enrollCourseExamS);
        }

        // GET: Trainer/EnrollCourseExam/Delete/5
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Delete Get")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Delete")]
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollCourseExamS = _enrollCourseExamService.GetEnrollCourseExamById(id.Value, langId);
            if (enrollCourseExamS == null || enrollCourseExamS.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;
            return PartialView("Delete", enrollCourseExamS);
        }

        // POST: Trainer/EnrollCourseExam/Delete/5
        [AuditLogFilter(ActionDescription = "Enroll Course Exam Delete Post")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteEnrollCourseExam(int id)
        {
            try
            {
                var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamByCourseExamId(id);
                if (enrollCourseExam != null && enrollCourseExam.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                        using (var context = new LearningManagementSystemContext())
                        {
                            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                #region //Delete All Enrolled Data
                              
                                _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(enrollCourseExam.Id, context);
                                _enrollCourseExamService.DeleteEnrollCourseExamByID_WithoutUsing(enrollCourseExam.Id, context);
                                #endregion

                                transaction.Commit();

                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Enroll Course Exam Dic");
                                    return Content("Fail");
                                }
                            }
                        }
                   
                }
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Enroll Course Exam (Post)");
                return Content("Fail");
            }

            return Content("Success");
        }


        // POST: Trainer/EnrollCourseExam/ShowExamQuestions
        [AuditLogFilter(ActionDescription = "Exam Questions View")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        public async Task<IActionResult> ShowExamQuestions(int id, int languageId,int? page)
        {



            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            ViewBag.id = id;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;
     
           
            ViewBag.ExamQuestionsCount = _enrollCourseExamQuestionService.GetCountEnrollCourseExamQuestionByEnrollCourseExamID(id);
            var result = _enrollCourseExamQuestionService.GetExamQuestions(page, languageId, 5, id);
            ViewBag.EnrollCourseExamId = id;
            return PartialView("_IndexExamQuestions", result);
        }

        [AuditLogFilter(ActionDescription = "Show Exam Correction")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowExamCorrection(int EnrollCourseExamId, int EnrollStudentExamID)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollStudentExamID = EnrollStudentExamID;
            var result = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            foreach (var Exam in result)
                Exam.EnrollStudentExamAnswers = _enrollStudentExamAnswerService.GetListEnrollStudentExamAnswer(EnrollStudentExamID);

            return View("_IndexExamCorrection", result);
        }
        [AuditLogFilter(ActionDescription = "Show Submitted Exams")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        public async Task<IActionResult> ShowExamView(int EnrollCourseExamId, int EnrollStudentExamID)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollStudentExamID = EnrollStudentExamID;
            var result = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            foreach (var Exam in result)
                Exam.EnrollStudentExamAnswers = _enrollStudentExamAnswerService.GetListEnrollStudentExamAnswer(EnrollStudentExamID);
            return View("_IndexExamView", result);
        }
        [AuditLogFilter(ActionDescription = "Show Submitted Exams")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "View")]
        public async Task<IActionResult> ShowSubmittedExams(int id, int languageId, int? page)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;
            ViewBag.id = id;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;





            var result = _enrollStudentExamService.GetEnrollStudentExam(page, languageId, 5, id);
            return PartialView("_IndexSubmittedExams", result);
        }
        [AuditLogFilter(ActionDescription = "Show  Submitted Exams")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
        public async Task<IActionResult> SubmittedExamsCorrection(IFormCollection form, int EnrollCourseExamId,int EnrollStudentExamID)
        {
            try
            {

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
             
                var EnrollCourseExamQuestions = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
                var EnrollStudentCourseExam = _enrollStudentExamService.GetEnrollStudentExam(EnrollStudentExamID);
                var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamById(EnrollCourseExamId);
           

                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<EnrollStudentExamAnswerViewModel> enrollStudentExamAnswers = new List<EnrollStudentExamAnswerViewModel>();
                            foreach (var exam in EnrollCourseExamQuestions)
                            {
                                if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.Text)
                                {
                                    
                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("RadioAnswer_" + exam.Question.Question.Id))
                                    {
                                        if(int.Parse(form["RadioAnswer_" + exam.Question.Question.Id]) == 1)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                        enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                        {
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            EnrollCourseExamQuestionId = exam.Id,
                                            Status = (int)GeneralEnums.StatusEnum.Active,
                                            Answer = form["textarea_" + exam.Question.Question.Id],
                                            EnrollStudentExamId= EnrollStudentCourseExam.Id,
                                            Mark = Mark,
                                            IsCorrect= IsCorrect
                                        });
                                    }
                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.CheckBox)
                                {
                                    var enrollStudentExamAnswerOption = new List<EnrollStudentExamAnswerOption>();
                                    var CorrectAnswers = exam.Question.Question.QuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("Checkbox_" + exam.Question.Question.Id))
                                    {
                                        var Checkboxs = form["Checkbox_" + exam.Question.Question.Id];
                                        foreach (var op in Checkboxs)
                                        {
                                            enrollStudentExamAnswerOption.Add(new EnrollStudentExamAnswerOption
                                            {
                                                QusetionOptionId = int.Parse(op),
                                                CreatedBy = User.Identity.Name,
                                                CreatedOn = DateTime.Now,
                                                Status = (int)GeneralEnums.StatusEnum.Active,

                                            });
                                        }
                                        var StudentsAnswers = enrollStudentExamAnswerOption.Select(e => e.QusetionOptionId).ToList();
                                        bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                                        if (isEqual)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                        enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                        {
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            EnrollCourseExamQuestionId = exam.Id,
                                            EnrollStudentExamId = EnrollStudentCourseExam.Id,
                                            Status = (int)GeneralEnums.StatusEnum.Active,
                                            EnrollStudentExamAnswerOptions = enrollStudentExamAnswerOption,
                                            Mark = Mark,
                                            IsCorrect = IsCorrect
                                        });
                                    }

                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.RadioButton)
                                {
                                    var enrollStudentExamAnswerOption = new List<EnrollStudentExamAnswerOption>();
                                    var CorrectAnswers = exam.Question.Question.QuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("Radios_" + exam.Question.Question.Id))
                                    {
                                        enrollStudentExamAnswerOption.Add(new EnrollStudentExamAnswerOption
                                        {
                                            QusetionOptionId = int.Parse(form["Radios_" + exam.Question.Question.Id]),
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            Status = (int)GeneralEnums.StatusEnum.Active,

                                        });

                                        var StudentsAnswers = enrollStudentExamAnswerOption.Select(e => e.QusetionOptionId).ToList();
                                        bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                                        if (isEqual)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                    }

                                    enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                    {
                                        CreatedBy = User.Identity.Name,
                                        CreatedOn = DateTime.Now,
                                        EnrollCourseExamQuestionId = exam.Id,
                                        EnrollStudentExamId = EnrollStudentCourseExam.Id,
                                        Status = (int)GeneralEnums.StatusEnum.Active,
                                        EnrollStudentExamAnswerOptions = enrollStudentExamAnswerOption,
                                        Mark = Mark,
                                        IsCorrect = IsCorrect
                                    });
                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.UploadAttachment)
                                {

                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("RadioAnswer_" + exam.Question.Question.Id))
                                    {
                                        if (int.Parse(form["RadioAnswer_" + exam.Question.Question.Id]) == 1)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                        enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                        {
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            EnrollCourseExamQuestionId = exam.Id,
                                            Status = (int)GeneralEnums.StatusEnum.Active,
                                            Answer = form["inputHidden_" + exam.Question.Question.Id],
                                            EnrollStudentExamId = EnrollStudentCourseExam.Id,
                                            Mark = Mark,
                                            IsCorrect = IsCorrect
                                        });
                                    }
                                }


                            }
                         
                            var FinalMarks = EnrollCourseExamQuestions.Select(e => e.Mark.Value).Sum();
                            var ExamMark = ((enrollCourseExam.ExamFinalMark.HasValue) ? enrollCourseExam.ExamFinalMark.Value : 0);
                            var MarkHeGot = enrollStudentExamAnswers.Select(e => e.Mark).Sum();
                            var MarkAfterConversion = MarkHeGot;
                            if (FinalMarks > 0 && ExamMark > 0)
                                MarkAfterConversion = Math.Ceiling((MarkHeGot * ExamMark) / (FinalMarks));
                            
                          
                           
                            _enrollStudentExamService.EditMarkHeGotEnrollStudentExam(new EnrollStudentExamViewModel{
                                FinalMark = FinalMarks,
                                MarkHeGot = MarkHeGot,
                                MarkAfterConversion= MarkAfterConversion
                            },
                             EnrollStudentCourseExam,
                             context
                             );

                            _enrollStudentExamAnswerService.EditEnrollStudentExamAnswer(enrollStudentExamAnswers, context);

                            transaction.Commit();


                            //////////////////////////////////////////Check Exam Prerequisite/////////////////////////////////////////////////////////////
                            if (enrollCourseExam.IsPrerequisite )
                            {
                                var EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(EnrollStudentCourseExam.EnrollStudentCourseId);
                                if (MarkHeGot >= (FinalMarks / 2))//mean the student succeeded in the exam
                                {
                                    
                                    var NeedApproval = false;
                                    var IsPass = true;
                                    _enrollStudentCourseService.EditEnrollStudentCourse(new EnrollStudentCourseViewModel()
                                    {
                                        Price = EnrollStudentCourse.Price,
                                        CourseId = EnrollStudentCourse.CourseId,
                                        NeedApproval = NeedApproval,
                                        IsPass = IsPass,
                                        StudentId = EnrollStudentCourse.StudentId,
                                    },
                                     EnrollStudentCourse
                                    );
                                }
                                    //Check if there another prerequisite exam not solved
                                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(EnrollStudentCourse.CourseId, EnrollStudentCourse.StudentId);
                                    if (CheckHasPreRequestsCourse != "done")
                                    {
                                        _enrollStudentCourseService.EditEnrollStudentCourse(new EnrollStudentCourseViewModel()
                                        {
                                            Price = EnrollStudentCourse.Price,
                                            CourseId = EnrollStudentCourse.CourseId,
                                            NeedApproval = true,
                                            IsPass = false,
                                            StudentId = EnrollStudentCourse.StudentId,
                                        },
                                          EnrollStudentCourse
                                         );
                                    }
                                
                            }
                            //////////////////////////////////////////////////////////////////////////////
                            return Json(new { result = "Success" });
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Course Exam");
                            return Json(new { result = "Fail" });
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Course Exam");
                return Json(new { result = "Fail" });
            }
        }

        [AuditLogFilter(ActionDescription = "Show Regenerate Exam Questions")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowRegenerateExamQuestions(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollCourseExamS = _enrollCourseExamService.GetEnrollCourseExamById(id.Value, langId);
            if (enrollCourseExamS == null || enrollCourseExamS.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            return PartialView("_Regenerate", enrollCourseExamS);
        }
       
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enrollment Course Details")]
        public async Task<IActionResult> RegenerateExamQuestions(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var EnrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamByCourseExamId(id.Value);
            if (EnrollCourseExam.ExamTemplateId != null && EnrollCourseExam.ExamTemplateId > 0)
                {
                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                #region //Delete All Enrolled Data
                                _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(EnrollCourseExam.Id, context);
                                #endregion

                                #region //Add Enroll Exam Questions
                                foreach (var ExamQuestions in _examQuestionService.GetQuestionByTemplateId_WithoutUsing(EnrollCourseExam.ExamTemplateId.Value, context))
                                {
                                    var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                                    {
                                        QuestionId = ExamQuestions.Id,
                                        Mark = ((ExamQuestions.Mark.HasValue) ? ExamQuestions.Mark.Value : 0),
                                        EnrollCourseExamId = EnrollCourseExam.Id,
                                        CreatedBy = User.Identity?.Name
                                    };
                                    _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion_WithoutUsing(enrollCourseExamQuestionViewModel, context);
                                }
                                #endregion
                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Enroll Course Exam Dic");
                                return Content("Fail");
                            }
                        }
                    }
                }


            ViewBag.LangId = langId;
            return Content("Sccuess");
        }

        [AuditLogFilter(ActionDescription = "Show Regenerate Exam Questions")]
        [CustomAuthentication(PageName = "EnrollCourseExam", PermissionKey = "MarkAdoption")]
        public async Task<IActionResult> MarkAdoption(int id, bool adopt)
        {
            try
            {
                var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamModelById(id);
                _enrollCourseExamService.EditMarkAdoption(enrollCourseExam, adopt);
                return Json(true);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editting MarkAdoption for EnrollCourseExam");
                return null;
            }
        }
    }
}
