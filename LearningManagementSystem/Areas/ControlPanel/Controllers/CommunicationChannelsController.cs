using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;
using Microsoft.AspNetCore.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CommunicationChannelsController : Controller
    {
        private readonly ICommunicationChannelService _communicationChannelService;
        private readonly ISubCommunicationChannelService _supCommunicationChannelService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
      
        public CommunicationChannelsController(ISubCommunicationChannelService supCommunicationChannelService, ICookieService cookieService, ICommunicationChannelService communicationChannelService
            , ISettingService settingService, ILogService logService)
        {
            _communicationChannelService = communicationChannelService;
            _supCommunicationChannelService = supCommunicationChannelService;
            _logService = logService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Lookups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Lookups List")]
        // GET: ControlPanel/CommunicationChannels        
        public async Task<IActionResult> Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var val = _cookieService.GetCookie(Constants.Pagenation.CommunicationChannelPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CommunicationChannelPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;            
            ViewBag.LangId = languageId;

            var result = _communicationChannelService.GetCommunicationChannels(searchText, page, languageId, pagination);

            return View(result);
        }

        // GET: ControlPanel/CommunicationChannels/SubCommunicationChannel/5
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Lookups Details")]
        public async Task<IActionResult> SubCommunicationChannel(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var communicationChannel = _communicationChannelService.GetCommunicationChannelById(id.Value, languageId);

            if (communicationChannel == null || communicationChannel.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            communicationChannel.SubCommunicationChannels = _supCommunicationChannelService.GetSubCommunicationChannels(communicationChannel.Id, languageId, null);
            communicationChannel.Page = page;
            communicationChannel.LanguageId = languageId;
            ViewBag.LangId = languageId;

            return View(communicationChannel);
        }

        // GET: ControlPanel/CommunicationChannels/SubCommunicationChannel/5
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Second SubCommunication Channel")]
        public async Task<IActionResult> SecondSubCommunicationChannel(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var supCommunicationChannel = _supCommunicationChannelService.GetSubCommunicationChannelById(id.Value, languageId);

            if (supCommunicationChannel == null || supCommunicationChannel.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            supCommunicationChannel.SubCommunicationChannels = _supCommunicationChannelService.GetSubCommunicationChannels(supCommunicationChannel.CommunicationChannelId, languageId, supCommunicationChannel.Id);
            supCommunicationChannel.Page = page;
            supCommunicationChannel.LanguageId = languageId;
            ViewBag.LangId = languageId;
            return View(supCommunicationChannel);
        }
        // POST: ControlPanel/CommunicationChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Lookups Create Post")]
        [HttpPost]
        public IActionResult Create(CommunicationChannelViewModel communicationChannelViewModel)
        {
            try
            {
                communicationChannelViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                _communicationChannelService.AddCommunicationChannel(communicationChannelViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add communication Channel");
                return View(communicationChannelViewModel);
            }
        }

        // POST: ControlPanel/CommunicationChannels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [HttpPost]
        [AuditLogFilter(ActionDescription = "Lookups Edit Post")]
        public IActionResult Edit(CommunicationChannelViewModel communicationChannelViewModel)
        {
            try
            {
                var communicationChannel = _communicationChannelService.GetCommunicationChannelById(communicationChannelViewModel.Id);
                if (communicationChannel != null && communicationChannel.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _communicationChannelService.EditCommunicationChannel(communicationChannelViewModel, communicationChannel);
                }
                return RedirectToAction(nameof(Index), new { page = communicationChannelViewModel.Page });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing communicationChannel (Post)");
                return View(communicationChannelViewModel);
            }
        }

        // POST: ControlPanel/CommunicationChannels/Delete/5
        [AuditLogFilter(ActionDescription = "Lookups Delete Post")]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Delete")]
        public IActionResult DeleteConfirmed(int id, int page)
        {
            try
            {
                var systemSettingDeleted = _communicationChannelService.GetCommunicationChannelById(id);
                if (systemSettingDeleted != null && systemSettingDeleted.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _communicationChannelService.DeleteCommunicationChannel(systemSettingDeleted);
                }
                return RedirectToAction(nameof(Index), new { page = page });
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete communication Channel");
                return RedirectToAction(nameof(Index));
            }
        }


        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lookups Get Get")]
        public async Task<IActionResult> GetCommunicationChannelByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _communicationChannelService.GetCommunicationChannelById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }

    }
}
