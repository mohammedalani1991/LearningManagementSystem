using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseExamQuestionService
    {
       
        EnrollCourseExamQuestion AddEnrollCourseExamQuestion(EnrollCourseExamQuestionViewModel EnrollCourseExamQuestion);
        void DeleteEnrollCourseExamQuestion(EnrollCourseExamQuestion EnrollCourseExamQuestion);
        void DeleteEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID);
        List<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID , int languageId);
        EnrollCourseExamQuestion AddEnrollCourseExamQuestion_WithoutUsing(EnrollCourseExamQuestionViewModel enrollCourseExamQuestionQuestionViewModel, LearningManagementSystemContext db);
        bool DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(int EnrollCourseExamID, LearningManagementSystemContext db);
        int GetCountEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID);
        IPagedList<EnrollCourseExamQuestion> GetExamQuestions(int? page, int languageId , int pagination , int? EnrollCourseExamId);
        IPagedList<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID(int id, int? page, int languageId, int pagination);
        //List<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID_WithAnswers(int EnrollCourseExamID, int languageId );
        EnrollCourseExamQuestion EditMarkEnrollCourseExamQuestion(int EnrollCourseExamQuestionId, int Mark, EnrollCourseExamQuestion enrollCourseExamQuestion, LearningManagementSystemContext db);
         EnrollCourseExamQuestion GetEnrollCourseExamQuestionByID(int ID);
        void DeleteEnrollCourseExamQuestionByEnrollCourseExamIDAndExamQuestionID(int EnrollCourseExamID, int ExamQuestionID);
         EnrollCourseExamQuestion GetEnrollCourseExamQuestionByQuestionId(int EnrollCourseExamID, int QuestionId);
    }
}
