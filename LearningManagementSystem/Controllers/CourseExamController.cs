using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LearningManagementSystem.Controllers
{
    public class CourseExamController : Controller
    {
        private readonly IEnrollCourseExamQuestionService _enrollCourseExamQuestionService;
        private readonly IEnrollCourseExamService _enrollCourseExamService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ILogService _logService;
        private readonly IEnrollStudentExamService _enrollStudentExamService;
        private readonly IUserProfileService _userProfileService;
        private readonly IEnrollStudentExamAnswerService _enrollStudentExamAnswerService;
        private readonly IStudentService _studentService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public CourseExamController(IEnrollStudentExamService enrollStudentExamService,

            IEnrollStudentExamAnswerService enrollStudentExamAnswerService,
            IUserProfileService userProfileService, ILogService logService,
            ISettingService settingService, ICookieService cookieService,
            IEnrollCourseExamQuestionService enrollCourseExamQuestionService,
            IEnrollCourseExamService enrollCourseExamService, IStudentService studentService, IEnrollStudentCourseService enrollStudentCourseService, IEnrollTeacherCourseService enrollTeacherCourseService, IWebHostEnvironment webHostEnvironment)
        {
            _enrollCourseExamQuestionService = enrollCourseExamQuestionService;
            _enrollCourseExamService = enrollCourseExamService;
            _cookieService = cookieService;
            _settingService = settingService;
            _logService = logService;
            _userProfileService = userProfileService;
            _enrollStudentExamAnswerService = enrollStudentExamAnswerService;
            _enrollStudentExamService = enrollStudentExamService;
            _studentService = studentService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _WebHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Index(int EnrollCourseExamId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = langId;

            var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamById(EnrollCourseExamId);
            if (enrollCourseExam == null)
                return RedirectToAction("Index", "Home");

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, enrollCourseExam.EnrollTeacherCourseId);
            var enrollTeacherCours = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollStudentCourse.CourseId, langId);

            ViewBag.Duration = enrollCourseExam.Duration;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollTeacherCourseId = enrollCourseExam.EnrollTeacherCourseId;
            ViewBag.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(_settingService.GetOrCreate(Constants.SystemSettings.TimeZone, "UTC").Value).DisplayName;
            ViewBag.IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.Online;

            var CheckAbilityToEnrollStudentExam = _enrollStudentExamService.CheckAbilityToEnrollStudentExam(EnrollCourseExamId, EnrollStudentCourse.Id, EnrollStudentCourse.StudentId);
            if (CheckAbilityToEnrollStudentExam != "done")
                ViewBag.CheckAbilityToEnrollStudentExam = CheckAbilityToEnrollStudentExam;

            return View(enrollCourseExam);
        }
        [Authorize]
        public async Task<IActionResult> ShowExamView(int EnrollCourseExamId, int EnrollStudentExamID)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollStudentExamID = EnrollStudentExamID;
            var result = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            foreach (var Exam in result)
                Exam.EnrollStudentExamAnswers = _enrollStudentExamAnswerService.GetListEnrollStudentExamAnswer(EnrollStudentExamID);
            return View("_IndexExamView", result);
        }
        [Authorize]
        public async Task<IActionResult> GetData(int EnrollCourseExamId, int EnrollTeacherCourseId, string CheckAbilityToEnrollStudentExam)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(EnrollTeacherCourseId);
            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollTeacherCourseId = EnrollTeacherCourseId;
            ViewBag.CourseId = enrollTeacherCourse.CourseId;
            ViewBag.CheckAbilityToEnrollStudentExam = CheckAbilityToEnrollStudentExam;
            var result = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            return View("_Index", result);
        }



        [Authorize]
        public async Task<IActionResult> Create(IFormCollection form, int EnrollCourseExamId, int EnrollTeacherCourseId, bool IsExamPreRequest = false, bool TakeAgain = false)
        {
            try
            {

                var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                var EnrollCourseExamQuestions = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
                var EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, EnrollTeacherCourseId);
                var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamById(EnrollCourseExamId);
                var NeedCorrectionFromTrainer = false;

                if (studenDetails == null || studenDetails.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    return Json(new { result = "Fail" });
                }

                if (EnrollStudentCourse == null || EnrollStudentCourse.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    return Json(new { result = "Fail" });
                }

                var CheckAbilityToEnrollStudentExam = _enrollStudentExamService.CheckAbilityToEnrollStudentExam(EnrollCourseExamId, EnrollStudentCourse.Id, EnrollStudentCourse.StudentId, IsExamPreRequest, TakeAgain);
                if (CheckAbilityToEnrollStudentExam != "done")
                    return Json(new { result = CheckAbilityToEnrollStudentExam });




                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<EnrollStudentExamAnswerViewModel> enrollStudentExamAnswers = new List<EnrollStudentExamAnswerViewModel>();
                            foreach (var exam in EnrollCourseExamQuestions)
                            {
                                if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.Text)
                                {
                                    NeedCorrectionFromTrainer = true;
                                    enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                    {
                                        CreatedBy = User.Identity.Name,
                                        CreatedOn = DateTime.Now,
                                        EnrollCourseExamQuestionId = exam.Id,
                                        Status = (int)GeneralEnums.StatusEnum.Active,
                                        Answer = form["textarea_" + exam.Question.Question.Id],
                                        Mark = 0,
                                        IsCorrect = null
                                    });
                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.CheckBox)
                                {
                                    var enrollStudentExamAnswerOption = new List<EnrollStudentExamAnswerOption>();
                                    var CorrectAnswers = exam.Question.Question.QuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("Checkbox_" + exam.Question.Question.Id))
                                    {
                                        var Checkboxs = form["Checkbox_" + exam.Question.Question.Id];
                                        foreach (var op in Checkboxs)
                                        {
                                            enrollStudentExamAnswerOption.Add(new EnrollStudentExamAnswerOption
                                            {
                                                QusetionOptionId = int.Parse(op),
                                                CreatedBy = User.Identity.Name,
                                                CreatedOn = DateTime.Now,
                                                Status = (int)GeneralEnums.StatusEnum.Active,

                                            });
                                        }
                                        var StudentsAnswers = enrollStudentExamAnswerOption.Select(e => e.QusetionOptionId).ToList();
                                        bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                                        if (isEqual)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                        enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                        {
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            EnrollCourseExamQuestionId = exam.Id,
                                            Status = (int)GeneralEnums.StatusEnum.Active,
                                            EnrollStudentExamAnswerOptions = enrollStudentExamAnswerOption,
                                            Mark = Mark,
                                            IsCorrect = IsCorrect
                                        });
                                    }

                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.RadioButton)
                                {
                                    var enrollStudentExamAnswerOption = new List<EnrollStudentExamAnswerOption>();
                                    var CorrectAnswers = exam.Question.Question.QuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                                    var Mark = 0.0;
                                    var IsCorrect = false;
                                    if (form.ContainsKey("Radios_" + exam.Question.Question.Id))
                                    {
                                        enrollStudentExamAnswerOption.Add(new EnrollStudentExamAnswerOption
                                        {
                                            QusetionOptionId = int.Parse(form["Radios_" + exam.Question.Question.Id]),
                                            CreatedBy = User.Identity.Name,
                                            CreatedOn = DateTime.Now,
                                            Status = (int)GeneralEnums.StatusEnum.Active,

                                        });

                                        var StudentsAnswers = enrollStudentExamAnswerOption.Select(e => e.QusetionOptionId).ToList();
                                        bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                                        if (isEqual)
                                        {
                                            Mark = ((exam.Mark.HasValue) ? exam.Mark.Value : 0);
                                            IsCorrect = true;
                                        }
                                    }

                                    enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                    {
                                        CreatedBy = User.Identity.Name,
                                        CreatedOn = DateTime.Now,
                                        EnrollCourseExamQuestionId = exam.Id,
                                        Status = (int)GeneralEnums.StatusEnum.Active,
                                        EnrollStudentExamAnswerOptions = enrollStudentExamAnswerOption,
                                        Mark = Mark,
                                        IsCorrect = IsCorrect
                                    });
                                }
                                else if (exam.Question.Question.Type == (int)GeneralEnums.QuestionEnum.UploadAttachment)
                                {
                                    NeedCorrectionFromTrainer = true;
                                    enrollStudentExamAnswers.Add(new EnrollStudentExamAnswerViewModel
                                    {
                                        CreatedBy = User.Identity.Name,
                                        CreatedOn = DateTime.Now,
                                        EnrollCourseExamQuestionId = exam.Id,
                                        Status = (int)GeneralEnums.StatusEnum.Active,
                                        Answer = form["inputHidden_" + exam.Question.Question.Id],
                                        Mark = 0,
                                        IsCorrect = null
                                    });
                                }
                            }


                            var FinalMarks = EnrollCourseExamQuestions.Select(e => e.Mark.Value).Sum();
                            var ExamMark = ((enrollCourseExam.ExamFinalMark.HasValue) ? enrollCourseExam.ExamFinalMark.Value : 0);
                            var MarkHeGot = enrollStudentExamAnswers.Select(e => e.Mark).Sum();
                            var MarkAfterConversion = MarkHeGot;
                            if (FinalMarks > 0 && ExamMark > 0)
                                MarkAfterConversion = Math.Ceiling((MarkHeGot * ExamMark) / (FinalMarks));


                            EnrollStudentExam enrollStudentExam = _enrollStudentExamService.AddEnrollStudentExam(new EnrollStudentExamViewModel
                            {
                                CreatedBy = User.Identity.Name,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                CreatedOn = DateTime.Now,
                                EnrollCourseExamId = EnrollCourseExamId,
                                EnrollStudentCourseId = EnrollStudentCourse.Id,
                                FinalMark = FinalMarks,
                                MarkHeGot = MarkHeGot,
                                MarkAfterConversion = MarkAfterConversion,
                            },
                             context
                             );
                            foreach (var r in enrollStudentExamAnswers)
                                r.EnrollStudentExamId = enrollStudentExam.Id;

                            _enrollStudentExamAnswerService.AddEnrollStudentAnswerExam(enrollStudentExamAnswers, context);

                            transaction.Commit();

                            //////////////////////////////////////////Check Exam Prerequisite/////////////////////////////////////////////////////////////
                            if (enrollCourseExam.IsPrerequisite && NeedCorrectionFromTrainer == false)
                            {
                                var EnrollStudentCourseExam = _enrollStudentExamService.GetEnrollStudentExamByEnrollStudentCourseId(EnrollStudentCourse.Id, EnrollCourseExamId);

                                var _EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseById(EnrollStudentCourseExam.EnrollStudentCourseId);
                                if (MarkHeGot >= (FinalMarks / 2))//mean the student succeeded in the exam
                                {
                                    var NeedApproval = false;
                                    var IsPass = true;
                                    _enrollStudentCourseService.EditEnrollStudentCourse(new EnrollStudentCourseViewModel()
                                    {
                                        Price = EnrollStudentCourse.Price,
                                        CourseId = EnrollStudentCourse.CourseId,
                                        NeedApproval = NeedApproval,
                                        IsPass = IsPass,
                                        StudentId = EnrollStudentCourse.StudentId,
                                    },
                                     _EnrollStudentCourse
                                    );
                                }
                                //Check if there another prerequisite exam not solved
                                var CheckHasPreRequestsCourse = _enrollStudentExamService.CheckHasPreRequestsExam(EnrollStudentCourse.CourseId, EnrollStudentCourse.StudentId);
                                if (CheckHasPreRequestsCourse != "done")
                                {
                                    _enrollStudentCourseService.EditEnrollStudentCourse(new EnrollStudentCourseViewModel()
                                    {
                                        Price = EnrollStudentCourse.Price,
                                        CourseId = EnrollStudentCourse.CourseId,
                                        NeedApproval = true,
                                        IsPass = false,
                                        StudentId = EnrollStudentCourse.StudentId,
                                    },
                                      _EnrollStudentCourse
                                     );
                                }

                            }
                            //////////////////////////////////////////////////////////////////////////////
                            return Json(new { result = "Success" });
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Course Exam");
                            return Json(new { result = "Fail" });
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Course Exam");
                return Json(new { result = "Fail" });
            }
        }




        [Authorize]
        public async Task<IActionResult> ShowExamPrerequisite(int EnrollCourseExamId, bool TakeAgain = false)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var enrollCourseExam = _enrollCourseExamService.GetEnrollCourseExamById(EnrollCourseExamId);
            if (enrollCourseExam == null)
                return RedirectToAction("Index", "Home");

            var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseById(enrollCourseExam.EnrollTeacherCourseId);
            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, enrollTeacherCourse.Id);

            ViewBag.LangId = languageId;
            ViewBag.EnrollCourseExamId = EnrollCourseExamId;
            ViewBag.EnrollTeacherCourseId = enrollTeacherCourse.Id;
            ViewBag.CourseId = enrollTeacherCourse.CourseId;
            ViewBag.TakeAgainV = TakeAgain.ToString();
            ViewBag.CheckAbilityToEnrollStudentExam = _enrollStudentExamService.CheckAbilityToEnrollStudentExam(EnrollCourseExamId, EnrollStudentCourse.Id, EnrollStudentCourse.StudentId, true, TakeAgain);
            var result = _enrollCourseExamQuestionService.GetEnrollCourseExamQuestionByEnrollCourseExamID(EnrollCourseExamId, languageId);
            return View("_IndexExamPrerequisite", result);
        }

        [Authorize]
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
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Download Student exam Documents");
                return NotFound();
            }
        }
    }
}
