using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Services.Reports.ISerives;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using X.PagedList;

namespace LearningManagementSystem.Services.Reports.Service
{
    public class StudentReportService : IStudentReportService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly IStudentService _studentService;

        public StudentReportService(LearningManagementSystemContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        public IPagedList<Student> GetStudents(string searchText, int? page, int languageId, int pagination, FilterViewModel filter)
        {
            var students = _context.Students.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.Contact.ContactTranslations).Include(r => r.StudentExpulsionHistories)
                .Include(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active)).AsQueryable();

            if (filter?.Gender > 0)
                students = students.Where(r => r.Contact.GenderId == filter.Gender);
            if (filter?.Country > 0)
                students = students.Where(r => r.Contact.CountryId == filter.Country);
            if (filter?.City > 0)
                students = students.Where(r => r.Contact.CityId == filter.City);
            if (filter?.AgeFrom > 0)
                students = students.Where(r => r.BirthDate <= DateTime.Now.AddYears(-filter.AgeFrom));
            if (filter?.AgeTo > 0)
                students = students.Where(r => r.BirthDate >= DateTime.Now.AddYears(-filter.AgeTo));
            if (filter.FromDate != default && filter.ToDate != default)
                students = students.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                students = students.Where(r => r.Status == filter.Status);
            if (filter?.CourseNum > 0)
                students = students.Where(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active).Count() == filter.CourseNum);
            if (filter?.Price > 0)
                students = students.Where(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active).Sum(s=>s.Price) == filter.Price);

            if (!string.IsNullOrWhiteSpace(searchText))
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    students = students.Where(r => r.Contact.FullName.Contains(searchText));
                else
                    students = students.Where(r => r.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) & t.LanguageId == languageId));

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = students;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                }

            return output;
        }

        public DataSet DownloadStudentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer)
        {
            var students = _context.Students.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
               .Include(r => r.Contact.ContactTranslations).Include(r => r.StudentExpulsionHistories)
               .Include(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active)).AsQueryable();

            if (filter?.Gender > 0)
                students = students.Where(r => r.Contact.GenderId == filter.Gender);
            if (filter?.Country > 0)
                students = students.Where(r => r.Contact.CountryId == filter.Country);
            if (filter?.City > 0)
                students = students.Where(r => r.Contact.CityId == filter.City);
            if (filter?.AgeFrom > 0)
                students = students.Where(r => r.BirthDate <= DateTime.Now.AddYears(-filter.AgeFrom));
            if (filter?.AgeTo > 0)
                students = students.Where(r => r.BirthDate >= DateTime.Now.AddYears(-filter.AgeTo));
            if (filter.FromDate != default && filter.ToDate != default)
                students = students.Where(r => r.CreatedOn.Date >= filter.FromDate.Date && r.CreatedOn.Date <= filter.ToDate.Date);
            if (filter?.Status > 0)
                students = students.Where(r => r.Status == filter.Status);
            if (filter?.CourseNum > 0)
                students = students.Where(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active).Count() == filter.CourseNum);
            if (filter?.Price > 0)
                students = students.Where(r => r.EnrollStudentCourses.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active).Sum(s => s.Price) == filter.Price);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
                if (filter.LanguageId == CultureHelper.GetDefaultLanguageId())
                    students = students.Where(r => r.Contact.FullName.Contains(filter.SearchText));
                else
                    students = students.Where(r => r.Contact.ContactTranslations.Any(t => t.FullName.Contains(filter.SearchText) & t.LanguageId == filter.LanguageId));

            var output = students.OrderByDescending(r => r.CreatedOn).ToList();

            if (filter.LanguageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == filter.LanguageId);
                    if (trans != null)
                        item.Contact.FullName = trans.FullName;
                }

            List<DetailsLookupViewModel> countries = LookupHelper.GetCountries(filter.LanguageId);
            List<DetailsLookupViewModel> cities = LookupHelper.GetCities(filter.LanguageId);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(localizer["Full Name"], typeof(string));
            dt.Columns.Add(localizer["Gender"], typeof(string));
            dt.Columns.Add(localizer["Email"], typeof(string));
            dt.Columns.Add(localizer["Paid Amount"], typeof(string));
            dt.Columns.Add(localizer["Course Number"], typeof(string));
            dt.Columns.Add(localizer["Age"], typeof(string));
            dt.Columns.Add(localizer["Country"], typeof(string));
            dt.Columns.Add(localizer["City"], typeof(string));
            dt.Columns.Add(localizer["Created On"], typeof(string));
            dt.Columns.Add(localizer["Created By"], typeof(string));
            dt.Columns.Add(localizer["Status"], typeof(string));
            foreach (var item in output)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.Contact.FullName;
                dr[1] = LookupHelper.GetLookupDetailsById(item.Contact.GenderId ?? 0, filter.LanguageId)?.Name;
                dr[1] = item.Contact.Email;
                dr[3] = _studentService.GetPaymentAmount(item.Id);
                dr[4] = _studentService.GetCourseCount(item.Id);
                dr[5] = LookupHelper.GetAge(item.BirthDate);
                dr[6] = countries.FirstOrDefault(r => r.Id == item.Contact.CountryId)?.Name ?? "";
                dr[7] = cities.FirstOrDefault(r => r.Id == item.Contact.CityId)?.Name ?? "";
                dr[8] = item.CreatedOn.ToShortDateString();
                dr[9] = LookupHelper.GetFullNameByEmail(item.CreatedBy, filter.LanguageId);
                dr[10] = LookupHelper.GetStatusById(item.Status, filter.LanguageId)?.Name;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        public IPagedList<StudentExpulsionHistory> GetStudentExpulsionHistory(int? page, int pagination, int languageId, int id)
        {
            var history = _context.StudentExpulsionHistories.Where(r => r.StudentId == id).Include(r => r.EnrollStudentCourse.Course.Course.CourseTranslations).AsQueryable();
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in history)
                {
                    var trans = item.EnrollStudentCourse.Course.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.EnrollStudentCourse.Course.Course.CourseName = trans.CourseName;
                }

            return history.OrderByDescending(r => r.CreatedOn).ToPagedList(page ?? 1, pagination);
        }
    }
}
