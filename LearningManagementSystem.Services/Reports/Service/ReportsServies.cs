using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.Reports.ISerives;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using X.PagedList;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.Reports.Service
{
    public class ReportsServies : IReportsServies
    {
        private readonly LearningManagementSystemContext _context;
        private readonly IEnrollTeacherCourseService _enrollTeacherCourseService;
        private readonly IEnrollStudentCourseService _enrollStudentCourseService;

        public ReportsServies(LearningManagementSystemContext context, IEnrollTeacherCourseService enrollTeacherCourseService, IEnrollStudentCourseService enrollStudentCourseService)
        {
            _context = context;
            _enrollTeacherCourseService = enrollTeacherCourseService;
            _enrollStudentCourseService = enrollStudentCourseService;
        }

        public CoursesReportViewModel GetCourses(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var enrollTeacherCourses = _context.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.Teacher.Contact.ContactTranslations).Include(r => r.EnrollStudentCourses).Include(r => r.Semester).Include(r => r.EnrollStudentAlerts)
                .Include(r => r.EnrollLectures).Include(r => r.Course.Category.CourseCategoryTranslations).Include(r => r.TeacherAttendances).AsQueryable();

            if (filter?.CourseCategory > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => filter.Courses.Contains(r.CourseId));
            if (filter?.Teacher > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SemesterId == filter.Semester);
            if (filter?.Gender > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.AgeGroup == filter.Gender);
            if (filter?.AgeFrom > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.AgeAllowedForRegistration == filter.AgeFrom);
            if (filter.FromDate != default && filter.ToDate != default)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.WorkStartDate >= filter.FromDate && r.WorkStartDate <= filter.ToDate);
            if (filter.FromPublisheDate != default && filter.ToPublisheDate != default)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.PublicationDate >= filter.FromPublisheDate && r.PublicationDate <= filter.ToPublisheDate);
            if (filter?.Status > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Status == filter.Status);

            if (filter?.MarkAdoption > 0)
                if (filter?.MarkAdoption == 1)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.IsCourseDone == true);
                else
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.IsCourseDone == false || r.IsCourseDone == null);

            if (filter?.CertificateAdoption > 0)
                if (filter.CertificateAdoption == 1)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CertificateAdoption == true);
                else
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CertificateAdoption == false || r.CertificateAdoption == null);


            if (!string.IsNullOrWhiteSpace(searchText))
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Teacher.Contact.FullName.Contains(searchText) || r.SectionName.Contains(searchText) || (r.Teacher.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) & t.LanguageId == languageId)));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SectionName == filter.SectionName);

            var enrollData = enrollTeacherCourses.Select(etc => new
            {
                StudentCourses = etc.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Select(esc => new
                {
                    esc.Status,
                    esc.IsPass
                }).ToList(),
                AlertCount = etc.EnrollStudentAlerts.Count(r => r.Status == (int)GeneralEnums.StatusEnum.Active)
            }).ToList();

            var model = new CoursesReportViewModel()
            {
                Capacity = enrollTeacherCourses.Sum(r => r.CountOfStudent).Value,
                StudentCount = enrollData.Sum(ed => ed.StudentCourses.Count),
                SeccessCount = enrollData.Sum(ed => ed.StudentCourses.Count(sc => sc.IsPass == true)),
                FaildCount = enrollData.Sum(ed => ed.StudentCourses.Count(sc => sc.IsPass == false)),
                WarningCount = enrollData.Sum(ed => ed.AlertCount),
            };

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = enrollTeacherCourses;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.CourseId == item.CourseId);
                    var trans2 = item.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Teacher.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.Course.Category.Name = trans2.Name;
                }


            model.EnrollTeacherCourses = output;
            return model;
        }

        public DataSet DownloadCoursesReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var enrollTeacherCourses = _context.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
               .Include(r => r.Teacher.Contact.ContactTranslations).Include(r => r.EnrollStudentCourses).Include(r => r.Semester).Include(r => r.EnrollStudentAlerts)
                .Include(r => r.EnrollLectures).Include(r => r.Course.Category.CourseCategoryTranslations).AsQueryable();

            if (filter?.CourseCategory > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => filter.Courses.Contains(r.CourseId));
            if (filter?.Teacher > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SemesterId == filter.Semester);
            if (filter?.Gender > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.AgeGroup == filter.Gender);
            if (filter?.AgeFrom > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.AgeAllowedForRegistration == filter.AgeFrom);
            if (filter.FromDate != default && filter.ToDate != default)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.WorkStartDate >= filter.FromDate && r.WorkStartDate <= filter.ToDate);
            if (filter.FromPublisheDate != default && filter.ToPublisheDate != default)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.PublicationDate >= filter.FromPublisheDate && r.PublicationDate <= filter.ToPublisheDate);
            if (filter?.Status > 0)
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Status == filter.Status);

            if (filter?.MarkAdoption > 0)
                if (filter?.MarkAdoption == 1)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.IsCourseDone == true);
                else
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.IsCourseDone == false || r.IsCourseDone == null);

            if (filter?.CertificateAdoption > 0)
                if (filter.CertificateAdoption == 1)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CertificateAdoption == true);
                else
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CertificateAdoption == false || r.CertificateAdoption == null);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.Teacher.Contact.FullName.Contains(filter.SearchText) || r.SectionName.Contains(filter.SearchText) || (r.Teacher.Contact.ContactTranslations.Any(t => t.FullName.Contains(filter.SearchText) & t.LanguageId == filter.LanguageId)));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SectionName == filter.SectionName);

            var output = enrollTeacherCourses.OrderByDescending(r => r.CreatedOn);

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans1 = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.CourseId == item.CourseId);
                    var trans2 = item.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Teacher.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.Course.Category.Name = trans2.Name;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Course Category"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Semester Name"], typeof(string));
            dt.Columns.Add(localizer["Allowed Age"], typeof(string));
            dt.Columns.Add(localizer["Students Gender"], typeof(string));
            dt.Columns.Add(localizer["Number Of Allowed Student"], typeof(string));
            dt.Columns.Add(localizer["Number Of Student"], typeof(string));
            dt.Columns.Add(localizer["Passed Number"], typeof(string));
            dt.Columns.Add(localizer["Warning"], typeof(string));
            dt.Columns.Add(localizer["Lectures Number"], typeof(string));
            dt.Columns.Add(localizer["Publication Start Date"], typeof(string));
            dt.Columns.Add(localizer["Publication End Date"], typeof(string));
            dt.Columns.Add(localizer["Start Date"], typeof(string));
            dt.Columns.Add(localizer["End Date"], typeof(string));
            dt.Columns.Add(localizer["Created On"], typeof(string));
            dt.Columns.Add(localizer["Created By"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            foreach (var item in output)
            {
                var count = item.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count();
                DataRow dr = dt.NewRow();
                dr[0] = item.CourseName;
                dr[1] = item.Course?.Category?.Name;
                dr[2] = item.Teacher.Contact.FullName;
                dr[3] = item.Semester?.Name;
                dr[4] = item.AgeAllowedForRegistration;
                dr[5] = LookupHelper.GetLookupDetailsById(item.AgeGroup, filter.LanguageId)?.Name;
                dr[6] = item.CountOfStudent;
                dr[7] = count;
                dr[8] = item.EnrollStudentCourses.Where(r => r.IsPass == true && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() + "/" + count;
                dr[9] = item.EnrollStudentAlerts.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count();
                dr[10] = item.EnrollLectures.Count();
                dr[11] = item.PublicationDate.ToShortDateString();
                dr[12] = item.PublicationEndDate?.ToShortDateString();
                dr[13] = item.WorkStartDate.ToShortDateString();
                dr[14] = item.WorkEndDate?.ToShortDateString();
                dr[15] = item.CreatedOn.ToShortDateString();
                dr[16] = LookupHelper.GetFullNameByEmail(item.CreatedBy, filter.LanguageId);
                dr[17] = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }



        public IPagedList<EnrollStudentCourse> GetStudentsCourses(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var enrollStudentCourses = _context.EnrollStudentCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Student.Contact.ContactTranslations)
                .Include(r => r.Course.Course.Category.CourseCategoryTranslations).Include(r => r.Course.Teacher.Contact.ContactTranslations)
                .Include(r => r.CourseAttendances).Include(r => r.EnrollStudentAlerts).Include(r => r.Course.Semester)
                .Where(r => r.Course.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (filter?.CourseCategory > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                enrollStudentCourses = enrollStudentCourses.Where(r => filter.Courses.Contains(r.Course.CourseId));
            if (filter?.Teacher > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.SemesterId == filter.Semester);
            if (filter?.Gender > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.GenderId == filter.Gender);
            if (filter?.Country > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.CountryId == filter.Country);
            if (filter?.AgeFrom > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.BirthDate <= DateTime.Now.AddYears(-filter.AgeFrom));
            if (filter?.AgeTo > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.BirthDate >= DateTime.Now.AddYears(-filter.AgeTo));
            if (filter.FromDate != default && filter.ToDate != default)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Status == filter.Status);
            if (filter?.SuccessStatus > 0)
                if (filter?.SuccessStatus == (int)GeneralEnums.SuccessStatusEnum.Success)
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == true);
                else if (filter?.SuccessStatus == (int)GeneralEnums.SuccessStatusEnum.Failed)
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == false);
                else
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == null);
            if (filter?.AttendanceFrom > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CourseAttendances.Where(r => r.IsPresent == false && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() >= filter.AttendanceFrom);
            if (filter?.AttendanceTo > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CourseAttendances.Where(r => r.IsPresent == false && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() <= filter.AttendanceTo);
            if (filter?.WarningNumber > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.EnrollStudentAlerts.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count() == filter.WarningNumber);

            if (!string.IsNullOrWhiteSpace(searchText))
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.FullName.Replace(" ", "").Contains(searchText.Replace(" ", "")) 
                || r.Student.Contact.ContactTranslations.Any(t => t.FullName.Replace(" ", "").Contains(searchText.Replace(" ", "")) & t.LanguageId == languageId));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.SectionName == filter.SectionName);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = enrollStudentCourses;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.CourseId == item.Course.CourseId);
                    var trans2 = item.Course.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans3 = item.Course.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Student.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.Course.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.Course.Teacher.Contact.FullName = trans2.FullName;
                    if (trans3 != null)
                        item.Course.Course.Category.Name = trans3.Name;
                }

            return output;
        }

        public DataSet DownloadStudentsCoursesReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var enrollStudentCourses = _context.EnrollStudentCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Student.Contact.ContactTranslations)
               .Include(r => r.Course.Course.Category.CourseCategoryTranslations).Include(r => r.Course.Teacher.Contact.ContactTranslations)
               .Include(r => r.CourseAttendances).Include(r => r.EnrollStudentAlerts).Include(r => r.Course.Semester)
               .Where(r => r.Course.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (filter?.CourseCategory > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                enrollStudentCourses = enrollStudentCourses.Where(r => filter.Courses.Contains(r.Course.CourseId));
            if (filter?.Teacher > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.SemesterId == filter.Semester);
            if (filter?.Gender > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.GenderId == filter.Gender);
            if (filter?.Country > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.CountryId == filter.Country);
            if (filter?.AgeFrom > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.BirthDate <= DateTime.Now.AddYears(-filter.AgeFrom));
            if (filter?.AgeTo > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.BirthDate >= DateTime.Now.AddYears(-filter.AgeTo));
            if (filter.FromDate != default && filter.ToDate != default)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Status == filter.Status);
            if (filter?.SuccessStatus > 0)
                if (filter?.SuccessStatus == (int)GeneralEnums.SuccessStatusEnum.Success)
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == true);
                else if (filter?.SuccessStatus == (int)GeneralEnums.SuccessStatusEnum.Failed)
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == false);
                else
                    enrollStudentCourses = enrollStudentCourses.Where(r => r.IsPass == null);
            if (filter?.AttendanceFrom > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CourseAttendances.Where(r => r.IsPresent == false && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() >= filter.AttendanceFrom);
            if (filter?.AttendanceTo > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.CourseAttendances.Where(r => r.IsPresent == false && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() <= filter.AttendanceTo);
            if (filter?.WarningNumber > 0)
                enrollStudentCourses = enrollStudentCourses.Where(r => r.EnrollStudentAlerts.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count() == filter.WarningNumber);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Student.Contact.FullName.Replace(" ", "").Contains(filter.SearchText.Replace(" ", "")) || r.Student.Contact.ContactTranslations.Any(t => t.FullName.Replace(" ", "").Contains(filter.SearchText.Replace(" ", "")) & t.LanguageId == filter.LanguageId));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                enrollStudentCourses = enrollStudentCourses.Where(r => r.Course.SectionName == filter.SectionName);

            var output = enrollStudentCourses.OrderByDescending(r => r.CreatedOn);

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans1 = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.CourseId == item.Course.CourseId);
                    var trans2 = item.Course.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans3 = item.Course.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Student.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.Course.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.Course.Teacher.Contact.FullName = trans2.FullName;
                    if (trans3 != null)
                        item.Course.Course.Category.Name = trans3.Name;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Course Category"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Semester Name"], typeof(string));
            dt.Columns.Add(localizer["Student Full Name"], typeof(string));
            dt.Columns.Add(localizer["Course Mark"], typeof(string));
            dt.Columns.Add(localizer["Success Mark"], typeof(string));
            dt.Columns.Add(localizer["Mark"], typeof(string));
            dt.Columns.Add(localizer["Evaluation"], typeof(string));
            dt.Columns.Add(localizer["Passed"], typeof(string));
            dt.Columns.Add(localizer["Attendance"], typeof(string));
            dt.Columns.Add(localizer["Warning"], typeof(string));
            dt.Columns.Add(localizer["Enrollment Date"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            foreach (var item in output)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.Course?.CourseName;
                dr[1] = item.Course?.Course?.Category?.Name;
                dr[2] = item.Course?.Teacher?.Contact.FullName;
                dr[3] = item.Course?.Semester?.Name;
                dr[4] = item.Student.Contact.FullName;
                dr[5] = _enrollTeacherCourseService.GetCourseMark(item.CourseId);
                dr[6] = item.Course?.Course?.SuccessMark;
                dr[7] = item.Mark;
                dr[8] = _enrollStudentCourseService.GetEvaluation(item.Id, filter.LanguageId);
                dr[9] = item.IsPass == true ? localizer["Passed"] : item.IsPass == false ? localizer["Failed"] : "";
                dr[10] = !LookupHelper.CheckIfClassOnline(item.CourseId) && item.Status == (int)GeneralEnums.StatusEnum.Active
                    ? item.CourseAttendances.Where(r => r.IsPresent == true && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() + "/" +
                      item.CourseAttendances.Where(r => r.IsPresent == false && r.Status == (int)GeneralEnums.StatusEnum.Active).Count() + "/" +
                      _enrollStudentCourseService.GetAttendanceDays(item.CourseId)
                    : item.Status == (int)GeneralEnums.StatusEnum.Expelled
                        ? localizer["Expelled"]
                        : localizer["Online Class"];
                dr[11] = item.EnrollStudentAlerts.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count();
                dr[12] = item.CreatedOn.ToShortDateString();
                dr[13] = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }



        public SenangPayReportsViewModel GetCoursePayments(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            List<SenangPayViewModel> data = new List<SenangPayViewModel>();
            _context.SenangPays.Where(r => r.EnrollTeacherCourseId > 0 && r.ProjectId == null).Include(r => r.EnrollTeacherCourse)
                .Include(r => r.EnrollTeacherCourse.Teacher.Contact.ContactTranslations).ToList().ForEach(r =>
                {
                    var senangPayViewModel = new SenangPayViewModel
                    {
                        Type = (int)GeneralEnums.PaymentType.SenangPay,
                        FullName = r.UserName,
                        TeacherName = r.EnrollTeacherCourse.Teacher.Contact.FullName,
                        CourseName = r.EnrollTeacherCourse.CourseName,
                        Status = r.Status,
                        Email = r.Email,
                        CreatedOn = r.ProcessDate,
                        Amount = r.Amount,
                        UserName = r.UserName,
                        TeacherId = r.EnrollTeacherCourse.TeacherId,
                        EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                        CourseId = r.EnrollTeacherCourse.CourseId
                    };
                    data.Add(senangPayViewModel);
                });

            _context.InvoicesPays.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Contact).Include(r => r.EnrollTeacherCourse.Course).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Invoice,
                    FullName = r.Contact.FullName,
                    CourseName = r.EnrollTeacherCourse.CourseName,
                    Status = r.ProcessStatus,
                    Email = r.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.EnrollTeacherCourse.Course.CoursePrice ?? 0,
                    UserName = r.Contact.FullName,
                    TeacherId = r.EnrollTeacherCourse.TeacherId,
                    EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                    CourseId = r.EnrollTeacherCourse.CourseId
                };
                data.Add(senangPayViewModel);
            });

            _context.StudentBalanceHistories.Include(r => r.EnrollStudentCourse.Course.Course).Include(r => r.Student.Contact).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Balance,
                    FullName = r.Student.Contact.FullName,
                    CourseName = r.EnrollStudentCourse?.Course?.CourseName,
                    Status = (int)GeneralEnums.StatusEnum.Paid,
                    Email = r.Student.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount ?? 0,
                    Title = r.Title,
                    UserName = r.Student.Contact.FullName,
                    TeacherId = r.EnrollStudentCourse?.Course?.TeacherId ?? 0,
                    EnrollTeacherCourseId = r.EnrollStudentCourse?.CourseId,
                    StudentId = r.StudentId,
                    CourseId = r.EnrollStudentCourse?.Course?.CourseId ?? 0
                };
                data.Add(senangPayViewModel);
            });

            if (filter?.Type > 0)
                data = data.Where(r => r.Type == filter.Type).ToList();

            if (filter?.Courses != null)
                data = data.Where(r => filter.Courses.Contains(r.CourseId)).ToList();

            if (filter?.Teacher > 0)
                data = data.Where(r => r.TeacherId == filter.Teacher).ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                data = data.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date).ToList();

            if (filter?.Status > 0)
                data = data.Where(r => r.Status == filter.Status).ToList();

            if (!string.IsNullOrWhiteSpace(searchText))
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    data = data.Where(r => r.UserName.Contains(searchText) || r.CourseName.Contains(searchText) || r.TeacherName.Contains(searchText)).ToList();
                else
                    data = data.Where(r => r.UserName.Contains(searchText)).ToList();

            var amount = data.Sum(r => r.Amount);
            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = data;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            foreach (var item in output)
            {
                var student = _context.Students.Include(r => r.Contact).FirstOrDefault(r => r.Contact.Email == item.Email);
                if (student != null)
                {
                    item.FullName = student.Contact.FullName;
                    item.StudentId = student.Id;
                }
            }
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.CourseId == item.CourseId);
                    var trans1 = _context.Contacts.Include(r => r.ContactTranslations).FirstOrDefault(r => r.Email == item.Email);
                    var trans2 = _context.Trainers.Include(r => r.Contact.ContactTranslations).FirstOrDefault(r => r.Id == item.TeacherId);
                    if (trans != null)
                        item.CourseName = trans.CourseName;
                    if (trans1?.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId) != null)
                        item.FullName = trans1.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId).FullName;
                    if (trans2?.Contact?.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId) != null)
                        item.TeacherName = trans2.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId).FullName;
                }

            return new SenangPayReportsViewModel() { Amount = amount, SenangPayViewModels = output };
        }

        public DataSet DownloadCoursePaymentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            List<SenangPayViewModel> data = new List<SenangPayViewModel>();
            _context.SenangPays.Where(r => r.EnrollTeacherCourseId > 0 && r.ProjectId == null).Include(r => r.EnrollTeacherCourse)
                .Include(r => r.EnrollTeacherCourse.Teacher.Contact.ContactTranslations).ToList().ForEach(r =>
                {
                    var senangPayViewModel = new SenangPayViewModel
                    {
                        Type = (int)GeneralEnums.PaymentType.SenangPay,
                        FullName = r.UserName,
                        CourseName = r.EnrollTeacherCourse.CourseName,
                        Status = r.Status,
                        Email = r.Email,
                        CreatedOn = r.ProcessDate,
                        Amount = r.Amount,
                        UserName = r.UserName,
                        TeacherId = r.EnrollTeacherCourse.TeacherId,
                        EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                        CourseId = r.EnrollTeacherCourse.CourseId
                    };
                    data.Add(senangPayViewModel);
                });

            _context.InvoicesPays.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Contact).Include(r => r.EnrollTeacherCourse.Course).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Invoice,
                    FullName = r.Contact.FullName,
                    CourseName = r.EnrollTeacherCourse.CourseName,
                    Status = r.ProcessStatus,
                    Email = r.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.EnrollTeacherCourse.Course.CoursePrice ?? 0,
                    UserName = r.Contact.FullName,
                    TeacherId = r.EnrollTeacherCourse.TeacherId,
                    EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                    CourseId = r.EnrollTeacherCourse.CourseId
                };
                data.Add(senangPayViewModel);
            });

            _context.StudentBalanceHistories.Include(r => r.EnrollStudentCourse.Course.Course).Include(r => r.Student.Contact).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Balance,
                    FullName = r.Student.Contact.FullName,
                    CourseName = r.EnrollStudentCourse?.Course?.CourseName,
                    Status = (int)GeneralEnums.StatusEnum.Paid,
                    Email = r.Student.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount ?? 0,
                    Title = r.Title,
                    UserName = r.Student.Contact.FullName,
                    TeacherId = r.EnrollStudentCourse?.Course?.TeacherId ?? 0,
                    EnrollTeacherCourseId = r.EnrollStudentCourse?.CourseId,
                    StudentId = r.StudentId,
                    CourseId = r.EnrollStudentCourse?.Course?.CourseId ?? 0
                };
                data.Add(senangPayViewModel);
            });

            if (filter?.Type > 0)
                data = data.Where(r => r.Type == filter.Type).ToList();

            if (filter?.Courses != null)
                data = data.Where(r => filter.Courses.Contains(r.CourseId)).ToList();

            if (filter?.Teacher > 0)
                data = data.Where(r => r.TeacherId == filter.Teacher).ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                data = data.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date).ToList();

            if (filter?.Status > 0)
                data = data.Where(r => r.Status == filter.Status).ToList();

            if (!string.IsNullOrWhiteSpace(filter.searchText))
                if (filter.LanguageId == CultureHelper.GetDefaultLanguageId())
                    data = data.Where(r => r.UserName.Contains(filter.searchText) || r.CourseName.Contains(filter.searchText) || r.TeacherName.Contains(filter.searchText)).ToList();
                else
                    data = data.Where(r => r.UserName.Contains(filter.searchText)).ToList();

            var output = data.OrderByDescending(r => r.CreatedOn);

            foreach (var item in output)
            {
                var student = _context.Students.Include(r => r.Contact).FirstOrDefault(r => r.Contact.Email == item.Email);
                if (student != null)
                {
                    item.FullName = student.Contact.FullName;
                    item.StudentId = student.Id;
                }
            }
            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.CourseId == item.CourseId);
                    var trans1 = _context.Contacts.Include(r => r.ContactTranslations).FirstOrDefault(r => r.Email == item.Email);
                    if (trans != null)
                        item.CourseName = trans.CourseName;
                    if (trans1?.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId) != null)
                        item.FullName = trans1.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).FullName;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Full Name"], typeof(string));
            dt.Columns.Add(localizer["Email"], typeof(string));
            dt.Columns.Add(localizer["Type"], typeof(string));
            dt.Columns.Add(localizer["Action"], typeof(string));
            dt.Columns.Add(localizer["Amount"], typeof(string));
            dt.Columns.Add(localizer["Payment Date"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            foreach (var item in output)
            {
                var statusName = "";
                if (item.Type == (int)GeneralEnums.PaymentType.Invoice)
                    statusName = LookupHelper.GetInvoicesPayStatusById(item.Status, filter.LanguageId)?.Name;
                else
                    statusName = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                DataRow dr = dt.NewRow();
                dr[0] = item.CourseName;
                dr[1] = item.FullName;
                dr[2] = item.FullName;
                dr[3] = item.Email;
                dr[4] = LookupHelper.GetPaymentTypeById(item.Type, filter.LanguageId)?.Name;
                dr[5] = localizer[item.Title ?? ""] + (item.Title != null ? item.CourseName : "");
                dr[6] = item.Amount;
                dr[7] = item.CreatedOn.ToShortDateString();
                dr[8] = statusName;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }



        public SenangPayReportsViewModel GetProjectPayments(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var senangPays = _context.SenangPays.Where(r => r.ProjectId > 0).Include(r => r.Project.CmsProjectTranslations)
                .AsQueryable();

            if (filter?.Project > 0)
                senangPays = senangPays.Where(r => r.ProjectId == filter.Project);
            if (filter.FromDate != default && filter.ToDate != default)
                senangPays = senangPays.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                senangPays = senangPays.Where(r => r.Status == filter.Status);

            if (!string.IsNullOrWhiteSpace(searchText))
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    senangPays = senangPays.Where(r => r.Project.Name.Contains(searchText) || r.UserName.Contains(searchText));
                else
                    senangPays = senangPays.Where(r => r.Project.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId).Name.Contains(searchText) || r.UserName.Contains(searchText));

            var amount = senangPays.Sum(r => r.Amount);
            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = senangPays;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Project?.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Project.Name = trans.Name;
                }

            return new SenangPayReportsViewModel() { Amount = amount, SenangPay = output };
        }

        public DataSet DownloadProjectPaymentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var senangPays = _context.SenangPays.Where(r => r.ProjectId > 0).Include(r => r.Project.CmsProjectTranslations)
                .AsQueryable();

            if (filter?.Project > 0)
                senangPays = senangPays.Where(r => r.ProjectId == filter.Project);
            if (filter.FromDate != default && filter.ToDate != default)
                senangPays = senangPays.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                senangPays = senangPays.Where(r => r.Status == filter.Status);

            if (!string.IsNullOrWhiteSpace(filter.searchText))
                if (filter.LanguageId == CultureHelper.GetDefaultLanguageId())
                    senangPays = senangPays.Where(r => r.Project.Name.Contains(filter.searchText) || r.UserName.Contains(filter.searchText));
                else
                    senangPays = senangPays.Where(r => r.Project.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).Name.Contains(filter.searchText) || r.UserName.Contains(filter.searchText));


            var output = senangPays.OrderByDescending(r => r.CreatedOn);

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Project?.CmsProjectTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Project.Name = trans.Name;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Project Name"], typeof(string));
            dt.Columns.Add(localizer["Full Name"], typeof(string));
            dt.Columns.Add(localizer["Email"], typeof(string));
            dt.Columns.Add(localizer["Phone Number"], typeof(string));
            dt.Columns.Add(localizer["Amount"], typeof(string));
            dt.Columns.Add(localizer["Payment Date"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            foreach (var item in output)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.Project?.Name;
                dr[1] = item.UserName;
                dr[2] = item.Email;
                dr[3] = item.PhoneNumber;
                dr[4] = item.Amount;
                dr[5] = item.CreatedOn.ToShortDateString();
                dr[6] = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        public IPagedList<ExamsAndAssignmentsViewModel> GetExamsAndAssignmentsReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            List<ExamsAndAssignmentsViewModel> data = new List<ExamsAndAssignmentsViewModel>();
            filter.LanguageId = languageId;

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam || filter?.ReportType == 0)
            {
                var examsQuery = _context.EnrollCourseExams.Include(e => e.EnrollTeacherCourse)
                            .ThenInclude(etc => etc.EnrollStudentCourses)
                            .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                            (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                            (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                            (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                            (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                            (filter.Status == 0 || x.Status == filter.Status)).Include(e => e.EnrollStudentExams);

                foreach (var r in examsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.PublishDate,
                        PublicationEndDate = r.PublishEndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSubmitted = r.EnrollStudentExams.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSuccess = r.EnrollStudentExams.Where(r => r.MarkAfterConversion >= filter.MarkFrom && r.MarkAfterConversion <= filter.MarkTo && r.MarkAfterConversion > 0).Count(),
                    };
                    data.Add(viewModel);
                }
            }

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams || filter?.ReportType == 0)
            {
                var practicalExamsQuery = _context.PracticalEnrollmentExams.Include(p => p.EnrollTeacherCourse)
                    .ThenInclude(etc => etc.EnrollStudentCourses)
                    .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                            (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                            (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                            (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                            (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                            (filter.Status == 0 || x.Status == filter.Status))
                    .Include(p => p.PracticalExam)
                    .Include(p => p.PracticalEnrollmentExamStudents);

                foreach (var r in practicalExamsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.PracticalExam.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.StartDate,
                        PublicationEndDate = r.EndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSubmitted = r.PracticalEnrollmentExamStudents.Where(r => r.MarkAfterConversion != null).Count(),
                        NumberOfSuccess = r.PracticalEnrollmentExamStudents.Where(r => r.MarkAfterConversion >= filter.MarkFrom && r.MarkAfterConversion <= filter.MarkTo && r.MarkAfterConversion > 0).Count(),
                    };
                    data.Add(viewModel);
                }
            }

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.Assignments || filter?.ReportType == 0)
            {
                var assignmentsQuery = _context.EnrollCourseAssigments.Include(a => a.EnrollTeacherCourse)
                    .ThenInclude(etc => etc.EnrollStudentCourses)
                    .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                            (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                            (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                            (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                            (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                            (filter.Status == 0 || x.Status == filter.Status));

                foreach (var r in assignmentsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.Assignments,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.PublishDate,
                        PublicationEndDate = r.PublishEndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSuccess = null,
                    };
                    data.Add(viewModel);
                }
            }

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in data)
                    if (item.Type == (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam)
                    {
                        var trans = _context.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.EnrollCourseExamId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }
                    else if (item.Type == (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams)
                    {
                        var trans = _context.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.PracticalExamId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }
                    else
                    {
                        var trans = _context.EnrollAssignmentTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.EnrollAssignmentId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }

            data = data.Where(d => (filter.ReportType == 0 || d.Type == filter.ReportType) && (!string.IsNullOrWhiteSpace(searchText) ? d.Name.Contains(searchText) : true) && (!string.IsNullOrWhiteSpace(filter.SectionName) ? d.EnrollCourse.SectionName == filter.SectionName : true)).ToList();

            var pageNumber = page ?? 1;
            var pageSize = pagination;
            var output = data.OrderByDescending(d => d.CreatedOn).ToPagedList(pageNumber, pageSize);

            foreach (var item in output)
            {
                var course = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.CourseId == item.EnrollCourse.CourseId);
                if (course != null)
                    item.EnrollCourse.CourseName = course.CourseName;
                var semester = _context.Semesters.Include(r => r.SemesterTranslations).FirstOrDefault(r => r.Id == item.SemesterId);
                if (semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId) != null)
                    item.SemesterName = semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).Name;
                else
                    item.SemesterName = semester.Name;

                var teacher = _context.Trainers.Include(r => r.Contact.ContactTranslations).FirstOrDefault(r => r.Id == item.TeacherId);
                if (teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId) != null)
                    item.FullName = teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).FullName;
                else
                    item.FullName = teacher.Contact.FullName;
            }

            return output;
        }

        public DataSet DownloadExamsAndAssignmentsReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            List<ExamsAndAssignmentsViewModel> data = new List<ExamsAndAssignmentsViewModel>();

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam || filter?.ReportType == 0)
            {
                var examsQuery = _context.EnrollCourseExams.Include(e => e.EnrollTeacherCourse)
                              .ThenInclude(etc => etc.EnrollStudentCourses)
                              .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                              (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                              (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                              (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                              (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                              (filter.Status == 0 || x.Status == filter.Status)).Include(e => e.EnrollStudentExams);

                foreach (var r in examsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.PublishDate,
                        PublicationEndDate = r.PublishEndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSubmitted = r.EnrollStudentExams.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSuccess = r.EnrollStudentExams.Where(r => r.MarkAfterConversion >= filter.MarkFrom && r.MarkAfterConversion <= filter.MarkTo && r.MarkAfterConversion > 0).Count(),
                    };
                    data.Add(viewModel);
                }
            }

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams || filter?.ReportType == 0)
            {
                var practicalExamsQuery = _context.PracticalEnrollmentExams.Include(p => p.EnrollTeacherCourse)
                     .ThenInclude(etc => etc.EnrollStudentCourses)
                     .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                             (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                             (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                             (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                             (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                             (filter.Status == 0 || x.Status == filter.Status))
                     .Include(p => p.PracticalExam)
                     .Include(p => p.PracticalEnrollmentExamStudents);

                foreach (var r in practicalExamsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.PracticalExam.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.StartDate,
                        PublicationEndDate = r.EndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSubmitted = r.PracticalEnrollmentExamStudents.Where(r => r.MarkAfterConversion != null).Count(),
                        NumberOfSuccess = r.PracticalEnrollmentExamStudents.Where(r => r.MarkAfterConversion >= filter.MarkFrom && r.MarkAfterConversion <= filter.MarkTo && r.MarkAfterConversion > 0).Count(),
                    };
                    data.Add(viewModel);
                }
            }

            if (filter?.ReportType == (int)GeneralEnums.ExamsAndAssignmentsEnums.Assignments || filter?.ReportType == 0)
            {
                var assignmentsQuery = _context.EnrollCourseAssigments.Include(a => a.EnrollTeacherCourse)
                   .ThenInclude(etc => etc.EnrollStudentCourses)
                   .Where(x => (filter.FromPublisheDate == default || x.CreatedOn >= filter.FromPublisheDate) &&
                           (filter.ToPublisheDate == default || x.CreatedOn <= filter.ToPublisheDate) &&
                           (filter.Courses == null || filter.Courses.Contains(x.EnrollTeacherCourse.CourseId)) &&
                           (filter.Teacher == 0 || x.EnrollTeacherCourse.TeacherId == filter.Teacher) &&
                           (filter.Semester == 0 || x.EnrollTeacherCourse.SemesterId == filter.Semester) &&
                           (filter.Status == 0 || x.Status == filter.Status));

                foreach (var r in assignmentsQuery)
                {
                    var viewModel = new ExamsAndAssignmentsViewModel
                    {
                        Id = r.Id,
                        EnrollCourseId = r?.EnrollTeacherCourseId ?? 0,
                        SemesterId = r?.EnrollTeacherCourse?.SemesterId ?? 0,
                        TeacherId = r?.EnrollTeacherCourse?.TeacherId ?? 0,
                        EnrollCourse = r?.EnrollTeacherCourse,
                        Name = r.Name,
                        Type = (int)GeneralEnums.ExamsAndAssignmentsEnums.Assignments,
                        Status = r.Status,
                        CreatedOn = r.CreatedOn,
                        PublicationDate = r.PublishDate,
                        PublicationEndDate = r.PublishEndDate,
                        NumberOfStudents = r.EnrollTeacherCourse.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active).Count(),
                        NumberOfSuccess = null,
                    };
                    data.Add(viewModel);
                }
            }

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in data)
                    if (item.Type == (int)GeneralEnums.ExamsAndAssignmentsEnums.Exam)
                    {
                        var trans = _context.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.EnrollCourseExamId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }
                    else if (item.Type == (int)GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams)
                    {
                        var trans = _context.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.PracticalExamId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }
                    else
                    {
                        var trans = _context.EnrollAssignmentTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.EnrollAssignmentId == item.Id);
                        if (trans != null)
                            item.Name = trans.Name;
                    }

            data = data.Where(d => (filter.ReportType == 0 || d.Type == filter.ReportType) && (!string.IsNullOrWhiteSpace(filter.SearchText) ? d.Name.Contains(filter.SearchText) : true) && (!string.IsNullOrWhiteSpace(filter.SectionName) ? d.EnrollCourse.SectionName == filter.SectionName : true)).ToList();
            var output = data.OrderByDescending(d => d.CreatedOn);

            foreach (var item in output)
            {
                var course = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId && r.CourseId == item.EnrollCourse.CourseId);
                if (course != null)
                    item.EnrollCourse.CourseName = course.CourseName;
                var semester = _context.Semesters.Include(r => r.SemesterTranslations).FirstOrDefault(r => r.Id == item.SemesterId);
                if (semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId) != null)
                    item.SemesterName = semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).Name;
                else
                    item.SemesterName = semester.Name;

                var teacher = _context.Trainers.Include(r => r.Contact.ContactTranslations).FirstOrDefault(r => r.Id == item.TeacherId);
                if (teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId) != null)
                    item.FullName = teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).FullName;
                else
                    item.FullName = teacher.Contact.FullName;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Type"], typeof(string));
            dt.Columns.Add(localizer["Name"], typeof(string));
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Semester Name"], typeof(string));
            dt.Columns.Add(localizer["Section Name"], typeof(string));
            dt.Columns.Add(localizer["Publication Start Date"], typeof(string));
            dt.Columns.Add(localizer["Publication End Date"], typeof(string));
            dt.Columns.Add(localizer["Created On"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            dt.Columns.Add(localizer["Number Of Students"], typeof(string));
            dt.Columns.Add(localizer["Number Of Submitted"], typeof(string));
            dt.Columns.Add(localizer["Number Of Success"], typeof(string));

            foreach (var item in output)
            {
                DataRow dr = dt.NewRow();
                dr[0] = LookupHelper.GetExamsAndAssignmentsTypeById(item.Type, filter.LanguageId)?.Name;
                dr[1] = item.Name;
                dr[2] = item.EnrollCourse?.CourseName;
                dr[3] = item.EnrollCourse?.Teacher?.Contact?.FullName;
                dr[4] = item.EnrollCourse?.Semester?.Name;
                dr[5] = item.EnrollCourse?.SectionName;
                dr[6] = item.PublicationDate?.ToShortDateString();
                dr[7] = item.PublicationEndDate?.ToShortDateString();
                dr[8] = item.CreatedOn.ToShortDateString();
                dr[9] = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                dr[10] = item.NumberOfStudents;
                dr[11] = item.NumberOfSubmitted;
                dr[12] = item.NumberOfSuccess;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
