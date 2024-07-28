using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Areas.Identity.Data;
using LearningManagementSystem.Services.ControlPanel;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Services.Helpers;
using System.Web.Mvc;
using PhoneNumbers;
using Microsoft.Extensions.Localization;

namespace LearningManagementSystem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStringLocalizer<IndexModel> _localizer;
        private readonly IUserProfileService _userProfileService;

        public IndexModel(IUserProfileService userProfileService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IStringLocalizer<IndexModel> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
            _userProfileService = userProfileService;

        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string Username { get; set; }
            public string PhoneNumber { get; set; }
            public int ContactId { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string IdNumber { get; set; }
            public DateTime CreatedOn { get; set; }
            public int Status { get; set; }
            public DateTime? DeletedOn { get; set; }
            public DateTime? LastLogin { get; set; }
            public DateTime? BirthDate { get; set; }
            public string ProfilePhoto { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string ThirdName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Signature { get; set; }
            public int? PreferedLanguageId { get; set; }
            public int? CountryId { get; set; }
            public int? CityId { get; set; }
            public int? GenderId { get; set; }
            public int EducationalLevelId { get; set; }
            public int StudentId { get; set; }
            public string PhoneNumberCode { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;
            var profile = _userProfileService.GetUserByUsername(user.UserName);
            Input = new InputModel
            {
                Mobile = profile.Mobile,
                IdNumber = profile.IdNumber,
                ContactId = profile.ContactId,
                ProfilePhoto = profile.ProfilePhoto,
                FirstName = profile.FirstName,
                SecondName = profile.SecondName,
                ThirdName = profile.ThirdName,
                LastName = profile.LastName,
                Address = profile.Address,
                Username = userName,
                PreferedLanguageId = profile.PreferedLanguageId,
                CountryId = profile.CountryId,
                CityId = profile.CityId,
                GenderId = profile.GenderId,
                BirthDate = profile.BirthDate,
                EducationalLevelId = profile.EducationalLevelId,
                PhoneNumber = profile.PhoneNumber,
                StudentId = profile.StudentId,
                Signature=profile.Signature,
                PhoneNumberCode = profile.PhoneNumberCode,
            };
        }

        public async Task<IActionResult> OnGetAsync(bool? success)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewData["LangId"] = langId;
            ViewData["Success"] = success.ToString()??"null";

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            PhoneNumber number = PhoneNumberUtil.GetInstance().Parse(Input.PhoneNumberCode + Input.PhoneNumber, null);
            bool isValid = PhoneNumberUtil.GetInstance().IsValidNumber(number);

            if (isValid == false)
            {
                StatusMessage = _localizer["Invalid Phone Number"].Value;
                return RedirectToPage();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = _localizer["Unexpected error when trying to set phone number."].Value;
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _localizer["Your profile has been updated"].Value;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);


            var profile = _userProfileService.GetUserProfileByUsername(user.UserName);
            _userProfileService.UpdateUserProfile(new EditProfileViewModel()
            {
                ContactId = Input.ContactId,
                Mobile = Input.Mobile,
                PhoneNumberCode = Input.PhoneNumberCode,
                IdNumber = Input.IdNumber,
                ProfilePhoto = Input.ProfilePhoto,
                FullName = Input.FirstName,
                Address = Input.Address,
                FirstName = Input.FirstName,
                SecondName = Input.SecondName,
                ThirdName = Input.ThirdName,
                LastName = Input.LastName,
                PreferedLanguageId = Input.PreferedLanguageId,
                CountryId = Input.CountryId,
                CityId = Input.CityId,
                GenderId = Input.GenderId,
                BirthDate = Input.BirthDate,
                EducationalLevelId = Input.EducationalLevelId,
                PhoneNumber = Input.PhoneNumber,
                LanguageId= langId,
                Signature = Input.Signature,
            });

            return RedirectToPage();
        }
    }
}
