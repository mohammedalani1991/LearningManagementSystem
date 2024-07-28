    using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;

        public DashboardController(ISettingService settingService, ILogService logService
            , IUserProfileService userProfileService)
        {
            _settingService = settingService;
            _logService = logService;
            _userProfileService = userProfileService;
        }

        [CustomAuthentication(PageName = "Dashboard", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Dashboard")]
        public ActionResult Index(int? page, string searchText, int resetTo = 0)
        {
            if (resetTo == 1)
            {
                page = 1;
            }

            var userId = _userProfileService.GetUserProfileByUsername(User.Identity?.Name)?.Id;
            
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ViewBag.searchText = searchText;
            }
            return View();
        }


    }
}
