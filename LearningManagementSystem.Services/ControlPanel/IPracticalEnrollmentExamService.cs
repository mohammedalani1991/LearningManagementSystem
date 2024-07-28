using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IPracticalEnrollmentExamService
    {
        int GetMark(int practicalExamId, int courseId);
        public PracticalEnrollmentExam GetPracticalEnrollmentExamById(int id);  
        IPagedList<PracticalEnrollmentExam> GetPracticalEnrollmentExam(int? typeId , int enrollTeacherCourseId, int page, string searchText, int languageId , int pagination);
        IPagedList<PracticalEnrollmentExam> GetSupportPracticalEnrollmentExam(int trainerId, int? typeId , int enrollTeacherCourseId, int page, string searchText, int languageId , int pagination);
        List<PracticalEnrollmentExam> GetPracticalEnrollmentExam(int enrollTeacherCourseId, int languageId);
        int? GetTypeId(int examId);
        void AddPracticalEnrollmentExam(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel);
        void AddPracticalEnrollmentExam_WithoutUsing(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel, LearningManagementSystemContext db);
        void EditPracticalEnrollmentExam(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel, PracticalEnrollmentExam practicalEnrollmentExam);
        List<PracticalEnrollmentExamTrainer> GetPracticalEnrollmentExamTrainers(int examId);
        PracticalEnrollmentExamTrainer GetPracticalEnrollmentExamTrainers(int practicalEnrollmentExamId, int TrainerId);
        void AddPracticalEnrollmentExamTrainer(PracticalEnrollmentExamTrainer practicalEnrollmentExamTrainer);
        void RemovePracticalEnrollmentExamTrainer(PracticalEnrollmentExamTrainer practicalEnrollmentExamTrainer);
    }
}
