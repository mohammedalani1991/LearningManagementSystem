using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Data;
using LearningManagementSystem.Services.Controllers;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Controllers
{
    public class SenangPayController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        private ISenangPayService _senangPayService;
        private IUserProfileService _userProfileService;
        private ILogService _logService;
        private IEnrollStudentCourseService _enrollStudentCourseService;
        private ISettingService _settingService;
        private IStudentService _StudentService;
        private readonly ISmsService _smsService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly ICmsProjectService _projectService;
        private readonly ICurrencyService _currencyService;
        private readonly ICookieService _cookieService;
        private readonly IBalanceHistoryService _balanceHistoryService;

        public SenangPayController(IStudentService StudentService, ISmsService smsService, ISettingService settingService
            , IEnrollStudentCourseService enrollStudentCourseService, ILogService logService, IUserProfileService userProfileService,
            ISenangPayService senangPayService, UserManager<IdentityUser> userMrg, SignInManager<IdentityUser> signinManager,
            IEnrollStudentExamService enrollStudentExamService, ICmsProjectService projectService, ICurrencyService currencyService, ICookieService cookieService
            , IBalanceHistoryService balanceHistoryService)
        {
            _userManager = userMrg;
            _signinManager = signinManager;
            _senangPayService = senangPayService;
            _userProfileService = userProfileService;
            _logService = logService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _settingService = settingService;
            _smsService = smsService;
            _StudentService = StudentService;
            _enrollStudentExamService = enrollStudentExamService;
            _projectService = projectService;
            _currencyService = currencyService;
            _cookieService = cookieService;
            _balanceHistoryService = balanceHistoryService;
        }

        public async Task<IActionResult> Create(SenangPayViewModel senangPayViewModel, bool donation = false)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cooke = _cookieService.GetCookie(CookieNames.SelectedCurrencyId);

                    var currancy = _currencyService.GetCurrencyById(Int32.Parse(cooke));
                    var exchangeCurrency = _currencyService.GetExchangeCurrency();

                    if (donation)
                    {
                        senangPayViewModel.SenangPayPaymentType = (int)GeneralEnums.SenangPayPaymentType.DonateForProjects;
                        senangPayViewModel.ProcessDate = DateTime.Now;
                        senangPayViewModel.FullName = senangPayViewModel.UserName;
                        senangPayViewModel.Country = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        senangPayViewModel.CreatedBy = senangPayViewModel.Email;
                        senangPayViewModel.CreatedOn = DateTime.Now;
                        senangPayViewModel.Status = (int)GeneralEnums.StatusEnum.NotPaid;
                        senangPayViewModel.CurrencyRate = decimal.Round((decimal)(currancy.Value / exchangeCurrency.Value), 2);
                        senangPayViewModel.CustomerCurrencyCode = currancy.Code;
                        senangPayViewModel.senangPayId = _senangPayService.AddSenangPay(senangPayViewModel)?.Id ?? 0;
                        var url = await _senangPayService.GetUrl(senangPayViewModel);
                        if (url != null)
                            return Json(new { url = url });
                        else
                            return Json(new { result = "Error While Create Senang Pay (Post)" });
                    }
                    else
                    {
                        var user = await _userManager.GetUserAsync(User);
                        senangPayViewModel.SenangPayPaymentType = (int)GeneralEnums.SenangPayPaymentType.BuyingCourse;
                        senangPayViewModel.ProcessDate = DateTime.Now;
                        senangPayViewModel.Email = user.Email;
                        senangPayViewModel.UserName = user.UserName;
                        senangPayViewModel.Country = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        senangPayViewModel.ApplicationUserId = _userManager.GetUserId(User);
                        senangPayViewModel.CreatedBy = User.Identity.Name;
                        senangPayViewModel.CreatedOn = DateTime.Now;
                        senangPayViewModel.EnrollTeacherCourseId = senangPayViewModel.EnrollTeacherCourseId;

                        var profile = _userProfileService.GetUserProfileByUsername(user.UserName);
                        var Student = _StudentService.GetStudentByContactId(profile.ContactId.Value);
                        if (Student != null && Student.Id > 0)
                        {
                            var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(senangPayViewModel.EnrollTeacherCourseId.Value, Student.Id);
                            if (CheckAbilityToEnrollStudentInCourse != "done")
                                return Json(new { result = CheckAbilityToEnrollStudentInCourse });
                        }

                        senangPayViewModel.FullName = profile.Contact.FullName;
                        senangPayViewModel.PhoneNumber = profile.Contact.Mobile;
                        senangPayViewModel.CurrencyRate = decimal.Round((decimal)(currancy.Value / exchangeCurrency.Value), 2);
                        senangPayViewModel.CustomerCurrencyCode = currancy.Code;
                        senangPayViewModel.Status = (int)GeneralEnums.StatusEnum.NotPaid;
                        senangPayViewModel.senangPayId = _senangPayService.AddSenangPay(senangPayViewModel)?.Id ?? 0;

                        var url = await _senangPayService.GetUrl(senangPayViewModel);
                        if (url != null)
                            return Json(new { url = url });
                        else
                            return Json(new { result = "Error While Create Senang Pay (Post)" });
                    }
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Create Senang Pay (Post)");
                    return Json(new { result = "Error While Create Senang Pay (Post)" });
                }
            }

            return Json(new { result = "Error While Create Senang Pay (Post)" });
        }



        public async Task<IActionResult> ThankYou(string status_id, string order_id, string msg, string transaction_id, string hash)
        {
            try
            {
                var senangpay = _senangPayService.GetSenangPayById(Convert.ToInt32(order_id));
                var secretkey = _settingService.GetOrCreate(Constants.SystemSettings.SenangpaySecretkey, "").Value;

                if (senangpay != null && senangpay.Project != null)
                {
                    var str = secretkey + status_id + order_id + transaction_id + msg;
                    var hashSeccretkey = _senangPayService.HMACSHA256Encode(str, secretkey);
                    var cu = await _userManager.GetUserAsync(User);

                    if (status_id == "1" /*&& hash == hashSeccretkey*/)
                    {
                        _projectService.AddProjectDonars(new DonationViewModel()
                        {
                            Cost = (double)senangpay.Amount,
                            Email = senangpay.Email,
                            Id = senangpay.ProjectId ?? 0,
                            Name = senangpay.UserName,
                            Phone = senangpay.PhoneNumber,
                            Item = senangpay.ProjectCostId ?? 0,
                        });
                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            Status = (int)GeneralEnums.StatusEnum.Paid,
                            TransactionId = transaction_id,
                            Msg = msg,
                        };
                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);

                        return RedirectToAction("ProjectDetails", "Projects", new { id = senangpay.ProjectId, success = true });
                    }
                    else
                    {
                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            TransactionId = transaction_id,
                            Msg = msg,
                        };

                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);
                        return RedirectToAction("ProjectDetails", "Projects", new { id = senangpay.ProjectId, success = false });
                    }
                }
                else if (senangpay != null && senangpay.EnrollTeacherCourse != null)
                {
                    ViewBag.CourseId = senangpay.EnrollTeacherCourse.CourseId;
                    var cu = await _userManager.GetUserAsync(User);

                    var str = secretkey + status_id + order_id + transaction_id + msg;
                    var hashSeccretkey = _senangPayService.HMACSHA256Encode(str, secretkey);

                    if (status_id == "1" /*&& hash == hashSeccretkey*/)
                    {
                        var profile = _userProfileService.GetUserProfileByUsername(cu.UserName);
                        //TODO : Move to correct position after payment don
                        if (profile.Contact.Students.Any(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted))
                        {

                            var NeedApproval = false;
                            var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(senangpay.EnrollTeacherCourse.Id, profile.Contact.Students.First(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted).Id);
                            if (CheckHasPreRequestsCourse != "done")
                                NeedApproval = true;

                            _enrollStudentCourseService.AddEnrollStudentCourse(new EnrollStudentCourseViewModel()
                            {
                                CreatedOn = DateTime.Now,
                                Price = senangpay.Amount,
                                CourseId = senangpay.EnrollTeacherCourseId.Value,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                NeedApproval = NeedApproval,
                                IsPass = null,
                                StudentId = profile.Contact.Students.First(s => s.Status != (int)GeneralEnums.StatusEnum.Deleted).Id,
                                CustomerCurrencyCode = senangpay.CustomerCurrencyCode,
                                CurrencyRate = senangpay.CurrencyRate,
                            });
                        }

                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            Status = (int)GeneralEnums.StatusEnum.Paid,
                            TransactionId = transaction_id,
                            Msg = msg,
                        };
                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);

                        try
                        {
                            await _smsService.SendEmail(new MessageViewModel
                            {
                                CreatedBy = User.Identity.Name,
                                Ids = new List<int>() { profile.ContactId.Value },
                                Message = $"You paid ${senangpay.Amount} to register for a course: {senangpay.EnrollTeacherCourse.Course.CourseName}",
                                Subject = "Al-Safa Payment",
                                Emails = new List<string>() { }
                            });
                        }
                        catch (Exception ex)
                        {
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Sending Email For register for a course");
                        }

                        ViewBag.Paymentstatus = 1;
                    }
                    else
                    {
                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            TransactionId = transaction_id,
                            Msg = msg,
                        };

                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);

                        ViewBag.Paymentstatus = 0;
                        return View(senangpay);
                    }
                    return RedirectToAction("Details", "Courses", new { id = senangpay.EnrollTeacherCourse.CourseId });
                }
                else if (senangpay != null && senangpay.SenangPayPaymentType == (int)GeneralEnums.SenangPayPaymentType.AddBalance)
                {
                    var cu = await _userManager.GetUserAsync(User);

                    var str = secretkey + status_id + order_id + transaction_id + msg;
                    var hashSeccretkey = _senangPayService.HMACSHA256Encode(str, secretkey);

                    if (status_id == "1" /*&& hash == hashSeccretkey*/)
                    {
                        var profile = _userProfileService.GetUserProfileByUsername(cu.UserName);
                        var student = _StudentService.GetStudentByContactId(profile.ContactId ?? 0);

                        _StudentService.ChangeBalance(student, senangpay.Amount);
                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            Status = (int)GeneralEnums.StatusEnum.Paid,
                            TransactionId = transaction_id,
                            Msg = msg
                        };
                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);

                        _balanceHistoryService.AddBalanceHistory(new StudentBalanceHistory()
                        {
                            CreatedOn = DateTime.Now,
                            Amount = senangpay.Amount,
                            Balance = student.Balance,
                            StudentId = student.Id,
                            Title = "Added Balance To Your Account",
                        });

                        try
                        {
                            await _smsService.SendEmail(new MessageViewModel
                            {
                                CreatedBy = User.Identity.Name,
                                Ids = new List<int>() { profile.ContactId.Value },
                                Message = $"You paid ${senangpay.Amount}",
                                Subject = "Al-Safa Payment",
                                Emails = new List<string>() { }
                            });
                        }
                        catch (Exception ex)
                        {
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Sending Email For Add Balance");
                        }

                        return RedirectToAction("Manage", "Account", new { area = "Identity", success = true });
                    }
                    else
                    {
                        var senangpayViewModel = new SenangPayViewModel(senangpay)
                        {
                            TransactionId = transaction_id,
                            Msg = msg,
                        };

                        _senangPayService.EditSenangPay(senangpayViewModel, senangpay);
                        return RedirectToAction("Manage", "Account", new { area = "Identity", success = false });
                    }
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error From Thank You Page");
                return NotFound();
            }
        }

        public void CallBack(string status_id, string order_id, string msg, string transaction_id, string hash)
        {
            if (order_id == null)
            {
                NotFound();
            }

            var senangpay = _senangPayService.GetSenangPayById(Convert.ToInt32(order_id));

            var senangpayViewModel = new SenangPayViewModel(senangpay)
            {
                TransactionId = transaction_id,
                Msg = msg
            };

            if (status_id == "1")
            {
                senangpayViewModel.Status = (int)GeneralEnums.StatusEnum.Paid;
            }
            else
            {
                senangpay.Status = (int)GeneralEnums.StatusEnum.Active;
            }

            _senangPayService.EditSenangPay(senangpayViewModel, senangpay);
        }
    }
}
