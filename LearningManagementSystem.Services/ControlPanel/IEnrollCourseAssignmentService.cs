using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseAssignmentService
    {
        EnrollCourseAssigment GetAssignmentById(int id);
        EnrollCourseAssigmentQuestion GetAssignmentQuestion(int id);
        List<EnrollStudentAssigmentAnswer> GetStudentAssigmentAnswer(int id);
        EnrollCourseAssigment GetAssignmentById(int id, int languageId);
        EnrollCourseAssigmentQuestion GetAssignmentQuestion(int id, int languageId);
        IPagedList<EnrollCourseAssigment> GetAssignments(string searchText, int page, int languageId, int pagination, int courseId);
        IPagedList<EnrollStudentAssigment> GetStudentAssignments(string searchText, int page, int languageId, int pagination, int assigmentQuestionId);
        void AddEnrollCourseAssignment(EnrollCourseAssignmentViewModel assignmentViewModel);
        void AddEnrollCourseAssignment_WithoutUsing(EnrollCourseAssignmentViewModel assignmentViewModel, LearningManagementSystemContext db);
        EnrollCourseAssigment EditEnrollCourseAssignment(EnrollCourseAssignmentViewModel assignmentViewModel, EnrollCourseAssigment enrollCourseAssigment);
        void DeleteAssignment(EnrollCourseAssigment assignment);
        IPagedList<EnrollCourseAssigmentQuestion> GetAssignmentQuestions(string searchText, int page, int languageId, int pagination, int courseId);
        void AddQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel);
        void EditQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel, EnrollCourseAssigmentQuestion question);
        void DeleteQuestion(EnrollCourseAssigmentQuestion question);
    }
}
