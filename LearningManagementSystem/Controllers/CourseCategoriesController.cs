using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningManagementSystem.Controllers
{
    public class CourseCategoriesController : Controller
    {
        private readonly ILogger<CourseCategoriesController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoriesController(ILogger<CourseCategoriesController> logger, ICookieService cookieService, ICourseCategoryService courseCategoryService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _courseCategoryService = courseCategoryService;

        }
        public IActionResult Index(int? id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

          var result=  _courseCategoryService.GetActiveCourseCategorysForGuest(true, id, null, languageId);
            if (result.Count > 0)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Courses", new { categoryId = id });
            }
        }
    }
}
