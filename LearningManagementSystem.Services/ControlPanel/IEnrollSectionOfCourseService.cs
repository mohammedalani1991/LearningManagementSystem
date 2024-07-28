using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollSectionOfCourseService
    {

        EnrollSectionOfCourse GetEnrollSectionOfCourseById(int id);
        IPagedList<EnrollSectionOfCourse> GetEnrollSectionOfCourses(int? TeacherId,int? studentId, int? page, int languageId, int pagination,int? EnrollTeacherCourseId);
        EnrollSectionOfCourseViewModel GetEnrollSectionOfCourseById(int id, int languageId);
        EnrollSectionOfCourse AddEnrollSectionOfCourseForAdmin(SectionOfCourse sectionoffcoursel, int EnrollCourseId, LearningManagementSystemContext db);
        EnrollSectionOfCourse AddEnrollSectionOfCourse(EnrollSectionOfCourseViewModel sectionoffcourseViewModel);
        void EditEnrollSectionOfCourse(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, EnrollSectionOfCourse sectionoffcourse);
        void DeleteEnrollSectionOfCourse(EnrollSectionOfCourse EnrollSectionOfCourse);
        void DeleteEnrollSectionByEnrollTeacherCourseID(int EnrollTeacherCourseID);
        List<EnrollSectionOfCourse> GetEnrollSectionByEnrollTeacherCourseId(int EnrollTeacherCourseId, int languageId);
        void DeleteEnrollSectionByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db);
        EnrollSectionOfCourse AddEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, LearningManagementSystemContext db);
        EnrollSectionOfCourse GetEnrollSectionOfCourseById_WithoutUsing(int id, LearningManagementSystemContext db);
        void EditEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourseViewModel sectionoffcourseViewModel, EnrollSectionOfCourse sectionoffcourse, LearningManagementSystemContext db);
        void DeleteEnrollSectionOfCourse_WithoutUsing(EnrollSectionOfCourse sectionoffcourse, LearningManagementSystemContext db);

        void FetchCoursesContent(int id);
    }
}
