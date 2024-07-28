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
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class CoursePackagesController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ICoursePackagesService _CoursePackagesService;
        private readonly ITrainerService _trainerService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;

        public CoursePackagesController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService,
            ICoursePackagesService CoursePackagesService ,
            ITrainerService trainerService,
            IEnrollTeacherCourseService enrollTeacherCourseService
            )
        {
            _logService = logService;
            _courseService = courseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _CoursePackagesService = CoursePackagesService;
            _trainerService = trainerService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
        }
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Packages List")]
        [CheckSuperAdmin(PageName = "Packages")]
        public async Task<IActionResult> GetData(int? page, string searchText, int pagination, string table,int? CourseId,string hdCourseName)
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

            ViewBag.Page = page;

            var val = _cookieService.GetCookie(Constants.Pagenation.CoursePackagesPagination);
            if (val == null && pagination == 0)
                pagination = 5;
            else if (pagination != 0)
                pagination = Int32.Parse(_cookieService.CreateCookie(Constants.Pagenation.CoursePackagesPagination, pagination.ToString(), 7));
            else
                pagination = int.Parse(val != "" ? val : "5");

            ViewBag.PaginationValue = pagination;

            List<string> tables = new List<string> { "Id", "PackageName", "TrainerName", "Courses", "Status", "CreatedOn", "CreatedBy" };

            var val1 = _cookieService.GetCookie(Constants.TableFields.CoursePackagesTable);

            if (val1 == null && table == null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePackagesTable, tables, 7);
            else if (table != null)
                val1 = _cookieService.CreateCookie(Constants.TableFields.CoursePackagesTable, table, 7);


            ViewBag.Table = val1;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var languageId = CultureHelper.GetCurrentLanguageId(requestCulture);
            ViewBag.LangId = languageId;
            
            var result =await _CoursePackagesService.GetCoursePackages(true , searchText, page, languageId, pagination,0, CourseId);
            return PartialView("_Index", result);
        }

        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "List Course Packages")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        public IActionResult ShowTable()
        {
            return PartialView("_Table");
        }
        // GET: ControlPanel/Assignment/CourseSearch
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Course Packages  Get")]
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

        // GET: ControlPanel/Assignment/TrainerCourses
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Trainer Courses Get")]
        public IActionResult TrainerCourses(int TrainerId, int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var EnrollTeacherCourse = _enrollTeacherCourseService.GetEnrollTeacherCourseByTeacherId(TrainerId, languageId);
            return Json(EnrollTeacherCourse);
        }

        // GET: ControlPanel/CoursePackages/Create
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Packages Create Get")]
        public IActionResult ShowCreate(int languageId = 0)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            ViewBag.LangId = languageId;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            return PartialView("Create");
        }

        // POST: ControlPanel/CoursePackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Create")]
        [AuditLogFilter(ActionDescription = "Course Packages Create Post")]
        public async Task<IActionResult> Create(
            CoursePackagesGeneralViewModel CoursePackagesGeneralViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel.Exists(s => s.CoursePackagesRelations == null))
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel.Exists(s => s.CoursePackagesRelations.Count == 0))
                        return Content("AddMassegeErrorInvalidData");

                  

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Packages in CoursePackagesGeneralViewModel.CoursePackageViewModel)
                                {
                                    var CoursePackagesViewModel = new CoursePackageViewModel()
                                    {

                                        CreatedBy = User.Identity?.Name,
                                        PackageName = Packages.PackageName,
                                        LanguageId= CoursePackagesGeneralViewModel.LanguageId,
                                        Status = CoursePackagesGeneralViewModel.Status
                                    };
                                    var _CoursePackage= _CoursePackagesService.AddCoursePackage(CoursePackagesViewModel, context);
                                    if (_CoursePackage == null || _CoursePackage.Id <=0)
                                        throw new Exception();

                                    foreach (var CoursePackagesRelations in Packages.CoursePackagesRelations)
                                    {
                                        var CoursePackagesRelationsViewModel = new CoursePackagesRelationsViewModel()
                                        {
                                            CourseId = CoursePackagesRelations.CourseId,
                                            CoursePackagesId = _CoursePackage.Id
                                        };
                                      var _CoursePackagesRelations =  _CoursePackagesService.AddCoursePackagesRelation(CoursePackagesRelationsViewModel, context);

                                    }
                                }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Packages Dic");
                                return Content("Fail");
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Course Packages Dic");
                    return View(CoursePackagesGeneralViewModel);
                }
            }
            return View(CoursePackagesGeneralViewModel);
        }

        // GET: ControlPanel/CoursePackages/Edit/5
        [AuditLogFilter(ActionDescription = "Courses Prerequisite Edit Get")]
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Edit")]
        public async Task<IActionResult> ShowEdit(int? CoursePackageId, int languageId = 0)
        {
            if (CoursePackageId == null)
            {
                return NotFound();
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);
            if (languageId == 0)
            {
                languageId = langId;
            }

            var CoursePackages = _CoursePackagesService.GetCoursePackageById(CoursePackageId.Value, languageId);
            if (CoursePackages == null)
            {
                return NotFound();
            }


            var CoursePackagesList = new CoursePackagesGeneralViewModel();
            CoursePackagesList.CoursePackageViewModel = new List<CoursePackageViewModel>();
            CoursePackagesList.CoursePackageViewModel.Add(new CoursePackageViewModel(CoursePackages));

            foreach (var _CoursePackagesRelations in CoursePackagesList.CoursePackageViewModel)
                _CoursePackagesRelations.CoursePackagesRelations = _CoursePackagesService.GetCoursePackagesRelationByPackageId(_CoursePackagesRelations.Id, languageId);
                
            ViewBag.LangId = languageId;
            ViewBag.Status = CoursePackages.Status;
            ViewBag.ListTrainers = _trainerService.GetTrainers(languageId);
            return PartialView("Edit", CoursePackagesList);
        }




        // POST: ControlPanel/CoursePackages/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Course Packages Edit Post")]
        public async Task<IActionResult> Edit(
            CoursePackagesGeneralViewModel CoursePackagesGeneralViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel == null)
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel.Exists(s => s.CoursePackagesRelations == null))
                        return Content("AddMassegeErrorInvalidData");

                    if (CoursePackagesGeneralViewModel.CoursePackageViewModel.Exists(s => s.CoursePackagesRelations.Count == 0))
                        return Content("AddMassegeErrorInvalidData");



                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                foreach (var Packages in CoursePackagesGeneralViewModel.CoursePackageViewModel)
                                {
                                    var CoursePackagesViewModel = new CoursePackageViewModel()
                                    {

                                        CreatedBy = User.Identity?.Name,
                                        PackageName = Packages.PackageName,
                                        LanguageId = CoursePackagesGeneralViewModel.LanguageId,
                                        Status = CoursePackagesGeneralViewModel.Status
                                    };
                                    if (Packages.ForEditModleID > 0)
                                    {

                                        var ExistsCoursePackages = _CoursePackagesService.GetCoursePackageById(Packages.ForEditModleID);
                                        CoursePackagesViewModel.Id = Packages.ForEditModleID;
                                        var _CoursePackage = _CoursePackagesService.EditCoursePackage(CoursePackagesViewModel, ExistsCoursePackages, context);
                                        if (_CoursePackage == null || _CoursePackage.Id <= 0)
                                            throw new Exception();

                                        _CoursePackagesService.DeleteCoursePackageRelation(_CoursePackage.Id, context);
                                        foreach (var CoursePackagesRelations in Packages.CoursePackagesRelations)
                                        {
                                            var CoursePackagesRelationsViewModel = new CoursePackagesRelationsViewModel()
                                            {

                                                CourseId = CoursePackagesRelations.CourseId,
                                                CoursePackagesId = _CoursePackage.Id
                                            };
                                            var _CoursePackagesRelations = _CoursePackagesService.AddCoursePackagesRelation(CoursePackagesRelationsViewModel, context);

                                        }
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while edit new Course Packages Dic");
                                return Content("Fail");
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while edit new Course Packages Dic");
                    return View(CoursePackagesGeneralViewModel);
                }
            }
            return View(CoursePackagesGeneralViewModel);

        }

        // GET: ControlPanel/CoursePackages/Details/5
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Packages Details")]
        public async Task<IActionResult> ShowDetails(int? CoursePackageId, int page)
        {
            if (CoursePackageId == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var CoursePackages = _CoursePackagesService.GetCoursePackageById(CoursePackageId.Value, langId);
            if (CoursePackages == null)
            {
                return NotFound();
            }
            var CoursePackagesList = new CoursePackagesGeneralViewModel();
            CoursePackagesList.CoursePackageViewModel = new List<CoursePackageViewModel>();
            CoursePackagesList.CoursePackageViewModel.Add(new CoursePackageViewModel(CoursePackages));

            foreach (var _CoursePackagesRelations in CoursePackagesList.CoursePackageViewModel)
                _CoursePackagesRelations.CoursePackagesRelations = _CoursePackagesService.GetCoursePackagesRelationByPackageId(_CoursePackagesRelations.Id, langId);

            ViewBag.LangId = langId;
            ViewBag.Status = CoursePackages.Status;


            return PartialView("Details", CoursePackagesList);
        }

        // GET: ControlPanel/CoursePackages/Details/5
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "View")]
        [AuditLogFilter(ActionDescription = "Courses Packages Details")]
        public async Task<IActionResult> ShowDelete(int? CoursePackageId, int page)
        {
            if (CoursePackageId == null)
            {
                return NotFound();
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var langId = CultureHelper.GetCurrentLanguageId(requestCulture);

            var CoursePackages = _CoursePackagesService.GetCoursePackageById(CoursePackageId.Value, langId);
            if (CoursePackages == null)
            {
                return NotFound();
            }
            var CoursePackagesList = new CoursePackagesGeneralViewModel();
            CoursePackagesList.CoursePackageViewModel = new List<CoursePackageViewModel>();
            CoursePackagesList.CoursePackageViewModel.Add(new CoursePackageViewModel(CoursePackages));

            foreach (var _CoursePackagesRelations in CoursePackagesList.CoursePackageViewModel)
                _CoursePackagesRelations.CoursePackagesRelations = _CoursePackagesService.GetCoursePackagesRelationByPackageId(_CoursePackagesRelations.Id, langId);

            ViewBag.LangId = langId;
            ViewBag.Status = CoursePackages.Status;


            return PartialView("Delete", CoursePackagesList);
        }

        // POST: ControlPanel/CoursePackages/Delete/5
        [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Delete")]
        [AuditLogFilter(ActionDescription = "Course Packages Delete Post")]
        public async Task<IActionResult> DeleteCoursePackages(int id)
        {

            var CoursePackages = _CoursePackagesService.GetCoursePackageById(id);
            if (CoursePackages != null && CoursePackages.Status != (int)GeneralEnums.StatusEnum.Deleted)
            {
                using (var context = new LearningManagementSystemContext())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _CoursePackagesService.DeleteCoursePackageRelation(id, context);
                            _CoursePackagesService.DeleteCoursePackageTranslations(id, context);
                            _CoursePackagesService.DeleteCoursePackages(id, context);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Course Packages Delete");
                            return Content("Fail");
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
        /*




 // POST: ControlPanel/Permissions/Delete/5
 [CustomAuthentication(PageName = "CoursePackages", PermissionKey = "Delete")]
 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id)
 {
     try
     {
         var CoursePackagess = _CoursePackagesService.GetCoursePackagesByCourseId(id);
         if (CoursePackagess != null)
         {
             _CoursePackagesService.DeleteCoursePackagess(CoursePackagess);
             return Json(true);
         }
         return null;
     }
     catch (Exception ex)
     {
         LogHelper.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Delete CoursePackages");
         return null;
     }
 }
*/
    }
}
