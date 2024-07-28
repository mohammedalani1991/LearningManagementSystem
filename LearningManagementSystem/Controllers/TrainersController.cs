using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningManagementSystem.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ILogger<TrainersController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ICourseService _courseService;
        private readonly ISettingService _settingService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ITrainerService _trainerService;



        public TrainersController(ILogger<TrainersController> logger, ICookieService cookieService, ICourseService courseService, ICourseCategoryService courseCategoryService, ITrainerService trainerService, ISettingService settingService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _courseService = courseService;
            _courseCategoryService = courseCategoryService;
            _trainerService = trainerService;
            _settingService = settingService;


        }
        public IActionResult Index(string search, int? specialty, int? page, int pagination)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);


            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            ViewBag.LangId = languageId;

            var val = _cookieService.GetCookie(Constants.Pagenation.TrainerPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.TrainerPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            if (!string.IsNullOrWhiteSpace(search))
                ViewBag.Search = search;

   


            if (specialty != null && specialty!=0)
                ViewBag.Specialty = specialty;


            ViewBag.GeneralSpecialty = LookupHelper.GetLookupDetailsByMasterCode(GeneralEnums.MasterLookupCodeEnums.GeneralSpecialty.ToString(), languageId);

            
            var result = _trainerService.GetActiveTrainers(search, specialty, page, languageId, pagination);

            return View(result);
        }

        public IActionResult Details(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _trainerService.GetActiveTrainerById(id, languageId);

            return View(result);


        }
    }
}
