using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollCourseExamQuestionService : IEnrollCourseExamQuestionService
    {
        private readonly ISettingService _settingService;

        public EnrollCourseExamQuestionService(ISettingService settingService)
        {
            _settingService = settingService;

        }
        public IPagedList<EnrollCourseExamQuestion> GetExamQuestions(int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? EnrollCourseExamId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Include(r => r.EnrollCourseExam).Include(d => d.Question).ThenInclude(r => r.Question).Where(r =>
                          r.Status != (int)GeneralEnums.StatusEnum.Deleted);


                if (EnrollCourseExamId > 0)
                {
                    EnrollCourseExamQuestions = EnrollCourseExamQuestions.Where(r => r.EnrollCourseExamId == EnrollCourseExamId);
                }


                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollCourseExamQuestions;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {

                        var transQuestionTrans = db.QuestionTranslations.Include(r => r.Question).FirstOrDefault(r => r.LanguageId == languageId && r.QuestionId == item.Question.QuestionId);
                        if (transQuestionTrans != null)
                        {
                            item.Question.Question.Name = transQuestionTrans.Name;
                        }

                    }
                }


                return output;
            }
        }
        public EnrollCourseExamQuestion AddEnrollCourseExamQuestion(EnrollCourseExamQuestionViewModel enrollCourseExamQuestionQuestionViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseExamQuestion = new EnrollCourseExamQuestion()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedBy = enrollCourseExamQuestionQuestionViewModel.CreatedBy,
                    QuestionId = enrollCourseExamQuestionQuestionViewModel.QuestionId,
                    EnrollCourseExamId = enrollCourseExamQuestionQuestionViewModel.EnrollCourseExamId,
                    Mark = enrollCourseExamQuestionQuestionViewModel.Mark,

                };
                db.EnrollCourseExamQuestions.Add(enrollCourseExamQuestion);
                db.SaveChanges();
                return enrollCourseExamQuestion;
            }
        }
        public EnrollCourseExamQuestion AddEnrollCourseExamQuestion_WithoutUsing(EnrollCourseExamQuestionViewModel enrollCourseExamQuestionQuestionViewModel, LearningManagementSystemContext db)
        {

            var enrollCourseExamQuestion = new EnrollCourseExamQuestion()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                CreatedBy = enrollCourseExamQuestionQuestionViewModel.CreatedBy,
                QuestionId = enrollCourseExamQuestionQuestionViewModel.QuestionId,
                EnrollCourseExamId = enrollCourseExamQuestionQuestionViewModel.EnrollCourseExamId,
                Mark = enrollCourseExamQuestionQuestionViewModel.Mark,

            };
            db.EnrollCourseExamQuestions.Add(enrollCourseExamQuestion);
            db.SaveChanges();
            return enrollCourseExamQuestion;

        }
        //public List<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID_WithAnswers(int EnrollCourseExamID, int languageId = (int)GeneralEnums.LanguageEnum.English)
        //{
        //    using (var db = new LearningManagementSystemContext())
        //    {

        //        var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Include(e => e.EnrollCourseExam).Include(s => s.EnrollStudentExamAnswers).ThenInclude(s => s.EnrollStudentExamAnswerOptions).Include(s => s.Question).ThenInclude(s => s.Question).ThenInclude(s => s.QuestionOptions).Where(r => r.EnrollCourseExamId == EnrollCourseExamID  && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

        //        if (languageId != CultureHelper.GetDefaultLanguageId())
        //        {
        //            foreach (var item in EnrollCourseExamQuestions)
        //            {

        //                var transQuestionTrans = db.QuestionTranslations.Include(r => r.Question).FirstOrDefault(r => r.LanguageId == languageId && r.QuestionId == item.Question.QuestionId );
        //                if (transQuestionTrans != null)
        //                {
        //                    item.Question.Question.Name = transQuestionTrans.Name;
        //                }

        //                foreach (var QuestionOptions in item.Question.Question.QuestionOptions)
        //                {
        //                    var QuestionOptionTranslations = db.QuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.OptionId == QuestionOptions.Id );
        //                    if (QuestionOptionTranslations != null)
        //                    {
        //                        QuestionOptions.Name = QuestionOptionTranslations.Name;
        //                    }
        //                }
        //            }
        //        }
        //        return EnrollCourseExamQuestions;
        //    }
        //}
        public List<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.EnrollCourseExamId == EnrollCourseExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    .Include(e => e.EnrollCourseExam).Include(s => s.Question).ThenInclude(s => s.Question).ThenInclude(s => s.QuestionOptions.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active)).ToList();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in EnrollCourseExamQuestions)
                    {

                        var transQuestionTrans = db.QuestionTranslations.Include(r => r.Question).FirstOrDefault(r => r.LanguageId == languageId && r.QuestionId == item.Question.QuestionId);
                        if (transQuestionTrans != null)
                        {
                            item.Question.Question.Name = transQuestionTrans.Name;
                        }

                        foreach (var QuestionOptions in item.Question.Question.QuestionOptions)
                        {
                            var QuestionOptionTranslations = db.QuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.OptionId == QuestionOptions.Id);
                            if (QuestionOptionTranslations != null)
                            {
                                QuestionOptions.Name = QuestionOptionTranslations.Name;
                            }
                        }
                    }
                }
                var exam = db.EnrollCourseExams.FirstOrDefault(r => r.Id == EnrollCourseExamID);
                Random random = new Random();

                if (exam?.Shuffle == true)
                {
                    foreach (var item in EnrollCourseExamQuestions)
                        item.Question.Question.QuestionOptions = item.Question.Question.QuestionOptions.OrderBy(x => random.Next()).ToList();
                    return EnrollCourseExamQuestions.OrderBy(x => random.Next()).ToList();
                }
                return EnrollCourseExamQuestions;
            }
        }
        public EnrollCourseExamQuestion GetEnrollCourseExamQuestionByID(int ID)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.Id == ID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefault();

                return EnrollCourseExamQuestions;
            }
        }
        public EnrollCourseExamQuestion GetEnrollCourseExamQuestionByQuestionId(int EnrollCourseExamID, int QuestionId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.QuestionId == QuestionId && r.EnrollCourseExamId == EnrollCourseExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefault();

                return EnrollCourseExamQuestions;
            }
        }
        public int GetCountEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.EnrollCourseExamId == EnrollCourseExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();

                return EnrollCourseExamQuestions;
            }
        }

        public void DeleteEnrollCourseExamQuestion(EnrollCourseExamQuestion enrollCourseExamQuestion)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseExamQuestion.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollCourseExamQuestion.DeletedOn = DateTime.Now;
                db.Entry(enrollCourseExamQuestion).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteEnrollCourseExamQuestionByEnrollCourseExamIDAndExamQuestionID(int EnrollCourseExamID, int ExamQuestionID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(e => e.EnrollCourseExamId == EnrollCourseExamID && e.QuestionId == ExamQuestionID).ToList();
                db.EnrollCourseExamQuestions.RemoveRange(EnrollCourseExamQuestions);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollCourseExamQuestionByEnrollCourseExamID(int EnrollCourseExamID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(e => e.EnrollCourseExamId == EnrollCourseExamID).ToList();
                db.EnrollCourseExamQuestions.RemoveRange(EnrollCourseExamQuestions);
                db.SaveChanges();
            }

        }
        public bool DeleteEnrollCourseExamQuestionByEnrollCourseExamID_WithoutUsing(int EnrollCourseExamID, LearningManagementSystemContext db)
        {

            var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(e => e.EnrollCourseExamId == EnrollCourseExamID).Include(r => r.EnrollStudentExamAnswers).ToList();
            if (EnrollCourseExamQuestions.Any(r => r.EnrollStudentExamAnswers.Any()))
                return true;
            db.EnrollCourseExamQuestions.RemoveRange(EnrollCourseExamQuestions);
            db.SaveChanges();
            return false;
        }

        public IPagedList<EnrollCourseExamQuestion> GetEnrollCourseExamQuestionByEnrollCourseExamID(int id, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Include(s => s.Question).ThenInclude(s => s.Question)
                    .ThenInclude(s => s.QuestionOptions).Where(r => r.EnrollCourseExamId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollCourseExamQuestions;
                var output = result.ToPagedList(pageNumber, pageSize);
                return output;
            }
        }
        public EnrollCourseExamQuestion EditMarkEnrollCourseExamQuestion(int EnrollCourseExamQuestionId, int Mark, EnrollCourseExamQuestion enrollCourseExamQuestion, LearningManagementSystemContext db)
        {

            enrollCourseExamQuestion.Id = EnrollCourseExamQuestionId;
            enrollCourseExamQuestion.Mark = Mark;

            db.Entry(enrollCourseExamQuestion).State = EntityState.Modified;
            db.SaveChanges();
            return enrollCourseExamQuestion;

        }
    }
}
