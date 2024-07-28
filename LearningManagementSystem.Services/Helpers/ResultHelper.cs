using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningManagementSystem.Services.Helpers
{
   public static class ResultHelper
    {
        public static JsonResult GetJsonResult(ControllerContext controllerContext,PartialViewResult partialViewResult, ICompositeViewEngine viewEngine, int resultStatus, string massage = "")
        {
            return new JsonResult(new ResultViewModel(resultStatus, ConvertViewToString(controllerContext, partialViewResult, viewEngine), massage));
        }

        private static string ConvertViewToString(ControllerContext controllerContext, PartialViewResult pvr, ICompositeViewEngine viewEngine)
        {
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = viewEngine.FindView(controllerContext, pvr.ViewName, false);
                ViewContext viewContext = new ViewContext(controllerContext, vResult.View, pvr.ViewData, pvr.TempData, writer, new HtmlHelperOptions());

                vResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
