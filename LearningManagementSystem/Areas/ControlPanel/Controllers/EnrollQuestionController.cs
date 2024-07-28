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
using LearningManagementSystem.Services.BankOfQuestion;
using System.Transactions;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class EnrollQuestionController : Controller
    {
        private readonly ILogService _logService;
        private readonly IExamTemplateService _examTemplateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;
        private readonly IQuestionService _questionService;
        private readonly IExamQuestionService _examQuestionService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollCourseExamQuestionService _enrollCourseExamQuestionService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        public EnrollQuestionController(
            ICookieService cookieService, ILogService logService,
            IExamTemplateService examTemplateService, ISettingService settingService,
            ICourseCategoryService courseCategoryService,
            ICourseService courseService,
            IQuestionService questionService,
            IExamQuestionService examQuestionService,
            ITrainerService trainerService,
            IUserProfileService userProfileService,
            IEnrollCourseExamQuestionService enrollCourseExamQuestionService, IEnrollStudentExamService enrollStudentExamService)
        {
            _logService = logService;
            _examTemplateService = examTemplateService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseCategoryService = courseCategoryService;
            _courseService = courseService;
            _questionService = questionService;
            _examQuestionService = examQuestionService;
            _trainerService = trainerService;
            _userProfileService = userProfileService;
            _enrollCourseExamQuestionService = enrollCourseExamQuestionService;
            _enrollStudentExamService = enrollStudentExamService;
        }



        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question List")]
        public async Task<IActionResult> BankExamQuestionsGetData(int? page, string QuestionName, int? CourseCategoryId, int? CourseId, int? TypeQuestion, int id, int languageId, int EnrollCourseExamId)
        {
            if (page == 0)
                page = 1;

            var TeacherId = _trainerService.GetTrainerByCourseExamId(EnrollCourseExamId)?.Id ?? 0;

            ViewBag.Page = page;
            ViewBag.id = id;

            if (!string.IsNullOrWhiteSpace(QuestionName))
                ViewBag.QuestionName = QuestionName;
            if (CourseCategoryId != null && CourseCategoryId != 0)
            {
                ViewBag.CourseCategoryId = CourseCategoryId;

            }
            else
            {
                CourseCategoryId = null;
            }
            if (CourseId != null && CourseId != 0)
            {
                ViewBag.CourseId = CourseId;
            }
            else
            {
                CourseId = null;
            }

            if (TypeQuestion != null && TypeQuestion != 0)
            {
                ViewBag.TypeQuestion = TypeQuestion;
            }
            else
            {
                TypeQuestion = null;
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;
            var enrollCourseExamQuestions = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            ViewBag.enrollCourseExamQuestions = enrollCourseExamQuestions;
            var result = _questionService.GetQuestionsForTemplateExam(enrollCourseExamQuestions, QuestionName, page, CourseCategoryId, CourseId, TypeQuestion, languageId, 5, 0, TeacherId);
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            return PartialView("_IndexTeacherExamQuestions", result);
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Exam Questions Create")]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        public async Task<IActionResult> AddRemoveBankExamQuestions(int EnrollCourseExamId, int Questionid, string checkedRow)
        {
            var ExamQuestion = _examQuestionService.GetExamQuestionByTemplateIdAndQuestionID(null, Questionid);
            var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollCourseExamId(EnrollCourseExamId);
            if (EnrollStudentExam == null)
            {
                if (checkedRow.ToLower() == "true")
                {
                    if (ExamQuestion == null)
                    {
                        ////////////////////////////////////////////////////////////////////////////////////
                        var AddExamQuestion = new ExamQuestionViewModel()
                        {
                            QuestionId = Questionid,
                            CreatedBy = User.Identity?.Name

                        };
                        var AddedExamQuestion = _examQuestionService.AddExamQuestion(AddExamQuestion);
                        ////////////////////////////////////////////////////////////////////////////////////
                        var question = _questionService.GetQuestionById(Questionid);
                        var enrollCourseExam = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByQuestionId(EnrollCourseExamId, AddedExamQuestion.Id);
                        if (enrollCourseExam == null)
                        {
                            var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                            {
                                QuestionId = AddedExamQuestion.Id,
                                Mark = ((question.Mark.HasValue) ? question.Mark.Value : 0),
                                EnrollCourseExamId = EnrollCourseExamId,
                                CreatedBy = User.Identity?.Name
                            };
                            _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion(enrollCourseExamQuestionViewModel);
                        }
                        ////////////////////////////////////////////////////////////////////////////////////

                    }
                    else
                    {
                        ////////////////////////////////////////////////////////////////////////////////////
                        var enrollCourseExam = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByQuestionId(EnrollCourseExamId, ExamQuestion.Id);
                        if (enrollCourseExam == null)
                        {
                            var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                            {
                                QuestionId = ExamQuestion.Id,
                                Mark = ((ExamQuestion.Question.Mark.HasValue) ? ExamQuestion.Question.Mark.Value : 0),
                                EnrollCourseExamId = EnrollCourseExamId,
                                CreatedBy = User.Identity?.Name
                            };
                            _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion(enrollCourseExamQuestionViewModel);
                        }
                        ////////////////////////////////////////////////////////////////////////////////////
                    }

                }
                else
                {
                    if (ExamQuestion != null && ExamQuestion.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamIDAndExamQuestionID(EnrollCourseExamId, ExamQuestion.Id);
                    }
                }
                return Content("Ok");
            }
            else
            {
                return Content("Fail");
            }


        }



        // POST: ControlPanel/ExamQuestions/ShowExamQuestions
        [AuditLogFilter(ActionDescription = "Exam Questions View")]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "View")]
        public async Task<IActionResult> ShowExamQuestions(int? page, int id, int languageId)
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
            ViewBag.TemplateId = id;
            ViewBag.ExamQuestionsCount = _examQuestionService.GetExamQuestionByTemplateId(id).Count;
            var result = _examQuestionService.GetExamQuestions(page, languageId, 5, id, 0);
            return PartialView("_IndexExamQuestions", result);
        }



        // GET: BankOfQuestion/Question/Create
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Question Create Get")]
        public IActionResult ShowCreate(int languageId, int EnrollCourseExamId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            return PartialView("Create");
        }

        // POST: BankOfQuestion/Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Question Create Post")]
        public async Task<IActionResult> Create(QuestionViewModel questionViewModel, int ViewModleEnrollCourseExamId)
        {
            if (questionViewModel.OptionList != null && questionViewModel.Type != (int)GeneralEnums.QuestionEnum.Text)
            {
                questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);
            }
            try
            {
                var TeacherId = _trainerService.GetTrainerByCourseExamId(ViewModleEnrollCourseExamId)?.Id ?? 0;

                if (questionViewModel.LanguageId == 0)
                    questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                questionViewModel.CreatedBy = User.Identity?.Name;
                questionViewModel.TeacherId = TeacherId;
                questionViewModel.CertifiedBankQuestion = false;
                _questionService.AddQuestion(questionViewModel);
                return Content("Ok");
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Question Dic");
                return null;
            }
        }

        // GET: BankOfQuestion/Question/Edit/5
        [AuditLogFilter(ActionDescription = "Question Edit Get")]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId, int EnrollCourseExamId)
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
            ViewBag.LangId = languageId;
            var question = _questionService.GetQuestionById(id.Value, languageId);
            question.LanguageId = languageId;
            if (question == null || question.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            return PartialView("Edit", question);
        }

        // POST: BankOfQuestion/Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Question Edit Pot")]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        public async Task<IActionResult> Edit(QuestionViewModel questionViewModel, int ViewModleEnrollCourseExamId)
        {
            try
            {
                if (questionViewModel.OptionList != null && questionViewModel.Type != (int)GeneralEnums.QuestionEnum.Text)
                {
                    questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);
                }
                var question = _questionService.GetQuestionById(questionViewModel.Id);
                if (question != null && question.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (questionViewModel.LanguageId == 0)
                        questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    _questionService.EditQuestion(questionViewModel, question);
                    return Content("Ok");
                }
                return Content("Fail");
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Question Dic (Post)");
                return null;
            }
        }


        // GET: ControlPanel/ExamQuestions/ShowAddRemoveExamQuestions/5
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question Add and Edit")]
        public async Task<IActionResult> ShowAddRemoveExamQuestions(int? id, int languageId)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
                languageId = langId;

            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            ViewBag.id = id;

            return PartialView("_AddRemoveExamQuestions");
        }

        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question List")]
        public async Task<IActionResult> TemplateBankExamQuestionsGetData(int? page, string QuestionName, int? CourseCategoryId, int? CourseId, int? TypeQuestion, int id, int languageId)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;
            ViewBag.id = id;

            if (!string.IsNullOrWhiteSpace(QuestionName))
                ViewBag.QuestionName = QuestionName;
            if (CourseCategoryId != null && CourseCategoryId != 0)
            {
                ViewBag.CourseCategoryId = CourseCategoryId;

            }
            else
            {
                CourseCategoryId = null;
            }
            if (CourseId != null && CourseId != 0)
            {
                ViewBag.CourseId = CourseId;
            }
            else
            {
                CourseId = null;
            }

            if (TypeQuestion != null && TypeQuestion != 0)
            {
                ViewBag.TypeQuestion = TypeQuestion;
            }
            else
            {
                TypeQuestion = null;
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = id;
            var list = _questionService.GetQuestionsList(id);
            ViewBag.ExamQuestions = list;
            var result = _questionService.GetQuestionsForTemplateExam(list, QuestionName, page, CourseCategoryId, CourseId, TypeQuestion, languageId, 5, 0);
            return PartialView("_IndexBankExamQuestions", result);
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Exam Questions Create")]
        [CustomAuthentication(PageName = "EnrollExamQuestion", PermissionKey = "Create")]
        public async Task<IActionResult> AddRemoveTemplateBankExamQuestions(int EnrollCourseExamId, int Questionid, string checkedRow)
        {
            try
            {
                var ExamQuestion = _questionService.GetExamQuestionByQuestionId(Questionid);
                var EnrollStudentExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollCourseExamId(EnrollCourseExamId);

                if (EnrollStudentExam == null)
                {
                    if (checkedRow.ToLower() == "true")
                    {
                        if (ExamQuestion == null)
                        {
                            ////////////////////////////////////////////////////////////////////////////////////
                            var AddExamQuestion = new ExamQuestionViewModel()
                            {
                                QuestionId = Questionid,
                                CreatedBy = User.Identity?.Name

                            };
                            var AddedExamQuestion = _examQuestionService.AddExamQuestion(AddExamQuestion);
                            ////////////////////////////////////////////////////////////////////////////////////
                            var question = _questionService.GetQuestionById(Questionid);
                            var enrollCourseExam = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByQuestionId(EnrollCourseExamId, AddedExamQuestion.Id);
                            if (enrollCourseExam == null)
                            {
                                var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                                {
                                    QuestionId = AddedExamQuestion.Id,
                                    Mark = ((question.Mark.HasValue) ? question.Mark.Value : 0),
                                    EnrollCourseExamId = EnrollCourseExamId,
                                    CreatedBy = User.Identity?.Name
                                };
                                _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion(enrollCourseExamQuestionViewModel);
                            }
                            ////////////////////////////////////////////////////////////////////////////////////

                        }
                        else
                        {
                            ////////////////////////////////////////////////////////////////////////////////////
                            var enrollCourseExam = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByQuestionId(EnrollCourseExamId, ExamQuestion.Id);
                            if (enrollCourseExam == null)
                            {
                                var enrollCourseExamQuestionViewModel = new EnrollCourseExamQuestionViewModel()
                                {
                                    QuestionId = ExamQuestion.Id,
                                    Mark = ((ExamQuestion.Question.Mark.HasValue) ? ExamQuestion.Question.Mark.Value : 0),
                                    EnrollCourseExamId = EnrollCourseExamId,
                                    CreatedBy = User.Identity?.Name
                                };
                                _enrollCourseExamQuestionService.AddEnrollCourseExamQuestion(enrollCourseExamQuestionViewModel);
                            }
                            ////////////////////////////////////////////////////////////////////////////////////
                        }

                    }
                    else
                    {
                        if (ExamQuestion != null && ExamQuestion.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            _enrollCourseExamQuestionService.DeleteEnrollCourseExamQuestionByEnrollCourseExamIDAndExamQuestionID(EnrollCourseExamId, ExamQuestion.Id);
                        }
                    }
                    return Content("Ok");
                }
                else
                {
                    return Content("Fail");
                }
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Enroll Exam Question From bank of Questions");
                return Content("error");
            }
        }
    }
}
