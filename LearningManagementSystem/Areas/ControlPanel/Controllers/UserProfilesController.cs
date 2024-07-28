using System;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class UserProfilesController : Controller
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ILogService _logService;
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IEmployeeService _employeeService;
        private readonly IContactsService _contactsService;
        private readonly ISmsService _smsService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IStudentService _studentService;
        private readonly ITrainerService _trainerService;


        public UserProfilesController(ISettingService settingService, ICookieService cookieService, LearningManagementSystemContext context,
            ILogService logService,
            IStudentService studentService,
            ITrainerService trainerService,
            IUserProfileService userProfileService, ICompositeViewEngine viewEngine,
            UserManager<IdentityUser> userManager, IEmailService emailService, IEmployeeService employeeService, IContactsService contactsService,
            ISmsService smsService)
        {
            _viewEngine = viewEngine;
            _studentService = studentService;
            _trainerService = trainerService;
            _context = context;
            _logService = logService;
            _userProfileService = userProfileService;
            _userManager = userManager;
            _emailService = emailService;
            _employeeService = employeeService;
            _contactsService = contactsService;
            _smsService = smsService;
            _cookieService = cookieService;
            _settingService = settingService;
        }


        // GET: ControlPanel/UserProfiles        
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "User List")]
        public IActionResult Index(int? page, FilterViewModel filter, int pagination, int? contactTypeId, int? companyId, int? VerifyEmailId, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                ViewBag.searchText = filter.SearchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.UserPrifilePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.UserPrifilePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "FullName", "Email", "Mobile", "IsEmailVerified", "Status", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.UserProfileTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.UserProfileTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.UserProfileTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = languageId;



            ViewBag.CompanyId = companyId;
            ViewBag.ContactTypeId = contactTypeId;
            ViewBag.VerifyEmailId = VerifyEmailId;

            ViewBag.ContactType = filter.ContactType;



            if (AuthenticationHelper.CheckAuthentication("Students", "ViewAll", User.Identity.Name))
            {
                var result = _contactsService.GetContacts(filter, page, languageId, pagination, contactTypeId, companyId, null, VerifyEmailId);
                return View(result);
            }
            else
            {
                var result = _contactsService.GetContacts(filter, page, languageId, pagination, contactTypeId, companyId, LookupHelper.GetEmployee(_userProfileService.GetUserProfileByUsername(User.Identity.Name)?.ContactId)?.Id ?? 0, VerifyEmailId);
                return View(result);
            }
        }

        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/UserProfiles        
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "User List")]
        public IActionResult GetContacts(int? page, FilterViewModel filter, int pagination, int? contactTypeId, int? companyId, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                ViewBag.searchText = filter.SearchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.UserPrifilePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.UserPrifilePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;


            List<string> tables = new List<string> { "FullName", "Email", "Mobile", "Status", "CreatedOn" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.UserProfileTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.UserProfileTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.UserProfileTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = languageId;



            ViewBag.CompanyId = companyId;
            ViewBag.ContactTypeId = contactTypeId;
            ViewBag.Job = filter.Job;
            ViewBag.ContactType = filter.ContactType;
            ViewBag.JobType = filter.JobType;

            if (AuthenticationHelper.CheckAuthentication("Visitors", "ViewAll", User.Identity.Name))
            {
                var result = _contactsService.GetContacts(filter, page, languageId, pagination, contactTypeId, companyId, null, null);
                return PartialView("_Index", result);
            }
            else
            {
                var result = _contactsService.GetContacts(filter, page, languageId, pagination, contactTypeId, companyId, LookupHelper.GetEmployee(_userProfileService.GetUserProfileByUsername(User.Identity.Name)?.ContactId).Id, null);
                return PartialView("_Index", result);
            }
        }

        // GET: ControlPanel/UserProfiles/Details/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "User Details")]
        public async Task<IActionResult> Details(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }

            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var userRoles = _userProfileService.GetUserRoles(userProfile.Id).ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();
                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return View(generalContactViewModel);
        }

        // GET: ControlPanel/UserProfiles/Details/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "User Details")]
        public async Task<IActionResult> ShowDetails(int? id, int? contactTypeId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ContactTypeId = contactTypeId;

            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }


            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var rols = _userProfileService.GetUserRoles(userProfile.Id)?.ToList();
                var userRoles = rols?.Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();
                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles?.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return PartialView("_Details", generalContactViewModel);
        }

        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "ResetPassword")]
        [AuditLogFilter(ActionDescription = "Users Reset Password")]
        public async Task<IActionResult> ResetPassword(int id, string password, int page, string searchText)
        {
            try
            {
                var profile = _userProfileService.GetUserProfileById(id);
                if (profile != null && profile.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    var aspUser = _userManager.Users.FirstOrDefault(r => r.UserName == profile.Contact.Email);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(aspUser);
                    //var newPassword = GeneratePassword();
                    await _userManager.ResetPasswordAsync(aspUser, token, password);
                    //TempData["ResetPasswordResult"] = $"تم تغيير كلمة المرور. الكلمة الجديدة هي {newPassword}";
                    //return RedirectToAction("Index", "UserProfiles", new { page = page, searchText = searchText, area = "ControlPanel" });
                    return Json(new { success = true });
                }

                return NotFound();
            }

            catch (Exception ex)
            {
                TempData["ResetPasswordResult"] = "حدث خطأ اثناء اعادة تعيين كلمة المرور";
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, $"Error While reset password for {id} profile");
                return null;
            }
        }
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "VerifyEmail")]
        [AuditLogFilter(ActionDescription = "Users Verify Email")]
        public async Task<IActionResult> ShowVerifyEmail(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var profile = _userProfileService.GetUserProfileById(id.Value);
                if (profile == null || profile.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    return NotFound();
                }

                ViewBag.ID = id.Value;
                return PartialView("_VerifyEmail");
            }

            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, $"Error While Users Verify Email for {id} profile");
                return null;
            }
        }
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "VerifyEmail")]
        [AuditLogFilter(ActionDescription = "Users Verify Email")]
        public async Task<IActionResult> ConfirmVerifyEmail(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var profile = _userProfileService.GetUserProfileById(id.Value);
                if (profile == null || profile.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    return NotFound();
                }


                var aspUser = _userManager.Users.FirstOrDefault(r => r.UserName == profile.Username);
                if (!(await _userManager.IsEmailConfirmedAsync(aspUser)))
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(aspUser);
                    var result = await _userManager.ConfirmEmailAsync(aspUser, code);

                    if (result.Succeeded)
                    {
                        var resultConfirm = _contactsService.ConfirmEmail(profile.Username);
                        if (resultConfirm == false)
                        {
                            LogHelper.LogException(User.Identity?.Name ?? string.Empty, null, $"Error While User Confirm Email for {profile.Username}");

                        }
                    }
                }
                else 
                    _contactsService.ConfirmEmail(profile.Username);


                return Json("Success");
            }

            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, $"Error While Users Verify Email for {id} profile");
                return null;
            }
        }
        private string GeneratePassword()
        {
            Random rnd = new Random();
            var passwordGenerated = rnd.Next(
                ).ToString() + "P@s" + rnd.Next(100).ToString() + "sW0r" + rnd.Next(6000).ToString();
            return passwordGenerated;
        }

        // GET: ControlPanel/UserProfiles/Create
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Users Create Get")]
        public IActionResult Create(int? contactTypeId)
        {
            var UserName = User.Identity?.Name;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            ViewBag.ContactTypeId = contactTypeId;
            ViewBag.LangId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.RoleId = _userProfileService.GetUserRole(UserName);

            return View();
        }

        // GET: ControlPanel/UserProfiles/Create
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Users Create Get")]
        public IActionResult ShowCreate(int? contactTypeId, int? companyId)
        {
            var UserName = User.Identity?.Name;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            ViewBag.ContactTypeId = contactTypeId;
            ViewBag.LangId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.RoleId = _userProfileService.GetUserRole(UserName);
            ViewBag.CompanyId = companyId;



            return PartialView("_Create");
        }

        // POST: ControlPanel/UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Users Create Post")]
        public async Task<IActionResult> Create(GeneralContactViewModel generalContactViewModel, List<int> contactType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (generalContactViewModel.ContactViewModel.LanguageId == 0)
                    {
                        generalContactViewModel.ContactViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    }

                    ViewBag.LangId = generalContactViewModel.ContactViewModel.LanguageId;
                    var userName = User.Identity?.Name;
                    ViewBag.RoleId = _userProfileService.GetUserRole(userName);

                    var contact = _contactsService.AddContacts(generalContactViewModel.ContactViewModel, contactType);
                    var status = generalContactViewModel.ContactViewModel.Status;
                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                    {
                        generalContactViewModel.EmployeeViewModel.Status = status;
                        generalContactViewModel.EmployeeViewModel.ContactId = contact.Id;
                        generalContactViewModel.EmployeeViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.EmployeeViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                        _employeeService.AddEmployee(generalContactViewModel.EmployeeViewModel);
                    }

                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                    {
                        generalContactViewModel.StudentViewModel.Status = status;
                        generalContactViewModel.StudentViewModel.CreatedOn = DateTime.Now;
                        generalContactViewModel.StudentViewModel.Email = generalContactViewModel.ContactViewModel.Email;
                        generalContactViewModel.StudentViewModel.ContactId = contact.Id;
                        generalContactViewModel.StudentViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.StudentViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                        _studentService.AddStudent(generalContactViewModel.StudentViewModel);
                    }

                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                    {
                        generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                        generalContactViewModel.TrainerViewModel.Status = status;
                        generalContactViewModel.TrainerViewModel.CreatedOn = DateTime.Now;
                        generalContactViewModel.TrainerViewModel.ContactId = contact.Id;
                        generalContactViewModel.TrainerViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.TrainerViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                        _trainerService.AddTrainer(generalContactViewModel.TrainerViewModel);
                    }


                    if (generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser)
                    {
                        var userProfileViewModel = generalContactViewModel.UserProfileViewModel;
                        userProfileViewModel.Email = contact.Email;
                        userProfileViewModel.FullName = contact.FullName;
                        userProfileViewModel.PreferedLanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        userProfileViewModel.Username = contact.FirstName;
                        userProfileViewModel.ContactId = contact.Id;
                        userProfileViewModel.Status = status;

                        var account = _userProfileService.GetUserProfileByUsername(userProfileViewModel.Username);
                        if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
                        {
                            string msg = "Account deactivated from the admin";
                            ModelState.AddModelError("", msg);
                        }

                        if (account != null)
                        {
                            string msg = "Account already exist";
                            ModelState.AddModelError("", msg);
                        }
                        var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString() == "on");

                        var user = new IdentityUser() { UserName = userProfileViewModel.Email, Email = userProfileViewModel.Email, EmailConfirmed = confirm };
                        var result = await _userManager.CreateAsync(user, userProfileViewModel.Password);
                        if (result.Succeeded)
                        {

                            EntityHelper.AddProfile(userProfileViewModel, _context);


                            #region send email
                            try
                            {
                                if (!confirm)
                                    await SendEmail(user, contact.Id);
                            }
                            catch (Exception ex)
                            {
                                return RedirectToAction(nameof(Index));

                            }
                            #endregion

                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while adding user profile");
                    return View(generalContactViewModel);
                }
            }

            return View(generalContactViewModel);
        }

        // POST: ControlPanel/UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Users Create Post")]
        public async Task<IActionResult> CreateContact(GeneralContactViewModel generalContactViewModel, List<int> contactType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (generalContactViewModel.ContactViewModel.LanguageId == 0)
                        generalContactViewModel.ContactViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();


                    ViewBag.LangId = generalContactViewModel.ContactViewModel.LanguageId;
                    var userName = User.Identity?.Name;
                    ViewBag.RoleId = _userProfileService.GetUserRole(userName);

                    var valid = _contactsService.CheckContactRepetition(ModelState, generalContactViewModel.ContactViewModel.Mobile, generalContactViewModel.ContactViewModel.Id, generalContactViewModel.ContactViewModel.IdNumber);

                    if (contactType.Count == 1)
                    {
                        ViewBag.ContactTypeId = contactType[0];
                    }

                    if (valid != null && !Boolean.Parse(_settingService.GetOrCreate("Allow_To_Create_Account_Without_Checking", "true").Value))
                        return Json(new { success = false, message = valid });

                    var contact = _contactsService.AddContacts(generalContactViewModel.ContactViewModel, contactType);
                    var status = generalContactViewModel.ContactViewModel.Status;
                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                    {
                        generalContactViewModel.EmployeeViewModel.Status = status;
                        generalContactViewModel.EmployeeViewModel.ContactId = contact.Id;
                        generalContactViewModel.EmployeeViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.EmployeeViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                        _employeeService.AddEmployee(generalContactViewModel.EmployeeViewModel);
                    }

                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                    {
                        generalContactViewModel.StudentViewModel.Status = status;
                        generalContactViewModel.StudentViewModel.CreatedOn = DateTime.Now;
                        generalContactViewModel.StudentViewModel.Email = generalContactViewModel.ContactViewModel.Email;
                        generalContactViewModel.StudentViewModel.ContactId = contact.Id;
                        generalContactViewModel.StudentViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.StudentViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;
                        generalContactViewModel.StudentViewModel.Country = string.Empty;
                        generalContactViewModel.StudentViewModel.Work = string.Empty;

                        _studentService.AddStudent(generalContactViewModel.StudentViewModel);
                    }

                    if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                    {
                        generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                        generalContactViewModel.TrainerViewModel.Status = status;
                        generalContactViewModel.TrainerViewModel.CreatedOn = DateTime.Now;
                        generalContactViewModel.TrainerViewModel.ContactId = contact.Id;
                        generalContactViewModel.TrainerViewModel.LanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        generalContactViewModel.TrainerViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                        _trainerService.AddTrainer(generalContactViewModel.TrainerViewModel);
                    }

                    if (generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser)
                    {
                        var userProfileViewModel = generalContactViewModel.UserProfileViewModel;
                        userProfileViewModel.Email = contact.Email;
                        userProfileViewModel.FullName = contact.FullName;
                        userProfileViewModel.PreferedLanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                        userProfileViewModel.Username = contact.FirstName;
                        userProfileViewModel.ContactId = contact.Id;
                        userProfileViewModel.Status = status;

                        var account = _userProfileService.GetUserProfileByUsername(userProfileViewModel.Username);
                        if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
                        {
                            string msg = "Account deactivated from the admin";
                            ModelState.AddModelError("", msg);
                        }

                        if (account != null)
                        {
                            string msg = "Account already exist";
                            ModelState.AddModelError("", msg);
                        }
                        var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString() == "on");

                        var user = new IdentityUser() { UserName = userProfileViewModel.Email, Email = userProfileViewModel.Email, EmailConfirmed = confirm };
                        var result = await _userManager.CreateAsync(user, userProfileViewModel.Password);
                        if (result.Succeeded)
                        {

                            EntityHelper.AddProfile(userProfileViewModel, _context);

                            #region send email
                            try
                            {
                                if (!confirm)
                                    await SendEmail(user, contact.Id);
                            }
                            catch (Exception ex)
                            {
                                return null;
                            }
                            #endregion
                            return Json(new { success = true, message = valid });
                        }
                    }
                    return Json(new { success = true, message = valid });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while adding user profile");
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });
        }

        // GET: ControlPanel/UserProfiles/Edit/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Users Edit Get")]
        public async Task<IActionResult> Edit(int? id, int? contactTypeId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ContactTypeId = contactTypeId;

            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }

            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var userRoles = _userProfileService.GetUserRoles(userProfile.Id).ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();

                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return View(generalContactViewModel);
        }


        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Users Edit Get")]
        public async Task<IActionResult> ShowEdit(int? id, int? contactTypeId, int? companyId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ContactTypeId = contactTypeId;

            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                    ViewBag.CompanyId = companyId;
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }

            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.FirstOrDefault();
                var rols = _userProfileService.GetUserRoles(userProfile.Id)?.ToList();
                var userRoles = rols?.Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();

                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles?.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);



            return PartialView("_Edit", generalContactViewModel);
        }

        //POST: ControlPanel/UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Users Edit Post")]
        public async Task<IActionResult> Edit(int id, GeneralContactViewModel generalContactViewModel, List<int> contactType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int languageId = generalContactViewModel.ContactViewModel.LanguageId;

                    var contactViewModel = new ContactViewModel(generalContactViewModel.ContactViewModel);
                    Contact contact = _contactsService.GetContactById(id);
                    var status = generalContactViewModel.ContactViewModel.Status;

                    if (languageId == 0)
                        contactViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();

                    var oldContractType = contact.ContactTypes.Select(c => c.TypeId);

                    contactViewModel.LanguageId = languageId;
                    ViewBag.LangId = languageId;

                    ViewBag.RoleId = _userProfileService.GetUserRole(User.Identity?.Name);

                    if (contact != null && contact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        _contactsService.EditContact(contactViewModel, contact, contactType, languageId);

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                        {
                            var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (employee != null)
                            {
                                generalContactViewModel.EmployeeViewModel.Status = status;
                                generalContactViewModel.EmployeeViewModel.Id = employee.Id;
                                generalContactViewModel.EmployeeViewModel.LanguageId = languageId;

                                _employeeService.EditEmployee(generalContactViewModel.EmployeeViewModel, languageId);
                            }
                            else
                            {
                                generalContactViewModel.EmployeeViewModel.Status = status;
                                generalContactViewModel.EmployeeViewModel.ContactId = contact.Id;
                                generalContactViewModel.EmployeeViewModel.LanguageId = languageId;
                                generalContactViewModel.EmployeeViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                                _employeeService.AddEmployee(generalContactViewModel.EmployeeViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                        {
                            var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (employee != null)
                            {
                                _employeeService.DeleteEmployee(employee);
                            }
                        }

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                        {
                            var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (student != null)
                            {
                                generalContactViewModel.StudentViewModel.Status = status;
                                generalContactViewModel.StudentViewModel.Email = contactViewModel.Email;
                                generalContactViewModel.StudentViewModel.LanguageId = languageId;

                                _studentService.EditStudent(generalContactViewModel.StudentViewModel, student);
                            }
                            else
                            {
                                generalContactViewModel.StudentViewModel.Status = status;
                                generalContactViewModel.StudentViewModel.Email = contactViewModel.Email;
                                generalContactViewModel.StudentViewModel.CreatedOn = DateTime.Now;
                                generalContactViewModel.StudentViewModel.ContactId = contact.Id;
                                generalContactViewModel.StudentViewModel.LanguageId = languageId;
                                generalContactViewModel.StudentViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                                _studentService.AddStudent(generalContactViewModel.StudentViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                        {
                            var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (student != null)
                            {
                                _studentService.DeleteStudent(student.Id);
                            }
                        }

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                        {
                            var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (traner != null)
                            {
                                generalContactViewModel.TrainerViewModel.Status = status;
                                generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                                generalContactViewModel.TrainerViewModel.LanguageId = languageId;
                                _trainerService.EditTrainer(generalContactViewModel.TrainerViewModel, traner);
                            }
                            else
                            {
                                generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                                generalContactViewModel.TrainerViewModel.Status = status;
                                generalContactViewModel.TrainerViewModel.CreatedOn = DateTime.Now;
                                generalContactViewModel.TrainerViewModel.ContactId = contact.Id;
                                generalContactViewModel.TrainerViewModel.LanguageId = languageId;

                                _trainerService.AddTrainer(generalContactViewModel.TrainerViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                        {
                            var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (traner != null)
                            {
                                _trainerService.DeleteTrainer(traner);
                            }
                        }


                        if (contact.UserProfiles.Any())
                        {
                            generalContactViewModel.UserProfileViewModel.Id = contact.UserProfiles.First().Id;

                            _userProfileService.EditUserProfile(generalContactViewModel.UserProfileViewModel);
                        }
                        else if (generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser && !contact.UserProfiles.Any())
                        {
                            var userProfileViewModel = generalContactViewModel.UserProfileViewModel;
                            userProfileViewModel.Email = contact.Email;
                            userProfileViewModel.FullName = contact.FullName;
                            userProfileViewModel.PreferedLanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                            userProfileViewModel.Username = contact.FirstName;
                            userProfileViewModel.ContactId = contact.Id;
                            userProfileViewModel.Status = status;

                            var account = _userProfileService.GetUserProfileByUsername(userProfileViewModel.Username);
                            if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
                            {
                                string msg = "Account deactivated from the admin";
                                ModelState.AddModelError("", msg);
                            }

                            if (account != null)
                            {
                                string msg = "Account already exist";
                                ModelState.AddModelError("", msg);
                            }

                            var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString() == "on");

                            var user = new IdentityUser() { UserName = userProfileViewModel.Email, Email = userProfileViewModel.Email, EmailConfirmed = confirm };
                            var result = await _userManager.CreateAsync(user, userProfileViewModel.Password);
                            if (result.Succeeded)
                            {
                                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                var confirmationLink = Url.Action("ConfirmEmail", "Authentication", new { Area = "", userId = user.Id, token = token }, Request.Scheme);

                                EntityHelper.AddProfile(userProfileViewModel, _context);

                                #region send email
                                try
                                {
                                    if (!confirm)
                                        await SendEmail(user, contact.Id);
                                }
                                catch (Exception ex)
                                {
                                    return RedirectToAction(nameof(Index));

                                }
                                #endregion


                            }
                            else
                            {
                                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.NotUser;
                                string msg = string.Join(",", result.Errors.Select(e => e.Description));
                                ModelState.AddModelError("", msg);
                                return View(generalContactViewModel);
                            }
                        }

                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logService.LogException(User.Identity.Name, ex, "Error While Editing Employee (Post)");
                    return View(generalContactViewModel);
                }
            }

            var UserName = User.Identity?.Name;
            ViewBag.LangId = CultureHelper.GetDefaultLanguageId();
            ViewBag.RoleId = _userProfileService.GetUserRole(UserName);

            return View(generalContactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Users Edit Post")]
        public async Task<IActionResult> EditContact(int id, GeneralContactViewModel generalContactViewModel, List<int> contactType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int languageId = generalContactViewModel.ContactViewModel.LanguageId;

                    var contactViewModel = new ContactViewModel(generalContactViewModel.ContactViewModel);
                    Contact contact = _contactsService.GetContactById(id);
                    var status = generalContactViewModel.ContactViewModel.Status;

                    if (languageId == 0)
                        contactViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();


                    var oldContractType = contact.ContactTypes.Select(c => c.TypeId).ToList();

                    contactViewModel.LanguageId = languageId;
                    ViewBag.LangId = languageId;

                    ViewBag.RoleId = _userProfileService.GetUserRole(User.Identity?.Name);

                    ViewBag.ContactType = contactType;

                    var valid = _contactsService.CheckContactRepetition(ModelState, generalContactViewModel.ContactViewModel.Mobile, contact.Id, generalContactViewModel.ContactViewModel.IdNumber);

                    if (valid != null && !Boolean.Parse(_settingService.GetOrCreate("Allow_To_Create_Account_Without_Checking", "true").Value))
                        return Json(new { success = false, message = valid });

                    if (contact != null && contact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        await _contactsService.EditContact(contactViewModel, contact, contactType, languageId);

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                        {
                            var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (employee != null)
                            {
                                generalContactViewModel.EmployeeViewModel.Status = status;
                                generalContactViewModel.EmployeeViewModel.Id = employee.Id;
                                generalContactViewModel.EmployeeViewModel.LanguageId = languageId;

                                _employeeService.EditEmployee(generalContactViewModel.EmployeeViewModel, languageId);
                            }
                            else
                            {
                                generalContactViewModel.EmployeeViewModel.Status = status;
                                generalContactViewModel.EmployeeViewModel.ContactId = contact.Id;
                                generalContactViewModel.EmployeeViewModel.LanguageId = languageId;
                                generalContactViewModel.EmployeeViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                                _employeeService.AddEmployee(generalContactViewModel.EmployeeViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                        {
                            var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (employee != null)
                            {
                                _employeeService.DeleteEmployee(employee);
                            }
                        }

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                        {
                            var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (student != null)
                            {
                                generalContactViewModel.StudentViewModel.Status = status;
                                generalContactViewModel.StudentViewModel.Email = contactViewModel.Email;
                                generalContactViewModel.StudentViewModel.LanguageId = languageId;

                                _studentService.EditStudent(generalContactViewModel.StudentViewModel, student);
                            }
                            else
                            {
                                generalContactViewModel.StudentViewModel.Status = status;
                                generalContactViewModel.StudentViewModel.Email = contactViewModel.Email;
                                generalContactViewModel.StudentViewModel.CreatedOn = DateTime.Now;
                                generalContactViewModel.StudentViewModel.ContactId = contact.Id;
                                generalContactViewModel.StudentViewModel.LanguageId = languageId;
                                generalContactViewModel.StudentViewModel.CreatedBy = User.Identity?.Name ?? string.Empty;

                                _studentService.AddStudent(generalContactViewModel.StudentViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                        {
                            var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (student != null)
                            {
                                _studentService.DeleteStudent(student.Id);
                            }
                        }

                        if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                        {
                            var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (traner != null)
                            {
                                generalContactViewModel.TrainerViewModel.Status = status;
                                generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                                generalContactViewModel.TrainerViewModel.LanguageId = languageId;
                                generalContactViewModel.TrainerViewModel.Id = traner.Id;
                                _trainerService.EditTrainer(generalContactViewModel.TrainerViewModel, traner);
                            }
                            else
                            {
                                generalContactViewModel.TrainerViewModel.IsUser = generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser;
                                generalContactViewModel.TrainerViewModel.Status = status;
                                generalContactViewModel.TrainerViewModel.CreatedOn = DateTime.Now;
                                generalContactViewModel.TrainerViewModel.ContactId = contact.Id;
                                generalContactViewModel.TrainerViewModel.LanguageId = languageId;

                                _trainerService.AddTrainer(generalContactViewModel.TrainerViewModel);
                            }
                        }
                        else if (oldContractType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                        {
                            var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                            if (traner != null)
                            {
                                _trainerService.DeleteTrainer(traner);
                            }
                        }

                        if (contact.UserProfiles.Any())
                        {
                            generalContactViewModel.UserProfileViewModel.Id = contact.UserProfiles.FirstOrDefault().Id;
                            if (generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.NotUser)
                            {
                                var user = await _userManager.FindByNameAsync(generalContactViewModel.ContactViewModel.Email);
                                if (user != null)
                                {
                                    _userProfileService.DeleteUserAndRoles(user.UserName);
                                    _userProfileService.RemoveUserProfileById(generalContactViewModel.UserProfileViewModel.Id);
                                }
                            }
                            else 
                                _userProfileService.EditUserProfile(generalContactViewModel.UserProfileViewModel);
                        }
                        else if (generalContactViewModel.IsUser == (int)GeneralEnums.UserEnum.IsUser && !contact.UserProfiles.Any())
                        {
                            var userProfileViewModel = generalContactViewModel.UserProfileViewModel;
                            userProfileViewModel.Email = contact.Email;
                            userProfileViewModel.FullName = contact.FullName;
                            userProfileViewModel.PreferedLanguageId = generalContactViewModel.ContactViewModel.LanguageId;
                            userProfileViewModel.Username = contact.FirstName;
                            userProfileViewModel.ContactId = contact.Id;
                            userProfileViewModel.Status = status;

                            var account = _userProfileService.GetUserProfileByUsername(userProfileViewModel.Username);
                            if (account != null && account.Status == (int)GeneralEnums.StatusEnum.Deactive)
                            {
                                string msg = "Account deactivated from the admin";
                                ModelState.AddModelError("", msg);
                            }

                            if (account != null)
                            {
                                string msg = "Account already exist";
                                ModelState.AddModelError("", msg);
                            }

                            var confirm = Convert.ToBoolean(SettingHelper.GetOrCreate("EmailConfirmation", "true").Value.ToString() == "on");

                            var user = new IdentityUser() { UserName = userProfileViewModel.Email, Email = userProfileViewModel.Email, EmailConfirmed = confirm };
                            var result = await _userManager.CreateAsync(user, userProfileViewModel.Password);
                            if (result.Succeeded)
                            {

                                EntityHelper.AddProfile(userProfileViewModel, _context);

                                #region send email
                                try
                                {
                                    if (!confirm)
                                        await SendEmail(user, contact.Id);
                                }
                                catch (Exception ex)
                                {
                                    return Json(new { success = false });

                                }
                                #endregion

                                return Json(new { success = true, message = valid });

                            }
                            else
                            {
                                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.NotUser;
                                string msg = string.Join(",", result.Errors.Select(e => e.Description));
                                ModelState.AddModelError("", msg);
                                return Json(new { success = false, message = msg });
                            }
                        }
                        return Json(new { success = true, message = valid });
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logService.LogException(User.Identity.Name, ex, "Error While Editing Employee (Post)");
                    return Json(new { success = false });
                }
            }

            return Json(new { success = false });
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIfUserExist(string email, int? id)
        {
            var account = _contactsService.GetContactByEmail(email);
            var contact = _contactsService.GetContactById(id ?? 0);
            if (contact != null && contact?.Email == email)
                return Json(new { exist = false });
            else if (account != null && email != null)
                return Json(new { exist = true });
            return Json(new { exist = false });
        }

        // GET: ControlPanel/UserProfiles/Delete/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Users Delete Get")]
        public async Task<IActionResult> Delete(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }

            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var userRoles = _userProfileService.GetUserRoles(userProfile.Id).ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();
                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return View(generalContactViewModel);
        }

        // GET: ControlPanel/UserProfiles/Delete/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Users Delete Get")]
        public async Task<IActionResult> ShowDelete(int? id, int? contactTypeId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ContactTypeId = contactTypeId;
            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }


            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var userRoles = _userProfileService.GetUserRoles(userProfile.Id).ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();
                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return PartialView("_Delete", generalContactViewModel);
        }


        // GET: ControlPanel/UserProfiles/Delete/5
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "DeleteNotVerifyEmail")]
        [AuditLogFilter(ActionDescription = "Users Delete Get")]
        public async Task<IActionResult> ShowDeleteNotVerifyEmail(int? id, int? contactTypeId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ContactTypeId = contactTypeId;
            var userName = User.Identity?.Name;

            var contact = _contactsService.GetContactById(id.Value);

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted || contact.IsEmailVerified.Value)
            {
                return NotFound();
            }

            var contactViewModel = _contactsService.GetContact(id.Value, languageId);
            ViewBag.LangId = languageId;

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            generalContactViewModel.ContactViewModel = contactViewModel;
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    generalContactViewModel.EmployeeViewModel = _employeeService.GetEmployeeViewModelById(employee.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    generalContactViewModel.StudentViewModel = _studentService.GetStudentById(student.Id, languageId);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    generalContactViewModel.TrainerViewModel = _trainerService.GetTrainerById(traner.Id, languageId);
                }
            }


            if (contact.UserProfiles.Any())
            {
                var userProfile = contact.UserProfiles.First();
                var userRoles = _userProfileService.GetUserRoles(userProfile.Id).ToList().Select(r => new RoleViewModel
                {
                    RoleId = r.RoleId,
                    Name = r.Role?.Name
                }).ToList();
                generalContactViewModel.IsUser = (int)GeneralEnums.UserEnum.IsUser;
                generalContactViewModel.UserProfileViewModel = new UserProfileViewModel(userProfile);
                generalContactViewModel.UserProfileViewModel.Roles = userRoles;
                generalContactViewModel.UserProfileViewModel.RoleIds = userRoles.Select(a => a.RoleId).ToList();
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            return PartialView("_DeleteNotVerifyEmail", generalContactViewModel);
        }




        [AuditLogFilter(ActionDescription = "Delete Contract")]
        public ActionResult DeleteContractNotVerifyEmail(int id)
        {
            try
            {
                Contact contact = _contactsService.GetContactById(id);

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                ViewBag.LangId = languageId;

                var userName = User.Identity?.Name;

                if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted || contact.IsEmailVerified.Value)
                {
                    return NotFound();
                }

                _contactsService.DeleteContact(contact);

                var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
                var generalContactViewModel = new GeneralContactViewModel();
                ViewBag.ContactType = contactType;

                if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
                {
                    var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (employee != null)
                    {
                        _employeeService.DeleteEmployee(employee);
                    }
                }

                if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
                {
                    var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (student != null)
                    {
                        _studentService.DeleteStudent(student.Id);
                    }
                }

                if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
                {
                    var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (traner != null)
                    {
                        _trainerService.DeleteTrainer(traner);
                    }
                }


                ViewBag.LangId = languageId;

                if (contact.UserProfiles.Any())
                {
                    var userProfile = _context.UserProfiles.Find(contact.UserProfiles.First().Id);
                    userProfile.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    _context.Update(userProfile);
                    _context.SaveChanges();

                    try
                    {
                        var profile = _userProfileService.GetUserProfileById(id);
                        if (profile != null && profile.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            _userProfileService.DeleteUserProfileById(profile);



                        }

                        _userProfileService.DeleteUserAndRoles(contact.Email);


                        return RedirectToAction(nameof(Index));
                    }

                    catch (Exception ex)
                    {
                        LogHelper.LogException(User.Identity.Name, ex, "Error While deleting profile");
                        return RedirectToAction(nameof(Index));
                    }
                }


                return Json("Success");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While student");
                return null;
            }
        }




        // POST: ControlPanel/UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "UserProfiles", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Users Delete Post")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Contact contact = _contactsService.GetContactById(id);

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var userName = User.Identity?.Name;

            if (contact == null || contact.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            _contactsService.DeleteContact(contact);

            var contactType = contact.ContactTypes.Select(c => c.TypeId).ToList();
            var generalContactViewModel = new GeneralContactViewModel();
            ViewBag.ContactType = contactType;

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Employee))
            {
                var employee = contact.Employees.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (employee != null)
                {
                    _employeeService.DeleteEmployee(employee);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Student))
            {
                var student = contact.Students.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (student != null)
                {
                    _studentService.DeleteStudent(student.Id);
                }
            }

            if (contactType.Contains((int)GeneralEnums.ContactTypeEnum.Trainer))
            {
                var traner = contact.Trainers.FirstOrDefault(c => c.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (traner != null)
                {
                    _trainerService.DeleteTrainer(traner);
                }
            }

            ViewBag.LangId = languageId;
            ViewBag.RoleId = _userProfileService.GetUserRole(userName);

            if (contact.UserProfiles.Any())
            {
                try
                {
                    var profile = _userProfileService.GetUserProfileById(contact.UserProfiles.FirstOrDefault().Id);
                    if (profile != null && profile.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        _userProfileService.DeleteUserProfileById(profile);
                    _userProfileService.DeleteUserAndRoles(contact.Email);

                    return Json(new { success = true });
                }

                catch (Exception ex)
                {
                    LogHelper.LogException(User.Identity.Name, ex, "Error While deleting profile");
                    return Json(new { success = false });
                }
            }

            return Json(new { success = true });
        }


        public IActionResult ReactivateAccount(int id)
        {
            try
            {
                _userProfileService.ReactivateAccount(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogHelper.LogException(User.Identity.Name, ex, "Error While Reactivate Account");
                return null;
            }
        }

        private async Task SendEmail(IdentityUser user, int contactId)
        {

            #region send email
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);


            await _smsService.SendEmail(new MessageViewModel
            {
                CreatedBy = user.Email,
                Ids = new List<int>() { contactId },
                Message =
               $"<h6>أهلا وسهلا بكم في أكاديمية الصفا للخدمات القرآنية </h6> نرجو تفعيل البريد الخاص بكم <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>بالضغط هنا</a>.",
                Subject = "Confirm your email",
                Emails = new List<string>() { }
            });
            #endregion
        }
    }
}
