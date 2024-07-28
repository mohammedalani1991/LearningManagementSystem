using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using X.PagedList;
using static LearningManagementSystem.Services.ControlPanel.EnrollTeacherCourseService;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollTeacherCourseService
    {
        IPagedList<EnrollTeacherCourse> GetEnrollTeacherCourses(int? page, int languageId, int pagination, int? CourseId = 0, int? TeacherId = 0, int? LearningMethodId = 0, int? SemesterId = 0);
        IPagedList<EnrollTeacherCourse> GetEnrollTeacherSupportCourses(int? page, int languageId, int pagination, int TeacherId);
        List<EnrollTeacherCourse> GetEnrollTeacherCoursesIds(int? CourseId = 0, int? TeacherId = 0, int? SemesterId = 0);
        IPagedList<EnrollStudentCourse> GetEnrollmentCourse(int id, int? page, int pagination, int languageId);
        EnrollTeacherCourse GetEnrollTeacherCourseById(int id);
        EnrollTeacherCourse GetEnrollById(int id, int languageId);
        EnrollTeacherCourseViewModel GetEnrollTeacherCourseById(int id, int languageId);
        EnrollTeacherCourse AddEnrollTeacherCourse(EnrollTeacherCourseViewModel EnrollTeacherCourse, Course course);
        void DeleteEnrollTeacherCourse(EnrollTeacherCourse EnrollTeacherCourse);
        EnrollTeacherCourse EditEnrollTeacherCourse(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, EnrollTeacherCourse enrollTeacherCourse, Course Course);
        void EditEnrollTeacherCourseNote(string note, EnrollTeacherCourse enrollTeacherCourse, DateTime start, DateTime end);
        EnrollTeacherCourse GetEnrollTeacherCourseByTeacherIdAndCourseId(int TeacherId, int CourseId, string SectionName = "");
        List<EnrollTeacherCourse> GetEnrollTeacherCourseByTeacherId(int TeacherId, int languageId);
        EnrollTeacherCourse AddEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, Course course, LearningManagementSystemContext db);
        EnrollTeacherCourse EditEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, EnrollTeacherCourse enrollTeacherCourse, Course course, LearningManagementSystemContext db);
        void DeleteEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourse enrollTeacherCourse, LearningManagementSystemContext db);
        List<EnrollTeacherCourse> GetEnrollTeacherCourseByCourseId(int CourseId);
        void PassStudent(int id);
        void ChangeEnrollStudent(int ChangeEnrollStudentId, int EnrollTeacherCourseId);
        List<Course> GetCourseByTeacherId(int TeacherId, int languageId);
        int GetEnrollmentCourseIdFormStudentId(int enrollTeacherCourseId, int studentId);
        List<Data> GetEnrollmentCourseList(int languageId);
        List<Course> GetCourseList(int languageId);
        List<Course> GetEnrollCourseListForSemester(int? semester,int languageId);
        List<Data> GetTeacherList(int languageId);
        List<Data> GetTeacherListByCourseId(int? semester, int? courseId, int languageId);
        decimal GetCourseMark(int courseId);
        void CloseCourse(int courseId);
    }
}
