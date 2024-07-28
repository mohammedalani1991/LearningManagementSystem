using LearningManagementSystem.Areas.Identity.Pages.Account;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static LearningManagementSystem.Core.Constants;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Localization;
using PhoneNumbers;
using System.Text.RegularExpressions;

namespace LearningManagementSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly IRolesPermissionService _rolesPermissionService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISettingService _settingService;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<AuthenticationController> _localizer;
        private readonly ISmsService _smsService;
        private readonly IContactsService _contactsService;
        private readonly ICookieService _cookieService;
        private readonly IAboutDicService _aboutDicService;
        private readonly IStudentService _studentService;

        public AuthenticationController(LearningManagementSystemContext context,
            ILogService logService,
            IUserProfileService userProfileService,
            IRolesPermissionService rolesPermissionService,
            UserManager<IdentityUser> userManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            SignInManager<IdentityUser> signInManager, ISettingService settingService, IEmailService emailService
            , IStringLocalizer<AuthenticationController> localizer, ISmsService smsService, IContactsService contactsService, ICookieService cookieService
            , IAboutDicService aboutDicService, IStudentService studentService)
        {
            _context = context;
            _logService = logService;
            _userProfileService = userProfileService;
            _rolesPermissionService = rolesPermissionService;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _settingService = settingService;
            _emailService = emailService;
            _localizer = localizer;
            _smsService = smsService;
            _contactsService = contactsService;
            _cookieService = cookieService;
            _aboutDicService = aboutDicService;
            _studentService = studentService;
        }

        [AuditLogFilter(ActionDescription = "Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Login")]
        public async Task<IActionResult> Login(string email, string password, bool remember)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, remember, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var userProfile = _userProfileService.GetUserProfileByUsername(email);
                    if (userProfile == null || userProfile.Status == (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        await _signInManager.SignOutAsync();
                        return Json(new { success = false, message = _localizer["Account Has Been Deleted"].Value });
                    }
                    _cookieService.RemoveCookie(".AspNetCore.Culture");
                    if (userProfile.PreferedLanguageId == (int)GeneralEnums.LanguageEnum.Arabic)
                        _cookieService.CreateCookie(".AspNetCore.Culture", "c=ar|uic=ar", 30);
                    else
                        _cookieService.CreateCookie(".AspNetCore.Culture", "c=en-US|uic=en-US", 30);

                    userProfile.LastLogin = DateTime.Now;
                    _userProfileService.UpdateUserProfile(userProfile);
                    return Json(new { success = true });
                }
                var user = await _userManager.FindByEmailAsync(email);

                if (result.IsLockedOut)
                    return Json(new { success = false, message = _localizer["Account is Locked Out"].Value });
                else if (result.IsNotAllowed && await _userManager.CheckPasswordAsync(user, password))
                    return Json(new { success = false, reSendEmail = true, message = _localizer["Your Email is not Verified"].Value });
                else
                    return Json(new { success = false, message = _localizer["Email or Password is Wrong"].Value });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Login");
                return Json(new { success = false, message = _localizer["Error while Login"].Value });
            }
        }

        [AuditLogFilter(ActionDescription = "Registration")]
        public IActionResult Register()
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Registration Post")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var account = _userProfileService.GetUserProfileByEmail(registerViewModel.Email);
            var emailValid = _userProfileService.GetContactEmail(registerViewModel.Email);

            if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
                return Json(new { success = false, message = _localizer["Account deactivated from the admin"].Value });

            if (emailValid)
                return Json(new { success = false, message = _localizer["Account already exist"].Value });

            if (ModelState.IsValid)
            {
                try
                {
                    var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString());

                    var user = new IdentityUser { UserName = registerViewModel.Email, Email = registerViewModel.Email, PhoneNumber = registerViewModel.PhoneNumber, EmailConfirmed = confirm };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        registerViewModel.Status = (int)GeneralEnums.StatusEnum.Active;
                        var contact = _contactsService.AddContacts(new ContactViewModel(registerViewModel), new List<int>() { (int)GeneralEnums.ContactTypeEnum.Student });

                        var studentViewModel = new StudentViewModel()
                        {
                            Email = contact.Email,
                            FullName = contact.FullName,
                            ContactId = contact.Id,
                            LanguageId = registerViewModel.LanguageId,
                            CreatedBy = User.Identity?.Name ?? string.Empty,
                            Country = string.Empty,
                            Work = string.Empty,
                            Status = contact.Status,
                            Mobile = registerViewModel.PhoneNumber,
                            PhoneNumberCode = registerViewModel.PhoneNumberCode,
                            EducationalLevelId = registerViewModel.EducationalLevelId,
                            BirthDate = registerViewModel.BirthDate,
                        };
                        _studentService.AddStudent(studentViewModel);

                        var userProfileViewModel = new UserProfileViewModel()
                        {
                            Email = contact.Email,
                            FullName = contact.FullName,
                            PreferedLanguageId = registerViewModel.LanguageId,
                            Username = contact.FirstName,
                            ContactId = contact.Id,
                            Status = contact.Status,
                        };
                        EntityHelper.AddProfile(userProfileViewModel, _context);

                        var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                        var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                        var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                        if (confirm)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                            var message = _aboutDicService.GetAboutDicByCode("RegistrationConfirmation", languageId).Value.ToString();
                            message = message.Replace("[Link]", HtmlEncoder.Default.Encode(callbackUrl).ToString());

                            await _smsService.SendEmail(new MessageViewModel
                            {
                                CreatedBy = user.Email,
                                Ids = new List<int>() { contact.Id },
                                Message = message,
                                Subject = "Confirm your email",
                                Emails = new List<string>() { }
                            });

                            string encodedEmail = System.Web.HttpUtility.UrlEncode(registerViewModel.Email);

                            return Json(new { success = true, link = $"{baseUrl}/Identity/Account/RegisterConfirmation?email={encodedEmail}" });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Json(new { success = true, link = $"{baseUrl}" });
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Signing up");
                    return null;
                }
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReSendConfirmtionEmail(string Email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = "" },
                        protocol: Request.Scheme);


                    await _smsService.SendEmail(new MessageViewModel
                    {
                        CreatedBy = user.Email,
                        Ids = new List<int>() { _contactsService.GetContactByEmail(user.Email).Id },
                        Message =
                       $"<h6>أهلا وسهلا بكم في أكاديمية الصفا للخدمات القرآنية </h6> نرجو تفعيل البريد الخاص بكم <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>بالضغط هنا</a>.",
                        Subject = "Confirm your email",
                        Emails = new List<string>() { }
                    });
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Re Sending ConfirmtionEmail");
                return null;
            }
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Dashboard", new { Area = "ControlPanel" });
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        static string GetMd5Sum(string productIdentifier)
        {
            System.Text.Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
            byte[] unicodeText = new byte[productIdentifier.Length * 2];
            enc.GetBytes(productIdentifier.ToCharArray(), 0, productIdentifier.Length, unicodeText, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static string FormatLicenseKey(string productIdentifier)
        {
            productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 28; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }
    }
}
