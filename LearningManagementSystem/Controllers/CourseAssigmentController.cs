using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Controllers;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.Controllers
{
    public class CourseAssigmentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollStudentAssigmentService _enrollStudentAssigmentService;
        private readonly ISettingService _settingService;
        private readonly ICookieService _cookieService;

        public CourseAssigmentController(ILogService logService, IEnrollStudentAssigmentService enrollStudentAssigmentService,
            ISettingService settingService, ICookieService cookieService)
        {
            _logService = logService;
            _enrollStudentAssigmentService = enrollStudentAssigmentService;
            _settingService = settingService;
            _cookieService = cookieService;
        }

        public IActionResult Index(int EnrollStudentAssigmentId)
        {
            var studentAssigment = _enrollStudentAssigmentService.GetEnrollStudentAssigment(EnrollStudentAssigmentId);
            return View(studentAssigment);
        }


        [Authorize]
        public async Task<IActionResult> GetData(int EnrollCourseAssigmentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var result = _enrollStudentAssigmentService.EnrollCourseAssigmentQuestionByEnrollCourseAssigmentId(EnrollCourseAssigmentId , languageId);
            return View("_Index", result);
        }

        [Authorize]
        public async Task<IActionResult> Create(IFormCollection form, int EnrollCourseAssigmentId, int EnrollStudentAssigmentId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var studentAssigmentQuestion = _enrollStudentAssigmentService.EnrollCourseAssigmentQuestionByEnrollCourseAssigmentId(EnrollCourseAssigmentId, languageId);
            try
            {
                List<EnrollStudentAssigmentAnswer> enrollStudentAssigmentAnswers = new List<EnrollStudentAssigmentAnswer>();
                foreach (var assigment in studentAssigmentQuestion)
                {
                    if (assigment.QuestionType == (int)GeneralEnums.QuestionEnum.Text)
                    {
                        enrollStudentAssigmentAnswers.Add(new EnrollStudentAssigmentAnswer
                        {
                            CreatedBy = User.Identity.Name,
                            CreatedOn = DateTime.Now,
                            EnrollCourseAssigmentQuestionId = assigment.Id,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            EnrollStudentAssigmentId = EnrollStudentAssigmentId,
                            Answer = form["textarea_" + assigment.Id],
                            IsCorrect = null
                        });
                    }
                    else if (assigment.QuestionType == (int)GeneralEnums.QuestionEnum.CheckBox)
                    {
                        var enrollStudentAssigmentAnswerOption = new List<EnrollStudentAssigmentAnswerOption>();
                        var CorrectAnswers = assigment.EnrollCourseAssigmentQuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                        var IsCorrect = false;
                        if (form.ContainsKey("Checkbox_" + assigment.Id))
                        {
                            var Checkboxs = form["Checkbox_" + assigment.Id];
                            foreach (var op in Checkboxs)
                            {
                                enrollStudentAssigmentAnswerOption.Add(new EnrollStudentAssigmentAnswerOption
                                {
                                    QusetionOptionId = int.Parse(op),
                                    CreatedBy = User.Identity.Name,
                                    CreatedOn = DateTime.Now,
                                    Status = (int)GeneralEnums.StatusEnum.Active,

                                });
                            }
                            var StudentsAnswers = enrollStudentAssigmentAnswerOption.Select(e => e.QusetionOptionId).ToList();
                            bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                            if (isEqual)
                                IsCorrect = true;

                            enrollStudentAssigmentAnswers.Add(new EnrollStudentAssigmentAnswer
                            {
                                CreatedBy = User.Identity.Name,
                                CreatedOn = DateTime.Now,
                                EnrollCourseAssigmentQuestionId = assigment.Id,
                                EnrollStudentAssigmentId = EnrollStudentAssigmentId,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                EnrollStudentAssigmentAnswerOptions = enrollStudentAssigmentAnswerOption,
                                IsCorrect = IsCorrect
                            });
                        }

                    }
                    else if (assigment.QuestionType == (int)GeneralEnums.QuestionEnum.RadioButton)
                    {
                        var enrollStudentAssigmentAnswerOption = new List<EnrollStudentAssigmentAnswerOption>();
                        var CorrectAnswers = assigment.EnrollCourseAssigmentQuestionOptions.Where(e => e.IsCorrect).Select(e => e.Id).ToList();
                        var IsCorrect = false;
                        if (form.ContainsKey("Radios_" + assigment.Id))
                        {
                            enrollStudentAssigmentAnswerOption.Add(new EnrollStudentAssigmentAnswerOption
                            {
                                QusetionOptionId = int.Parse(form["Radios_" + assigment.Id]),
                                CreatedBy = User.Identity.Name,
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                            });

                            var StudentsAnswers = enrollStudentAssigmentAnswerOption.Select(e => e.QusetionOptionId).ToList();
                            bool isEqual = StudentsAnswers.All(CorrectAnswers.Contains) && CorrectAnswers.All(StudentsAnswers.Contains);
                            if (isEqual)
                                IsCorrect = true;
                        }

                        enrollStudentAssigmentAnswers.Add(new EnrollStudentAssigmentAnswer
                        {
                            CreatedBy = User.Identity.Name,
                            CreatedOn = DateTime.Now,
                            EnrollCourseAssigmentQuestionId = assigment.Id,
                            EnrollStudentAssigmentId = EnrollStudentAssigmentId,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            EnrollStudentAssigmentAnswerOptions = enrollStudentAssigmentAnswerOption,
                            IsCorrect = IsCorrect
                        });
                    }
                    else if (assigment.QuestionType == (int)GeneralEnums.QuestionEnum.UploadAttachment)
                    {
                        enrollStudentAssigmentAnswers.Add(new EnrollStudentAssigmentAnswer
                        {
                            CreatedBy = User.Identity.Name,
                            CreatedOn = DateTime.Now,
                            EnrollCourseAssigmentQuestionId = assigment.Id,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            EnrollStudentAssigmentId = EnrollStudentAssigmentId,
                            Answer = form["inputHidden_" + assigment.Id],
                            IsCorrect = null
                        });
                    }
                }

                _enrollStudentAssigmentService.AddEnrollStudentAssigmentAnswer(enrollStudentAssigmentAnswers);
                return Json(new { result = "Success" });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Course Assigment");
                return null;
            }
        }
    }
}
