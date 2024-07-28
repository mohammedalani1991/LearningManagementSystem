using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISectionOfCourseService
    {
        IPagedList<SectionOfCourse> GetSectionOfCourses(string searchText, int? page, int languageId, int pagination,int? CourseId);
        SectionOfCourse GetSectionOfCourseById(int id);
        SectionOfCourse GetSectionOfCourseById_WithoutUsing(int id, LearningManagementSystemContext db);
        SectionOfCourseViewModel GetSectionOfCourseById(int id, int languageId);
        List<SectionOfCourse> GetSectionOfCourseByCourseId(int Courseid);
        List<SectionOfCourseViewModel> GetSectionOfCourseByCourseId(int Courseid, int languageId);
        SectionOfCourse AddSectionOfCourse(SectionOfCourseViewModel sectionofCourse);
        SectionOfCourse AddSectionOfCourse_WithoutUsing(SectionOfCourseViewModel sectionoffcourseViewModel, LearningManagementSystemContext db);
        void EditSectionOfCourse(SectionOfCourseViewModel sectionofCourseViewModel, SectionOfCourse sectionofCourse);
        void EditSectionOfCourse_WithoutUsing(SectionOfCourseViewModel sectionoffcourseViewModel, SectionOfCourse sectionoffcourse, LearningManagementSystemContext db);
        void DeleteSectionOfCourse(SectionOfCourse sectionofCourse, bool WithoutUsing = false, LearningManagementSystemContext context = null);
        List<SectionOfCourse> GetSectionOfCourseByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db);
    }
}
