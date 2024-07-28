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
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Areas.ControlPanel.Controllers;
using Microsoft.Extensions.Localization;
using LearningManagementSystem.Areas.Trainer.Controllers;

namespace LearningManagementSystem.Controllers
{
    public class PracticalExamController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly ILogService _logService;
        private readonly ISettingService _settingService;
        private readonly IPracticalEnrollmentExamService _practicalEnrollmentExamService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;
        private readonly IPracticalEnrollmentExamStudentService _practicalEnrollmentExamStudentService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStudentService _studentService;

        public PracticalExamController(ICookieService cookieService, ILogService logService, ISettingService settingService
            , IPracticalEnrollmentExamService practicalEnrollmentExamService, IEnrollTeacherCourseService enrollTeacherCourseService,
            IEnrollStudentCourseService enrollStudentCourseService, IPracticalEnrollmentExamStudentService practicalEnrollmentExamStudentService
            , IUserProfileService userProfileService , IStudentService studentService)
        {
            _cookieService = cookieService;
            _logService = logService;
            _settingService = settingService;
            _practicalEnrollmentExamService = practicalEnrollmentExamService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollStudentCourseService = enrollStudentCourseService;
            _practicalEnrollmentExamStudentService = practicalEnrollmentExamStudentService;
            _userProfileService = userProfileService;
            _studentService = studentService;
        }

        [AuditLogFilter(ActionDescription = "List Practical Exams For Student")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> GetPracticalExams(int? TecherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            if (User.Identity.Name != null)
            {
                var courseId = _enrollTeacherCourseService.GetEnrollById(TecherCourseId ?? 0, languageId)?.CourseId;
                var result = _practicalEnrollmentExamService.GetPracticalEnrollmentExam(TecherCourseId ?? 0, languageId);
                var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
                var studenDetails = _studentService.GetStudentByContactId(ContactID);
                var enrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndCourseId(studenDetails.Id, courseId??0);
                ViewBag.NeedApproval = enrollStudentCourse.NeedApproval.HasValue && enrollStudentCourse.NeedApproval.Value;
                ViewBag.Enroll = enrollStudentCourse.Id != 0;
                ViewBag.CourseId = courseId;
                ViewBag.EnrollTeacherCourseId = TecherCourseId;
                return PartialView("_ShowPracticalExams", result);
            }
            else
            {
                ViewBag.Enroll = false;
                ViewBag.NeedApproval = false;
            }
            return PartialView("_ShowPracticalExams", new List<PracticalEnrollmentExam>());
        }

        [AuditLogFilter(ActionDescription = "Practical Exams Marks For Student")]
        [CheckSuperAdmin(PageName = "PracticalExams")]
        public async Task<IActionResult> ShowPracticalExamMarks(int practicalEnrollmentExamId , int enrollTeacherCourseId)
        {

            var ContactID = _userProfileService.GetUserProfileByUsername(User.Identity.Name).Contact.Id;
            var studenDetails = _studentService.GetStudentByContactId(ContactID);
            var EnrollStudentCourse = _enrollStudentCourseService.GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(studenDetails.Id, enrollTeacherCourseId);

            var practicalEnrollmentExamStudent = _practicalEnrollmentExamStudentService.GetOrCreate(practicalEnrollmentExamId, EnrollStudentCourse.Id, User?.Identity?.Name ?? string.Empty);
            var exam = _practicalEnrollmentExamService.GetPracticalEnrollmentExamById(practicalEnrollmentExamId);
            var mark = _practicalEnrollmentExamService.GetMark(exam.PracticalExamId ?? 0, exam.EnrollTeacherCourse.CourseId);
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.EnrollStudentCourseId = EnrollStudentCourse.Id;
            ViewBag.PracticalEnrollmentExamId = practicalEnrollmentExamId;
            ViewBag.Mark = exam.PracticalExam.Mark;
            ViewBag.Type = LookupHelper.GetLookupDetailsById(exam.TypeId ?? 0, languageId)?.Code == "QuranMemorization";
            ViewBag.MarkAfterConversion = exam.PracticalExam.MarkAfterConversion;
            ViewBag.SubjectMark = mark > 0 ? mark : int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Exam_Subject_Points, "20").Value);
            ViewBag.Subjects = _practicalEnrollmentExamStudentService.GetStudentSubjects(practicalEnrollmentExamStudent.Id, languageId);
            ViewBag.PracticalEnrollmentExamStudentId = practicalEnrollmentExamStudent.Id;

            var questions = _practicalEnrollmentExamStudentService.GetQuestions(exam?.PracticalExamId ?? 0, languageId);

            return PartialView("_PracticalExam", questions);
        }
    }
}
