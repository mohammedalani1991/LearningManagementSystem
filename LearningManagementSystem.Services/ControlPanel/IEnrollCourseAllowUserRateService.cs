using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseAllowUserRateService
    {
        EnrollCourseAllowUserRate GetAllowUserRateById(int id);
        EnrollCourseAllowUserRate GetAllowUserRateById(int id, int languageId);
        IPagedList<EnrollCourseAllowUserRate> GetAllowUserRates(string searchText, int page, int languageId, int pagination, int courseId);
        void AddEnrollCourseAllowUserRate(EnrollCourseAllowUserRateViewModel assignmentViewModel);
        EnrollCourseAllowUserRate EditEnrollCourseAllowUserRate(EnrollCourseAllowUserRateViewModel allowUserRateViewModel, EnrollCourseAllowUserRate enrollCourseAllowUser);
        void DeleteAllowUserRate(EnrollCourseAllowUserRate allowUserRate);
        bool CheckAllowUserToRate(string username, int enrollTeacherCourseId, int rateTypeId);
    }
}
