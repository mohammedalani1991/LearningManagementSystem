using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;
using System.Linq;
using System.Collections.Generic;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class GeneralizationController : Controller
    {
        private readonly ILogService _logService;
        private readonly IGeneralizationService _generalizationService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        public GeneralizationController(ISettingService settingService, ICookieService cookieService, ILogService logService, IGeneralizationService generalizationService
            ,IUserProfileService userProfileService)
        {
            _logService = logService;
            _generalizationService = generalizationService;
            _userProfileService = userProfileService;
            _cookieService = cookieService;
            _settingService = settingService;
        }

        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Generalizations")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Generalization List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.GeneralizationPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.GeneralizationPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "Title", "Description", "Job", "Type", "Status", "CreatedBy", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.GeneralizationTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.GeneralizationTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.GeneralizationTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            int branchId = Int32.Parse(_cookieService.GetCookie(Constants.CookieNames.SelectedBranchId) ?? "0");
            if (!AuthenticationHelper.CheckAuthentication("Branches", "Select", User.Identity.Name))
                branchId = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact?.BranchId ?? 0;
            
            var result = _generalizationService.GetGeneralization(searchText, page, languageId, pagination, branchId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        [HttpGet]
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Create")]
        public ActionResult GetEmployeesCreate(int pagination, int jobId, int generalizationId, int branchId)
        {
            try
            {
                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;

                if (!(branchId > 0))
                {
                    branchId = Int32.Parse(_cookieService.GetCookie(Constants.CookieNames.SelectedBranchId) ?? "0");
                    if (!AuthenticationHelper.CheckAuthentication("Branches", "Select", User.Identity.Name))
                        branchId = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact?.BranchId ?? 0;
                }

                var result = _generalizationService.GetGeneralizationEmployees(languageId, branchId, jobId, generalizationId);
                return PartialView("_GeneralizationEmployees", result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While Editing CustomerOrder (Get)");
                return PartialView("_GeneralizationEmployees", null);
            }
        }


        // GET: ControlPanel/Generalizations/Details/5
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Generalization Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var generalization = _generalizationService.GetGeneralizationById(id.Value, langId);

            if (generalization == null || generalization.Status == (int)GeneralEnums.StatusEnum.Deleted)
                return NotFound();

            ViewBag.LangId = langId;

            var result = new GeneralizationViewModel(generalization);
            result.GeneralizationEmployees = _generalizationService.GetGeneralizationEmployees(result.Id, langId);
            result.Page = page;

            return PartialView("Details", result);
        }

        // GET: ControlPanel/Generalizations/Create
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Generalization Create Get")]
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

        // POST: ControlPanel/Generalizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Generalization Create Post")]
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Create")]
        public async Task<IActionResult> Create(GeneralizationViewModel GeneralizationViewModel)
        {
            try
            {
                if (GeneralizationViewModel.LanguageId == 0)
                    GeneralizationViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                if (GeneralizationViewModel.GeneralizationContactsText != null)
                {
                    if (GeneralizationViewModel.GeneralizationContactsText.Trim().Length > 0)
                    {
                        GeneralizationViewModel.GeneralizationContacts = GeneralizationViewModel.GeneralizationContactsText.Split(',').ToList();
                    }
                }

                GeneralizationViewModel.CreatedBy = User.Identity?.Name;
                _generalizationService.AddGeneralization(GeneralizationViewModel);
                return Json(true);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Generalization Dic");
                return null;
            }
        }

        // GET: ControlPanel/Generalizations/Edit/5
        [AuditLogFilter(ActionDescription = "Generalization Edit Get")]
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
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

            var generalization = _generalizationService.GetGeneralizationById(id.Value, languageId);

            if (generalization == null || generalization.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var result = new GeneralizationViewModel(generalization);
            result.Page = page;
            result.LanguageId = languageId;
            result.GeneralizationContacts = generalization.GeneralizationEmployees.Select(a => a.ContactId.ToString()).ToList();
            if (result.GeneralizationContacts.Count() > 0)
            {
                result.SelectEmployee = true;
                result.GeneralizationContactsText = "";
                for (int i = 0; i < result.GeneralizationContacts.Count(); i++)
                {
                    if (i == 0)
                    {
                        result.GeneralizationContactsText += result.GeneralizationContacts[i].ToString();
                    }
                    else
                    {
                        result.GeneralizationContactsText += "," + result.GeneralizationContacts[i];
                    }
                }

            }
            return PartialView("Edit", result);
        }

        // POST: ControlPanel/Generalizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuditLogFilter(ActionDescription = "Generalization Edit Pot")]
        public async Task<IActionResult> Edit(GeneralizationViewModel generalizationViewModel)
        {
            try
            {
                var Generalization = _generalizationService.GetGeneralizationById(generalizationViewModel.Id);
                if (Generalization != null && Generalization.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    if (generalizationViewModel.LanguageId == 0)
                        generalizationViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    if (generalizationViewModel.GeneralizationContactsText != null)
                    {
                        if (generalizationViewModel.GeneralizationContactsText.Trim().Length > 0)
                        {
                            generalizationViewModel.GeneralizationContacts = generalizationViewModel.GeneralizationContactsText.Split(',').ToList();
                        }
                    }
                    generalizationViewModel.CreatedBy = User.Identity.Name;
                    _generalizationService.EditGeneralization(generalizationViewModel, Generalization);
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Generalization Dic (Post)");
                return null;
            }

        }
        [AuditLogFilter(ActionDescription = "Generalization Delete Get")]
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Delete")]
        // GET: ControlPanel/Generalizations/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var generalization = _generalizationService.GetGeneralizationById(id.Value, langId);
            if (generalization == null || generalization.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var result = new GeneralizationViewModel(generalization);
            ViewBag.LangId = langId;
            result.Page = page;
            return PartialView("Delete", result);
        }

        // POST: ControlPanel/Generalizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Generalization Delete Post")]
        [CustomAuthentication(PageName = "Generalizations", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int Page)
        {
            var generalization = _generalizationService.GetGeneralizationById(id);
            if (generalization != null && generalization.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                _generalizationService.DeleteGeneralization(generalization);
                return Json(true);

            }
            return null;
        }


        public async Task<IActionResult> ReadNotification(int id)
        {
            try
            {
                bool isRead = _generalizationService.ReadNotification(id);
                return Json(isRead);
            }
            catch(Exception ex)
            {
                return Json(null);
            }
        }
    }
}
