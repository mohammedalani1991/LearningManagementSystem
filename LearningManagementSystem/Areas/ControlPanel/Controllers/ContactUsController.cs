using System;
using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;


namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        public ContactUsController(IContactUsService contactUsService, ISettingService settingService, ILogService logService, ICookieService cookieService)
        {
            _contactUsService = contactUsService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;
        }


        [CustomAuthentication(PageName = "ContactUs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "ContactUs List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.ContactUsPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.ContactUsPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var result =await _contactUsService.GetContacts(searchText, page);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "ContactUs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List ContactUs")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
