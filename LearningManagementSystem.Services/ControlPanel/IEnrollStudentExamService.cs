using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
  public  interface IEnrollStudentExamService
    {
        EnrollStudentExam GetEnrollStudentExamByEnrollCourseExamId(int EnrollCourseExamId, int EnrollStudentCourseId);
        EnrollStudentExam AddEnrollStudentExam(EnrollStudentExamViewModel studentViewModel, LearningManagementSystemContext db);
        string CheckAbilityToEnrollStudentExam(int EnrollCourseExamId, int EnrollStudentCourseId, int StudentId, bool IsExamPreRequest = false, bool TakeAgain = false);
        IPagedList<EnrollStudentExam> GetEnrollStudentExam(int? page, int languageId, int pagination = 25, int? EnrollCourseExamId = 0);
        EnrollStudentExam GetEnrollStudentExam(int Id);
        void EditMarkHeGotEnrollStudentExam(EnrollStudentExamViewModel enrollStudentExamViewModel, EnrollStudentExam enrollStudentExam, LearningManagementSystemContext db);
        string CheckHasPreRequestsExam(int EnrollTeacherCourseId, int StudentId);
        EnrollStudentExam GetEnrollStudentExamByEnrollStudentCourseId(int EnrollStudentCourseId, int EnrollCourseExamId);
        EnrollStudentExam GetEnrollStudentExamByEnrollCourseExamId(int EnrollCourseExamId);

    }
}
