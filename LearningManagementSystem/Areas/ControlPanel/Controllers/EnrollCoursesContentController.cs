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
using Microsoft.EntityFrameworkCore.Storage;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class EnrollCoursesContentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        private readonly IUserProfileService _userProfileService;
        private readonly ITrainerService _trainerService;
        private readonly ISectionOfCourseService _sectionOfCourseService;
        private readonly IAssignmentService _assignmentService;
        private readonly IEnrollAssignmentService _enrollAssignmentService;
        private readonly ILectureService _lectureService;
        private readonly ICourseResourceService _courseResourceService;

        public EnrollCoursesContentController(
            ICookieService cookieService,
            ILogService logService,
            IEnrollTeacherCourseService enrollTeacherCourseService,
            ISettingService settingService,
            IEnrollSectionOfCourseService enrollSectionOfCourseService,
            IEnrollLectureService enrollLectureService,
            IEnrollCourseResourceService enrollCourseResourceService,
            IUserProfileService userProfileService,
            ITrainerService trainerService, ISectionOfCourseService sectionOfCourseService, IAssignmentService assignmentService,
            IEnrollAssignmentService enrollAssignmentService, ILectureService lectureService, ICourseResourceService courseResourceService
            )
        {
            _logService = logService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _enrollLectureService = enrollLectureService;
            _enrollCourseResourceService = enrollCourseResourceService;
            _userProfileService = userProfileService;
            _trainerService = trainerService;
            _sectionOfCourseService = sectionOfCourseService;
            _assignmentService = assignmentService;
            _enrollAssignmentService = enrollAssignmentService;
            _lectureService = lectureService;
            _courseResourceService = courseResourceService;
        }
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Content List")]
        public async Task<IActionResult> GetData(int? page, int pagination, string table, int courseId)
        {
            ViewBag.CourseId = courseId;
            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.EnrollCoursesContentPagination);
            if (val == null && pagination == 0)
                pagination = 5;
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.EnrollCoursesContentPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "5");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "SectionName", "CourseId", "Description", "Lectures", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.EnrollCoursesContentTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollCoursesContentTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.EnrollCoursesContentTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            var course = _enrollTeacherCourseService.GetEnrollById(courseId, languageId);
            var result = _enrollSectionOfCourseService.GetEnrollSectionOfCourses(course?.TeacherId, 0, page, languageId, pagination , courseId);

            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Enroll Courses Content")]
        public async Task<IActionResult> Index()
        {
            return PartialView("_Index");
        }

        // GET: Trainer/EnrollCoursesContent/Create
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enroll Courses Content Create Get")]
        public IActionResult ShowCreate(int enrollTeacherCourseId)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            ViewBag.LangId = langId;
            ViewBag.EnrollTeacherCourseId = enrollTeacherCourseId;
            return PartialView("Create");
        }


        // POST: Trainer/EnrollCoursesContent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Enroll Courses Content Create Post")]
        public async Task<IActionResult> Create(
            EnrollCoursesContentViewModel EnrollCoursesContentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel == null)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.Id == 0)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel == null)
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.SectionName == null || s.SectionName == ""))
                        return Content("AddMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.EnrollLectureViewModel == null))
                        return Content("AddMassegeErrorInvalidLectureData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.EnrollLectureViewModel.Exists(b => b.LectureName == null || b.LectureName == "")))
                        return Content("AddMassegeErrorInvalidLectureData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Section in EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel)
                                {
                                    Section.LanguageId = (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                    Section.CreatedBy = User.Identity?.Name;
                                    Section.EnrollCourseId = EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.Id;
                                    var SectionAdded = _enrollSectionOfCourseService.AddEnrollSectionOfCourse_WithoutUsing(Section, context);

                                    foreach (var Lecture in Section.EnrollLectureViewModel)
                                    {
                                        Lecture.LanguageId = (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                        Lecture.CreatedBy = User.Identity?.Name;
                                        Lecture.EnrollSectionId = SectionAdded.Id;
                                        Lecture.EnrollCourseId = SectionAdded.EnrollCourseId;
                                        _enrollLectureService.AddEnrollLecture_WithoutUsing(Lecture, context);
                                    }
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Enroll Courses Content");
                                return Content("Fail");
                            }
                        }
                    }

                    return RedirectToAction(nameof(GetData));
                }

                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add Enroll Courses Content");
                    return Content("Fail");
                }
            }
            return View(EnrollCoursesContentViewModel);
        }


        // GET: Trainer/EnrollCoursesContent/Edit/5
        [AuditLogFilter(ActionDescription = "Enroll Courses Content Edit Get")]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? id, int page, int languageId = 0)
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

            var section = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById(id.Value, languageId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            section.LanguageId = languageId;
            ViewBag.LangId = languageId;

            var TeacherCourseDetails = _enrollTeacherCourseService.GetEnrollTeacherCourseById(section.EnrollCourseId, languageId);
            section.EnrollLecture = _enrollLectureService.GetEnrollLectureBySectionId(section.Id, languageId);
            var EnrollCoursesContentViewModel = new EnrollCoursesContentViewModel();

            var EnrollTeacherCourse = new EnrollTeacherCourseViewModel();
            EnrollTeacherCourse.Id = section.EnrollCourseId;
            EnrollTeacherCourse.LanguageId = section.LanguageId;
            EnrollTeacherCourse.CourseName = TeacherCourseDetails.CourseName;
            EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel = EnrollTeacherCourse;
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel = new List<EnrollSectionOfCourseViewModel>();
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Add(section);

            var course = _enrollTeacherCourseService.GetEnrollById(TeacherCourseDetails.Id, languageId);
            ViewBag.TrainerCourses = _enrollTeacherCourseService.GetEnrollTeacherCourseByTeacherId(course.TeacherId, languageId);
            return PartialView("Edit", EnrollCoursesContentViewModel);
        }

        // POST: Trainer/EnrollCoursesContent/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enroll Courses Content Edit Post")]
        public async Task<IActionResult> Edit(
            EnrollCoursesContentViewModel EnrollCoursesContentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel == null)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.Id == 0)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel == null)
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.SectionName == null || s.SectionName == ""))
                        return Content("EditMassegeErrorInvalidSectionData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.EnrollLectureViewModel == null))
                        return Content("EditMassegeErrorInvalidLectureData");

                    if (EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Exists(s => s.EnrollLectureViewModel.Exists(b => b.LectureName == null || b.LectureName == "")))
                        return Content("EditMassegeErrorInvalidLectureData");

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                foreach (var Section in EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel)
                                {
                                    //Delete lectures
                                    var ExistsAllLectureINSection = _enrollLectureService.GetEnrollLectureBySectionId_WithoutUsing(Section.Id, context);
                                    foreach (var Lecture in ExistsAllLectureINSection)
                                    {
                                        if (!Section.EnrollLectureViewModel.Exists(e => e.ForEditModleID == Lecture.Id))
                                            _enrollLectureService.DeleteEnrollLecture_WithoutUsing(Lecture, context);
                                    }

                                    var ExistsSection = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById_WithoutUsing(Section.Id, context);
                                    Section.LanguageId = (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                    Section.CreatedBy = User.Identity?.Name;
                                    Section.EnrollCourseId = EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.Id;
                                    _enrollSectionOfCourseService.EditEnrollSectionOfCourse_WithoutUsing(Section, ExistsSection, context);

                                    foreach (var Lecture in Section.EnrollLectureViewModel)
                                    {
                                        Lecture.LanguageId = (EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel.LanguageId;
                                        Lecture.CreatedBy = User.Identity?.Name;
                                        Lecture.EnrollSectionId = Section.Id;

                                        if (Lecture.ForEditModleID != null)//Edit lecture
                                        {
                                            Lecture.Id = (int)Lecture.ForEditModleID;
                                            Lecture.LanguageId = Section.LanguageId;
                                            Lecture.EnrollCourseId = Section.EnrollCourseId;
                                            var ExistsLecture = _enrollLectureService.GetEnrollLectureById_WithoutUsing((int)Lecture.ForEditModleID, context);
                                            _enrollLectureService.EditEnrollLecture_WithoutUsing(Lecture, ExistsLecture, context);
                                        }
                                        else //Add New lecture
                                        {
                                            Lecture.CreatedBy = User.Identity?.Name;
                                            Lecture.LanguageId = Section.LanguageId;
                                            Lecture.EnrollSectionId = Section.Id;
                                            Lecture.EnrollCourseId = Section.EnrollCourseId;
                                            _enrollLectureService.AddEnrollLecture_WithoutUsing(Lecture, context);
                                        }
                                    }
                                }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit Enroll Courses Content");
                                return Content("Fail");
                            }
                        }
                    }
                    return RedirectToAction(nameof(GetData));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Edit Enroll Courses Content");
                    return Content("Fail");
                }
            }
            return View(EnrollCoursesContentViewModel);
        }


        [AuditLogFilter(ActionDescription = "Courses Content Delete Get")]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Delete")]
        // GET: Trainer/EnrollCoursesContent/Delete/5
        public async Task<IActionResult> ShowDelete(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var section = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById(id.Value, langId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }
            var Course = _enrollTeacherCourseService.GetEnrollTeacherCourseById(section.EnrollCourseId, langId);
            section.EnrollLecture = _enrollLectureService.GetEnrollLectureBySectionId(section.Id, langId);
            var EnrollCoursesContentViewModel = new EnrollCoursesContentViewModel();

            var EnrollTeacherCourse = new EnrollTeacherCourseViewModel();
            EnrollTeacherCourse.Id = section.EnrollCourseId;
            EnrollTeacherCourse.LanguageId = section.LanguageId;
            EnrollTeacherCourse.CourseName = Course.CourseName;

            EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel = EnrollTeacherCourse;
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel = new List<EnrollSectionOfCourseViewModel>();
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Add(section);
            ViewBag.LangId = langId;
            return PartialView("Delete", EnrollCoursesContentViewModel);
        }

        // POST: Trainer/EnrollCoursesContent/Delete/5
        [AuditLogFilter(ActionDescription = "Courses Content Delete Post")]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Delete")]
        public async Task<IActionResult> DeleteCoursesContent(int id, int Page)
        {
            var SectionOfCourse = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById(id);
            if (SectionOfCourse != null && SectionOfCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _enrollCourseResourceService.DeleteEnrollCourseResourceBySectionId_WithoutUsing(SectionOfCourse.Id, context);
                            _enrollLectureService.DeleteEnrollLLectureBySectionID_WithoutUsing(SectionOfCourse.Id, context);
                            _enrollSectionOfCourseService.DeleteEnrollSectionOfCourse_WithoutUsing(SectionOfCourse, context);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Delete Courses Content");
                            return Content("Fail");
                        }
                    }
                }
            }

            return RedirectToAction(nameof(GetData), new { page = Page });
        }

        // GET: Trainer/EnrollCoursesContent/Details/5
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Content Details")]
        public async Task<IActionResult> ShowDetails(int? id, int page)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var section = _enrollSectionOfCourseService.GetEnrollSectionOfCourseById(id.Value, langId);
            if (section == null || section.Status == (int)GeneralEnums.StatusEnum.Deleted)
            {
                return NotFound();
            }

            var Course = _enrollTeacherCourseService.GetEnrollTeacherCourseById(section.EnrollCourseId, langId);
            section.EnrollLecture = _enrollLectureService.GetEnrollLectureBySectionId(section.Id, langId);
            var EnrollCoursesContentViewModel = new EnrollCoursesContentViewModel();
            var EnrollTeacherCourse = new EnrollTeacherCourseViewModel();
            EnrollTeacherCourse.Id = section.EnrollCourseId;
            EnrollTeacherCourse.LanguageId = section.LanguageId;
            EnrollTeacherCourse.CourseName = Course.CourseName;
            EnrollCoursesContentViewModel.EnrollTeacherCourseViewModel = EnrollTeacherCourse;
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel = new List<EnrollSectionOfCourseViewModel>();
            EnrollCoursesContentViewModel.EnrollSectionOfCourseViewModel.Add(section);

            ViewBag.LangId = langId;
            return PartialView("Details", EnrollCoursesContentViewModel);
        }

        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
    }
}
