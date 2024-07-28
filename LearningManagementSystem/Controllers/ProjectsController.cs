using DataEntity.Models.ViewModels;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;

namespace LearningManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ICmsProjectService _projectService;

        public ProjectsController(ICmsProjectService projectService)
        {
            _projectService = projectService;
        }

        [CheckSuperAdmin(PageName = "Projects")]
        public IActionResult Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var result = _projectService.GetCmsProjectsList(languageId);
            return View(result);
        }

        [CheckSuperAdmin(PageName = "Projects")]
        public IActionResult ProjectModelDetails(int id, bool active)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var result = _projectService.GetCmsProjectById(id, languageId);

            ViewBag.Active = active;
            return PartialView("_Details", result);
        }

        [CheckSuperAdmin(PageName = "Projects")]
        public IActionResult ProjectDetails(int id , bool? success = null)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var result = _projectService.GetCmsProjectById(id, languageId);

            ViewBag.Success = success;

            return View(result);
        }
    }
}
