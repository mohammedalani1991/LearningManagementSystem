using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISectionOfCourseQuizService
    {
        SectionOfCourseQuiz GetQuizById(int id);
        IPagedList<SectionOfCourseQuiz> GetQuizzes(int id, int? page, int languageId, int pagination = 25);
        void RefetchQuizzes(int id, string createdBy);
        void RefetchCourseQuizzes(int id, string createdBy);
        void EditQuiz(SectionOfCourseQuiz quiz, int num, bool isTrue);
        void DeleteQuizzesBySectionID(int id);
    }
}
