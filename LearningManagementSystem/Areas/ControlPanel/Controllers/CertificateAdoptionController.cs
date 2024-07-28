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
    public class CertificateAdoptionController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICertificateAdoptionService _certificateAdoptionService;

        public CertificateAdoptionController(ICookieService cookieService, ILogService logService, ISettingService settingService,
            ICertificateAdoptionService certificateAdoptionService)
        {
            _logService = logService;
            _cookieService = cookieService;
            _settingService = settingService;
            _certificateAdoptionService = certificateAdoptionService;
        }

        [CustomAuthentication(PageName = "CertificateAdoption", PermissionKey = "View")]
        public async Task<IActionResult> Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return View();
        }

        [CustomAuthentication(PageName = "CertificateAdoption", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CertificateAdoption List")]
        public async Task<IActionResult> GetData(int? page, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.CertificateAdoptionPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CertificateAdoptionPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _certificateAdoptionService.GetCertificateAdoption(page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [AuditLogFilter(ActionDescription = "Create CertificateAdoption")]
        [CustomAuthentication(PageName = "CertificateAdoption", PermissionKey = "Create")]
        public async Task<IActionResult> AddCertificateAdoption(int semesterId, int courseId)
        {
            try
            {
                _certificateAdoptionService.AddCertificateAdoption(semesterId, courseId , User?.Identity?.Name ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding new CertificateAdoption");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Delete CertificateAdoption")]
        [CustomAuthentication(PageName = "CertificateAdoption", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCertificateAdoption(int id)
        {
            try
            {
                _certificateAdoptionService.DeleteCertificateAdoption(id);
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While deleting CertificateAdoption");
                return null;
            }
        }
    }
}
