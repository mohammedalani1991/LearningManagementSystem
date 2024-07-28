using System;
using System.Threading.Tasks;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningManagementSystem.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<CourseCategoriesController> _logger;
        private readonly ICookieService _cookieService;
        private readonly IContactUsService _ContactUsService;
        private readonly ISettingService _settingService;
        private readonly IEmailService _emailService;
        private readonly ILogService _logService;
        public ContactUsController(ILogger<CourseCategoriesController> logger,
            ICookieService cookieService,
            IContactUsService ContactUsService,
            ISettingService systemSetting,
            IEmailService emailService,
            ILogService logService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _ContactUsService = ContactUsService;
            _settingService = systemSetting;
            _emailService = emailService;
            _logService = logService;
        }


        public async Task<IActionResult> Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

           // ViewData["ReCaptchaKey"] = _settingService.GetOrCreate(Constants.SystemSettings.GoogleReCaptcha_Site_key, "").Value;

            ViewBag.SystemSettings = await _settingService.GetMultipleSystemSettings(new string[] { 
                Constants.SystemSettings.GoogleReCaptcha_Site_key, 
                Constants.SystemSettings.ContactUs_Address,
                Constants.SystemSettings.ContactUs_Email,
                Constants.SystemSettings.ContactUs_Map_Lat_Long ,
                Constants.SystemSettings.ContactUs_Phone  }, languageId);
 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContactUsForGuest(ContactUsViewModel contactUsViewModel)
        {
            if (!Request.Form.ContainsKey("g-recaptcha-response")) return Content("-3");
            if(string.IsNullOrEmpty(Request.Form["g-recaptcha-response"])) return Content("-3");

            var result =await _ContactUsService.AddContactUs(contactUsViewModel);
            if(result > 0)
            {
                try
                {
                    _emailService.SendContactUsEmail(contactUsViewModel.Email, contactUsViewModel.Subject, contactUsViewModel.Message, contactUsViewModel.Name);
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send ContactUsEmail");
                }
               
            }
            return Content(result.ToString());
        }

    }
}
