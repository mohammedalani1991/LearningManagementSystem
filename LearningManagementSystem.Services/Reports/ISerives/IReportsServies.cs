using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.Reports.ISerives
{
    public interface IReportsServies
    {
        CoursesReportViewModel GetCourses(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadCoursesReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer);

        IPagedList<EnrollStudentCourse> GetStudentsCourses(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadStudentsCoursesReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer);

        SenangPayReportsViewModel GetCoursePayments(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadCoursePaymentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer);

        SenangPayReportsViewModel GetProjectPayments(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadProjectPaymentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer);

        IPagedList<ExamsAndAssignmentsViewModel> GetExamsAndAssignmentsReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadExamsAndAssignmentsReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer);
    }
}
