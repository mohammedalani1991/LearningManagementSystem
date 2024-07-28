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
using LearningManagementSystem.Core;
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class BranchController : Controller
    {
        private readonly ILogService _logService;
        private readonly IBranchService _branchService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public BranchController(
            ICookieService cookieService,ILogService logService, 
            IBranchService branchService, ISettingService settingService)
        {
            _logService = logService;
            _branchService = branchService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Branches", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Branches")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Branches", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Branch List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.BranchPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.BranchPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Id", "Name", "Code", "Color", "Status", "CreatedBy", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.BranchTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.BranchTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.BranchTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _branchService.GetBranches(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Branches", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Branchs/Details/5
        [CustomAuthentication(PageName = "Branches", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Branch Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var branch = _branchService.GetBranchById(id.Value, langId);
            
            if (branch == null || branch.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;

            branch.Page = page;
            return PartialView("Details", branch);
        }

        // GET: ControlPanel/Branchs/Create
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Branch Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Branchs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Branch Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Code,ColorId,LanguageId,Status")]
            BranchViewModel BranchViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (BranchViewModel.LanguageId == 0)
                        BranchViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    BranchViewModel.CreatedBy = User.Identity?.Name;                    
                    _branchService.AddBranch(BranchViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Branch Dic");
                    return View(BranchViewModel);
                }
            }
            return View(BranchViewModel);
        }

        // GET: ControlPanel/Branchs/Edit/5
        [AuditLogFilter(ActionDescription = "Branch Edit Get")]
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId=0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }           
            ViewBag.LangId = languageId;
            var branch = _branchService.GetBranchById(id.Value, languageId);
            branch.LanguageId = languageId;
            if (branch == null || branch.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            branch.Page = page;
            return PartialView("Edit", branch);
        }

        // POST: ControlPanel/Branchs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Branch Edit Pot")]
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,ColorId,Page,Name,Code,LanguageId,Status")]
            BranchViewModel branchViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var branch = _branchService.GetBranchById(branchViewModel.Id);
                    if (branch != null && branch.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (branchViewModel.LanguageId == 0)
                            branchViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _branchService.EditBranch(branchViewModel, branch);
                    }

                    return RedirectToAction(nameof(Index), new { page = branchViewModel.Page});
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Branch Dic (Post)");
                    return View(branchViewModel);
                }
            }
            return View(branchViewModel);
        }
        [AuditLogFilter(ActionDescription = "Branch Delete Get")]
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Delete")]

        // GET: ControlPanel/Branchs/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var branch = _branchService.GetBranchById(id.Value, langId);
            if (branch == null || branch.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            branch.Page = page;
            return PartialView("Delete", branch);
        }

        // POST: ControlPanel/Branchs/Delete/5
        [AuditLogFilter(ActionDescription = "Branch Delete Post")]
        [CustomAuthentication(PageName = "Branches", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteBranch(int id, int Page)
        {
            var branch = _branchService.GetBranchById(id);
            if (branch != null && branch.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _branchService.DeleteBranch(branch);
            }

            return RedirectToAction(nameof(Index),new {page=Page });
        }
    }
}
