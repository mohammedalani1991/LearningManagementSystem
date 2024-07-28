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
using DataEntity.Models.EfModels;
using LearningManagementSystem.Services.ControlPanel.IServices;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CourseController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _coursecategoryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICoursePrerequisiteService _coursePrerequisiteService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IPracticalExamService _practicalExamService;
        private readonly ISubjectService _subjectService;
        private readonly ISectionOfCourseQuizService _sectionOfCourseQuizService;
        private readonly ICourseMarksService _courseMarksService;

        public CourseController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService, ICourseCategoryService coursecategoryService
            , ICoursePrerequisiteService coursePrerequisiteService, IEnrollTeacherCourseService enrollTeacherCourseService
            , IPracticalExamService practicalExamService, ISubjectService subjectService, ISectionOfCourseQuizService sectionOfCourseQuizService,
            ICourseMarksService courseMarksService)
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _coursecategoryService = coursecategoryService;
            _coursePrerequisiteService = coursePrerequisiteService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _practicalExamService = practicalExamService;
            _subjectService = subjectService;
            _sectionOfCourseQuizService = sectionOfCourseQuizService;
            _courseMarksService = courseMarksService;
        }

        [CustomAuthentication(PageName = "Courses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Course")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "Courses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course List")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table)
        {
            if (page == 0)
                page = 1;

            ViewBag.Page = page;

            if (!string.IsNullOrWhiteSpace(searchText))
                ViewBag.searchText = searchText;

            var val = _cookieService.GetCookie(Constants.Pagenation.CoursePagination);

            if (val == null && pagination == 0)
                pagination = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CoursePagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "CourseName", "CourseDuration", "CoursePrice", "SuccessMark", "CategoryId", "LearningMethodId", "Status", "CreatedOn", "ImageUrl", "ShowInHomePage" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CourseTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CourseTable, table, 7);


            ViewBag.Table = val1;

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var result = _courseService.GetCourses(searchText, page, languageId, pagination);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "Courses", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        // GET: ControlPanel/Course/ShowImage
        [CustomAuthentication(PageName = "Courses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Image Details")]
        public IActionResult ShowImage(string ImageUrl)
        {
            var result = new CourseViewModel();
            result.ImageUrl = ImageUrl;
            return PartialView("ShowImage", result);
        }

        // GET: ControlPanel/Course/Details/5
        [CustomAuthentication(PageName = "Courses", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var course = _courseService.GetCourseById(id.Value, langId);

            if (course == null || course.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            course.Page = page;
            return PartialView("Details", course);
        }

        // GET: ControlPanel/Course/Create
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.ListCourseCategorys = _coursecategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // POST: ControlPanel/Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Create Post")]
        public async Task<IActionResult> Create(
            [Bind("Id,CourseName,CourseDuration,QuestionDescription,NeedQuestion,CoursePrice,SuccessMark,AssignmentMark,AcquiredSkills,TargetGroup,Notes,Requirements,CategoryId,LearningMethodId,LanguageId,Status,ImageUrl,ShowInHomePage","Type","TemplateIds")]
            CourseViewModel CourseViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CourseViewModel.LanguageId == 0)
                        CourseViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                    CourseViewModel.CreatedBy = User.Identity?.Name;
                    _courseService.AddCourse(CourseViewModel);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Dic");
                    return View(CourseViewModel);
                }
            }
            return View(CourseViewModel);
        }

        // GET: ControlPanel/Course/Edit/5
        [AuditLogFilter(ActionDescription = "Course Edit Get")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
                languageId = langId;

            var course = _courseService.GetCourseById(id.Value, languageId);
            course.LanguageId = languageId;
            if (course == null || course.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            course.Page = page;
            ViewBag.ListCourseCategorys = _coursecategoryService.GetAllCourseCategorys(languageId);
            ViewBag.LangId = languageId;
            return PartialView("Edit", course);
        }

        // POST: ControlPanel/Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuditLogFilter(ActionDescription = "Course Edit Pot")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Edit")]
        public async Task<IActionResult> Edit(int id,
             [Bind("Id,CourseName,CourseDuration,CoursePrice,QuestionDescription,NeedQuestion,SuccessMark,AssignmentMark,AcquiredSkills,TargetGroup,Notes,Requirements,CategoryId,LearningMethodId,LanguageId,Status,ImageUrl,ShowInHomePage","Type","TemplateIds")]
            CourseViewModel courseViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var course = _courseService.GetCourseById(courseViewModel.Id);
                    if (course != null && course.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        if (courseViewModel.LanguageId == 0)
                            courseViewModel.LanguageId = CultureHelper.GetDefaultLanguageId();
                        _courseService.EditCourse(courseViewModel, course);
                    }

                    return RedirectToAction(nameof(Index), new { page = courseViewModel.Page });
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Editing Course Dic (Post)");
                    return View(courseViewModel);
                }
            }
            return View(courseViewModel);
        }
        [AuditLogFilter(ActionDescription = "Course Delete Get")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Delete")]

        // GET: ControlPanel/Course/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            var course = _courseService.GetCourseById(id.Value, langId);
            if (course == null || course.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            ViewBag.LangId = langId;
            course.Page = page;
            return PartialView("Delete", course);
        }

        // POST: ControlPanel/Course/Delete/5
        [AuditLogFilter(ActionDescription = "Course Delete Post")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCourse(int id, int Page)
        {

            var course = _courseService.GetCourseById(id);
            if (course != null && course.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                var enrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseByCourseId(id);
                if (enrollTeacherCourse != null && enrollTeacherCourse.Count > 0)
                {
                    return Content("FailEnrollTeacherCourse");
                }
                else
                {
                    _courseService.DeleteCourse(course);
                    _coursePrerequisiteService.DeleteCoursePrerequisiteByCourseId(id);
                    _coursePrerequisiteService.DeleteCoursePrerequisiteByPrerequisiteCourseId(id);
                }
            }

            return RedirectToAction(nameof(Index), new { page = Page });
        }

        [CustomAuthentication(PageName = "Courses", PermissionKey = "AddPracticalExams")]
        public async Task<IActionResult> GetExams(int? Type, int? page1, int pagination1, string searchText1, int CourseId)
        {
            if (!string.IsNullOrWhiteSpace(searchText1))
                ViewBag.searchText1 = searchText1;

            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalExamPagination);

            if (val == null && pagination1 == 0)
                pagination1 = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination1 != 0)
                pagination1 = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalExamPagination, pagination1.ToString(), 7));
            else
                pagination1 = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue1 = pagination1;
            ViewBag.Page1 = page1;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.CourseId = CourseId;
            ViewBag.Type = Type;

            var result = _practicalExamService.GetPracticalExams(Type, searchText1, page1 ?? 1, languageId, pagination1);
            ViewBag.Exams = _practicalExamService.GetPracticalExamForCourse(CourseId);

            return PartialView("_AddExams", result);
        }

        // POST: ControlPanel/ExamQuestions/Create
        [AuditLogFilter(ActionDescription = "Practical Exam Course Create")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "AddPracticalExams")]
        public async Task<IActionResult> AddOrRemoveExams(int CourseId, int Examid, string checkedRow)
        {
            try
            {
                var examCourse = _practicalExamService.GetCourseExam(CourseId, Examid);
                if (checkedRow.ToLower() == "true")
                {
                    var eq = new PracticalExamCourse()
                    {
                        CourseId = CourseId,
                        PracticalExamId = Examid,
                        CreatedBy = User?.Identity?.Name ?? string.Empty,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        SubjectMark = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Exam_Subject_Points, "20").Value),
                    };
                    _practicalExamService.AddPracticalExamCourse(eq);
                }
                else
                {
                    if (_practicalExamService.CheckIfItHasSubjects(CourseId, Examid))
                        return Json(new { success = false, message = "There is Subjects For this Exam" });
                    else
                        _practicalExamService.RemovePracticalExamCourse(examCourse);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Practical Exam to Course");
                return Json(new { success = false });
            }
        }

        [CustomAuthentication(PageName = "Courses", PermissionKey = "AddSubjects")]
        public async Task<IActionResult> GetExamsForCourse(int? Type, int? page1, int pagination1, string searchText1, int CourseId)
        {
            if (!string.IsNullOrWhiteSpace(searchText1))
                ViewBag.searchText1 = searchText1;

            var val = _cookieService.GetCookie(Constants.Pagenation.PracticalExamPagination);

            if (val == null && pagination1 == 0)
                pagination1 = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination1 != 0)
                pagination1 = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.PracticalExamPagination, pagination1.ToString(), 7));
            else
                pagination1 = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue1 = pagination1;
            ViewBag.Page1 = page1;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.CourseId = CourseId;
            ViewBag.Type = Type;

            var result = _practicalExamService.GetPracticalExamForCourse(CourseId, Type, searchText1, page1 ?? 1, languageId, pagination1);
            return PartialView("_AddSubjects", result);
        }

        public async Task<IActionResult> GetSubjectData(int PracticalExamCourseId, int? TypeId, int? ExamTypeId, int? page2, int pagination2, string searchText2)
        {
            ViewBag.Page2 = page2;
            ViewBag.TypeId = TypeId;
            ViewBag.PracticalExamCourseId = PracticalExamCourseId;
            ViewBag.ExamTypeId = ExamTypeId;

            if (!string.IsNullOrWhiteSpace(searchText2))
                ViewBag.searchText = searchText2;

            var val = _cookieService.GetCookie(Constants.Pagenation.SubjectPagination);

            if (val == null && pagination2 == 0)
                pagination2 = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
            else if (pagination2 != 0)
                pagination2 = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.SubjectPagination, pagination2.ToString(), 7));
            else
                pagination2 = int.Parse(val != "" ? val : "10");

            ViewBag.PaginationValue2 = pagination2;


            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;

            var result = _subjectService.GetSubjects(TypeId, ExamTypeId, searchText2, page2 ?? 1, languageId, pagination2);
            ViewBag.Subejcts = _practicalExamService.GetSubjectsForPracticalExam(PracticalExamCourseId);

            return PartialView("_Subjects", result);
        }

        [AuditLogFilter(ActionDescription = "Adding Subject to Exam")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "AddSubjects")]
        public async Task<IActionResult> AddOrRemoveSubjects(int PracticalExamCourseId, int SubjectId, string checkedRow)
        {
            try
            {
                var examCourse = _practicalExamService.GetPracticalExamCourseSubject(PracticalExamCourseId, SubjectId);
                if (checkedRow.ToLower() == "true")
                {
                    var eq = new PracticalExamCourseSubject()
                    {
                        PracticalExamCourseId = PracticalExamCourseId,
                        SubjectId = SubjectId,
                        CreatedBy = User?.Identity?.Name ?? string.Empty,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                    };
                    _practicalExamService.AddPracticalExamCourseSubject(eq);
                }
                else
                {
                    if (_practicalExamService.CheckIfItHasStudents(PracticalExamCourseId, SubjectId))
                        return Json(new { success = false, message = "There is Student that have this Subject" });
                    else
                        _practicalExamService.RemovePracticalExamCourseSubject(examCourse);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Subject to Exam");
                return Json(new { success = false });
            }
        }

        [AuditLogFilter(ActionDescription = "Edit Subject Mark")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "EditSubjectMark")]
        public async Task<IActionResult> GetSubjectMark(int PracticalExamCourseId)
        {
            var practicalExamCourse = _practicalExamService.GetPracticalExamCourse(PracticalExamCourseId);
            return Json(practicalExamCourse);
        }

        [AuditLogFilter(ActionDescription = "Edit Subject Mark")]
        [CustomAuthentication(PageName = "Courses", PermissionKey = "EditSubjectMark")]
        public async Task<IActionResult> EditSubjectMark(int PracticalExamCourseId, int mark)
        {
            try
            {
                var practicalExamCourse = _practicalExamService.GetPracticalExamCourse(PracticalExamCourseId);
                _practicalExamService.EditPracticalExamCourseMark(practicalExamCourse, mark);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Edit Subject Mark");
                return null;
            }
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "AddAndEditQuiz")]
        public IActionResult RefetchQuizzes(int id)
        {
            try
            {
                _sectionOfCourseQuizService.RefetchCourseQuizzes(id, User?.Identity?.Name ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Refetching Quizzes from Course");
                return null;
            }
        }


        [CustomAuthentication(PageName = "CourseMarks", PermissionKey = "View")]
        public IActionResult ShowMarks(int id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.Id = id;

            var marks = _courseMarksService.GetCourseMarks(id, languageId);
            return PartialView("_Marks", marks);
        }

        [AuditLogFilter(ActionDescription = "Create Course Mark")]
        [CustomAuthentication(PageName = "CourseMarks", PermissionKey = "Create")]
        public async Task<IActionResult> CreateMark(CourseMarkViewModel courseMark)
        {
            try
            {
                courseMark.CreatedBy = User?.Identity?.Name ?? string.Empty;
                _courseMarksService.AddCourseMark(courseMark);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Creating Course Mark");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Edit Course Mark")]
        [CustomAuthentication(PageName = "CourseMarks", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEditCourseMark(int id ,int langId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (langId < 1)
                langId = languageId;

            var mark = _courseMarksService.GetCourseMarkById(id , langId);
            if (mark.Status != (int)GeneralEnums.StatusEnum.Deleted && mark != null)
            {
                return Json(mark);
            }
            return null;
        }

        [AuditLogFilter(ActionDescription = "Edit Course Mark")]
        [CustomAuthentication(PageName = "CourseMarks", PermissionKey = "Edit")]
        public async Task<IActionResult> EditCourseMark(CourseMarkViewModel courseMark)
        {
            try
            {
                var mark = _courseMarksService.GetCourseMarkById(courseMark.Id);
                if (mark.Status != (int)GeneralEnums.StatusEnum.Deleted && mark != null)
                {
                    _courseMarksService.EditCourseMark(courseMark, mark);
                    return Ok();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Edit Course Mark");
                return null;
            }
        }

        [AuditLogFilter(ActionDescription = "Delete Course Mark")]
        [CustomAuthentication(PageName = "CourseMarks", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCourseMark(int id)
        {
            try
            {
                var mark = _courseMarksService.GetCourseMarkById(id);
                if (mark.Status != (int)GeneralEnums.StatusEnum.Deleted && mark != null)
                {
                    _courseMarksService.DeleteCourseMark(mark);
                    return Ok();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Deleting Course Mark");
                return null;
            }
        }
    }
}
