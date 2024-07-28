using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollCourseQuizService
    {
        EnrollCourseQuiz GetQuizById(int id);
        List<EnrollCourseQuizPointe> GetQuizPointsByQuizId(int id);
        IPagedList<EnrollCourseQuiz> GetQuizzes(int id, int? page, int languageId, int pagination = 25);
        void RefetchQuizzes(int id, string createdBy);
        void RefetchQuizzes_WithoutUsing(int id, string createdBy , LearningManagementSystemContext db);
        void EditQuiz(EnrollCourseQuiz quiz, int num, bool isTrue);
        List<EnrollCourseQuiz> GetStudentQuizMarks(int enrollStudentCourseId,int enrollTeacherCourseId, int languageId);
        List<EnrollCourseQuiz> GetAllStudentQuizMarks(int enrollStudentCourseId, int enrollTeacherCourseId , int languageId);
        void AddQuizPoint(decimal? value ,int quizId ,int enrollStudentCourseId, int num, string createdBy);
        int GetFailuresCount(int id);
    }
}
