using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.BankOfQuestion
{
    public interface IQuestionOptionService
    {
        List<QuestionOption> GetQuestionOptiosByQuestionId(int id);
        List<QuestionOptionViewModel> GetQuestionOptiosByQuestionId(int id, int languageId);
        void EditQuestionOption(int questionId,List<QuestionOptionViewModel> questionOptionViewModel, int languageId);
    }
}
