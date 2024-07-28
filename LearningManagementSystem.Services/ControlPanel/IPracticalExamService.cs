using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IPracticalExamService
    {
        IPagedList<PracticalExam> GetPracticalExams(int? type,string searchText, int? page, int languageId, int pagination);
        IPagedList<PracticalExamCourse> GetPracticalExamForCourse(int courseId, int? type, string searchText, int? page, int languageId, int pagination);
        PracticalExam GetPracticalExamById(int id);
        PracticalExam GetPracticalExamById(int id, int languageId);
        PracticalExam GetPracticalExamByIdWitoutStatus(int id, int languageId);
        void AddPracticalExam(PracticalExamViewModel practicalExamViewModel);
        void EditPracticalExam(PracticalExamViewModel practicalExamViewModel, PracticalExam practicalExam);
        void DeletePracticalExam(PracticalExam practicalExam); 

        List<PracticalExamQuestion> GetPracticalExamQuestions(int id);
        PracticalExamQuestion GetExamQuestion(int id, int questionId);
        void AddPracticalExamQuestion(PracticalExamQuestion practicalExamQuestion);
        void RemovePracticalExamQuestion(PracticalExamQuestion practicalExamQuestion);

        PracticalExamCourse GetPracticalExamCourse(int id);
        List<PracticalExamCourse> GetPracticalExamForCourse(int id);
        PracticalExamCourse GetCourseExam(int courseId, int examId);
        void AddPracticalExamCourse(PracticalExamCourse practicalExamCourse);
        void RemovePracticalExamCourse(PracticalExamCourse practicalExamCourse);
        void EditPracticalExamCourseMark(PracticalExamCourse practicalExamCourse, int mark);

        List<PracticalExamCourseSubject> GetSubjectsForPracticalExam(int id);
        PracticalExamCourseSubject GetPracticalExamCourseSubject(int practicalExamCourseId, int subjectId);
        void AddPracticalExamCourseSubject(PracticalExamCourseSubject practicalExamCourseSubject);
        void RemovePracticalExamCourseSubject(PracticalExamCourseSubject practicalExamCourseSubject);

        bool CheckIfItHasSubjects(int courseId, int examid);
        bool CheckIfItHasStudents(int practicalExamCourseId, int subjectId);
    }
}
