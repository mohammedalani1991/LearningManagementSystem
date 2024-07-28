using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class UnauthorizedController : Controller
    {
        public IActionResult Index(string exceptiontext)
        {
            ViewBag.Message = exceptiontext;
            return View();
        }
    }
}
