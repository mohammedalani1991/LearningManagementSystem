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

    public class CityController : Controller
    {
        private readonly ICityService _Cityervice;
        private readonly ILogService _logService;

        public CityController(ICityService Cityervice, ILogService logService)
        {
            _Cityervice = Cityervice;
            _logService = logService;
        }

        // POST: ControlPanel/DetailsCity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "City", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Details City Create Post")]
        public IActionResult Create(CityViewModel CityViewModel)
        {
            try
            {
                try
                {
                    CityViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                    _Cityervice.AddCity(CityViewModel);

                    return RedirectToAction("Details", "Country", new { id = CityViewModel.CountryId });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity.Name, ex, "Error while add City");
                    return View(CityViewModel);
                }
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add City");
                return View(CityViewModel);
            }
        }

        // GET: ControlPanel/DetailsCity/Edit/        
        [CustomAuthentication(PageName = "City", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Details City Edit Get")]
        public async Task<IActionResult> Edit(int? id, int countryId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            ViewData["CountryId"] = countryId;
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var lookup = _Cityervice.GetCityById(id.Value);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new CityViewModel(lookup));
        }

        // POST: ControlPanel/DetailsCity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthentication(PageName = "City", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Details City Edit Post")]
        public IActionResult Edit(CityViewModel CityViewModel)
        {
            try
            {
                var lookup = _Cityervice.GetCityById(CityViewModel.Id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _Cityervice.EditDetailLookup(CityViewModel, lookup);
                }

                return RedirectToAction("Details", "Country", new { id = CityViewModel.CountryId });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing City (Get)");
                return View(CityViewModel);
            }
        }

        // POST: ControlPanel/DetailsCity/Delete/5

        [AuditLogFilter(ActionDescription = "Details City Delete Post")]
        [CustomAuthentication(PageName = "City", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var lookup = _Cityervice.GetCityById(id);
                if (lookup != null && lookup.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _Cityervice.DeleteCity(lookup);
                    return RedirectToAction("Details", "Country", new { id = lookup.CountryId });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete City");
            }
            return RedirectToAction(nameof(Index), "Country");
        }


        [CustomAuthentication(PageName = "City", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "City Get Get")]
        public async Task<IActionResult> GetCityByLanguage(int? id, int languageId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lookup = _Cityervice.GetCityById(id.Value, languageId);
            if (lookup == null || lookup.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return Json(lookup);
        }
    }
}
