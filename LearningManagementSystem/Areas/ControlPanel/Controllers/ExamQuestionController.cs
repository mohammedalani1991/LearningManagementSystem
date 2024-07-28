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

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class ExamQuestionController : Controller
    {
        private readonly ILogService _logService;
        private readonly IExamTemplateService _examTemplateService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;
        private readonly IQuestionService _questionService;
        private readonly IExamQuestionService _examQuestionService;

        public ExamQuestionController(
            ICookieService cookieService, ILogService logService,
            IExamTemplateService examTemplateService, ISettingService settingService, 
            ICourseCategoryService courseCategoryService,
            ICourseService courseService,
            IQuestionService questionService, IExamQuestionService examQuestionService)
        {
            _logService = logService;
            _examTemplateService = examTemplateService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseCategoryService = courseCategoryService;
            _courseService = courseService;
            _questionService = questionService;
            _examQuestionService = examQuestionService;
        }



        // GET: ControlPanel/ExamQuestions/CourseSearch
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question Courses Get")]
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


        // GET: ControlPanel/ExamQuestions/ShowAddRemoveExamQuestions/5
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question Add and Edit")]
        public async Task<IActionResult> ShowAddRemoveExamQuestions( int? id, int languageId)
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
            var examTemplate = _examTemplateService.GetExamTemplateById(id.Value, languageId);
            if (examTemplate == null || examTemplate.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.ListCourseCategorys = _courseCategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
           
            return PartialView("_AddRemoveExamQuestions", examTemplate);
        }

        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Question List")]
        public async Task<IActionResult> TemplateBankExamQuestionsGetData(int? page, string QuestionName, int? CourseCategoryId, int? CourseId, int? TypeQuestion, int id,int languageId)
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
            ViewBag.TemplateId = id;
            var ExamQuestions = _examQuestionService.GetExamQuestionByTemplateId(id);
            ViewBag.ExamQuestions = ExamQuestions;
            var result = _questionService.GetQuestionsForTemplateExam(ExamQuestions, QuestionName, page, CourseCategoryId, CourseId, TypeQuestion, languageId, 5,0);
            return PartialView("_IndexBankExamQuestions", result);
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Exam Questions Create")]
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        public async Task<IActionResult> AddRemoveTemplateBankExamQuestions(int Tamplateid, int Questionid,string checkedRow)
        {
            var examQuestion = _examQuestionService.GetExamQuestionByTemplateIdAndQuestionID(Tamplateid, Questionid);

            if (checkedRow.ToLower() == "true")
            {
                if (examQuestion == null)
                {
                    var AddExamQuestion = new ExamQuestionViewModel()
                    {
                        QuestionId = Questionid,
                        TemplateId = Tamplateid,
                        CreatedBy = User.Identity?.Name

                    };
                    _examQuestionService.AddExamQuestion(AddExamQuestion);
                }

            }
            else
            {
                if (examQuestion != null && examQuestion.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _examQuestionService.DeleteExamQuestion(examQuestion);
                }
            }
           
            return Content(_examQuestionService.GetCountExamQuestionByTemplateId(Tamplateid).ToString());
        }


        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Exam Questions List")]
        public async Task<IActionResult> TemplateRandomExamQuestionsGetData(int? page,  int id,int languageId)
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
            ViewBag.ExamQuestions = _examQuestionService.GetExamQuestionByTemplateId(id);
            var result = _examQuestionService.GetExamQuestions(page, languageId,5, id, 0);
            return PartialView("_IndexRandomExamQuestions", result);
           
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Exam Questions Create")]
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        public async Task<IActionResult> AddRemoveTemplateRandomExamQuestions(string QuestionName, int? CourseCategoryId, int? CourseId, int? TypeQuestion, int id,int NumberRandomQuestions,int languageId)
        {
        
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

            var result = _questionService.GetQuestionsForTemplateExam(QuestionName, 1, CourseCategoryId, CourseId, TypeQuestion, languageId, NumberRandomQuestions,1,0, id);
            if (result.Count == 0)
            {
                return Content("No Data");
            }
            else
            {
                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                        
                            foreach (var r in result)
                            {
                                var AddExamQuestion = new ExamQuestionViewModel()
                                {
                                    QuestionId = r.Id,
                                    TemplateId = id,
                                    CreatedBy = User.Identity?.Name

                                };
                                _examQuestionService.AddExamQuestion_WithoutUsing(AddExamQuestion, context);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Add Remove Template Random Exam Questions");
                            return Content("Fail");
                        }
                    }
                }
                return Content(_examQuestionService.GetCountExamQuestionByTemplateId(id).ToString());
            }
        }
        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Exam Questions Delete")]
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "Create")]
        public async Task<IActionResult> DeleteTemplateBankExamQuestions( int Tamplateid)
        {
            using (var context = new LearningManagementSystemContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        _examQuestionService.DeleteExamQuestionByTamplateID_WithoutUsing(Tamplateid, context);
                        transaction.Commit();

                        return Content("Success");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delete Template Exam Questions");
                        return Content("Fail");
                    }
                }
            }
        }

        // POST: ControlPanel/ExamQuestions/ShowExamQuestions
        [AuditLogFilter(ActionDescription = "Exam Questions View")]
        [CustomAuthentication(PageName = "ExamQuestions", PermissionKey = "View")]
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
        
        
    }
}
