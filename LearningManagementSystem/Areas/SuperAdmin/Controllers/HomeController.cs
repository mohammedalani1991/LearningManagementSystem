using DataEntity.Models.EfModels;
using LearningManagementSystem.Services.ControlPanel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]

    public class HomeController : Controller
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ICookieService _cookieService;

        public HomeController(LearningManagementSystemContext context,ICookieService cookieService)
        {
            _context = context;
            _cookieService = cookieService;
        }
        public IActionResult Index()
        {
            ViewBag.Allow = _cookieService.GetCookie("SuperAdmin") == "superadmin";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(string password)
        {
            if (password == "superadmin" || _cookieService.GetCookie("SuperAdmin") == "superadmin")
            {
                if(password == "superadmin")
                    _cookieService.CreateCookie("SuperAdmin", password , 7);
                var result = _context.SuperAdmins;
                var settings = _context.SuperAdminSettings.FirstOrDefault();
                ViewBag.Logo = settings.ImageUrl;
                ViewBag.LogoAr = settings.ImageUrlAr;
                ViewBag.NameEnglish = settings.NameEnglish;
                ViewBag.NameArabic = settings.NameArabic;
                ViewBag.Color = settings.SiteColor;
                ViewBag.SecondaryColor = settings.SecondarySiteColor;
                return PartialView("_ControlPanel" , result);
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name)
        {
            try
            {
                var val = _context.SuperAdmins.FirstOrDefault(r => r.Name == name);
                if (val == null)
                {
                    _context.SuperAdmins.Add(new DataEntity.Models.EfModels.SuperAdmin { Name = name, Show = true });
                    _context.SaveChanges();
                    var result = _context.SuperAdmins;
                    var settings = _context.SuperAdminSettings.FirstOrDefault();
                    ViewBag.Logo = settings.ImageUrl;
                    ViewBag.LogoAr = settings.ImageUrlAr;
                    ViewBag.NameEnglish = settings.NameEnglish;
                    ViewBag.NameArabic = settings.NameArabic;
                    ViewBag.Color = settings.SiteColor;
                    ViewBag.SecondaryColor = settings.SecondarySiteColor;
                    return PartialView("_ControlPanel", result);
                }
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetValue(int id)
        {
            try
            {
                var val = _context.SuperAdmins.FirstOrDefault(r => r.Id == id);
                if (val != null)
                {
                    val.Show = !val.Show;
                    _context.Entry(val).State = EntityState.Modified;
                    _context.SaveChanges();
                    var result = _context.SuperAdmins;
                    var settings = _context.SuperAdminSettings.FirstOrDefault();
                    ViewBag.Logo = settings.ImageUrl;
                    ViewBag.LogoAr = settings.ImageUrlAr;
                    ViewBag.NameEnglish = settings.NameEnglish;
                    ViewBag.NameArabic = settings.NameArabic;
                    ViewBag.Color = settings.SiteColor;
                    ViewBag.SecondaryColor = settings.SecondarySiteColor;
                    return PartialView("_ControlPanel", result);
                }
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeSettings(string img ,string imgAr, string english , string arabic, string color ,string secondaryColor)
        {
            try
            {
                var settings = _context.SuperAdminSettings.FirstOrDefault();
                if (settings != null)
                {
                    settings.NameEnglish = english;
                    settings.NameArabic = arabic;
                    settings.ImageUrl = img;
                    settings.ImageUrlAr = imgAr;
                    settings.SiteColor = color;
                    settings.SecondarySiteColor = secondaryColor;
                    _context.Entry(settings).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
