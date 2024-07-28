using LearningManagementSystem.Services.ControlPanel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem
{
    public class RoleSelectViewComponent : ViewComponent
    {
        private readonly IRolesPermissionService svc;
        public RoleSelectViewComponent( IRolesPermissionService _rolesPermissionService)
        {
            svc = _rolesPermissionService;
        }
        public IViewComponentResult Invoke(string id)
        {
            var result = svc.GetRoles(null, null);
            return View(result);
        }
    }
}
