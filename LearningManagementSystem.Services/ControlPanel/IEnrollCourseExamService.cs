using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseExamService
    {
       
        EnrollCourseExam AddEnrollCourseExamForAdmin(ExamTemplate ExamTemplate, int EnrollCourseId, LearningManagementSystemContext db);
        int DeleteEnrollCourseExam(EnrollCourseExam EnrollCourseExam);
        void DeleteEnrollCourseExamByEnrollTeacherCourseID(int EnrollTeacherCourseID);
        List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId(int EnrollTeacherCourseId, int languageId);
        List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId(int EnrollTeacherCourseId);
        EnrollCourseExam GetEnrollCourseExamModelById(int Id);
        EnrollCourseExam GetEnrollCourseExamById_WithoutUsing(int CourseExamID, LearningManagementSystemContext db);
        void DeleteEnrollCourseExamByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db);
        IPagedList<EnrollCourseExam> GetEnrollCourseExams(int? page, int languageId, int pagination , int? EnrollTeacherCourseId, int? TeacherId);
        EnrollCourseExam AddEnrollCourseExam(EnrollCourseExamViewModel EnrollCourseExamViewModel);
        EnrollCourseExam AddEnrollCourseExam_WithoutUsing(EnrollCourseExamViewModel EnrollCourseExamViewModel, LearningManagementSystemContext db);
        EnrollCourseExamViewModel GetEnrollCourseExamById(int Id, int languageId);
        EnrollCourseExamViewModel GetEnrollCourseExamById(int Id);
        EnrollCourseExam EditEnrollCourseExam(EnrollCourseExamViewModel EnrollCourseExamViewModel, EnrollCourseExam enrollCourseExam);
        EnrollCourseExam GetEnrollCourseExamByCourseExamId(int Id);
        List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId_WithoutUsing(int EnrollTeacherCourseId, LearningManagementSystemContext db);
        void DeleteEnrollCourseExamByID_WithoutUsing(int CourseExamID, LearningManagementSystemContext db);
        List<EnrollCourseExam> GetPrerequisiteEnrollCourseExam(int EnrollTeacherCourseId, int languageId);
        List<EnrollCourseExam> GetEnrollCourseQuizByEnrollTeacherCourseId(int EnrollTeacherCourseId, int? EnrollLectureId, int languageId);
        void EditMarkAdoption(EnrollCourseExam enrollCourseExam, bool adopt);
    }
}
