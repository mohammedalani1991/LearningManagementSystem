using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.BankOfQuestion
{
    public interface IQuestionService
    {
        IPagedList<Question> GetQuestions(string searchText, int? page, int? category, int? course, int? type, int languageId, int pagination, int TeacherId = 0);
        Question GetQuestionById(int id);
        ExamQuestion GetExamQuestionByQuestionId(int id);
        QuestionViewModel GetQuestionById(int id, int languageId);
        void AddQuestion(QuestionViewModel questionViewModel);
        void AddQuestions(QuestionViewModel questionViewModel);
        void EditQuestion(QuestionViewModel questionViewModel, Question question);
        void DeleteQuestion(Question question);
        IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(string searchText, int? page, int? category, int? course, int? type, int languageId, int pagination = 25, int IsRandomly=0, int TeacherId = 0, int TemplateId = 0);
        IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(List<ExamQuestion> examQuestions, string searchText, int? page, int? category, int? course, int? type, int languageId, int pagination = 25, int IsRandomly=0, int TeacherId = 0, int TemplateId = 0);
        IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(List<EnrollCourseExamQuestion> examQuestions, string searchText, int? page, int? category, int? course, int? type, int languageId, int pagination = 25, int IsRandomly=0, int TeacherId = 0, int TemplateId = 0);
        List<EnrollCourseExamQuestion> GetQuestionsList(int EnrollCourseExamID);
    }
}
