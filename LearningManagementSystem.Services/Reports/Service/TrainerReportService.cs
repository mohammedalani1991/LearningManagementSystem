using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using EllipticCurve.Utils;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.Reports.ISerives;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using X.PagedList;
using static iTextSharp.text.pdf.AcroFields;

namespace LearningManagementSystem.Services.Reports.Service
{
    public class TrainerReportService : ITrainerReportService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ISettingService _settingService;

        public TrainerReportService(LearningManagementSystemContext context,ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public IPagedList<EnrollCourseAllowUserRateViewModel> GetTrainerRateReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var id = _context.DetailsLookups.FirstOrDefault(r => r.Code == "Management_Rate")?.Id;
            var id2 = _context.DetailsLookups.FirstOrDefault(r => r.Code == "rate_standard")?.Id;

            var AllowUserRates = _context.EnrollCourseAllowUserRates.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.RateTypeId == id)
                            .Include(r => r.Contact.ContactTranslations).Include(r => r.EnrollTeacherCourse.Course.CourseTranslations)
                            .Include(r => r.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations)
                            .Include(r => r.EnrollTeacherCourse.Semester.SemesterTranslations).AsQueryable();

            if (filter?.CourseCategory > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                AllowUserRates = AllowUserRates.Where(r => filter.Courses.Contains(r.EnrollTeacherCourse.CourseId));
            if (filter?.Teacher > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SemesterId == filter.Semester);

            if (!string.IsNullOrWhiteSpace(searchText))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.CourseName.Contains(searchText)
                || r.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId).CourseName.Contains(searchText)
                || r.Contact.FullName.Contains(searchText) || r.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId).FullName.Contains(searchText));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SectionName == filter.SectionName);

            var output = AllowUserRates.OrderByDescending(r => r.Id).ToPagedList(page ?? 1, pagination);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = item.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans2 = item.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans3 = item.EnrollTeacherCourse.Semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.EnrollTeacherCourse.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.EnrollTeacherCourse.Course.Category.Name = trans2.Name;
                    if (trans3 != null)
                        item.EnrollTeacherCourse.Semester.Name = trans3.Name;
                }

            var management = _context.ManagementRates.Include(a => a.ManagementRateLines).ThenInclude(r=>r.Standard).ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                management = management.Where(r => r.CreatedOn >= filter.FromDate && r.CreatedOn <= filter.ToDate).ToList();

            var max = double.Parse(_settingService.GetOrCreate("Max_Rate_Value", "3").Value);

            var trainerRateMeasures = _context.TrainerRateMeasures.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Type == id).Include(r => r.TrainerRateMeasureTranslations).AsQueryable();
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in trainerRateMeasures)
                {
                    var trans = item.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Measure = trans.Measure;
                }

            return output.Select(r =>
            {
                var managementRateLines = management.Where(s => s.EnrollTeacherCourseId == r.EnrollTeacherCourseId && s.CreatedBy.Equals(r.Contact.Email))
                    .SelectMany(s => s.ManagementRateLines).Where(r=>r.Standard.Type == id2);

                var total =(decimal)(managementRateLines.Sum(d => Int32.Parse(d.Value))/ Math.Max(1, managementRateLines.Count() * max) * 100);

                return new EnrollCourseAllowUserRateViewModel()
                {
                    FullName = r.Contact.FullName,
                    User = r.Contact.Email,
                    CourseName = r.EnrollTeacherCourse?.Course?.CourseName,
                    CourseId = r.EnrollTeacherCourse?.Id??0,
                    CourseCategory = r.EnrollTeacherCourse?.Course?.Category?.Name,
                    SemesterName = r.EnrollTeacherCourse?.Semester?.Name,
                    SectionName = r.EnrollTeacherCourse?.SectionName,
                    Total = total,
                    Rate = trainerRateMeasures.FirstOrDefault(r=>r.FromRange.Value <= total && r.ToRange.Value >= total)?.Measure??"No Data"
                };
            });
        }

        public IPagedList<ManagementRateLine> GetManagementItems(int page , int id , string user)
        {
            var id2 = _context.DetailsLookups.FirstOrDefault(r => r.Code == "rate_standard")?.Id;

            var management = _context.ManagementRates.Where(r=>r.EnrollTeacherCourseId == id && r.CreatedBy.Equals(user))
                .Include(r => r.ManagementRateLines).ThenInclude(r=>r.Standard).SelectMany(r=>r.ManagementRateLines).Include(r=>r.ManagementRate)
                .Where(r => r.Standard.Type == id2);

            return management.OrderByDescending(r=>r.ManagementRate.CreatedOn).ToPagedList(page, 10);
        }

        public DataSet DownloadTrainerRateReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var id = _context.DetailsLookups.FirstOrDefault(r => r.Code == "Management_Rate")?.Id;
            var id2 = _context.DetailsLookups.FirstOrDefault(r => r.Code == "rate_standard")?.Id;

            var AllowUserRates = _context.EnrollCourseAllowUserRates.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.RateTypeId == id)
                            .Include(r => r.Contact.ContactTranslations).Include(r => r.EnrollTeacherCourse.Course.CourseTranslations)
                            .Include(r => r.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations)
                            .Include(r => r.EnrollTeacherCourse.Semester.SemesterTranslations).AsQueryable();

            if (filter?.CourseCategory > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                AllowUserRates = AllowUserRates.Where(r => filter.Courses.Contains(r.EnrollTeacherCourse.CourseId));
            if (filter?.Teacher > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SemesterId == filter.Semester);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.CourseName.Contains(filter.SearchText)
                || r.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).CourseName.Contains(filter.SearchText)
                || r.Contact.FullName.Contains(filter.SearchText) || r.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).FullName.Contains(filter.SearchText));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SectionName == filter.SectionName);

            var output = AllowUserRates.OrderByDescending(r => r.Id);
            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans1 = item.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans2 = item.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans3 = item.EnrollTeacherCourse.Semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.EnrollTeacherCourse.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.EnrollTeacherCourse.Course.Category.Name = trans2.Name;
                    if (trans3 != null)
                        item.EnrollTeacherCourse.Semester.Name = trans3.Name;
                }

            var management = _context.ManagementRates.Include(a => a.ManagementRateLines).ThenInclude(r => r.Standard).ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                management = management.Where(r => r.CreatedOn >= filter.FromDate && r.CreatedOn <= filter.ToDate).ToList();

            var max = double.Parse(_settingService.GetOrCreate("Max_Rate_Value", "3").Value);

            var trainerRateMeasures = _context.TrainerRateMeasures.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Type == id).Include(r => r.TrainerRateMeasureTranslations).AsQueryable();
            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in trainerRateMeasures)
                {
                    var trans = item.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Measure = trans.Measure;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Course Category"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Semester Name"], typeof(string));
            dt.Columns.Add(localizer["Section Name"], typeof(string));
            dt.Columns.Add(localizer["Total"], typeof(string));
            dt.Columns.Add(localizer["Rate"], typeof(string));
            foreach (var item in output)
            {
                var managementRateLines = management.Where(s => s.EnrollTeacherCourseId == item.EnrollTeacherCourseId && s.CreatedBy.Equals(item.Contact.Email))
                                   .SelectMany(s => s.ManagementRateLines).Where(r => r.Standard.Type == id2);

                var total = (decimal)(managementRateLines.Sum(d => Int32.Parse(d.Value)) / Math.Max(1, managementRateLines.Count() * max) * 100);
                
                DataRow dr = dt.NewRow();
                dr[0] = item.EnrollTeacherCourse?.Course?.CourseName;
                dr[1] = item.EnrollTeacherCourse?.Course?.Category?.Name;
                dr[2] = item.Contact.FullName;
                dr[3] = item.EnrollTeacherCourse?.Semester?.Name;
                dr[4] = item.EnrollTeacherCourse?.SectionName;
                dr[5] = total.ToString("F2");
                dr[6] = trainerRateMeasures.FirstOrDefault(r => r.FromRange.Value <= total && r.ToRange.Value >= total)?.Measure ?? localizer["No Data"];
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        public IPagedList<EnrollCourseAllowUserRateViewModel> GetTrainerRateMeasureReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var id = _context.DetailsLookups.FirstOrDefault(r => r.Code == "Academic_Rate")?.Id;

            var AllowUserRates = _context.EnrollCourseAllowUserRates.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.RateTypeId == id)
                            .Include(r => r.Contact.ContactTranslations).Include(r => r.EnrollTeacherCourse.Course.CourseTranslations)
                            .Include(r => r.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations)
                            .Include(r => r.EnrollTeacherCourse.Semester.SemesterTranslations).AsQueryable();

            if (filter?.CourseCategory > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                AllowUserRates = AllowUserRates.Where(r => filter.Courses.Contains(r.EnrollTeacherCourse.CourseId));
            if (filter?.Teacher > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SemesterId == filter.Semester);

            if (!string.IsNullOrWhiteSpace(searchText))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.CourseName.Contains(searchText)
                || r.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId).CourseName.Contains(searchText)
                || r.Contact.FullName.Contains(searchText)|| r.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId).FullName.Contains(searchText));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SectionName == filter.SectionName);

            var output = AllowUserRates.OrderByDescending(r => r.Id).ToPagedList(page ?? 1, pagination);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = item.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans2 = item.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans3 = item.EnrollTeacherCourse.Semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.EnrollTeacherCourse.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.EnrollTeacherCourse.Course.Category.Name = trans2.Name;
                    if (trans3 != null)
                        item.EnrollTeacherCourse.Semester.Name = trans3.Name;
                }

            var supervisionRates = _context.AcademicSupervisionRates.ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                supervisionRates = supervisionRates.Where(r => r.CreatedOn >= filter.FromDate && r.CreatedOn <= filter.ToDate).ToList();

            var max = double.Parse(_settingService.GetOrCreate("Max_Rate_Value", "3").Value);

            var trainerRateMeasures = _context.TrainerRateMeasures.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Type == id).Include(r => r.TrainerRateMeasureTranslations).AsQueryable();
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in trainerRateMeasures)
                {
                    var trans = item.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Measure = trans.Measure;
                }

            return output.Select(r =>
            {
                var supervisionRate = supervisionRates.Where(s => s.EnrollTeacherCourseId == r.EnrollTeacherCourseId && s.CreatedBy.Equals(r.Contact.Email));

                var total = (decimal)(supervisionRate.Sum(d => d.Rate) / Math.Max(1, supervisionRate.Count() * max) * 100);

                return new EnrollCourseAllowUserRateViewModel()
                {
                    FullName = r.Contact.FullName,
                    User = r.Contact.Email,
                    CourseName = r.EnrollTeacherCourse?.Course?.CourseName,
                    CourseId = r.EnrollTeacherCourse?.Id ?? 0,
                    CourseCategory = r.EnrollTeacherCourse?.Course?.Category?.Name,
                    SemesterName = r.EnrollTeacherCourse?.Semester?.Name,
                    SectionName = r.EnrollTeacherCourse?.SectionName,
                    Total = total,
                    Rate = trainerRateMeasures.FirstOrDefault(r => r.FromRange.Value <= total && r.ToRange.Value >= total)?.Measure ?? "No Data"
                };
            });
        }

        public IPagedList<AcademicSupervisionRate> GetAcademicItems(int page, int id, string user)
        {
            var management = _context.AcademicSupervisionRates.Where(r => r.EnrollTeacherCourseId == id && r.CreatedBy.Equals(user)).Include(r=>r.Standard);

            return management.ToPagedList(page, 10);
        }

        public DataSet DownloadTrainerRateMeasureReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var id = _context.DetailsLookups.FirstOrDefault(r => r.Code == "Academic_Rate")?.Id;

            var AllowUserRates = _context.EnrollCourseAllowUserRates.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.RateTypeId == id)
                            .Include(r => r.Contact.ContactTranslations).Include(r => r.EnrollTeacherCourse.Course.CourseTranslations)
                            .Include(r => r.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations)
                            .Include(r => r.EnrollTeacherCourse.Semester.SemesterTranslations).AsQueryable();

            if (filter?.CourseCategory > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.Course.CategoryId == filter.CourseCategory);
            if (filter?.Courses != null)
                AllowUserRates = AllowUserRates.Where(r => filter.Courses.Contains(r.EnrollTeacherCourse.CourseId));
            if (filter?.Teacher > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.TeacherId == filter.Teacher);
            if (filter?.Semester > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SemesterId == filter.Semester);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.CourseName.Contains(filter.SearchText)
                || r.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).CourseName.Contains(filter.SearchText)
                || r.Contact.FullName.Contains(filter.SearchText) || r.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId).FullName.Contains(filter.SearchText));

            if (!string.IsNullOrWhiteSpace(filter.SectionName))
                AllowUserRates = AllowUserRates.Where(r => r.EnrollTeacherCourse.SectionName == filter.SectionName);

            var output = AllowUserRates.OrderByDescending(r => r.Id);
            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans1 = item.EnrollTeacherCourse.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans2 = item.EnrollTeacherCourse.Course.Category.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    var trans3 = item.EnrollTeacherCourse.Semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.EnrollTeacherCourse.CourseName = trans1.CourseName;
                    if (trans2 != null)
                        item.EnrollTeacherCourse.Course.Category.Name = trans2.Name;
                    if (trans3 != null)
                        item.EnrollTeacherCourse.Semester.Name = trans3.Name;
                }

            var supervisionRates = _context.AcademicSupervisionRates.ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                supervisionRates = supervisionRates.Where(r => r.CreatedOn >= filter.FromDate && r.CreatedOn <= filter.ToDate).ToList();

            var max = double.Parse(_settingService.GetOrCreate("Max_Rate_Value", "3").Value);

            var trainerRateMeasures = _context.TrainerRateMeasures.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Type == id).Include(r => r.TrainerRateMeasureTranslations).AsQueryable();
            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in trainerRateMeasures)
                {
                    var trans = item.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Measure = trans.Measure;
                }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Course Name"], typeof(string));
            dt.Columns.Add(localizer["Course Category"], typeof(string));
            dt.Columns.Add(localizer["Teacher Name"], typeof(string));
            dt.Columns.Add(localizer["Semester Name"], typeof(string));
            dt.Columns.Add(localizer["Section Name"], typeof(string));
            dt.Columns.Add(localizer["Total"], typeof(string));
            dt.Columns.Add(localizer["Rate"], typeof(string));
            foreach (var item in output)
            {
                var supervisionRate = supervisionRates.Where(s => s.EnrollTeacherCourseId == item.EnrollTeacherCourseId && s.CreatedBy.Equals(item.Contact.Email));

                var total = (decimal)(supervisionRate.Sum(d => d.Rate) / Math.Max(1, supervisionRate.Count() * max) * 100);

                DataRow dr = dt.NewRow();
                dr[0] = item.EnrollTeacherCourse?.Course?.CourseName;
                dr[1] = item.EnrollTeacherCourse?.Course?.Category?.Name;
                dr[2] = item.Contact.FullName;
                dr[3] = item.EnrollTeacherCourse?.Semester?.Name;
                dr[4] = item.EnrollTeacherCourse?.SectionName;
                dr[5] = total.ToString("F2");
                dr[6] = trainerRateMeasures.FirstOrDefault(r => r.FromRange.Value <= total && r.ToRange.Value >= total)?.Measure ?? localizer["No Data"];
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
