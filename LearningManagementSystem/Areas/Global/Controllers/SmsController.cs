using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Global.IService;

namespace LearningManagementSystem.Areas.Global.Controllers
{
    [Area("Global")]
    public class SmsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly IEmailsService _emailsService;
        private readonly IGlobalSmsService _globalSmsService;
        private readonly ISmsService _smsService;

        public SmsController(ICookieService cookieService, ISettingService settingService, ILogService logService,
            IEmailsService emailsService, IGlobalSmsService globalSmsService,ISmsService smsService)
        {
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;
            _emailsService = emailsService;
            _globalSmsService = globalSmsService;
            _smsService = smsService;
        }

        [CustomAuthentication(PageName = "Sms", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Open Send Sms Modal")]
        public IActionResult GetSms()
        {
            return PartialView("_Sms");
        }

        [CustomAuthentication(PageName = "Sms", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Open Send Student Sms Modal")]
        public IActionResult GetSmsStudent()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            return PartialView("_StudentSms");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Sms", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Send Sms")]
        public async Task<IActionResult> SendSms(SmsViewModel smsViewModel)
        {
            try
            {
                if (smsViewModel.Ids == null && smsViewModel.Ids?.Count == 0)
                    return null;

                smsViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                smsViewModel.Status = (int)GeneralEnums.SmsStatusEnum.NotSend;

                foreach (var item in smsViewModel.Ids)
                {
                    
                        var contact = _globalSmsService.GetContactById(item);
                        if (contact != null)
                        {
                            smsViewModel.BranchId = contact.BranchId;
                            smsViewModel.Mobile = contact.Mobile;
                            smsViewModel.ToId = contact.Id;
                            if (smsViewModel.IsExtraMobile ?? false)
                                smsViewModel.ExtraMobile = _globalSmsService.GetStudentFromContactId(item)?.ExtraMobile;
                        }
                    

                    var sms = _globalSmsService.SendSms(smsViewModel);

                }

                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Sending Sms");
                return null;
            }
        }
    }
}
