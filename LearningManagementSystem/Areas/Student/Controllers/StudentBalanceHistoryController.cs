using LearningManagementSystem.Services.Controllers;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using System;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentBalanceHistoryController : Controller
    {
        private readonly IBalanceHistoryService _balanceHistoryService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStudentService _studentService;
        private readonly ICookieService _cookieService;
        private readonly ILogService _logService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISenangPayService _senangPayService;
        private readonly ICurrencyService _currencyService;

        public StudentBalanceHistoryController(IBalanceHistoryService balanceHistoryService, IUserProfileService userProfileService, IStudentService studentService,
            ICookieService cookieService, ILogService logService, UserManager<IdentityUser> userManager, ISenangPayService senangPayService
            , ICurrencyService currencyService)
        {
            _balanceHistoryService = balanceHistoryService;
            _userProfileService = userProfileService;
            _studentService = studentService;
            _cookieService = cookieService;
            _logService = logService;
            _userManager = userManager;
            _senangPayService = senangPayService;
            _currencyService = currencyService;
        }

        [Authorize]
        public IActionResult GetData(int? page)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var student = _studentService.GetStudentByContactId(ContactID);

            var result = _balanceHistoryService.GetStudentBalanceHistory(student.Id, page ?? 1, languageId);
            return PartialView("_index", result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBalance(int balance)
        {
            try
            {
                if (balance % 5 == 0 && balance > 0)
                {
                    SenangPayViewModel senangPayViewModel = new SenangPayViewModel();

                    var exchangeCurrency = _currencyService.GetExchangeCurrency();

                    var user = await _userManager.GetUserAsync(User);
                    var profile = _userProfileService.GetUserProfileByUsername(user.UserName);

                    senangPayViewModel.SenangPayPaymentType = (int)GeneralEnums.SenangPayPaymentType.AddBalance;
                    senangPayViewModel.ProcessDate = DateTime.Now;
                    senangPayViewModel.Email = user.Email;
                    senangPayViewModel.UserName = user.UserName;
                    senangPayViewModel.Country = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    senangPayViewModel.ApplicationUserId = _userManager.GetUserId(User);
                    senangPayViewModel.CreatedBy = User.Identity.Name;
                    senangPayViewModel.CreatedOn = DateTime.Now;
                    senangPayViewModel.FullName = profile.Contact.FullName;
                    senangPayViewModel.PhoneNumber = profile.Contact.Mobile;
                    senangPayViewModel.Amount = balance;
                    senangPayViewModel.senangPayId = _senangPayService.AddSenangPay(senangPayViewModel)?.Id ?? 0;

                    var url = await _senangPayService.GetUrl(senangPayViewModel);
                    if (url != null)
                        return Json(url);
                    else
                        return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Adding balence");
                return null;
            }
        }
    }
}
