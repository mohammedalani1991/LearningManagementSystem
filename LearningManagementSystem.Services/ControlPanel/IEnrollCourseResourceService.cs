using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseResourceService
    {
        public List<EnrollCourseResource> GetEnrollCourseResourceByLectureId(int LectureId, int languageId);
        EnrollCourseResource AddEnrollCourseResourceForAdmin(CourseResource CourseResource, int EnrollCourseId, int EnrollLectureId, LearningManagementSystemContext db);
        EnrollCourseResource AddEnrollCourseResource(EnrollCourseResourceViewModel EnrollCourseResource);
        void DeleteEnrollCourseResource(EnrollCourseResource EnrollCourseResource);
        void DeleteEnrollCourseResourceByEnrollTeacherCourseID(int EnrollTeacherCourseID);
        void DeleteEnrollCourseResourceBySectionId(int SectionId);
        List<EnrollCourseResource> GetEnrollCourseResourceByLectureId(int EnrollLectureId);
        void DeleteEnrollCourseResourceByLectureId(int EnrollLectureId);
        void EditEnrollCourseResource(EnrollCourseResourceViewModel EnrollCourseResourceViewModel, EnrollCourseResource EnrollCourseResource);
        EnrollCourseResource GetEnrollCourseResourceById(int id);
        List<EnrollCourseResource> GetEnrollCourseResource(int enrollLectureId, int languageId);
        void DeleteEnrollCourseResourceByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db);
        void DeleteEnrollCourseResourceBySectionId_WithoutUsing(int SectionId, LearningManagementSystemContext db);
        List<EnrollCourseResource> GetEnrollCourseResourceByLectureId_WithoutUsing(int EnrollLectureId, LearningManagementSystemContext db);
        void DeleteEnrollCourseResource_WithoutUsing(EnrollCourseResource enrollCourseResource, LearningManagementSystemContext db);
        void DeleteEnrollCourseResourceByLectureId_WithoutUsing(int EnrollLectureId, LearningManagementSystemContext db);
        EnrollCourseResource GetEnrollCourseResourceById_WithoutUsing(int id, LearningManagementSystemContext db);
        void EditEnrollCourseResource_WithoutUsing(EnrollCourseResourceViewModel EnrollCourseResourceViewModel, EnrollCourseResource EnrollCourseResource, LearningManagementSystemContext db);
        EnrollCourseResource AddEnrollCourseResource_WithoutUsing(EnrollCourseResourceViewModel enrollCourseResourceViewModel, LearningManagementSystemContext db);
    }
}
