using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
   public interface IEnrollStudentCourseService
    {
        IPagedList<EnrollStudentCourse> GetEnrollStudentCourses(int? page, int? CourseId = null, int? studentId = null, int? TeacherId = null, int? EnrollTeacherCourseID = null, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, string searchText = "");
        IPagedList<EnrollStudentCourse> GetEnrollStudentCoursesForQuiz(int? page, int? EnrollTeacherCourseID, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, string searchText = "");
        EnrollStudentCourse GetEnrollStudentCourseById(int id);
        string GetStudentNameFromEnrollStudentCourseById(int id, int languageId);
        EnrollStudentCourseViewModel GetEnrollStudentCourseById(int id, int languageId);
        EnrollStudentCourseViewModel GetEnrollStudentCourseByStudentIdAndCourseId(int studentId, int courseId);
        int AddEnrollStudentCourse(EnrollStudentCourseViewModel EnrollStudentCourse);
        void EditEnrollStudentCourse(EnrollStudentCourseViewModel EnrollStudentCourseViewModel, EnrollStudentCourse EnrollStudentCourse);
        void DeleteEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse);
        void ExpelEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse ,string createdBy);
        void CancelExpulsionEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse, string createdBy);
        int GetCountEnrollStudentCourses(int EnrollTeacherCourseId, int studentId = 0);
        int GetAttendanceDays(int EnrollTeacherCourseId);
        void AddEnrollStudentCourse_WthoutUsing(EnrollStudentCourseViewModel EnrollStudentCourseViewModel, LearningManagementSystemContext db);
        List<EnrollStudentCourse> GetEnrollStudentCourseByStudentId(int StudentId, int languageId);
        EnrollStudentCourse GetEnrollStudentCourse(int StudentId, int enrollTeacherCourseId);
        string CheckAbilityToEnrollStudentInCourse(int EnrollTeacherCourseId, int StudentId);
        DateTime? CheckIfHasBeenExpilled(int StudentId);
        EnrollStudentCourseViewModel GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(int studentId, int EnrollTeacherCourseId);
        bool IsPassed(int CourseId, int StudentId);
        Byte[] GetCertificate(EnrollStudentCourse course, int templateId,int languageId,string baseUri);
        int GetStudentCount(int enrollTeacherCourseId);
        string GetEvaluation(int id, int languageId);


        void CalculateMarks(EnrollTeacherCourse enrollTeacherCourse);
    }
}
