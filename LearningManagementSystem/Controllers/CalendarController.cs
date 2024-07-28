using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningManagementSystem.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger<CourseCategoriesController> _logger;
        private readonly ICookieService _cookieService;
        private readonly ICalendarService _calendarService;
        private readonly IUserProfileService _userProfileService;

        public CalendarController(ILogger<CourseCategoriesController> logger, ICookieService cookieService
            , ICalendarService calendarService, IUserProfileService userProfileService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _calendarService = calendarService;
            _userProfileService = userProfileService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCalendarData(string Name, int? TypeID, DateTime startDate, DateTime endDate)
        {

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var result = await _calendarService.GetCalendarsForGuest(Name, startDate, endDate, languageId, TypeID);
            ViewBag.LangId = languageId;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportForGoogleCalendar(string Name, int? TypeID, DateTime startDate, DateTime endDate)
        {
            var builder = new StringBuilder();
            //“Import events into Google Calendar.”
            //Format headers & events in .csv files
            //https://support.google.com/calendar/answer/37118?hl=en&co=GENIE.Platform%3DDesktop#zippy=%2Ccreate-or-edit-a-csv-file
            builder.AppendLine("Subject,Start Date,Start Time,End Date,End Time");


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var result = await _calendarService.GetCalendarsForGuest(Name, startDate, endDate, languageId, TypeID);
            foreach (var calendar in result)
            {
                builder.AppendLine($"{calendar.Name},{calendar.StartDate.ToString("dd/MM/yyyy")},{calendar.StartDate.ToString("hh:mm tt")},{calendar.EndDate.ToString("dd/MM/yyyy")},{calendar.EndDate.ToString("hh:mm tt")}");
            }
            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var output = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
            return File(output, "text/csv;charset=UTF-8", "ExportedForGoogleCalendar-" + DateTime.Now.ToString("ddMMyyyyhhmm") + ".csv");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetCalendarDataForProfile(string Name, int? TypeID, DateTime startDate, DateTime endDate)
        {
            var role = LookupHelper.GetUserRole(User?.Identity?.Name ?? string.Empty);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;

            if (role == "Student")
            {
                var result = await _calendarService.GetCalendarsForStudent(ContactID, null, Name, startDate, endDate, languageId, TypeID);
                return Json(new { data = result, lang = languageId });
            }
            else if (role == "Trainer" || role == "Trainer2")
            {
                var result = await _calendarService.GetCalendarsForTrainer(ContactID, Name, startDate, endDate, languageId, TypeID);
                return Json(new { data = result, lang = languageId });
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> ExportForGoogleCalendarForProfile(string Name, int? TypeID, DateTime startDate, DateTime endDate)
        {
            var builder = new StringBuilder();
            //“Import events into Google Calendar.”
            //Format headers & events in .csv files
            //https://support.google.com/calendar/answer/37118?hl=en&co=GENIE.Platform%3DDesktop#zippy=%2Ccreate-or-edit-a-csv-file
            builder.AppendLine("Subject,Start Date,Start Time,End Date,End Time");
            var role = LookupHelper.GetUserRole(User?.Identity?.Name ?? string.Empty);

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;

            List<PrivateCalendarViewModel> result = new List<PrivateCalendarViewModel>();
            if (role == "Student")
            {
                result = await _calendarService.GetCalendarsForStudent(ContactID, null, Name, startDate, endDate, languageId, TypeID);
            }
            else if (role == "Trainer" || role == "Trainer2")
            {
                result = await _calendarService.GetCalendarsForTrainer(ContactID, Name, startDate, endDate, languageId, TypeID);
            }

            foreach (var calendar in result)
            {
                builder.AppendLine($"{calendar.Name},{calendar.StartDate.ToString("dd/MM/yyyy")},{calendar.StartDate.ToString("hh:mm tt")},{calendar.EndDate.ToString("dd/MM/yyyy")},{calendar.EndDate.ToString("hh:mm tt")}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var output = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
            return File(output, "text/csv;charset=UTF-8", "ExportedForGoogleCalendar-" + DateTime.Now.ToString("ddMMyyyyhhmm") + ".csv");
        }

        public IActionResult Index()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.initialDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
    }
}
