using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.Controllers
{
    public interface IEnrollStudentAssigmentService
    {
        EnrollStudentAssigment GetEnrollStudentAssigment(int id);
        List<EnrollCourseAssigmentQuestion> EnrollCourseAssigmentQuestionByEnrollCourseAssigmentId(int id ,int languageId);
        void AddEnrollStudentAssigmentAnswer(List<EnrollStudentAssigmentAnswer> enrollStudentAssigmentAnswers);
    }
}
