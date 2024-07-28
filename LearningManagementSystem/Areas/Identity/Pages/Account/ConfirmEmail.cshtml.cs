using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IContactsService _contactsService;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, IContactsService contactsService)
        {
            _userManager = userManager;
            _contactsService = contactsService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                var resultConfirm = _contactsService.ConfirmEmail(user.Email);
                if (resultConfirm == false)
                {
                    LogHelper.LogException(User.Identity?.Name ?? string.Empty, null, $"Error While User Confirm Email for {user.Email}");

                }
            }
            else
                _contactsService.ConfirmEmail(user.Email);

            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
