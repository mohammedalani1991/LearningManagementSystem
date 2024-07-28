using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICourseResourceService
    {
        IPagedList<CourseResource> GetCourseResources(string searchText, int? page, int languageId, int pagination);
        CourseResource GetCourseResourceById(int id);
        CourseResourceViewModel GetCourseResourceById(int id, int languageId);
        List<CourseResource> GetCourseResourceByLectureId(int LectureId, int languageId);
        List<CourseResource> GetCourseResourceByLectureId(int LectureId);
        List<CourseResource> GetCourseResourceByLectureId_WithoutUsing(int LectureId, LearningManagementSystemContext db);
        void AddCourseResource(CourseResourceViewModel CourseResource);
        void EditCourseResource(CourseResourceViewModel CourseResourceViewModel, CourseResource CourseResource);
        void DeleteCourseResource(CourseResource CourseResource);
        void DeleteCourseResource_WithoutUsing(CourseResource CourseResource, LearningManagementSystemContext db);
        void DeleteCourseResourceByLectureId(int LectureId);
        void DeleteCourseResourceBySectionId(int SectionId,bool WithoutUsing = false, LearningManagementSystemContext context = null);
        void DeleteCourseResourceByLectureId_WithoutUsing(int LectureId, LearningManagementSystemContext db);
        CourseResource GetCourseResourceById_WithoutUsing(int id, LearningManagementSystemContext db);
        void EditCourseResource_WithoutUsing(CourseResourceViewModel CourseResourceViewModel, CourseResource CourseResource, LearningManagementSystemContext db);
        void AddCourseResource_WithoutUsing(CourseResourceViewModel CourseResourceViewModel, LearningManagementSystemContext db);
    }
}
