using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class AuditLogsController : Controller
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ICookieService _cookieService;

        public AuditLogsController(IAuditLogService auditLogService, ICookieService cookieService)
        {
            _auditLogService = auditLogService;
            _cookieService = cookieService;
        }

        // GET: ControlPanel/AuditLogs
        [CustomAuthentication(PageName = "AuditLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Audit Logs List")]
        public async Task<IActionResult> Index(int? page, string searchText, int resetTo = 0, int pagination = 10)
        {
            if (resetTo == 1)
            {
                page = 1;
            }            
            var auditLog = _auditLogService.GetAuditLogs(searchText, page, pagination);

            return View(auditLog);
        }

        // GET: ControlPanel/AuditLogs/Details/5
        [CustomAuthentication(PageName = "AuditLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Audit Logs Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = _auditLogService.GetAuditLogById(id.Value);
            if (auditLog == null || auditLog.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // GET: ControlPanel/AuditLogs/Delete/5
        [CustomAuthentication(PageName = "AuditLogs", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Audit Logs Delete Get")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditLog = _auditLogService.GetAuditLogById(id.Value);
            if (auditLog == null || auditLog.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // POST: ControlPanel/AuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuditLogFilter(ActionDescription = "Audit Logs Delete Post")]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "AuditLogs", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var systemSettingDeleted = _auditLogService.GetAuditLogById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _auditLogService.DeleteAuditLog(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete AuditLogs");
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
