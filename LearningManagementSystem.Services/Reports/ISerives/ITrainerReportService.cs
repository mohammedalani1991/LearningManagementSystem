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
    public interface ITrainerReportService
    {
        IPagedList<EnrollCourseAllowUserRateViewModel> GetTrainerRateReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        IPagedList<ManagementRateLine> GetManagementItems(int page, int id, string user);
        DataSet DownloadTrainerRateReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer);

        IPagedList<EnrollCourseAllowUserRateViewModel> GetTrainerRateMeasureReports(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        IPagedList<AcademicSupervisionRate> GetAcademicItems(int page, int id, string user);
        DataSet DownloadTrainerRateMeasureReports<T>(FilterViewModel filter, IStringLocalizer<T> localizer);
    }
}
