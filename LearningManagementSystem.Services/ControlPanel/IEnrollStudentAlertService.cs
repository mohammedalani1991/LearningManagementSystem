using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollStudentAlertService
    {
        EnrollStudentAlert GetAllowUserRateById(int id);
        EnrollStudentAlert GetAllowUserRateById(int id, int languageId);
        IPagedList<EnrollStudentAlert> GetAllowUserRates(string searchText, int page, int languageId, int pagination, int courseId, int? enrollStudentCourseId);
        void AddEnrollStudentAlert(EnrollStudentAlertViewModel assignmentViewModel);
        EnrollStudentAlert EditEnrollStudentAlert(EnrollStudentAlertViewModel allowUserRateViewModel, EnrollStudentAlert enrollCourseAllowUser);
        void DeleteAllowUserRate(EnrollStudentAlert allowUserRate);
    }
}
