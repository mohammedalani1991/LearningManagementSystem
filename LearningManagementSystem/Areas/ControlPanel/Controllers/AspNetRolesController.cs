using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystem.Core;
using System;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    //[Area("ControlPanel")]
    public class AspNetRolesController : Controller
    {
        private readonly IRolesPermissionService _rolesPermissionService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;

        public AspNetRolesController(ICookieService cookieService, IRolesPermissionService rolesPermissionService, ISettingService settingService, ILogService logService)
        {
            _rolesPermissionService = rolesPermissionService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;
        }

        // GET: ControlPanel/AspNetRoles
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Roles List")]
        public async Task<IActionResult> Index(int? page, string searchText, int pagination, int resetTo = 0)
        {
            if (resetTo == 1 || page == null || page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.RolePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.RolePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            var result = _rolesPermissionService.GetRoles(searchText, page, pagination);

            return View(result);
        }

        //[CustomAuthentication(PageName = "Roles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Roles Details")]
        // GET: ControlPanel/AspNetRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _rolesPermissionService.GetRoleViewModelById(id);

            return View(role);
        }

        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Roles Permission Post ")]
        [HttpPost]
        public async Task<IActionResult> AddPermission(string roleId)
        {
            var keys = Request.Form.Keys;
            var permissions = new List<RolePermission>();
            foreach (var key in keys.Where(r => r.StartsWith("Permission_")))
            {
                var permissionId = key.Split('_').ElementAt(1);
                var permission = new RolePermission()
                {
                    PermissionId = int.Parse(permissionId),
                    RoleId = roleId
                };
                permissions.Add(permission);
            }

            _rolesPermissionService.EditRole(permissions, roleId, null);
            return RedirectToAction(nameof(Details), new { id = roleId });
        }

        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Roles Create Get")]
        // GET: ControlPanel/AspNetRoles/Create
        public IActionResult Create()
        {
            ViewBag.AlertMessage = "";
            return View();
        }

        // POST: ControlPanel/AspNetRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Roles Create Post")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var s =_rolesPermissionService.AddRole(roleViewModel, out AspNetRole role);
                    if (s)
                    {
                        ViewBag.AlertMessage = "Error occurred while add New Role";
                        return View(roleViewModel);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(roleViewModel);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error while add New Role");
                ViewBag.AlertMessage = "Error occurred while add New Role";
                return View(roleViewModel);
            }
        }

        // GET: ControlPanel/AspNetRoles/Edit/5
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Roles Edit Get")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.AlertMessage = "";

            var aspNetRoles = _rolesPermissionService.GetRoleById(id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }
            return View(new AspNetRoleViewModel(aspNetRoles));
        }

        // POST: ControlPanel/AspNetRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Roles Edit Post")]
        public async Task<IActionResult> Edit(AspNetRoleViewModel aspNetRoles)
        {
            try
            {
                var role = _rolesPermissionService.GetRoleById(aspNetRoles.NewId);
                if(role != null)
                    return Json(new {success = false});

                if (ModelState.IsValid)
                {
                    _rolesPermissionService.EditRole(aspNetRoles);
                    return Json(new { success = true });
                }

                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity.Name, ex, "Error occurred while editing Role");
                ViewBag.AlertMessage = "Error occurred while editing Role";
                return null;
            }

        }

        // GET: ControlPanel/AspNetRoles/Delete/5
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Roles Delete Get")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = _rolesPermissionService.GetRoleById(id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }


            return View(aspNetRoles);
        }

        // POST: ControlPanel/AspNetRoles/Delete/5
        //[CustomAuthentication(PageName = "Roles", PermissionKey = "Delete")]
        [HttpPost, ActionName("Delete")]
        [AuditLogFilter(ActionDescription = "Roles Delete Post")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _rolesPermissionService.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
