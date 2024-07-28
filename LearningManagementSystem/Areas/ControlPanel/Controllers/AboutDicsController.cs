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
    public class AboutDicsController : Controller
    {
        private readonly ILogService _logService;
        private readonly IAboutDicService _aboutDicService;

        public AboutDicsController(ILogService logService, IAboutDicService aboutDicService)
        {
            _logService = logService;
            _aboutDicService = aboutDicService;
        }

        // User with Role Administrator can access this.
        // [Authorize("Administrator")]
        // GET: ControlPanel/AboutDics
        [CustomAuthentication(PageName = "AboutDics", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "About List")]
        public async Task<IActionResult> Index(int? page, string searchText, int resetTo = 0)
        {
            if (resetTo == 1)
            {
                page = 1;
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ViewBag.searchText = searchText;
            }

            var result = _aboutDicService.GetAboutDic(searchText, page);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            return View(result);
        }

        // GET: ControlPanel/AboutDics/Details/5
        [CustomAuthentication(PageName = "AboutDics", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "About Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutDic = _aboutDicService.GetAboutDicById(id.Value);
            if (aboutDic == null || aboutDic.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new AboutDicViewModel(aboutDic));
        }

        // GET: ControlPanel/AboutDics/Create
        [CustomAuthentication(PageName = "AboutDics", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "About Create Get")]
        public IActionResult Create()
        {
            ViewBag.LangId = CultureHelper.GetDefaultLanguageId();
            return View();
        }

        // POST: ControlPanel/AboutDics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "About Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,GroupName,Name,Value,LanguageId,Status")]
            AboutDicViewModel aboutDicViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutDicViewModel.LanguageId == 0)
                        aboutDicViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    aboutDicViewModel.CreatedBy = User.Identity?.Name;
                    _aboutDicService.AddAboutDic(aboutDicViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new About Dic");
                    return View(aboutDicViewModel);
                }
            }
            return View(aboutDicViewModel);
        }

        // GET: ControlPanel/AboutDics/Edit/5
        [AuditLogFilter(ActionDescription = "About Edit Get")]
        public async Task<IActionResult> Edit(int? id, int languageId = (int)GeneralEnums.LanguageEnum.Arabic)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;

            var aboutDic = _aboutDicService.GetAboutDicById(id.Value, languageId);
            if (aboutDic == null || aboutDic.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(aboutDic);
        }

        // POST: ControlPanel/AboutDics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "About Edit Pot")]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,GroupName,Name,Value,LanguageId,Status")]
            AboutDicViewModel aboutDicViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var aboutDic = _aboutDicService.GetAboutDicById(aboutDicViewModel.Id);
                    if (aboutDic != null && aboutDic.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (aboutDicViewModel.LanguageId == 0)
                            aboutDicViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _aboutDicService.EditAboutDic(aboutDicViewModel, aboutDic);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing About Dic (Post)");
                    return View(aboutDicViewModel);
                }
            }
            return View(aboutDicViewModel);
        }
        [AuditLogFilter(ActionDescription = "About Delete Get")]

        // GET: ControlPanel/AboutDics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aboutDic = _aboutDicService.GetAboutDicById(id.Value);
            if (aboutDic == null || aboutDic.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return View(new AboutDicViewModel(aboutDic));
        }

        // POST: ControlPanel/AboutDics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "About Delete Post")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutDic = _aboutDicService.GetAboutDicById(id);
            if (aboutDic != null && aboutDic.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _aboutDicService.DeleteAboutDic(aboutDic);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
