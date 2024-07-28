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
using System.Transactions;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class LecturesContentController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _coursecategoryService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly ISectionOfCourseService _sectionOfCourseService;
        private readonly ILectureService _lectureService;
        private readonly ICourseResourceService _courseResourceService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LecturesContentController(
            ICookieService cookieService, ILogService logService,
            ICourseService courseService, ISettingService settingService, 
            ICourseCategoryService coursecategoryService,
            ISectionOfCourseService sectionOfCourseService,
            ILectureService lectureService,
            ICourseResourceService courseResourceService,
            IWebHostEnvironment hostingEnvironment
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
            _hostingEnvironment = hostingEnvironment;
        }



        // GET: ControlPanel/LecturesContent/Edit/5
        [AuditLogFilter(ActionDescription = "Lectures Content Edit Get")]
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Edit")]
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
          
            ViewBag.LangId = languageId;
           
            var SectionOfCourse = new SectionOfCourseViewModel();
            SectionOfCourse.Id = section.Id;
            SectionOfCourse.SectionName = section.SectionName;
            SectionOfCourse.ForEditModleID = section.Id;
            SectionOfCourse.CourseId = section.CourseId;
            SectionOfCourse.LanguageId = languageId;

            var LecturesContentViewModel = new LecturesContentViewModel();
            LecturesContentViewModel.SectionOfCourseViewModel = SectionOfCourse;
            LecturesContentViewModel.LectureViewModel = new List<LectureViewModel>();
            var lectures = _lectureService.GetLectureBySectionId(section.Id, languageId);
            foreach (var lecture in lectures)
            {
                
                LecturesContentViewModel.LectureViewModel.Add(new LectureViewModel(lecture));
            }
            foreach (var LecturesViewModel in LecturesContentViewModel.LectureViewModel)
            {
                LecturesViewModel.CourseResourceViewModel = new List<CourseResourceViewModel>();
                var Resources = _courseResourceService.GetCourseResourceByLectureId(LecturesViewModel.Id, languageId);
                foreach(var Resource in Resources)
                LecturesViewModel.CourseResourceViewModel.Add(new CourseResourceViewModel(Resource));
            }
            return PartialView("Edit", LecturesContentViewModel);
        }

        
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lectures Content Download")]
        public ActionResult DownloadDocument(string filePath)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                var fullPath = HttpUtility.UrlDecode(filePath);
                var fileName = Path.GetFileName(fullPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fullPath);
                return File(fileBytes, "application/force-download", fileName);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Download Lectures Documents");
                 return NotFound();
            }
        }
        // POST: ControlPanel/LecturesContent/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "CoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Lectures Content Edit Post")]
        public async Task<IActionResult> Edit(
            LecturesContentViewModel LecturesContentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    using (var context = new LearningManagementSystemContext())
                    {
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var Lecture in LecturesContentViewModel.LectureViewModel)
                                {
                                    //Delete Resource
                                    var ExistsAllResourceINLectures = _courseResourceService.GetCourseResourceByLectureId_WithoutUsing((int)Lecture.ForEditModleID, context);
                                    foreach (var Resource in ExistsAllResourceINLectures)
                                    {
                                        if (Lecture.CourseResourceViewModel != null)
                                        {
                                            if (!Lecture.CourseResourceViewModel.Exists(e => e.ForEditModleID == Resource.Id))
                                                _courseResourceService.DeleteCourseResource_WithoutUsing(Resource, context);
                                        }
                                        else
                                        {
                                            _courseResourceService.DeleteCourseResourceByLectureId_WithoutUsing((int)Lecture.ForEditModleID, context);
                                            break;
                                        }
                                    }

                                    if (Lecture.CourseResourceViewModel != null)
                                    {
                                        foreach (var Resource in Lecture.CourseResourceViewModel)
                                        {

                                            Resource.LanguageId = (LecturesContentViewModel.SectionOfCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : LecturesContentViewModel.SectionOfCourseViewModel.LanguageId;
                                            Resource.CreatedBy = User.Identity?.Name;
                                            Resource.LectureId = (int)Lecture.ForEditModleID;
                                            Resource.CourseId = LecturesContentViewModel.SectionOfCourseViewModel.CourseId;
                                            if (Resource.Source == (int)GeneralEnums.ResourceSourceEnum.UploadFile)
                                                Resource.Type = LookupHelper.GetFileType(Path.GetExtension(Resource.Link));
                                            else
                                                Resource.Type = Resource.Type;

                                            Resource.Link = Resource.Link;
                                            Resource.Description = Resource.Description;
                                            Resource.Name = Resource.Name;

                                            if (Resource.ForEditModleID != null)//Edit Resource
                                            {
                                                Resource.Id = (int)Resource.ForEditModleID;
                                                var ExistsResource = _courseResourceService.GetCourseResourceById_WithoutUsing(Resource.Id, context);
                                                _courseResourceService.EditCourseResource_WithoutUsing(Resource, ExistsResource, context);
                                            }
                                            else //Add New Resource
                                            {
                                                _courseResourceService.AddCourseResource_WithoutUsing(Resource, context);
                                            }
                                        }
                                    }
                                }
                                transaction.Commit();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Lectures Content Dic");
                                return Content("Fail");
                            }
                        }
                    }
                    return Content("Done");
                }
                catch (Exception ex)
                {
                    _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while add new Lectures Content Dic");
                    return Content("Fail");
                }
            }
            return View(LecturesContentViewModel);
        }

     
    }
}
