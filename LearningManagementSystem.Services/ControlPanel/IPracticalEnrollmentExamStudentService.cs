using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IPracticalEnrollmentExamStudentService
    {
        PracticalEnrollmentExam GetPracticalEnrollmentExamById(int id);   
        PracticalEnrollmentExamStudent GetOrCreate(int practicalEnrollmentExamId, int enrollStudentCourseId, string createdBy);
        List<PracticalEnrollmentExamStudentSubject> GetStudentSubjects(int id, int languageId);
        PracticalEnrollmentExamStudentSubject GetStudentSubject(int practicalEnrollmentExamStudentId, int subjectId);
        void AddPracticalEnrollmentExamStudentSubject(PracticalEnrollmentExamStudentSubject practicalEnrollmentExamStudentSubject);
        void RemovePracticalEnrollmentExamStudentSubject(PracticalEnrollmentExamStudentSubject practicalEnrollmentExamStudentSubject);
        IPagedList<Subject> GetSubjectsForStudents(int practicalEnrollmentExamId, int courseId, int? TypeId, string searchText, int? page, int languageId, int pagination);
        int GetSubjectCount(int id, int courseId);
        bool DidExamStart(int id);
        bool DidExamNotEnd(int id);
        bool IsExamSubmited(int id);
        void ReCalculateMark(int id);
        bool IsExamSubmited(int practicalEnrollmentExamId, int courseId);
        int GetSubjectCoumtForStudent(int practicalEnrollmentExamId, int enrollStudentCourseId, string createdBy);
        List<PracticalQuestion> GetQuestions(int examId , int languageId);
        void DeletePracticalEnrollmentExam(PracticalEnrollmentExam practicalEnrollmentExam);
        void EditMarkAdoption(PracticalEnrollmentExam practicalEnrollmentExam, bool adopt);
        void AddExam(int practicalEnrollmentExamStudentId, List<ExamObjectViewModel> examObjects ,decimal subjectMark);
        void EditMark(int practicalEnrollmentExamStudentId, decimal mark , decimal markAfterConversion);
    }
}
