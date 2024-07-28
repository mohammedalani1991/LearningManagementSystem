using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CommunicationLogController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICommunicationLogService _communicationLogService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ISmsService _smsService;
        public CommunicationLogController(ISmsService smsService ,ISettingService settingService,ICookieService cookieService, ILogService logService, ICommunicationLogService communicationLogService)
        {
            _logService = logService;
            _communicationLogService = communicationLogService;
            _cookieService = cookieService;
            _settingService = settingService;
            _smsService = smsService;
        }

        // GET: ControlPanel/AboutDics
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CommunicationLog List")]
        public async Task<IActionResult> Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (resetTo == 1 || page == null || page == 0)
            {
                page = 1;
            }
            ViewBag.Page = page;
            if (pagination == 0)
            {
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            }
            var val = _cookieService.GetCookie(Constants.Pagenation.CommunicationLogPagination);
            if (val == null)
            {
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CommunicationLogPagination, pagination.ToString(), 7));
            }
            else
            {
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CommunicationLogPagination, val, 7));
            }
            ViewBag.PaginationValue = pagination;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ViewBag.searchText = searchText;
            }
            var result = _communicationLogService.GetCommunicationLogs(searchText, page, pagination);
            return View(result);
        }

        // GET: ControlPanel/AboutDics/Details/5
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Communication Log Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutDic = _communicationLogService.GetCommunicationLogById(id.Value);
            if (aboutDic == null || aboutDic.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new CommunicationLogsViewModel(aboutDic));
        }

        [AuditLogFilter(ActionDescription = "Communication Log Delete Get")]
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "Delete")]
        // GET: ControlPanel/AboutDics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try {
                
                if (id == null)
                {
                    return NotFound();
                }
                var communicationLog = _communicationLogService.GetCommunicationLogById(id.Value);
                if (communicationLog == null || communicationLog.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    return NotFound();
                }
                var result = new CommunicationLogsViewModel();
                result.ContactName = communicationLog.Contact.FullName;
                return View(new CommunicationLogsViewModel(communicationLog));
            }
            catch(Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delete Log (Get)");
                return null;
            }
}


        [AuditLogFilter(ActionDescription = "Send Messages To Customers")]
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "SendMessage")]
        // GET: ControlPanel/AboutDics/Delete/5
        public async Task<IActionResult> SendMessages(MessageViewModel messageViewModel)
        {
            try
            {
                string isSendMessage = _settingService.GetOrCreate("Is_Send_Message", "off").Value;
                if (isSendMessage == "on")
                {
                    messageViewModel.CreatedBy = User.Identity.Name;
                    _smsService.SendSms(messageViewModel);
                }                
                return RedirectToAction("Index", "CommunicationLog");
            }
            catch(Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send Message");
                return RedirectToAction("Index", "CommunicationLog");
            }            
        }

        [AuditLogFilter(ActionDescription = "Send Messages To Customers")]
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "SendEmail")]
        // GET: ControlPanel/AboutDics/Delete/5
        public async Task<IActionResult> SendEmail(MessageViewModel messageViewModel)
        {
            try
            {
                string isSendEmail =_settingService.GetOrCreate("Is_Send_Email", "off").Value;
                if (isSendEmail == "on")
                {
                    messageViewModel.CreatedBy = User.Identity.Name;                    
                   await _smsService.SendEmail(messageViewModel);
                }
                return RedirectToAction("Index", "CommunicationLog");
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send Email");
                return RedirectToAction("Index", "CommunicationLog");
            }
        }

        // POST: ControlPanel/AboutDics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CommunicationLog Delete Post")]
        [CustomAuthentication(PageName = "CommunicationLogs", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var aboutDic = _communicationLogService.GetCommunicationLogById(id);
                if (aboutDic != null && aboutDic.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _communicationLogService.DeleteCommunicationLog(aboutDic);
                }
                return RedirectToAction(nameof(Index));
            }            
            catch(Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delete Communication Log");
                return RedirectToAction(nameof(Index));
            }                                    
        }
    }
}
