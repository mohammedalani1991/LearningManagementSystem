using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{

    [Area("ControlPanel")]
    public class CmsProjectController : Controller
    {
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICmsProjectService _CmsProjectervice;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CmsProjectController(
            ICookieService cookieService, ILogService logService, IUserProfileService userProfileService,
            ICmsProjectService CmsProjectervice, ISettingService settingService)
        {
            _logService = logService;
            _userProfileService = userProfileService;
            _CmsProjectervice = CmsProjectervice;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsProject")]
        [CheckSuperAdmin(PageName = "Projects")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsProject List")]
        [CheckSuperAdmin(PageName = "Projects")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsProjectPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsProjectPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Name", "Description", "ShortDescription", "ImageUrl", "Keyword", "PublishDate", "EndDate", "SortOrder", "ShowInHomePage", "IsFeatured", "PaymentType", "ProjectCost", "OneObjectFees", "ProjectStatus", "ProjectCategoryId", "BranchId", "Status", "CreatedBy", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsProjectTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsProjectTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsProjectTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;


            var result = _CmsProjectervice.GetCmsProjects(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/CmsProject/ShowImage
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsProject Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CmsProjectViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/CmsProject/Details/5
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsProject Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;


            var CmsProject = _CmsProjectervice.GetCmsProjectById(id.Value, languageId);
            if (CmsProject == null || CmsProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", CmsProject);
        }


        // GET: ControlPanel/CmsProject/Create
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsProject Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

            int branchId = Int32.Parse(_cookieService.GetCookie(Constants.CookieNames.SelectedBranchId) ?? "0");
            if (!AuthenticationHelper.CheckAuthentication("Branches", "Select", User.Identity.Name))
                branchId = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact?.BranchId ?? 0;
            ViewBag.BranchId = branchId;

            return PartialView("Create");
        }

        // POST: ControlPanel/CmsProject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsProject Create Post")]
        public async Task<IActionResult> Create(CmsProjectViewModel CmsProjectViewModel)
        {
            try
            {
                if (CmsProjectViewModel.LanguageId == 0)
                    CmsProjectViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                CmsProjectViewModel.CreatedBy = User.Identity?.Name;
                _CmsProjectervice.AddCmsProject(CmsProjectViewModel);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsProject Dic");
                return null;
            }
        }

        // GET: ControlPanel/CmsProject/Edit/5
        [AuditLogFilter(ActionDescription = "CmsProject Edit Get")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId)
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
            var CmsProject = _CmsProjectervice.GetCmsProjectById(id.Value, languageId);
            CmsProject.LanguageId = languageId;
            if (CmsProject == null || CmsProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return PartialView("Edit", CmsProject);
        }

        // POST: ControlPanel/CmsProject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsProject Edit Pot")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(CmsProjectViewModel CmsProjectViewModel)
        {
            try
            {
                var CmsProject = _CmsProjectervice.GetCmsProjectById(CmsProjectViewModel.Id);
                if (CmsProject != null && CmsProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (CmsProjectViewModel.LanguageId == 0)
                        CmsProjectViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    _CmsProjectervice.EditCmsProject(CmsProjectViewModel, CmsProject);
                    return Json(true);

                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsProject Dic (Post)");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "CmsProject Delete Get")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsProject/Delete/5
        public async Task<IActionResult> ShowDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

            var CmsProject = _CmsProjectervice.GetCmsProjectById(id.Value, langId);
            if (CmsProject == null || CmsProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Delete", CmsProject);
        }

        // POST: ControlPanel/CmsProject/Delete/5
        [AuditLogFilter(ActionDescription = "CmsProject Delete Post")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsProject(int id)
        {
            try
            {
                var CmsProject = _CmsProjectervice.GetCmsProjectById(id);
                if (CmsProject != null && CmsProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _CmsProjectervice.DeleteCmsProject(CmsProject);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While company Note");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Show Edit Source CmsProject")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEditSource(int? id)
        {
            var project = _CmsProjectervice.GetCmsProjectById(id.Value);
            if (project == null || project.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            return PartialView("_EditSource", new CmsProjectResourcesViewModel() { Id = id.Value, Name = project.Name, Link = project.CmsProjectResources.Select(r => r.Link).ToList() });
        }

        [AuditLogFilter(ActionDescription = "Edit Source CmsProject")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> EditSource(CmsProjectResourcesViewModel resources)
        {
            try
            {
                var CmsProject = _CmsProjectervice.GetCmsProjectById(resources.Id);
                if (CmsProject != null && CmsProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _CmsProjectervice.EditResources(CmsProject, resources);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Source CmsProject");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Show Edit Source CmsProject")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEditProjectCosts(int? id, int? LanguageId)
        {
            var project = _CmsProjectervice.GetCmsProjectById(id.Value);
            if (project == null || project.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (LanguageId != null)
                langId = LanguageId.Value;

            ViewBag.LangId = langId;

            return PartialView("_EditCosts", new CmsProjectCostsViewModel() { Id = id.Value, Name = project.Name, CmsProjectCosts = _CmsProjectervice.GetCmsProjectCosts(project.Id, langId) });
        }

        [AuditLogFilter(ActionDescription = "Edit Project Costs")]
        [CustomAuthentication(PageName = "CmsProject", PermissionKey = "Edit")]
        public async Task<IActionResult> EditProjectCosts(CmsProjectCostsViewModel costs)
        {
            try
            {
                var CmsProject = _CmsProjectervice.GetCmsProjectById(costs.Id);
                costs.CreatedBy = User?.Identity?.Name??string.Empty;

                if (CmsProject != null && CmsProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _CmsProjectervice.EditProjectCosts(costs);
                    return Json(true);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Project Costs");
                return null;
            }
        }
    }
}

