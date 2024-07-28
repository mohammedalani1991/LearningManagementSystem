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
    public class CmsCategoryProjectController : Controller
    {
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICmsCategoryProjectService _CmsCategoryProjectervice;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public CmsCategoryProjectController(
            ICookieService cookieService, ILogService logService, IUserProfileService userProfileService,
            ICmsCategoryProjectService CmsCategoryProjectervice, ISettingService settingService)
        {
            _logService = logService;
            _userProfileService = userProfileService;
            _CmsCategoryProjectervice = CmsCategoryProjectervice;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List CmsCategoryProject")]
        [CheckSuperAdmin(PageName = "ProjectCategory")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsCategoryProject List")]
        [CheckSuperAdmin(PageName = "ProjectCategory")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CmsProjectCategoryPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CmsProjectCategoryPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Name", "Description", "Status", "CreatedBy", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CmsProjectCategoryTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsProjectCategoryTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CmsProjectCategoryTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

    
            var result = _CmsCategoryProjectervice.GetCmsCategoryProjects(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/CmsCategoryProject/Details/5
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Details")]
        public async Task<IActionResult> ShowDetails(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.LangId = languageId;


            var CmsCategoryProject = _CmsCategoryProjectervice.GetCmsCategoryProjectById(id.Value, languageId);
            if (CmsCategoryProject == null || CmsCategoryProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Details", CmsCategoryProject);
        }


        // GET: ControlPanel/CmsCategoryProject/Create
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Create Get")]
        public IActionResult ShowCreate()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

        

            return PartialView("Create");
        }

        // POST: ControlPanel/CmsCategoryProject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Create Post")]
        public async Task<IActionResult> Create(CmsCategoryProjectViewModel CmsCategoryProjectViewModel)
        {
            try
            {
                if (CmsCategoryProjectViewModel.LanguageId == 0)
                    CmsCategoryProjectViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                CmsCategoryProjectViewModel.CreatedBy = User.Identity?.Name;
                _CmsCategoryProjectervice.AddCmsCategoryProject(CmsCategoryProjectViewModel);
                return Json(true);
            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new CmsCategoryProject Dic");
                return null;
            }
        }

        // GET: ControlPanel/CmsCategoryProject/Edit/5
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Edit Get")]
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Edit")]
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
            var CmsCategoryProject = _CmsCategoryProjectervice.GetCmsCategoryProjectById(id.Value, languageId);
            CmsCategoryProject.LanguageId = languageId;
            if (CmsCategoryProject == null || CmsCategoryProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            return PartialView("Edit", CmsCategoryProject);
        }

        // POST: ControlPanel/CmsCategoryProject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Edit Pot")]
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(CmsCategoryProjectViewModel CmsCategoryProjectViewModel)
        {
            try
            {
                var CmsCategoryProject = _CmsCategoryProjectervice.GetCmsCategoryProjectById(CmsCategoryProjectViewModel.Id);
                if (CmsCategoryProject != null && CmsCategoryProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (CmsCategoryProjectViewModel.LanguageId == 0)
                        CmsCategoryProjectViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    _CmsCategoryProjectervice.EditCmsCategoryProject(CmsCategoryProjectViewModel, CmsCategoryProject);
                    return Json(true);

                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing CmsCategoryProject Dic (Post)");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "CmsCategoryProject Delete Get")]
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Delete")]

        // GET: ControlPanel/CmsCategoryProject/Delete/5
        public async Task<IActionResult> ShowDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;

            var CmsCategoryProject = _CmsCategoryProjectervice.GetCmsCategoryProjectById(id.Value, langId);
            if (CmsCategoryProject == null || CmsCategoryProject.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            return PartialView("Delete", CmsCategoryProject);
        }

        // POST: ControlPanel/CmsCategoryProject/Delete/5
        [AuditLogFilter(ActionDescription = "CmsCategoryProject Delete Post")]
        [CustomAuthentication(PageName = "CmsCategoryProject", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCmsCategoryProject(int id)
        {
            try
            {
                var CmsCategoryProject = _CmsCategoryProjectervice.GetCmsCategoryProjectById(id);
                if (CmsCategoryProject != null && CmsCategoryProject.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    _CmsCategoryProjectervice.DeleteCmsCategoryProject(CmsCategoryProject);
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
    }

}
