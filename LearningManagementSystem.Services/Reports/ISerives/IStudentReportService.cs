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
    public interface IStudentReportService
    {
        IPagedList<Student> GetStudents(string searchText, int? page, int languageId, int pagination, FilterViewModel filter);
        DataSet DownloadStudentsReport<T>(FilterViewModel filter, IStringLocalizer<T> localizer);
        IPagedList<StudentExpulsionHistory> GetStudentExpulsionHistory(int? page, int pagination, int languageId, int id);
    }
}
