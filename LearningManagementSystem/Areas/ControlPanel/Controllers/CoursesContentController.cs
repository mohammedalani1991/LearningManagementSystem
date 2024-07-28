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

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CoursesContentController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _coursecategoryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ISectionOfCourseService _sectionOfCourseService;
        private readonly ILectureService _lectureService;
        private readonly ICourseResourceService _courseResourceService;
        private readonly ISectionOfCourseQuizService _sectionOfCourseQuizService;

        public CoursesContentController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            ICourseCategoryService coursecategoryService,
            ISectionOfCourseService sectionOfCourseService,
            ILectureService lectureService,
            ICourseResourceService courseResourceService, ISectionOfCourseQuizService sectionOfCourseQuizService
            )
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _coursecategoryService = coursecategoryService;
            _sectionOfCourseService = sectionOfCourseService;
            _lectureService = lectureService;
            _courseResourceService = courseResourceService;
            _sectionOfCourseQuizService = sectionOfCourseQuizService;
        }
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Content List")]
        public async Task<IActionResult> GetData(int page, string searchText, int pagination, string table, int? CourseId, string hdCourseName)
        {

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ViewBag.searchText = searchText;
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

            if (page == 0)
                page = 1;

            ViewBag.page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.CoursesContentPagination);
            if (val == null && pagination == 0)
                pagination = 5;
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CoursesContentPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "5");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "SectionName", "CourseId", "Description", "Lectures", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CoursesContentTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursesContentTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursesContentTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            //ViewBag.ListCourse = _courseService.GetAllCourses(languageId);
            var result = _sectionOfCourseService.GetSectionOfCourses(searchText, page, languageId, pagination, CourseId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Courses Content")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ControlPanel/CoursesContent/Create
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Courses Content Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.LangId = languageId;
            return PartialView("Create");
        }

        // GET: ControlPanel/CoursesContent/CourseSearch
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Content  Get")]
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


        // POST: ControlPanel/CoursesContent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Courses Content Create Post")]
        public async Task<IActionResult> Create(
            CoursesContentViewModel CoursesContentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CoursesContentViewModel.CourseViewModel == null)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.CourseViewModel.Id == 0)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel == null)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.SectionName == null || s.SectionName == ""))
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.LectureViewModel == null))
                        return Content("AddMassegeErrorInvalidLectureData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.LectureViewModel.Exists(b => b.LectureName == null || b.LectureName == "")))
                        return Content("AddMassegeErrorInvalidLectureData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Section in CoursesContentViewModel.SectionOfCourseViewModel)
                                {
                                    Section.LanguageId = (CoursesContentViewModel.CourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : CoursesContentViewModel.CourseViewModel.LanguageId;
                                    Section.CreatedBy = User.Identity?.Name;
                                    Section.CourseId = CoursesContentViewModel.CourseViewModel.Id;
                                    Section.Status = CoursesContentViewModel.CourseViewModel.Status;
                                    var SectionAdded = _sectionOfCourseService.AddSectionOfCourse_WithoutUsing(Section, context);

                                    foreach (var Lecture in Section.LectureViewModel)
                                    {
                                        Lecture.LanguageId = (CoursesContentViewModel.CourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : CoursesContentViewModel.CourseViewModel.LanguageId;
                                        Lecture.CreatedBy = User.Identity?.Name;
                                        Lecture.SectionId = SectionAdded.Id;
                                        Lecture.Status = CoursesContentViewModel.CourseViewModel.Status;
                                        _lectureService.AddLecture_WithoutUsing(Lecture, context);
                                    }
                                }
                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Courses Content Dic");
                                return View(CoursesContentViewModel);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Contents Dic");
                    return View(CoursesContentViewModel);
                }
            }
            return View(CoursesContentViewModel);
        }


        // GET: ControlPanel/CoursesContent/Edit/5
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Courses Content Edit Get")]
        public async Task<IActionResult> ShowEdit(int? id, int languageId = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var section = _sectionOfCourseService.GetSectionOfCourseById(id.Value, languageId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            section.LanguageId = languageId;
            ViewBag.LangId = languageId;

            var Course = _courseService.GetCourseById(section.CourseId, languageId);
            if (Course != null)
            {
                SelectListItem selListItem = new SelectListItem() { Value = Course.Id.ToString(), Text = Course.CourseName };
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(selListItem);
                ViewBag.SelectedCourse = newList;
            }
            else
            {
                SelectListItem selListItem = new SelectListItem() { Value = "", Text = "" };
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(selListItem);
                ViewBag.SelectedCourse = newList;
            }

            section.Listlecture = _lectureService.GetLectureBySectionId(section.Id, languageId);

            var CoursesContentViewModel = new CoursesContentViewModel();

            var Courses = new CourseViewModel();
            Courses.Id = section.CourseId;
            Courses.LanguageId = section.LanguageId;
            Courses.Status = section.Status;
            if (Course != null)
                Courses.CourseName = Course.CourseName;
            CoursesContentViewModel.CourseViewModel = Courses;

            CoursesContentViewModel.SectionOfCourseViewModel = new List<SectionOfCourseViewModel>();
            CoursesContentViewModel.SectionOfCourseViewModel.Add(section);
            return PartialView("Edit", CoursesContentViewModel);
        }

        // POST: ControlPanel/CoursesContent/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Courses Content Edit Post")]
        public async Task<IActionResult> Edit(
            CoursesContentViewModel CoursesContentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CoursesContentViewModel.CourseViewModel == null)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.CourseViewModel.Id == 0)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel == null)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.SectionName == null || s.SectionName == ""))
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.LectureViewModel == null))
                        return Content("EditMassegeErrorInvalidLectureData");

                    if (CoursesContentViewModel.SectionOfCourseViewModel.Exists(s => s.LectureViewModel.Exists(b => b.LectureName == null || b.LectureName == "")))
                        return Content("EditMassegeErrorInvalidLectureData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Section in CoursesContentViewModel.SectionOfCourseViewModel)
                                {
                                    //Delete lectures
                                    var ExistsAllLectureINSection = _lectureService.GetLectureBySectionId_WithoutUsing(Section.Id, context);
                                    foreach (var Lecture in ExistsAllLectureINSection)
                                    {
                                        if (!Section.LectureViewModel.Exists(e => e.ForEditModleID == Lecture.Id))
                                            _lectureService.DeleteLecture_WithoutUsing(Lecture, context);
                                    }

                                    var ExistsSection = _sectionOfCourseService.GetSectionOfCourseById_WithoutUsing(Section.Id, context);
                                    Section.LanguageId = (CoursesContentViewModel.CourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : CoursesContentViewModel.CourseViewModel.LanguageId;
                                    Section.CreatedBy = User.Identity?.Name;
                                    Section.CourseId = CoursesContentViewModel.CourseViewModel.Id;
                                    Section.Status = CoursesContentViewModel.CourseViewModel.Status;
                                    _sectionOfCourseService.EditSectionOfCourse_WithoutUsing(Section, ExistsSection, context);

                                    foreach (var Lecture in Section.LectureViewModel)
                                    {
                                        Lecture.LanguageId = (CoursesContentViewModel.CourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : CoursesContentViewModel.CourseViewModel.LanguageId;
                                        Lecture.CreatedBy = User.Identity?.Name;
                                        Lecture.SectionId = Section.Id;
                                        Lecture.Status = Section.Status;

                                        if (Lecture.ForEditModleID != null)//Edit lecture
                                        {
                                            Lecture.Id = (int)Lecture.ForEditModleID;
                                            Lecture.LanguageId = Section.LanguageId;
                                            Lecture.SectionId = Section.Id;
                                            Lecture.Status = Section.Status;
                                            var ExistsLecture = _lectureService.GetLectureById_WithoutUsing((int)Lecture.ForEditModleID, context);
                                            _lectureService.EditLecture_WithoutUsing(Lecture, ExistsLecture, context);
                                        }
                                        else //Add New lecture
                                        {
                                            Lecture.SectionId = Section.Id;
                                            Lecture.LanguageId = Section.LanguageId;
                                            Lecture.CreatedBy = User.Identity?.Name;
                                            Lecture.Status = Section.Status;

                                            _lectureService.AddLecture_WithoutUsing(Lecture, context);
                                        }
                                    }
                                }
                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit  Courses Content Dic");
                                return View(CoursesContentViewModel);
                            }
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit  Courses Content Dic");
                    return View(CoursesContentViewModel);
                }
            }
            return View(CoursesContentViewModel);
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Courses Content Delete Get")]
        // GET: ControlPanel/CoursesContent/Delete/5
        public async Task<IActionResult> ShowDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var section = _sectionOfCourseService.GetSectionOfCourseById(id.Value, langId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var Course = _courseService.GetCourseById(section.CourseId, langId);
            section.Listlecture = _lectureService.GetLectureBySectionId(section.Id, langId);
            var CoursesContentViewModel = new CoursesContentViewModel();
            var Courses = new CourseViewModel();
            if (Course != null)
            {
                Courses.Id = Course.Id;
                Courses.CourseName = Course.CourseName;
            }
            Courses.LanguageId = section.LanguageId;
            CoursesContentViewModel.CourseViewModel = Courses;
            CoursesContentViewModel.SectionOfCourseViewModel = new List<SectionOfCourseViewModel>();
            CoursesContentViewModel.SectionOfCourseViewModel.Add(section);



            ViewBag.LangId = langId;
            return PartialView("Delete", CoursesContentViewModel);
        }

        // POST: ControlPanel/CoursesContent/Delete/5
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Courses Content Delete Post")]
        public async Task<IActionResult> DeleteCoursesContent(int id)
        {

            var SectionOfCourse = _sectionOfCourseService.GetSectionOfCourseById(id);
            if (SectionOfCourse != null && SectionOfCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _courseResourceService.DeleteCourseResourceBySectionId(SectionOfCourse.Id, true, context);
                            _lectureService.DeleteLectureBySectionID(SectionOfCourse.Id, true, context);
                            _sectionOfCourseQuizService.DeleteQuizzesBySectionID(SectionOfCourse.Id);
                            _sectionOfCourseService.DeleteSectionOfCourse(SectionOfCourse, true, context);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "DeleteCoursesContent");
                            return Content("Fail");
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ControlPanel/CoursesContent/Details/5
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Content Details")]
        public async Task<IActionResult> ShowDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var section = _sectionOfCourseService.GetSectionOfCourseById(id.Value, langId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var Course = _courseService.GetCourseById(section.CourseId, langId);
            section.Listlecture = _lectureService.GetLectureBySectionId(section.Id, langId);
            var CoursesContentViewModel = new CoursesContentViewModel();
            var Courses = new CourseViewModel();
            if (Course != null)
            {
                Courses.Id = Course.Id;
                Courses.CourseName = Course.CourseName;
            }
            Courses.LanguageId = section.LanguageId;
            CoursesContentViewModel.CourseViewModel = Courses;
            CoursesContentViewModel.SectionOfCourseViewModel = new List<SectionOfCourseViewModel>();
            CoursesContentViewModel.SectionOfCourseViewModel.Add(section);



            ViewBag.LangId = langId;
            return PartialView("Details", CoursesContentViewModel);
        }
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "AddAndEditQuiz")]
        public IActionResult GetQuizzes(int id, int? page)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            ViewBag.SectionOfCourseId = id;

            var quizzes = _sectionOfCourseQuizService.GetQuizzes(id, page ?? 1, languageId);
            return PartialView("_Quizzes", quizzes);
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "AddAndEditQuiz")]
        public IActionResult RefetchQuizzes(int id)
        {
            try
            {
                _sectionOfCourseQuizService.RefetchQuizzes(id, User?.Identity?.Name ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Refetching Quizzes");
                return null;
            }
        }

        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "AddAndEditQuiz")]
        public async Task<IActionResult> AddOrRemoveQuiz(int quizId , int quizNum, string checkedRow)
        {
            try
            {
                var quiz = _sectionOfCourseQuizService.GetQuizById(quizId);
                if (quiz == null) return Content("error");

                _sectionOfCourseQuizService.EditQuiz(quiz, quizNum, checkedRow == "true");
                return Content("Ok");
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While adding Quiz");
                return Content("error");
            }
        }
    }
}
