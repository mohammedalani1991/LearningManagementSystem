using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class SubCommunicationChannelsController : Controller
    {
        private readonly ISubCommunicationChannelService _subCommunicationChannelService;
        private readonly ILogService _logService;

        public SubCommunicationChannelsController(ISubCommunicationChannelService subCommunicationChannelService, ILogService logService)
        {
            _subCommunicationChannelService = subCommunicationChannelService;
            _logService = logService;
        }

        // POST: ControlPanel/SubCommunicationChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Communication Channelss Create Post")]
        public IActionResult Create(SubCommunicationChannelViewModel SubCommunicationChannelViewModel)
        {
            try
            {
                try
                {
                    SubCommunicationChannelViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                    _subCommunicationChannelService.AddSubCommunicationChannel(SubCommunicationChannelViewModel);

                    return RedirectToAction("SubCommunicationChannel", "CommunicationChannels", new { id = SubCommunicationChannelViewModel.CommunicationChannelId });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity.Name, ex, "Error while add Communication Channels");
                    return View(SubCommunicationChannelViewModel);
                }
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add Communication Channels");
                return View(SubCommunicationChannelViewModel);
            }
        }

        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Communication Channelss Create Post")]
        public IActionResult CreateSecond(SubCommunicationChannelViewModel SubCommunicationChannelViewModel)
        {
            try
            {
                try
                {
                    SubCommunicationChannelViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                    _subCommunicationChannelService.AddSubCommunicationChannel(SubCommunicationChannelViewModel);

                    return RedirectToAction("SecondSubCommunicationChannel", "CommunicationChannels", new { id = SubCommunicationChannelViewModel.ParentId });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity.Name, ex, "Error while add Second sub Communication Channels");
                    return View(SubCommunicationChannelViewModel);
                }
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add Second sub Communication Channels");
                return View(SubCommunicationChannelViewModel);
            }
        }

        // GET: ControlPanel/SubCommunicationChannels/Edit/        
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Communication Channelss Edit Get")]
        public async Task<IActionResult> Edit(int? id, int communicationChannelId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            ViewData["CommunicationChannelId"] = communicationChannelId;
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(id.Value);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new SubCommunicationChannelViewModel(lookup));
        } 
        // GET: ControlPanel/SubCommunicationChannels/Edit/        
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Communication Channelss Edit Get")]
        public async Task<IActionResult> EditSecond(int? id, int parentId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            ViewData["ParentId"] = parentId;
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(id.Value);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new SubCommunicationChannelViewModel(lookup));
        }

        // POST: ControlPanel/SubCommunicationChannels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "sub Communication Channelss Edit Post")]
        public IActionResult Edit(SubCommunicationChannelViewModel SubCommunicationChannelViewModel)
        {
            try
            {
                var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(SubCommunicationChannelViewModel.Id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _subCommunicationChannelService.EditSubCommunicationChannel(SubCommunicationChannelViewModel, lookup);
                }

                return RedirectToAction("SubCommunicationChannel", "CommunicationChannels", new { id = lookup.CommunicationChannelId });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Communication Channels (Get)");
                return View(SubCommunicationChannelViewModel);
            }
        }
        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Second sub Communication Channelss Edit Post")]
        public IActionResult EditSecond(SubCommunicationChannelViewModel SubCommunicationChannelViewModel)
        {
            try
            {
                var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(SubCommunicationChannelViewModel.Id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _subCommunicationChannelService.EditSubCommunicationChannel(SubCommunicationChannelViewModel, lookup);
                }

                return RedirectToAction("SecondSubCommunicationChannel", "CommunicationChannels", new { id = lookup.ParentId });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Second Sub CommunicationChannel (Get)");
                return View(SubCommunicationChannelViewModel);
            }
        }

        // POST: ControlPanel/SubCommunicationChannels/Delete/5

        [AuditLogFilter(ActionDescription = "Communication Channelss Delete Post")]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _subCommunicationChannelService.DeleteSubCommunicationChannel(lookup);
                    return RedirectToAction("SubCommunicationChannel", "CommunicationChannels", new { id = lookup.CommunicationChannelId });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Communication Channels");
            }
            return RedirectToAction(nameof(Index), "CommunicationChannels");
        }

        [AuditLogFilter(ActionDescription = "Communication Channelss Delete Post")]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmedSecond(int id)
        {
            try
            {
                var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _subCommunicationChannelService.DeleteSubCommunicationChannel(lookup);
                    return RedirectToAction("SecondSubCommunicationChannel", "CommunicationChannels", new { id = lookup.ParentId });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Communication Channels");
            }
            return RedirectToAction(nameof(Index), "CommunicationChannels");
        }


        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lookups Get Get")]
        public async Task<IActionResult> GetSubCommunicationChannelByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _subCommunicationChannelService.GetSubCommunicationChannelById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }

        public IActionResult GetFirstSubChannel(int communicationChannelId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            return Ok(_subCommunicationChannelService.GetSubCommunicationChannels(communicationChannelId, languageId, parentId: null));
        } 
        
        public IActionResult GetSecondSubChannel(int communicationChannelId, int parentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            return Ok(_subCommunicationChannelService.GetSubCommunicationChannels(communicationChannelId, languageId, parentId));
        }
    }
}
