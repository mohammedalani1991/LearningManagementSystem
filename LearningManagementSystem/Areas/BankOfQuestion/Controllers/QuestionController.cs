using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.BankOfQuestion;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Constants = LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Areas.BankOfQuestion.Controllers
{
    [Area("BankOfQuestion")]
    public class QuestionController : Controller
    {
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IQuestionService _questionService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly IExamQuestionService _examQuestionService;

        public QuestionController(
            ICookieService cookieService, ILogService logService, IUserProfileService userProfileService,
            IQuestionService questionService, ISettingService settingService,
            ICourseService courseService, ICourseCategoryService courseCategoryService, IExamQuestionService examQuestionService
            )
        {
            _logService = logService;
            _userProfileService = userProfileService;
            _questionService = questionService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseService = courseService;
            _courseCategoryService = courseCategoryService;
            _examQuestionService = examQuestionService;
        }

        [CustomAuthentication(PageName = "Question", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Question")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Question", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Question List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int? Category, int? Course, int? type, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;
            if (Category != null)
                ViewBag.Category = Category;
            if (Course != null)
                ViewBag.Category = Category;
            if (type != null)
                ViewBag.type = type;

            var val = _cookieService.GetCookie(Constants.Pagenation.QuestionPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.QuestionPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Name", "Status", "CreatedBy", "Mark", "CreatedOn", "CategoryId", "CourseId", "Type" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.QuestionTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.QuestionTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.QuestionTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;


            var result = _questionService.GetQuestions(searchText, page, Category, Course, type, languageId, pagination);

            return PartialView("_Index", result);
        }

        public IActionResult GetCourses(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var courses = _courseCategoryService.GetCourseByCategoryId(id, languageId);

            var coursessHtml = "<option value=''></option>";

            foreach (var course in courses)
                coursessHtml += "<option value='" + course.Id + "'>" + course.CourseName + "</option>";

            return Json(new { result = coursessHtml });
        }

        [CustomAuthentication(PageName = "Question", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: BankOfQuestion/Question/Details/5
        [CustomAuthentication(PageName = "Question", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Question Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;


            var question = _questionService.GetQuestionById(id.Value, languageId);
            if (question == null || question.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", question);
        }


        // GET: BankOfQuestion/Question/Create
        [CustomAuthentication(PageName = "Question", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Question Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            ViewBag.ListCourseId = (_courseService.GetCourses(false, 1, langId, int.MaxValue)).ToList();
            ViewBag.ListCategoryId = _courseCategoryService.GetAllCourseCategorys(langId);



            return PartialView("Create");
        }

        // POST: BankOfQuestion/Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Question Create Post")]
        public async Task<IActionResult> Create(QuestionViewModel questionViewModel)
        {
            if (questionViewModel.OptionList != null && questionViewModel.Type != (int)GeneralEnums.QuestionEnum.Text)
            {
                questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);
            }
            try
            {

                if (questionViewModel.LanguageId == 0)
                    questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                questionViewModel.CreatedBy = User.Identity?.Name;
                _questionService.AddQuestion(questionViewModel);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Question Dic");
                return null;
            }
        }

        // GET: BankOfQuestion/Question/Edit/5
        [AuditLogFilter(ActionDescription = "Question Edit Get")]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId)
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

            ViewBag.ListCourseId = (_courseService.GetCourses(false, 1, langId, int.MaxValue)).ToList();
            ViewBag.ListCategoryId = _courseCategoryService.GetAllCourseCategorys(langId);
            return PartialView("Edit", question);
        }

        // POST: BankOfQuestion/Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Question Edit Pot")]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(QuestionViewModel questionViewModel)
        {
            try
            {
                if (questionViewModel.OptionList != null && questionViewModel.Type != (int)GeneralEnums.QuestionEnum.Text)
                    questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);

                var question = _questionService.GetQuestionById(questionViewModel.Id);
                if (question != null && question.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (questionViewModel.LanguageId == 0)
                        questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    _questionService.EditQuestion(questionViewModel, question);
                    return Json(true);

                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Question Dic (Post)");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Question Delete Get")]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Delete")]

        // GET: BankOfQuestion/Question/Delete/5
        public async Task<IActionResult> ShowDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;

            var question = _questionService.GetQuestionById(id.Value, langId);
            if (question == null || question.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Delete", question);
        }

        // POST: BankOfQuestion/Question/Delete/5
        [AuditLogFilter(ActionDescription = "Question Delete Post")]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = _questionService.GetQuestionById(id);
                if (question != null && question.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _questionService.DeleteQuestion(question);
                    _examQuestionService.DeleteExamQuestionByQuestionID(question.Id);

                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While Delete Question");
                return null;
            }
        }

        public async Task<List<Question>> GetExcelData()
        {
            try
            {
                var dataList = new List<Question>();
                var excelFile = Request.Form.Files["file"];

                if (excelFile != null && excelFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        excelFile.CopyTo(memoryStream);
                        using (var package = new ExcelPackage(memoryStream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];

                            var columnList = new List<string>();

                            for (int column = 1; column <= worksheet.Dimension.End.Column; column++)
                            {
                                var cellValue = worksheet.Cells[1, column].Value?.ToString();

                                if (!string.IsNullOrEmpty(cellValue))
                                    columnList.Add(cellValue);

                            }
                            var optionCount = columnList.Select(r => r == "Option").Count();

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                var model = new Question();

                                model.CreatedOn = DateTime.Now;
                                model.CreatedBy = User?.Identity?.Name ?? string.Empty;
                                model.Status = (int)GeneralEnums.StatusEnum.Active;
                                model.Name = worksheet.Cells[row, 1].Value?.ToString();
                                model.Type = Convert.ToInt32(worksheet.Cells[row, 2].Value);
                                model.Mark = worksheet.Cells[row, 3].Value != null ? Convert.ToInt32(worksheet.Cells[row, 3].Value) : (int?)null;

                                if (model.Type == 2 || model.Type == 3)
                                    for (int i = 4; i < (optionCount * 2) + 3; i += 2)
                                        if (worksheet.Cells[row, i].Value != null)
                                            model.QuestionOptions.Add(new QuestionOption()
                                            {
                                                CreatedOn = DateTime.Now,
                                                CreatedBy = User?.Identity?.Name ?? string.Empty,
                                                Status = (int)GeneralEnums.StatusEnum.Active,
                                                Name = worksheet.Cells[row, i].Value?.ToString(),
                                                IsCorrect = worksheet.Cells[row, i + 1].Value != null ? Convert.ToBoolean(worksheet.Cells[row, i + 1].Value) : false,
                                            });

                                dataList.Add(model);
                            }
                        }
                    }
                }

                return dataList;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While Getting execl file data");
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Question", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Create Questions")]
        public async Task<IActionResult> CreateQuestions(QuestionViewModel questionViewModel)
        {
            try
            {
                if (questionViewModel.LanguageId == 0)
                    questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                _questionService.AddQuestions(questionViewModel);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while adding Questions");
                return null;
            }
        }
    }
}
