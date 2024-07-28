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
using System.Collections.Generic;
using System.Transactions;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using System.IO;

namespace LearningManagementSystem.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class EnrollLecturesContentController : Controller
    {
        private readonly ILogService _logService;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly ICookieService _cookieService;
        private readonly ISettingService _settingService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly IEnrollLectureService _enrollLectureService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EnrollLecturesContentController(
            ICookieService cookieService,
            ILogService logService,
            IEnrollTeacherCourseService enrollTeacherCourseService, 
            ISettingService settingService, 
            IEnrollSectionOfCourseService enrollSectionOfCourseService,
            IEnrollLectureService enrollLectureService,
            IEnrollCourseResourceService enrollCourseResourceService,
             IWebHostEnvironment hostingEnvironment
            )
        {
            _logService = logService;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _cookieService = cookieService;
            _settingService = settingService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _enrollLectureService = enrollLectureService;
            _enrollCourseResourceService = enrollCourseResourceService;
            _hostingEnvironment = hostingEnvironment;
        }

        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enroll Lectures Content Download")]
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
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error while Download Enroll Lectures Documents");
                return NotFound();
            }
        }

        // GET: ControlPanel/LecturesContent/Edit/5
        [AuditLogFilter(ActionDescription = "Enroll Lectures Content Edit Get")]
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
            section.Page = page;
            ViewBag.LangId = languageId;
           
            var SectionOfCourse = new EnrollSectionOfCourseViewModel();
            SectionOfCourse.Id = section.Id;
            SectionOfCourse.SectionName = section.SectionName;
            SectionOfCourse.ForEditModleID = section.Id;
            SectionOfCourse.EnrollCourseId = section.EnrollCourseId;
            SectionOfCourse.LanguageId = languageId;

            var LecturesContentViewModel = new EnrollLecturesContentViewModel();
            LecturesContentViewModel.EnrollSectionOfCourseViewModel = SectionOfCourse;
            LecturesContentViewModel.EnrollLectureViewModel = new List<EnrollLectureViewModel>();
            var lectures = _enrollLectureService.GetEnrollLectureBySectionId(section.Id, languageId);
            foreach (var lecture in lectures)
            {
                
                LecturesContentViewModel.EnrollLectureViewModel.Add(new EnrollLectureViewModel(lecture));
            }
            foreach (var LecturesViewModel in LecturesContentViewModel.EnrollLectureViewModel)
            {
                LecturesViewModel.EnrollCourseResourceViewModel = new List<EnrollCourseResourceViewModel>();
                var Resources = _enrollCourseResourceService.GetEnrollCourseResourceByLectureId(LecturesViewModel.Id, languageId);
                foreach(var Resource in Resources)
                LecturesViewModel.EnrollCourseResourceViewModel.Add(new EnrollCourseResourceViewModel(Resource));
            }

            return PartialView("Edit", LecturesContentViewModel);
        }

        // POST: ControlPanel/LecturesContent/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthentication(PageName = "EnrollCoursesContent", PermissionKey = "Edit")]
        [AuditLogFilter(ActionDescription = "Enroll Lectures Content Edit Post")]
        public async Task<IActionResult> Edit(
            EnrollLecturesContentViewModel LecturesContentViewModel)
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
                                foreach (var Lecture in LecturesContentViewModel.EnrollLectureViewModel)
                                {
                                    //Delete Resource
                                    var ExistsAllResourceINLectures = _enrollCourseResourceService.GetEnrollCourseResourceByLectureId_WithoutUsing((int)Lecture.ForEditModleID, context);
                                    foreach (var Resource in ExistsAllResourceINLectures)
                                    {
                                        if (Lecture.EnrollCourseResourceViewModel != null)
                                        {
                                            if (!Lecture.EnrollCourseResourceViewModel.Exists(e => e.ForEditModleID == Resource.Id))
                                                _enrollCourseResourceService.DeleteEnrollCourseResource_WithoutUsing(Resource, context);
                                        }
                                        else
                                        {
                                            _enrollCourseResourceService.DeleteEnrollCourseResourceByLectureId_WithoutUsing((int)Lecture.ForEditModleID, context);
                                            break;
                                        }
                                    }

                                    if (Lecture.EnrollCourseResourceViewModel != null)
                                    {
                                        foreach (var Resource in Lecture.EnrollCourseResourceViewModel)
                                        {

                                            Resource.LanguageId = (LecturesContentViewModel.EnrollSectionOfCourseViewModel.LanguageId == 0) ? CultureHelper.GetDefaultLanguageId() : LecturesContentViewModel.EnrollSectionOfCourseViewModel.LanguageId;
                                            Resource.CreatedBy = User.Identity?.Name;
                                            Resource.EnrollLectureId = (int)Lecture.ForEditModleID;
                                            Resource.EnrollCourseId = LecturesContentViewModel.EnrollSectionOfCourseViewModel.EnrollCourseId;
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
                                                var ExistsResource = _enrollCourseResourceService.GetEnrollCourseResourceById_WithoutUsing(Resource.Id, context);
                                                _enrollCourseResourceService.EditEnrollCourseResource_WithoutUsing(Resource, ExistsResource, context);
                                            }
                                            else //Add New Resource
                                            {
                                                _enrollCourseResourceService.AddEnrollCourseResource_WithoutUsing(Resource, context);
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
