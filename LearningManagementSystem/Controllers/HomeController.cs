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
using static LearningManagementSystem.Core.Constants;
using LearningManagementSystem.Services.General;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.IO;
using DataEntity.Models.EfModels;
using System.Linq;

namespace LearningManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ICmsSliderService _cmsSliderService;
        private readonly ICourseService _courseService;
        private readonly ICmsWhatPeopleSayService _cmsWhatPeopleSayService;
        private readonly ICmsProjectService _cmsProjectService;
        private readonly ITrainerService _trainerService;
        private readonly IAboutDicService _aboutDicService;
        private readonly ISettingService _systemSetting;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LearningManagementSystemContext _context;

        public HomeController(ILogger<HomeController> logger, ICookieService cookieService, ICmsSliderService cmsSliderService,
            ICourseService courseService, ICmsWhatPeopleSayService cmsWhatPeopleSayService,
            ITrainerService trainerService,ICmsProjectService cmsProjectService,
             IAboutDicService aboutDicService, ISettingService systemSetting, IWebHostEnvironment webHostEnvironment,LearningManagementSystemContext context)
        {
            _logger = logger;
            _cookieService = cookieService;
            _cmsSliderService = cmsSliderService;
            _courseService = courseService;
            _cmsWhatPeopleSayService = cmsWhatPeopleSayService;
            _cmsProjectService = cmsProjectService;
            _trainerService = trainerService;
            _aboutDicService = aboutDicService;
            _systemSetting = systemSetting;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            ViewBag.Slider = await _cmsSliderService.GetPublicSliders(languageId);
            ViewBag.Course = await _courseService.GetCoursesForHomePgae(true, 1, languageId,25);
            ViewBag.WhatPeopleSay = await _cmsWhatPeopleSayService.GetAllCmsWhatPeopleSays(true,languageId);
            ViewBag.Projects = await _cmsProjectService.GetActiveProjects("", 1, languageId, 10);
            ViewBag.Trainer = await _trainerService.GetActiveTrainersForHonePage(true, languageId);
            ViewBag.HomePageContent = await _aboutDicService.GetAboutDicByCodeForHomePage(StaticContentDescription.HomePageContent, languageId);
            ViewBag.SystemSettings = await _systemSetting.GetMultipleSystemSettings(new string[] { SystemSettings.HomePage_YoutubeEmbed, SystemSettings.HomePage_OurStudents, 
                                      SystemSettings.HomePage_StudentsNationalities,SystemSettings.HomePage_OurCourses ,SystemSettings.HomePage_TeachersNationalities ,SystemSettings.HomePage_GraduateStudents,SystemSettings.HomePage_QuranicEpisodes  }, languageId);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
        {
            // set the cookie on the local machine of the Http Response to keep track of the language in question.
            // append the cookie and its language options.
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName, // name of the cookie
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),  // create a string representation of the culture for storage
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true, //<- there
                } // expiration after one day.
            );

            return await Task.Run(() => LocalRedirect(returnUrl));   // redirect to the original URL, the account page.
        }


        public async Task<IActionResult> SetBranch(int branchId, string returnUrl)
        {
              _cookieService.CreateCookie(CookieNames.SelectedBranchId, branchId.ToString(), 7);
            return await Task.Run(() => LocalRedirect(returnUrl));   // redirect to the original URL, the account page.
        }

        public async Task<IActionResult> SetCurrency(int currencyId, string returnUrl)
        {
            _cookieService.CreateCookie(CookieNames.SelectedCurrencyId, currencyId.ToString(), 7);
            return await Task.Run(() => LocalRedirect(returnUrl));   // redirect to the original URL, the account page.
        }

        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(() => View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.Run(() => View(new DataEntity.Models.EfModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }

        public async Task<IActionResult> Null()
        {
            return null;
        }

        public async Task<IActionResult> NotFound()
        {
            return View();
        }

        [Route("/Home/HandleError/{statusCode:int}")]
        public IActionResult HandleError(int statusCode)
        {
            if(statusCode == 404)
                return RedirectToAction("NotFound" ,"Home");

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> GetCountryCities(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var citiesHtml = "<option value=''> </option>";
            var cities = LookupHelper.GetCities(languageId, id);

            foreach (var citie in cities)
            {
                citiesHtml += "<option value='" + citie.Id + "'>" + citie.Name + "</option>";
            }
            return Json(new { result = citiesHtml });
        }


        [HttpGet("dynamic-css")]
        public IActionResult DynamicCss()
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "theme", "Public", "assets", "css", "index.css");

            if (!System.IO.File.Exists(filePath))
                return null;

            var cssContent = System.IO.File.ReadAllText(filePath);

            var setting = _context.SuperAdminSettings.FirstOrDefault();
            var primaryColorFromDb = setting?.SiteColor ?? "#1088A2";
            var secondaryColorFromDb = setting?.SecondarySiteColor ?? "#E3F2F6";

            var newCssContent = Regex.Replace(Regex.Replace(cssContent, @"(--primary:\s*)#[\da-fA-F]+;", "$1" + primaryColorFromDb + ";"), @"(--secondary:\s*)#[\da-fA-F]+;", "$1" + secondaryColorFromDb + ";");

            return Content(newCssContent, "text/css");
        }
    }
}
