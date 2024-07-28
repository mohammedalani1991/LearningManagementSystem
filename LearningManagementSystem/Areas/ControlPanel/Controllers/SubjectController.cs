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
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public SubjectController(ICookieService cookieService, ISubjectService subjectService, ISettingService settingService, ILogService logService)
        {
            _subjectService = subjectService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;

        }
        [CustomAuthentication(PageName = "Subject", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/Subjects
        [CustomAuthentication(PageName = "Subject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Subject")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> GetData(int? page, int pagination, string searchText, int Type)
        {
            ViewBag.Page = page;
            ViewBag.Type = Type;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.SubjectPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SubjectPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _subjectService.GetSubjects(0,Type, searchText, page??1, languageId, pagination);

            return PartialView("_Index", result);
        }

        // GET: ControlPanel/Subjects/Details/5
        [CustomAuthentication(PageName = "Subject", PermissionKey = "View")]
        public async Task<IActionResult> Details(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
                return NotFound();

            var subject = _subjectService.GetSubjectById(id.Value, languageId);
            if (subject == null || subject.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.LangId = languageId;
            return PartialView("Details", subject);
        }

        // GET: ControlPanel/Subjects/Create
        [CustomAuthentication(PageName = "Subject", PermissionKey = "Create")]
        public IActionResult Create()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Subject", PermissionKey = "Create")]
        public async Task<IActionResult> Create(SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subject.CreatedOn = DateTime.Now;
                    subject.CreatedBy = User.Identity?.Name ?? string.Empty;

                    if (subject.LanguageId == 0)
                        subject.LanguageId = CultureHelper.GetDefaultLanguageId();

                    ViewBag.LangId = subject.LanguageId;

                    _subjectService.AddSubject(subject);


                    return Ok();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Subject");
                    return null;
                }

            }
            return null;
        }

        // GET: ControlPanel/Subjects/Edit/5
        [CustomAuthentication(PageName = "Subject", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var subject = _subjectService.GetSubjectById(id.Value, languageId);
            if (subject == null || subject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Edit", new SubjectViewModel(subject));
        }

        // POST: ControlPanel/Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Subject", PermissionKey = "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permiss = _subjectService.GetSubjectById(subject.Id);
                    if (permiss != null && permiss.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (subject.LanguageId == 0)
                        {
                            subject.LanguageId = CultureHelper.GetDefaultLanguageId();
                        }

                        _subjectService.EditSubject(subject, permiss);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new Subject");
                    return null;
                }
            }
            return null;
        }

        // POST: ControlPanel/Subjects/Delete/5
        [CustomAuthentication(PageName = "Subject", PermissionKey = "Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettingDeleted = _subjectService.GetSubjectById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _subjectService.DeleteSubject(systemSettingDeleted);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Subject");
                return null;
            }
        }
    }
}
