using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.General;
using DataEntity.Models;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;

        public DashboardController(ISettingService settingService, ILogService logService)
        {
            _settingService = settingService;
            _logService = logService;
        }

        //[CustomAuthentication(PageName = "Dashboard", PermissionKey = "View")]
        //[AuditLogFilter(ActionDescription = "Dashboard")]
        public IActionResult Index()
        {
            var result = new DashboardViewModel()
            {
                
            };
            return View(result);
        }
    }
}