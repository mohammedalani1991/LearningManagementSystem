using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollLectureService
    {
        void EditEnrollLecture(EnrollLectureViewModel lectureViewModel, EnrollLecture lecture);
        EnrollLecture GetEnrollLectureById(int id);
        List<EnrollLecture> GetEnrollLectureBySectionId(int SectionId);
        List<EnrollLecture> GetEnrollLectureBySectionId(int SectionId, int languageId);
        EnrollLecture AddEnrollLectureForAdmin(Lecture Lecture, int EnrollCourseId, int EnrollSectionId, LearningManagementSystemContext db);
        EnrollLecture AddEnrollLecture(EnrollLectureViewModel enrollLectureViewModel);
        void DeleteEnrollLecture(EnrollLecture enrollLecture);
        
        void DeleteEnrollLectureByEnrollTeacherCourseID(int TeacherCourseID);
        void DeleteEnrollLLectureBySectionID(int SectionID);
        void DeleteEnrollLectureByEnrollTeacherCourseID_WithoutUsing(int TeacherCourseID, LearningManagementSystemContext db);
        EnrollLecture AddEnrollLecture_WithoutUsing(EnrollLectureViewModel enrollLectureViewModel, LearningManagementSystemContext db);
        List<EnrollLecture> GetEnrollLectureBySectionId_WithoutUsing(int SectionId, LearningManagementSystemContext db);
        void DeleteEnrollLecture_WithoutUsing(EnrollLecture enrollLecture, LearningManagementSystemContext db);
        EnrollLecture GetEnrollLectureById_WithoutUsing(int id, LearningManagementSystemContext db);
        void EditEnrollLecture_WithoutUsing(EnrollLectureViewModel lectureViewModel, EnrollLecture lecture, LearningManagementSystemContext db);
        void DeleteEnrollLLectureBySectionID_WithoutUsing(int SectionID, LearningManagementSystemContext db);
        List<EnrollLecture> GetEnrollLectureByEnrollTeacherCoursId(int EnrollTeacherCoursId, int languageId);
    }
}
