using System;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace LearningManagementSystem.Filters
{
    public class CheckSuperAdmin : Attribute, IActionFilter
    {
        public string PageName { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var valid = AuthenticationHelper.CheckSuperAuthentication(PageName);
            if (!valid)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    { {"area", ""},{"controller", "Home"}, {"action", "Null"}});
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
