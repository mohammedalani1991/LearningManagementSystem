using LearningManagementSystem.Core;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICmsPageService _cmsPageService;

        public BlogController(ILogger<BlogController> logger,
            ICookieService cookieService,
             ISettingService settingService,
             ICmsPageService cmsPageService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _settingService = settingService;
            _cmsPageService = cmsPageService;


        }

        public IActionResult Index(string search,int? category, DateTime? fromDate, DateTime? toDate, int? page, int pagination)
        {
            //var blogCategoryId = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.BlogCategoryId, "1").Value);

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);


            if (page == 0)
                page = 1;




            pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);

            ViewBag.PaginationValue = pagination;
            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(search))
                ViewBag.Search = search;

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.LangId = languageId;
            ViewBag.category = category;

            var result = _cmsPageService.GetActiveCmsPages(search, category.Value, fromDate, toDate, page, languageId, pagination);
            return View(result);
        }


        public IActionResult Details(int id)
        {

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);

            var result = _cmsPageService.GetActiveCmsPageById(id, languageId);
            ViewBag.LangId = languageId;
            ViewBag.Pages = _cmsPageService.GetActiveCmsPages(string.Empty, result?.CateryId ?? 0, null, null, 1, languageId, pagination);
            return View(result);
        }
    }
}
