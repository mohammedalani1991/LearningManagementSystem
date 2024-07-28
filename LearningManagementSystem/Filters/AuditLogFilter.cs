using System;
using DataEntity.Models.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Filters
{
    public class AuditLogFilter : Attribute, IActionFilter
    {
        public string ActionDescription { get; set; }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

            var payload = JsonConvert.SerializeObject(filterContext.ActionArguments);
            var controller = filterContext.Controller as Controller;
            if (controller == null) return;
            var controllerName = filterContext.RouteData.Values["controller"]?.ToString();
            var actionName = filterContext.RouteData.Values["action"]?.ToString();
            using (var db = new LearningManagementSystemContext())
            {
                AuditLog log = new AuditLog()
                {
                    Controller = controllerName,
                    Action = actionName,
                    IpAddress = filterContext.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                    CreatedOn = DateTime.Now,
                    CreatedBy = filterContext.HttpContext.User.Identity?.Name ?? string.Empty,
                    Description = ActionDescription,
                    RequestDetails = payload,
                    Status = (int) GeneralEnums.StatusEnum.Active
                };
                try
                {
                    db.AuditLogs.Add(log);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("System", ex, "Audit Log Filter Error");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
