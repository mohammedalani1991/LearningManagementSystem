using LearningManagementSystem.Filters;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Core;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.General;
using System.Linq;


namespace LearningManagementSystem.Controllers
{
    public class PageController : Controller
    {
        private readonly ILogger<PageController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ITrainerService _trainerService;
        private readonly ISettingService _settingService;
        private readonly ICmsPageService _cmsPageService;
        private readonly IAboutDicService _aboutDicService;

        public PageController(ILogger<PageController> logger, ICookieService cookieService,
            ITrainerService trainerService, ISettingService settingService,
            ICmsPageService cmsPageService,
            IAboutDicService aboutDicService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _trainerService = trainerService;
            _settingService = settingService;
            _cmsPageService = cmsPageService;
            _aboutDicService = aboutDicService;
        }


        public IActionResult Index(string code)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

   
   
            var page = _aboutDicService.GetAboutDicByCode(code, languageId);

            ViewBag.LangId = languageId;
            ViewBag.Trainer = _trainerService.GetActiveTrainers(true,languageId).Take(4);
            return View(page);

        }



    }
}
