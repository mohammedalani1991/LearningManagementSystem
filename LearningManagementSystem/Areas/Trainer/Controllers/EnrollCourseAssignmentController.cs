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
using DataEntity.Models.EfModels;

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollCourseAssignmentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollCourseAssignmentService _assignmentService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;

        public EnrollCourseAssignmentController(
            ICookieService cookieService, ILogService logService, IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollCourseAssignmentService assignmentService, ISettingService settingService, ICourseService courseService, IUserProfileService userProfileService, ITrainerService trainerService)
        {
            _logService = logService;
            _assignmentService = assignmentService;
            _cookieService = cookieService;
            _settingService = settingService;
            _courseService = courseService;
            _userProfileService = userProfileService;
            _trainerService = trainerService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
        }

        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, int CourseId)
        {
            ViewBag.Page = page ?? 1;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            ViewBag.CourseId = CourseId;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCourseAssignmentsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCourseAssignmentsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _assignmentService.GetAssignments(searchText, page ?? 1, languageId, pagination, CourseId);
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/EnrollCourseAssignment/Details/5
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Details")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId > 0)
                langId = languageId;


            var assignment = _assignmentService.GetAssignmentById(id.Value, langId);

            ViewBag.LangId = langId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            return PartialView("Details", assignment);
        }

        // GET: ControlPanel/EnrollCourseAssignment/Create
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Create Get")]
        public IActionResult ShowCreate(int EnrollTeacherCoursId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

            var enrollTeacherCoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCoursId);
            if (enrollTeacherCoursDetails == null || enrollTeacherCoursDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var result = new EnrollCourseAssignmentViewModel() { EnrollTeacherCourseId = EnrollTeacherCoursId };
            ViewBag.IsOnlineLearningMethod = enrollTeacherCoursDetails.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("Create", result);
        }

        // POST: ControlPanel/EnrollCourseAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Create Post")]
        public async Task<IActionResult> Create(EnrollCourseAssignmentViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentViewModel.IsOnlineLearningMethod == true)
                        return null;
                    if (assignmentViewModel.PublishDate >= assignmentViewModel.PublishEndDate)
                        return Content("Fail");

                    if (assignmentViewModel.LanguageId == 0)
                        assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    assignmentViewModel.CreatedBy = User.Identity?.Name;
                    _assignmentService.AddEnrollCourseAssignment(assignmentViewModel);
                    return Content("Success");
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new EnrollCourseAssignment Dic");
                    return null;
                }
            }
            return null;
        }

        // GET: ControlPanel/EnrollCourseAssignment/Edit/5
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Edit Get")]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
        {
            if (id == null)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId == 0)
                languageId = langId;

            ViewBag.LangId = languageId;
            var assignment = _assignmentService.GetAssignmentById(id.Value, languageId);
            if (assignment == null || assignment.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            var enrollTeacherCoursDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(assignment.EnrollTeacherCourseId);
            if (enrollTeacherCoursDetails == null || enrollTeacherCoursDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.IsOnlineLearningMethod = enrollTeacherCoursDetails.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce;
            return PartialView("Edit", new EnrollCourseAssignmentViewModel(assignment));
        }

        // POST: ControlPanel/EnrollCourseAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Edit Pot")]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(EnrollCourseAssignmentViewModel assignmentViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (assignmentViewModel.IsOnlineLearningMethod == true)
                        return null;

                    if (assignmentViewModel.PublishDate >= assignmentViewModel.PublishEndDate)
                        return Content("Fail");

                    var assignment = _assignmentService.GetAssignmentById(assignmentViewModel.Id);
                    if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (assignmentViewModel.LanguageId == 0)
                            assignmentViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                        _assignmentService.EditEnrollCourseAssignment(assignmentViewModel, assignment);
                        return Content("Success");
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing EnrollCourseAssignment Dic (Post)");
                    return null;
                }
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Delete Get")]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Delete")]

        // GET: ControlPanel/EnrollCourseAssignment/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int languageId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId > 0)
                langId = languageId;

            var assignment = _assignmentService.GetAssignmentById(id.Value, langId);

            ViewBag.LangId = langId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "Coordinated Universal Time").Value).DisplayName;
            return PartialView("Delete", assignment);
        }

        // POST: ControlPanel/EnrollCourseAssignment/Delete/5
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Delete Post")]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteEnrollCourseAssignment(int id)
        {
            var assignment = _assignmentService.GetAssignmentById(id);
            if (assignment != null && assignment.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _assignmentService.DeleteAssignment(assignment);
                return Content("Success");
            }

            return Content("Fail");
        }

        // GET: ControlPanel/EnrollCourseAssignment/Create
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Create Get")]
        public IActionResult ShowQuestions(int? page, string searchText, int pagination, int EnrollCourseAssigmentId)
        {
            ViewBag.Page = page ?? 1;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            ViewBag.EnrollCourseAssigmentId = EnrollCourseAssigmentId;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCourseAssignmentsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCourseAssignmentsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _assignmentService.GetAssignmentQuestions(searchText, page ?? 1, languageId, pagination, EnrollCourseAssigmentId);

            return PartialView("_Questions", result);
        }

        // GET: ControlPanel/EnrollCourseAssignment/Create
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "EnrollCourseAssignment Create Get")]
        public IActionResult ShowAssignmentQuestions(int EnrollCourseAssigmentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;
            ViewBag.Edit = false;
            var assignment = new EnrollCourseAssigmentQuestion() { EnrollCourseAssigmentId = EnrollCourseAssigmentId };

            return PartialView("_CreateQuestion", assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enroll Course Assignments Create Question")]
        public async Task<IActionResult> CreateAssignmentQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel)
        {
            if (questionViewModel.OptionList != null && questionViewModel.QuestionType != (int)GeneralEnums.QuestionEnum.Text && questionViewModel.QuestionType != (int)GeneralEnums.QuestionEnum.UploadAttachment)
                questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);
            try
            {
                if (questionViewModel.LanguageId == 0)
                    questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                questionViewModel.CreatedBy = User.Identity?.Name;
                _assignmentService.AddQuestion(questionViewModel);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Question");
                return null;
            }
        }

        // GET: ControlPanel/EnrollCourseAssignment/Create
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "ShowEditAssignmentQuestion Edit Get")]
        public IActionResult ShowEditAssignmentQuestion(int id, int languageId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (languageId > 0)
                langId = languageId;

            ViewBag.LangId = langId;

            var result = _assignmentService.GetAssignmentQuestion(id, languageId);
            ViewBag.EnrollCourseAssigmentId = result.EnrollCourseAssigmentId;
            ViewBag.Edit = true;

            return PartialView("_CreateQuestion", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enroll Course Assignments Edit Question")]
        public async Task<IActionResult> EditAssignmentQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel)
        {
            if (questionViewModel.OptionList != null && questionViewModel.QuestionType != (int)GeneralEnums.QuestionEnum.Text && questionViewModel.QuestionType != (int)GeneralEnums.QuestionEnum.UploadAttachment)
                questionViewModel.OptionList?.ForEach(c => c.CreatedBy = User.Identity?.Name);
            try
            {
                if (questionViewModel.LanguageId == 0)
                    questionViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                var question = _assignmentService.GetAssignmentQuestion(questionViewModel.Id);

                questionViewModel.CreatedBy = User.Identity?.Name;
                _assignmentService.EditQuestion(questionViewModel, question);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Editing Question");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Enroll Course Assignments Delete Question")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = _assignmentService.GetAssignmentQuestion(id);
                if (question != null)
                {
                    _assignmentService.DeleteQuestion(question);
                    return Json(true);
                }
                return null;
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Deleteing Question");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Get Student Assigments")]
        public async Task<IActionResult> GetStudentAssigments(int? page, string searchText, int pagination, int assigmentId)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = langId;

                ViewBag.Page = page ?? 1;

                if (!string.IsNullOrWhiteSpace(searchText))
                    ViewBag.searchText = searchText;

                ViewBag.AssigmentId = assigmentId;

                var val = _cookieService.GetCookie(Constants.Pagenation.StudentAssigmentsPagination);

                if (val == null && pagination == 0)
                    pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                else if (pagination != 0)
                    pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.StudentAssigmentsPagination, pagination.ToString(), 7));
                else
                    pagination = int.Parse(val != "" ? val : "10");

                ViewBag.PaginationValue = pagination;

                var result = _assignmentService.GetStudentAssignments(searchText, page ?? 1, langId, pagination, assigmentId);

                return PartialView("_StudentAssignments", result);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Getting Student Assigments");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollCourseAssignments", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Enroll Course Assignments Delete Question")]
        public async Task<IActionResult> GetAssignmentQuestions(int id)
        {
            try
            {
                var questions = _assignmentService.GetStudentAssigmentAnswer(id);
                return PartialView("_QuestionDetails", questions);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Deleteing Question");
                return null;
            }
        }

    }
}
