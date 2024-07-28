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
    public class SubscribersController : Controller
    {
        private readonly ILogger<CourseCategoriesController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ISubscribersService _SubscribersService;
        private readonly ISettingService _settingService;
        private readonly IEmailService _emailService;
        private readonly ILogService _logService;
        public SubscribersController(ILogger<CourseCategoriesController> logger,
            ICookieService cookieService,
            ISubscribersService SubscribersService,
            ISettingService systemSetting,
            IEmailService emailService,
            ILogService logService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _SubscribersService = SubscribersService;
            _settingService = systemSetting;
            _emailService = emailService;
            _logService = logService;
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubscribersForGuest(string Email)
        {
            
            var result =await _SubscribersService.AddSubscribers(Email);
            return Content(result.ToString());
        }

    }
}
