using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Localization;
using PhoneNumbers;
using LearningManagementSystem.Areas.ControlPanel.Controllers;
using Microsoft.Extensions.Localization;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace LearningManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IAboutDicService _aboutDicService;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<RegisterModel> _localizer;
        private readonly IContactsService _contactsService;
        private readonly IStudentService _studentService;
        private readonly IUserProfileService _userProfileService;
        private readonly LearningManagementSystemContext _context;
        private readonly ISmsService _smsService;

        public RegisterModel(ISmsService smsService, IContactsService contactsService, IStudentService studentService, IUserProfileService userProfileService, LearningManagementSystemContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger, IAboutDicService aboutDicService,
            IEmailSender emailSender, IStringLocalizer<RegisterModel> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _aboutDicService = aboutDicService;
            _emailSender = emailSender;
            _localizer = localizer;
            _contactsService = contactsService;
            _studentService = studentService;
            _userProfileService = userProfileService;
            _context = context;
            _smsService = smsService;
        }

        [BindProperty]
        public GeneralContactViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public IActionResult OnGetAsync(string returnUrl = null)
        {
            return RedirectToAction("Register", "Authentication" , new {area = ""});
            //ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var account = _userProfileService.GetUserProfileByUsername(Input.UserProfileViewModel.Username);
            if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
            {
                string msg = _localizer["Account deactivated from the admin"].Value;
                ModelState.AddModelError("", msg);
            }

            if (account != null)
            {
                string msg = _localizer["Account already exist"].Value;
                ModelState.AddModelError("", msg);
            }

            if (!Regex.IsMatch(Input.UserProfileViewModel.PhoneNumber, @"^\d+$"))
            {
                string msg = _localizer["Invalid Phone Number"].Value;
                ModelState.AddModelError("", msg);
            }
            else
            {
                PhoneNumber number = PhoneNumberUtil.GetInstance().Parse(Input.UserProfileViewModel.PhoneNumberCode + Input.UserProfileViewModel.PhoneNumber, null);
                bool isValid = PhoneNumberUtil.GetInstance().IsValidNumber(number);

                if (isValid == false)
                {
                    string msg = _localizer["Invalid Phone Number"].Value;
                    ModelState.AddModelError("", msg);
                }
            }

            if (ModelState.IsValid)
            {
                var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString() == "on");

                var user = new IdentityUser { UserName = Input.ContactViewModel.Email, Email = Input.ContactViewModel.Email, PhoneNumber = Input.UserProfileViewModel.PhoneNumber, EmailConfirmed = confirm };
                var result = await _userManager.CreateAsync(user, Input.UserProfileViewModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    Input.ContactViewModel.Status = (int)GeneralEnums.StatusEnum.Active;
                    var contact = _contactsService.AddContacts(Input.ContactViewModel, new List<int>() { (int)GeneralEnums.ContactTypeEnum.Student });

                    Input.StudentViewModel.Status = contact.Status;
                    Input.StudentViewModel.CreatedOn = DateTime.Now;
                    Input.StudentViewModel.Email = Input.ContactViewModel.Email;
                    Input.StudentViewModel.ContactId = contact.Id;
                    Input.StudentViewModel.LanguageId = Input.ContactViewModel.LanguageId;
                    Input.StudentViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                    Input.StudentViewModel.Country = string.Empty;
                    Input.StudentViewModel.Work = string.Empty;
                    Input.StudentViewModel.Mobile = Input.UserProfileViewModel.PhoneNumber;
                    Input.StudentViewModel.PhoneNumberCode = Input.UserProfileViewModel.PhoneNumberCode;
                    _studentService.AddStudent(Input.StudentViewModel);

                    var userProfileViewModel = Input.UserProfileViewModel;
                    userProfileViewModel.Email = contact.Email;
                    userProfileViewModel.FullName = contact.FullName;
                    userProfileViewModel.PreferedLanguageId = Input.ContactViewModel.LanguageId;
                    userProfileViewModel.Username = contact.FirstName;
                    userProfileViewModel.ContactId = contact.Id;
                    userProfileViewModel.Status = contact.Status;
                    EntityHelper.AddProfile(userProfileViewModel, _context);

                    var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                    var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

                    if (!confirm)
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

                    }


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.ContactViewModel.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}