using System;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using LearningManagementSystem.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using DataEntity.Models.EfModels;
using System.Linq;
using X.PagedList;

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollCourseQuizController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollCourseQuizService _enrollCourseQuizService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly ISettingService _settingService;

        public EnrollCourseQuizController(ILogService logService, IEnrollCourseQuizService enrollCourseQuizService
            ,IEnrollStudentCourseService enrollStudentCourseService, ISettingService settingService)
        {
            _logService = logService;
            _enrollCourseQuizService = enrollCourseQuizService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _settingService = settingService;
        }
        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuiz")]
        [CheckSuperAdmin(PageName = "Quizzes")]
        public IActionResult GetQuizzes(int id, int? page)
        {
            ViewBag.Page = page ?? 1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.EnrollTeacherCourseId = id;

            var quizzes = _enrollCourseQuizService.GetQuizzes(id, page ?? 1, languageId);
            ViewBag.Count = _enrollStudentCourseService.GetStudentCount(id);
            return PartialView("_Quizzes", quizzes);
        }

        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuiz")]
        public IActionResult RefetchQuizzes(int id)
        {
            try
            {
                _enrollCourseQuizService.RefetchQuizzes(id, User?.Identity?.Name ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Refetching Quizzes");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuiz")]
        public async Task<IActionResult> AddOrRemoveQuiz(int quizId, int quizNum, string checkedRow)
        {
            try
            {
                var quiz = _enrollCourseQuizService.GetQuizById(quizId);
                if (quiz == null) return Content("error");

                _enrollCourseQuizService.EditQuiz(quiz, quizNum, checkedRow == "true");
                return Content("Ok");
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Quiz");
                return Content("error");
            }
        }
        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuizPointes")]
        public async Task<IActionResult> ShowEnrolledStudents(int quizId, int enrollTeacherCourseId, int? page, string searchText)
        {
            if (page == 0 || page == null)
                page = 1;

            ViewBag.Page = page;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;
            ViewBag.EnrollTeacherCourseId = enrollTeacherCourseId;
            ViewBag.QuizId = quizId;
            ViewBag.QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);

            ViewBag.Quiz = _enrollCourseQuizService.GetQuizById(quizId);
            ViewBag.QuizPoints = _enrollCourseQuizService.GetQuizPointsByQuizId(quizId);
            var result = _enrollStudentCourseService.GetEnrollStudentCoursesForQuiz(page, enrollTeacherCourseId, langId, 10, searchText);

            return PartialView("_Students", result);
        }

        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuizPointes")]
        public async Task<IActionResult> SubmitMark(decimal? value, int quizId, int enrollStudentCourseId, int num)
        {
            try
            {
                _enrollCourseQuizService.AddQuizPoint(value, quizId, enrollStudentCourseId, num, User?.Identity?.Name ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Quiz Point");
                return null;
            }
        }

        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuizPointes")]
        public async Task<IActionResult> ShowEnrolledStudentsForMarks(int enrollTeacherCourseId, int? page, string searchText)
        {
            if (page == 0 || page == null)
                page = 1;

            ViewBag.Page = page;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;
            ViewBag.EnrollTeacherCourseId = enrollTeacherCourseId;
            ViewBag.QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);

            var result = _enrollStudentCourseService.GetEnrollStudentCoursesForQuiz(page, enrollTeacherCourseId, langId, 10, searchText);
            return PartialView("_StudentsMarks", result);
        }


        [CustomAuthentication(PageName = "EnrollCourseQuiz", PermissionKey = "AddAndEditQuizPointes")]
        public async Task<IActionResult> ShowStudentQuizMarks(int enrollStudentCourseId, int enrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var marks = _enrollCourseQuizService.GetStudentQuizMarks(enrollStudentCourseId, enrollTeacherCourseId, langId);

            ViewBag.QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);
            ViewBag.StudentName = _enrollStudentCourseService.GetStudentNameFromEnrollStudentCourseById(enrollStudentCourseId, langId);
            ViewBag.EnrollTeacherCourseId = enrollTeacherCourseId;

            return PartialView("_StudentsMarksDetails", marks);
        }
    }
}
