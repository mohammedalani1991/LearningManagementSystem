using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IExamQuestionService
    {
        IPagedList<ExamQuestion> GetExamQuestions( int? page, int languageId, int pagination, int? TemplateId = 0, int? QuestionId = 0);
        ExamQuestion GetExamQuestionById(int id);
        List<ExamQuestion> GetExamQuestionByTemplateId(int TemplateId);
        ExamQuestion AddExamQuestion(ExamQuestionViewModel ExamQuestion);
        void DeleteExamQuestion(ExamQuestion ExamQuestion);
        ExamQuestion GetExamQuestionByTemplateIdAndQuestionID(int? TemplateId,int QuestionId);
        void DeleteExamQuestionByQuestionID(int QuestionID);
        List<ExamQuestionViewModel> GetQuestionByTemplateId(int TemplateId);
        List<ExamQuestionViewModel> GetQuestionByTemplateId_WithoutUsing(int TemplateId, LearningManagementSystemContext db);
        void DeleteExamQuestionByTamplateID_WithoutUsing(int TamplateID, LearningManagementSystemContext db);
        void AddExamQuestion_WithoutUsing(ExamQuestionViewModel ExamQuestionViewModel, LearningManagementSystemContext db);
        int GetCountExamQuestionByTemplateId(int TemplateId);

    }
}
