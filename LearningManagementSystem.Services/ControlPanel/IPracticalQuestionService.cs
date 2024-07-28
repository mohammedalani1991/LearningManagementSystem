using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IPracticalQuestionService
    {
        IPagedList<PracticalQuestion> GetPracticalQuestions(string searchText, int? page, int? TypeId, int languageId, int pagination);
        PracticalQuestion GetPracticalQuestionById(int id);
        PracticalQuestion GetPracticalQuestionById(int id, int languageId);
        void AddPracticalQuestion(PracticalQuestionViewModel practicalQuestionViewModel);
        void EditPracticalQuestion(PracticalQuestionViewModel practicalQuestionViewModel, PracticalQuestion practicalQuestion);
        void DeletePracticalQuestion(PracticalQuestion practicalQuestion);
    }
}
