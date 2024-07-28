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
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class TrainerRateController : Controller
    {
        private readonly ILogService _logService;
        private readonly ITrainerRateService _trainerRateService;

        public TrainerRateController(ILogService logService, ITrainerRateService trainerRateService)
        {
            _logService = logService;
            _trainerRateService = trainerRateService;
        }
        
        [CustomAuthentication(PageName = "AcademicSupervisionRates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Academic Supervision Rates List")]
        public async Task<IActionResult> GetEnrollAcademicSupervisionRates(DateTime date,int enrollId)
        {

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (date == default)
            {
                date = DateTime.Today;
            }
            var result = _trainerRateService.GetAcademicSupervisionRates(date, enrollId,User.Identity.Name, languageId);            
            ViewBag.LangId = languageId;
            ViewBag.Date = date;
            ViewBag.EnrollId = enrollId;
            return PartialView("_EnrollAcademicSupervisionRates", result);
        }
        

        [HttpPost]
        [CustomAuthentication(PageName = "AcademicSupervisionRates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Add Edit Academic Supervision Rates Post")]
        public async Task<IActionResult> AddEditAcademicSupervisionRates(List<AcademicSupervisionRateViewModel> rates)
        {            
            try
            {
                _trainerRateService.AddEditAcademicSupervisionRates(rates,User.Identity.Name);
                return Json(new { check=true});
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add edit management rates");
                return Json(new { check=false});
            }            
        }


        [CustomAuthentication(PageName = "ManagementRates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Management Rates List")]
        public async Task<IActionResult> GetEnrollManagementRates(DateTime date, int enrollId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (date == default)
            {
                date = DateTime.Today;
            }
            ViewBag.Date = date;
            ViewBag.EnrollId = enrollId;
            var result = _trainerRateService.GetManagmentRates(date, enrollId, User.Identity.Name, languageId);
            ViewBag.LangId = languageId;
            return PartialView("_EnrollManagementRates", result);
        }

        [HttpPost]
        [CustomAuthentication(PageName = "ManagementRates", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Add Edit Management Rates Post")]
        public async Task<IActionResult> AddEditManagementRates(ManagementRateViewModel rate)
        {
            try
            {
                _trainerRateService.AddEditManagmentRates(rate, User.Identity.Name);
                return Json(new { check = true });
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add edit management rates");
                return Json(new { check = false });
            }
        }
    }
}
