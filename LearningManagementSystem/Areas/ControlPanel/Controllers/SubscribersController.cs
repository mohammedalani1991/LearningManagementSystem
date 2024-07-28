using System;
using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class SubscribersController : Controller
    {
        private readonly ISubscribersService _SubscribersService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        public SubscribersController(ISubscribersService SubscribersService, ISettingService settingService, ILogService logService, ICookieService cookieService)
        {
            _SubscribersService = SubscribersService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;
        }


        [CustomAuthentication(PageName = "Subscribers", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Subscribers List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.SubscribersPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SubscribersPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var result = await _SubscribersService.GetSubscribers(searchText, page);

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Subscribers", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Subscribers")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
