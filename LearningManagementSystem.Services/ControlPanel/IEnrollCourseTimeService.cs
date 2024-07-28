using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseTimeService
    {
       
        EnrollCourseTime GetEnrollCourseTimeById(int id);
       
      
        EnrollCourseTime AddEnrollCourseTime(EnrollCourseTimeViewModel EnrollCourseTime);
        List<EnrollCourseTime> GetEnrollCourseTimeByEnrollTeacherCourseId(int EnrollTeacherCourseID);
        void DeleteEnrollCourseTime(EnrollCourseTime EnrollCourseTime);
        void DeleteEnrollCourseTimeByEnrollTeacherCourseID(int EnrollTeacherCourseID);
        EnrollCourseTime AddEnrollCourseTime_WithoutUsing(EnrollCourseTimeViewModel enrollCourseTimeViewModel, LearningManagementSystemContext db);
        void DeleteEnrollCourseTimeByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db);
    }
}
