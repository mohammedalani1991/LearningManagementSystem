using System;
using System.Text.RegularExpressions;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace LearningManagementSystem.Filters
{
    public class CustomAuthenticationAttribute : Attribute, IActionFilter
    {
        public string PageName { get; set; }
        public string PermissionKey { get; set; }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = filterContext.HttpContext.User.Identity?.Name;
            var valid = AuthenticationHelper.CheckAuthentication(PageName, PermissionKey, userName);
            if (!valid)
            {
                string message = Regex.Replace(PageName, @"(\p{Ll})(\p{Lu})", "$1 $2");
                var values = new RouteValueDictionary(new
                {
                    area = "ControlPanel",
                    controller = "Unauthorized",
                    action = "Index",
                    exceptiontext = message
                });
                filterContext.Result = new RedirectToRouteResult(values);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
