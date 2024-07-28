using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace LearningManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentCertificatesController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUserProfileService _userProfileService;
        private readonly IEnrollStudentCourseService _enrollStudentCourse;

        public StudentCertificatesController(IStudentService studentService, IUserProfileService userProfileService ,IEnrollStudentCourseService enrollStudentCourse )
        {
            _studentService = studentService;
            _userProfileService = userProfileService;
            _enrollStudentCourse = enrollStudentCourse;
        }

        [Authorize]
        [AuditLogFilter(ActionDescription = "Student Certificates")]
        public async Task<IActionResult> GetData()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);

            var result = _studentService.GetStudentCertificates(studenDetails.Id, languageId);

            return PartialView("_Index", result);
        }

        public IActionResult ShowCertificates(int courseId , int templatetId)
        {
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var course = _enrollStudentCourse.GetEnrollStudentCourseById(courseId);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            if (course.StudentId == studenDetails.Id)
            {
                var baseUri = $"{Request.Scheme}://{Request.Host}/";
                var stream = _enrollStudentCourse.GetCertificate(course, templatetId, languageId, baseUri);
                return File(stream, "application/pdf", course.Course.CourseName+".pdf");
            }
            return null;
        }
    }
}
