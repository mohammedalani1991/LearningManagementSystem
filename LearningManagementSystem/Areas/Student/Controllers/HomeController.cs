using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {

        [Authorize]
        [AuditLogFilter(ActionDescription = "Student Home Page")]
        public async Task<IActionResult> Index()
        {
            return PartialView("_Index");
        }
    }
}
