using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class SystemLogsController : Controller
    {
        private readonly ISystemLogService _systemLogService;

        public SystemLogsController(ISystemLogService systemLogService)
        {
            _systemLogService = systemLogService;
        }

        [CustomAuthentication(PageName = "SystemLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "SystemLogs List")]
        public async Task<IActionResult> Index(int? page, string searchText, int resetTo = 0, int pagination = 25)
        {
            if (resetTo == 1)
            {
                page = 1;
            }                    
            var systemLog = _systemLogService.GetSystemLogs(searchText, page, pagination);

            return View(systemLog);
        }

        // GET: ControlPanel/SystemLogs/Details/5
        [CustomAuthentication(PageName = "SystemLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "SystemLogs Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLog = _systemLogService.GetSystemLogById(id.Value);
            if (systemLog == null || systemLog.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // GET: ControlPanel/SystemLogs/Delete/5
        [CustomAuthentication(PageName = "SystemLogs", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "SystemLogs Delete Get")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLog = _systemLogService.GetSystemLogById(id.Value);
            if (systemLog == null || systemLog.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // POST: ControlPanel/SystemLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "SystemLogs", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "SystemLogs Delete Post")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var systemSettingDeleted = _systemLogService.GetSystemLogById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _systemLogService.DeleteSystemLog(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete SystemLog");
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
