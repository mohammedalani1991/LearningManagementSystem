using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class DetailsLookupsController : Controller
    {
        private readonly ILookupService _lookupService;
        private readonly ILogService _logService;

        public DetailsLookupsController(ILookupService lookupService, ILogService logService)
        {
            _lookupService = lookupService;
            _logService = logService;
        }

        // POST: ControlPanel/DetailsLookups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Details Lookups Create Post")]
        public IActionResult Create(DetailsLookupViewModel detailsLookupViewModel)
        {
            try
            {
                try
                {                                        
                    detailsLookupViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                    _lookupService.AddDetailsLookup(detailsLookupViewModel);

                    return RedirectToAction("Details", "MasterLookups", new { id = detailsLookupViewModel.MasterId });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity.Name, ex, "Error while add master lookup");
                    return View(detailsLookupViewModel);
                }
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add Details lookup");
                return View(detailsLookupViewModel);
            }
        }

        // GET: ControlPanel/DetailsLookups/Edit/        
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Details Lookups Edit Get")]
        public async Task<IActionResult> Edit(int? id, int masterId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            ViewData["MasterId"] = masterId;
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var lookup = _lookupService.GetDetailsLookupById(id.Value);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new DetailsLookupViewModel(lookup));
        }

        // POST: ControlPanel/DetailsLookups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Details Lookups Edit Post")]
        public IActionResult Edit(DetailsLookupViewModel detailsLookupViewModel)
        {
            try
            {
                var lookup = _lookupService.GetDetailsLookupById(detailsLookupViewModel.Id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _lookupService.EditDetailLookup(detailsLookupViewModel, lookup);
                }

                return RedirectToAction("Details", "MasterLookups", new { id = detailsLookupViewModel.MasterId });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Details Lookup (Get)");
                return View(detailsLookupViewModel);
            }
        }

        // POST: ControlPanel/DetailsLookups/Delete/5
        
        [AuditLogFilter(ActionDescription = "Details Lookups Delete Post")]        
        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var lookup = _lookupService.GetDetailsLookupById(id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _lookupService.DeleteDetailLookup(lookup);
                    return RedirectToAction("Details", "MasterLookups", new { id = lookup.MasterId });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete Details Lookup");
            }
            return RedirectToAction(nameof(Index), "MasterLookups");
        }


        [CustomAuthentication(PageName = "Lookups", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lookups Get Get")]
        public async Task<IActionResult> GetDetailsLookupByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _lookupService.GetDetailsLookupById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }
    }
}
