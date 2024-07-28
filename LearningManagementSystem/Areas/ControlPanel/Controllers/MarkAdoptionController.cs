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
using LearningManagementSystem.Services.ControlPanel.IServices;
using DataEntity.Models.EfModels;
using System.Linq;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class MarkAdoptionController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IMarkAdoptionService _markAdoptionService;
        private readonly ICourseService _courseService;

        public MarkAdoptionController(ICookieService cookieService, ILogService logService, ISettingService settingService,
            IMarkAdoptionService markAdoptionService,ICourseService courseService)
        {
            _logService = logService;
            _cookieService = cookieService;
            _settingService = settingService;
            _markAdoptionService = markAdoptionService;
            _courseService = courseService;
        }

        [CustomAuthentication(PageName = "MarkAdoptionForExam", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return View();
        }

        [CustomAuthentication(PageName = "MarkAdoptionForExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "MarkAdoptionForExam List")]
        public async Task<IActionResult> GetMarkAdoptionForExam(int semesterId, int courseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _markAdoptionService.GetMarkAdoptionForExam(semesterId, courseId);
            return PartialView("_MarkAdoptionForExam", result);
        }

        [CustomAuthentication(PageName = "MarkAdoptionForExam", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "AdoptMarkAdoptionForExam")]
        public async Task<IActionResult> AdoptMarkAdoptionForExam(int semesterId, int courseId, int examTemplateId, bool adopted)
        {
            try
            {
                _markAdoptionService.AdoptMarkAdoptionForExam(semesterId, courseId, examTemplateId, adopted);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [CustomAuthentication(PageName = "MarkAdoptionForPracticalExam", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "MarkAdoptionForPracticalExam List")]
        [CheckSuperAdmin(PageName = "MarkAdoptionForPracticalExam")]
        public async Task<IActionResult> GetMarkAdoptionForPracticalExam(int semesterId, int courseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _markAdoptionService.GetMarkAdoptionForPracticalExam(semesterId, courseId);
            return PartialView("_MarkAdoptionForPracticalExam", result);
        }

        [CustomAuthentication(PageName = "MarkAdoptionForPracticalExam", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "AdoptPracticalExam")]
        [CheckSuperAdmin(PageName = "MarkAdoptionForPracticalExam")]
        public async Task<IActionResult> AdoptPracticalExam(int semesterId, int courseId, int practicalExamId, bool adopted)
        {
            try
            {
                _markAdoptionService.AdoptPracticalExam(semesterId, courseId, practicalExamId, adopted);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IActionResult GetCourses(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var courses = _courseService.GetCoursesList(id ,languageId);

            return Json(courses);
        }
    }
}
