using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Core;
using LearningManagementSystem.Services.General;
using DocumentFormat.OpenXml.Wordprocessing;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Localization;

namespace LearningManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailsService _emailsService;
        private readonly ISettingService _settingService;
        private readonly ISmsService _smsService;
        private readonly IAboutDicService _aboutDicService;
        private readonly IContactsService _contactsService;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailsService emailsService, ISettingService settingService,ISmsService smsService
            ,IAboutDicService aboutDicService,IContactsService contactsService)
        {
            _userManager = userManager;
            _emailsService = emailsService;
            _settingService = settingService;
            _smsService = smsService;
            _aboutDicService = aboutDicService;
            _contactsService = contactsService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                protocol: Request.Scheme);

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

                var message = _aboutDicService.GetAboutDicByCode("PasswordReset", languageId).Value.ToString();
                message = message.Replace("[Link]", HtmlEncoder.Default.Encode(callbackUrl).ToString());
                var contact = _contactsService.GetContactByEmail(Input.Email);
                await _smsService.SendEmail(new MessageViewModel
                {
                    CreatedBy = Input.Email,
                    Ids = new List<int>() { contact.Id },
                    Message = message,
                    Subject = "Reset Password",
                    Emails = new List<string>() { }
                });

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
