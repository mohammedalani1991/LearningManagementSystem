using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningManagementSystem.Services.Controllers
{
    public class EnrollStudentAssigmentService : IEnrollStudentAssigmentService
    {
        private readonly LearningManagementSystemContext _context;

        public EnrollStudentAssigmentService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public EnrollStudentAssigment GetEnrollStudentAssigment(int id)
        {
            return _context.EnrollStudentAssigments.Include(r=>r.EnrollCourseAssigment.EnrollTeacherCourse).FirstOrDefault(r=>r.Id==id && r.Status == (int)GeneralEnums.StatusEnum.Active);
        }

        public List<EnrollCourseAssigmentQuestion> EnrollCourseAssigmentQuestionByEnrollCourseAssigmentId(int id ,int languageId)
        {
            var qustions = _context.EnrollCourseAssigmentQuestions.Where(r => r.EnrollCourseAssigmentId == id 
                && r.Status == (int)GeneralEnums.StatusEnum.Active).Include(r => r.EnrollCourseAssigmentQuestionOptions).Include(r=>r.EnrollCourseAssigmentQuestionTranslations).ToList();

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in qustions)
                {
                    var trans = item.EnrollCourseAssigmentQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.QuestionName = trans.Name;
                    foreach (var item1 in item.EnrollCourseAssigmentQuestionOptions)
                    {
                        var trans1 = _context.EnrollCourseAssigmentQuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.OptionId == item1.Id);
                        if (trans1 != null)
                            item1.Name = trans1.Name;
                    }
                }

            return qustions;
        }

        public void AddEnrollStudentAssigmentAnswer(List<EnrollStudentAssigmentAnswer> enrollStudentAssigmentAnswers)
        {
            _context.EnrollStudentAssigmentAnswers.AddRange(enrollStudentAssigmentAnswers);
            _context.SaveChanges();
        }
    }
}
