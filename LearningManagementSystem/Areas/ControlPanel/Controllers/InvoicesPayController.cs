using System;
using System.Threading.Tasks;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Mvc;
using LearningManagementSystem.Core;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using System.IO;
using System.Linq;
using Microsoft.Graph;
using System.Collections.Generic;
using Constants = LearningManagementSystem.Core.Constants;
using Microsoft.Extensions.Localization;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class InvoicesPayController : Controller
    {
        private readonly IInvoicesPayService _InvoicesPayService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly ICookieService _cookieService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IStudentService _studentService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseServic;
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly ISmsService _smsService;
        private readonly IStringLocalizer<InvoicesPayController> _localizer;

        public InvoicesPayController(IInvoicesPayService InvoicesPayService, 
            ISettingService settingService, 
            ILogService logService,
            ICookieService cookieService, 
            IEnrollStudentCourseService enrollStudentCourseService, 
            IStudentService studentService, 
            IEnrollTeacherCourseService enrollTeacherCourseService,
            ICourseService courseService,
            IWebHostEnvironment WebHostEnvironment,
             IEnrollStudentExamService enrollStudentExamService,ISmsService smsService, IStringLocalizer<InvoicesPayController> localizer
            )
        {
            _InvoicesPayService = InvoicesPayService;
            _settingService = settingService;
            _logService = logService;
            _cookieService = cookieService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _studentService = studentService;
            _enrollTeacherCourseServic = enrollTeacherCourseService;
            _courseService = courseService;
            _WebHostEnvironment = WebHostEnvironment;
            _enrollStudentExamService = enrollStudentExamService;
            _smsService = smsService;
            _localizer = localizer;
        }


        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Invoices Pay List")]
        public async Task<IActionResult> GetData(int? page ,string searchText,int? ProcessStatusID, int pagination,  int? CourseId, string hdCourseName)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            if (ProcessStatusID > 0)
            {
                ViewBag.ProcessStatusID = ProcessStatusID;
            }

            if (CourseId > 0)
            {
                ViewBag.CourseId = CourseId;
                ViewBag.hdCourseName = hdCourseName;
            }
            else
            {
                ViewBag.CourseId = 0;
                ViewBag.hdCourseName = "";
            }

            var val = _cookieService.GetCookie(Constants.Pagenation.InvoicesPayPagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.InvoicesPayPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result =await _InvoicesPayService.GetInvoicesPay(searchText, page, languageId, CourseId,ProcessStatusID);
            ViewBag.languageId = languageId;
            return PartialView("_Index", result);
        }

        // GET: ControlPanel/InvoicesPay/ShowImage
        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new InvoicesPayViewModel();
            if (!string.IsNullOrEmpty(ImageUrl))
                result.AttachmentUrl = ImageUrl;
         
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/InvoicesPay/DownloadDocument
        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "File Download")]
        public ActionResult DownloadDocument(string filePath)
        {
            try
            {
                string webRootPath = _WebHostEnvironment.WebRootPath;
                string contentRootPath = _WebHostEnvironment.ContentRootPath;
                var fullPath = HttpUtility.UrlDecode(filePath);
                var fileName = Path.GetFileName(fullPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fullPath);
                return File(fileBytes, "application/force-download", fileName);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Download Invoices Pay Documents");
                return NotFound();
            }
        }

        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Invoices Pay")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: ControlPanel/InvoicesPay/CourseSearch
        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "InvoicesPay Course  Get")]
        public IActionResult CourseSearch(string param, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var Courses = _courseService.GetCoursesByName(param, languageId);
            return Json(Courses);
        }

        // GET: ControlPanel/InvoicesPay/Edit/5
        [AuditLogFilter(ActionDescription = "Invoices Pay Edit Get")]
        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "ChangeInvoicesPayStatus")]
        public async Task<IActionResult> ShowChangeInvoicesPayStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var InvoicesPay =await _InvoicesPayService.GetInvoicesPayById(id.Value);
            if (InvoicesPay == null || InvoicesPay.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.languageId = langId;
            return PartialView("ShowChangeStatus", InvoicesPay);
        }

        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "InvoicesPay", PermissionKey = "ChangeInvoicesPayStatus")]
        public async Task<IActionResult> ChangeInvoicesPayStatus(InvoicesPayViewModel InvoicesPayViewModel)
        {
            if (ModelState.IsValid)
            {
                var InvoicesPayDetails = await _InvoicesPayService.GetInvoicesPayById(InvoicesPayViewModel.Id);
                var studenDetails = _studentService.GetStudentByContactId(InvoicesPayDetails.ContactId);
                if ((studenDetails == null || studenDetails.Id <= 0) && InvoicesPayViewModel.ProcessStatus == (int)GeneralEnums.InvoicesPayStatusEnum.Accepted)
                    return Json(new { result = "FailContactNotRegisteredAsStudent" });//The contact is not registered as a student

                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var ChangeInvoicesPay = await _InvoicesPayService.ChangeInvoicesPay(InvoicesPayViewModel, context);
                            if (ChangeInvoicesPay != null)
                            {
                                if (ChangeInvoicesPay.ProcessStatus == (int)GeneralEnums.InvoicesPayStatusEnum.Accepted)
                                {
                                    var CheckAbilityToEnrollStudentInCourse = _enrollStudentCourseService.CheckAbilityToEnrollStudentInCourse(InvoicesPayDetails.EnrollTeacherCourseId, studenDetails.Id);
                                    if (CheckAbilityToEnrollStudentInCourse != "done")
                                        return Json(new { result = CheckAbilityToEnrollStudentInCourse });

     
                                    var CoursDetails = _enrollTeacherCourseServic.GetEnrollTeacherCourseById(InvoicesPayDetails.EnrollTeacherCourseId);
                                    var Price = 0m;
                                    if (CoursDetails.Course.CoursePrice != null)
                                        Price = CoursDetails.Course.CoursePrice.Value;

                                    var NeedApproval = false;
                                    var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(InvoicesPayDetails.EnrollTeacherCourseId, studenDetails.Id);
                                    if (CheckHasPreRequestsCourse != "done")
                                    {
                                        NeedApproval = true;
                                    }
                                   
                                    _enrollStudentCourseService.AddEnrollStudentCourse_WthoutUsing(new EnrollStudentCourseViewModel()
                                    {
                                        CreatedOn = DateTime.Now,
                                        Price = Price,
                                        CourseId = ChangeInvoicesPay.EnrollTeacherCourseId,
                                        Status = (int)GeneralEnums.StatusEnum.Active,
                                        StudentId = studenDetails.Id,
                                        NeedApproval= NeedApproval,
                                        CurrencyRate =ChangeInvoicesPay.CurrencyRate,
                                        CustomerCurrencyCode= ChangeInvoicesPay.CustomerCurrencyCode,
                                        
                                    }, context);
                                }

                                try
                                {
                                    var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                                    var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
                                    var course = _enrollTeacherCourseServic.GetEnrollById(ChangeInvoicesPay.EnrollTeacherCourseId, langId);
                                    var request = ChangeInvoicesPay.ProcessStatus == (int)GeneralEnums.InvoicesPayStatusEnum.Accepted ? _localizer["Your Request has been Approved"].Value : _localizer["Your Request has been Denied"].Value;
                                    await _smsService.SendEmail(new MessageViewModel
                                    {
                                        CreatedBy = User.Identity.Name,
                                        Ids = new List<int>() { InvoicesPayDetails.ContactId },
                                        Message = $"{request} {_localizer["for"]} ({course.CourseName})",
                                        Subject = $"{request}",
                                        Emails = new List<string>() { },
                                    });
                                }
                                catch (Exception ex)
                                {
                                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Send email for ChangeInvoicesPayStatus");
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Change Invoices Pay Status");
                            return Json(new { result = "Fail" });
                        }
                    }
                }

                return Json(new { result = "Success" });

            }
            else
            {
                return Json(new { result = "Fail" });
            }
           
        }
        

    }
}
