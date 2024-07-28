using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel
{
  public  interface IEnrollStudentExamAnswerService
    {
        void AddEnrollStudentAnswerExam(List<EnrollStudentExamAnswerViewModel> studentViewModel, LearningManagementSystemContext db);
        EnrollStudentExamAnswer GetEnrollStudentExamAnswer(int EnrollCourseExamQuestionId, int EnrollStudentExamID, LearningManagementSystemContext db);
        void EditEnrollStudentExamAnswer(List<EnrollStudentExamAnswerViewModel> enrollStudentExamAnswerViewModelList, LearningManagementSystemContext db);
        List<EnrollStudentExamAnswer> GetListEnrollStudentExamAnswer(int EnrollStudentExamID);
    }
}
