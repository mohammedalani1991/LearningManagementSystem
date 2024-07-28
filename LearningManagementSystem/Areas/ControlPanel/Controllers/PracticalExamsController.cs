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

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class PracticalExamsController : Controller
    {
        private readonly IPracticalExamService _practicalExamService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IPracticalQuestionService _practicalQuestionService;
        private readonly ICookieService _cookieService;

        public PracticalExamsController(ICookieService cookieService, IPracticalExamService practicalExamService, ISettingService settingService, ILogService logService
            , IPracticalQuestionService practicalQuestionService)
        {
            _practicalExamService = practicalExamService;
            _settingService = settingService;
            _logService = logService;
            _practicalQuestionService = practicalQuestionService;
            _cookieService = cookieService;

        }
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/PracticalExams
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List PracticalExam")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> GetData(int? page, int pagination, string searchText, int? Type)
        {
            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalExamPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalExamPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.Type = Type;

            var result = _practicalExamService.GetPracticalExams(Type ,searchText, page??1, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/PracticalExams/Details/5
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
                return NotFound();

            var practicalExam = _practicalExamService.GetPracticalExamById(id.Value, languageId);
            if (practicalExam == null || practicalExam.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.LangId = languageId;
            return PartialView("Details", practicalExam);
        }

        // GET: ControlPanel/PracticalExams/Create
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/PracticalExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "Create")]
        public async Task<IActionResult> Create(PracticalExamViewModel practicalExam)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    practicalExam.CreatedOn = DateTime.Now;
                    practicalExam.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (practicalExam.LanguageId == 0)
                        practicalExam.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = practicalExam.LanguageId;

                    _practicalExamService.AddPracticalExam(practicalExam);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalExam");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/PracticalExams/Edit/5
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var practicalExam = _practicalExamService.GetPracticalExamById(id.Value, languageId);
            if (practicalExam == null || practicalExam.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new PracticalExamViewModel(practicalExam));
        }

        // POST: ControlPanel/PracticalExams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PracticalExamViewModel practicalExam)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _practicalExamService.GetPracticalExamById(practicalExam.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (practicalExam.LanguageId == 0)
                        {
                            practicalExam.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _practicalExamService.EditPracticalExam(practicalExam, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalExam");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/PracticalExams/Delete/5
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _practicalExamService.GetPracticalExamById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _practicalExamService.DeletePracticalExam(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete PracticalExam");
                return null;
            }
        }

        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "AddQuestions")]
        public async Task<IActionResult> GetQuestions(int PracticalExamId,int? TypeId, int? page1, int pagination1, string searchText1)
        {
            if (page1 == null || page1 == 0)
                page1 = 1;

            ViewBag.Page1 = page1;

            if (!string.IsNullOrWhiteSpace(searchText1))
                ViewBag.searchText = searchText1;

            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalQuestionPagination);

            if (val == null && pagination1 == 0)
                pagination1 = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination1 != 0)
                pagination1 = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalQuestionPagination, pagination1.ToString(), 7));
            else
                pagination1 = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination1;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.PracticalExamId = PracticalExamId;
            ViewBag.TypeId = TypeId;

            var result = _practicalQuestionService.GetPracticalQuestions(searchText1, page1, TypeId, languageId, pagination1 );
            ViewBag.Questions = _practicalExamService.GetPracticalExamQuestions(PracticalExamId);
            return PartialView("_AddQuestions", result);
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Practical Exam Question Create")]
        [CustomAuthentication(PageName = "PracticalExam", PermissionKey = "AddQuestions")]
        public async Task<IActionResult> AddOrRemoveQuestions(int PracticalExamId, int Questionid, string checkedRow)
        {
            try
            {
                var examQuestion = _practicalExamService.GetExamQuestion(PracticalExamId, Questionid);
                if (checkedRow.ToLower() == "true")
                {
                    var eq = new PracticalExamQuestion()
                    {
                        PracticalQuestionId = Questionid,
                        PracticalExamId = PracticalExamId,
                        CreatedBy = User?.Identity?.Name ?? string.Empty,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                    };
                    _practicalExamService.AddPracticalExamQuestion(eq);
                }
                else
                    _practicalExamService.RemovePracticalExamQuestion(examQuestion);
                
                return Content("Ok");
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Practical Question to Exam");
                return Content("error");
            }
        }
    }
}
