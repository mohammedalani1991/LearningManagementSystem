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
    public class PracticalQuestionsController : Controller
    {
        private readonly IPracticalQuestionService _practicalQuestionService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public PracticalQuestionsController(ICookieService cookieService, IPracticalQuestionService practicalQuestionService, ISettingService settingService, ILogService logService)
        {
            _practicalQuestionService = practicalQuestionService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/PracticalQuestions
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List PracticalQuestion")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> GetData(int? page, int pagination, string searchText, int? TypeId)
        {
            if (page == null || page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalQuestionPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalQuestionPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.TypeId = TypeId;

            var result = _practicalQuestionService.GetPracticalQuestions(searchText, page, TypeId, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/PracticalQuestions/Details/5
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
                return NotFound();

            var practicalQuestion = _practicalQuestionService.GetPracticalQuestionById(id.Value, languageId);
            if (practicalQuestion == null || practicalQuestion.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.LangId = languageId;
            return PartialView("Details", practicalQuestion);
        }

        // GET: ControlPanel/PracticalQuestions/Create
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/PracticalQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "Create")]
        public async Task<IActionResult> Create(PracticalQuestionViewModel practicalQuestion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    practicalQuestion.CreatedOn = DateTime.Now;
                    practicalQuestion.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (practicalQuestion.LanguageId == 0)
                        practicalQuestion.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = practicalQuestion.LanguageId;

                    _practicalQuestionService.AddPracticalQuestion(practicalQuestion);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalQuestion");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/PracticalQuestions/Edit/5
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var practicalQuestion = _practicalQuestionService.GetPracticalQuestionById(id.Value, languageId);
            if (practicalQuestion == null || practicalQuestion.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new PracticalQuestionViewModel(practicalQuestion));
        }

        // POST: ControlPanel/PracticalQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PracticalQuestionViewModel practicalQuestion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _practicalQuestionService.GetPracticalQuestionById(practicalQuestion.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (practicalQuestion.LanguageId == 0)
                        {
                            practicalQuestion.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _practicalQuestionService.EditPracticalQuestion(practicalQuestion, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new PracticalQuestion");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/PracticalQuestions/Delete/5
        [CustomAuthentication(PageName = "PracticalQuestion", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _practicalQuestionService.GetPracticalQuestionById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _practicalQuestionService.DeletePracticalQuestion(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete PracticalQuestion");
                return null;
            }
        }
    }
}
