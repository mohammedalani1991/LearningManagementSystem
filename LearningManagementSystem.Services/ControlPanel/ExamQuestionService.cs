using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class ExamQuestionService : IExamQuestionService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _courseCategoryService;
        public ExamQuestionService(ISettingService settingService, ICourseService courseService, ICourseCategoryService courseCategoryService)
        {
            _settingService = settingService;
            _courseService = courseService;
            _courseCategoryService = courseCategoryService;
        }
        public IPagedList<ExamQuestion> GetExamQuestions(int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25,int? TemplateId = 0,int? QuestionId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestions = db.ExamQuestions.Include(r => r.Template).Include(d=>d.Question).Where(r =>
                        r.Status != (int)GeneralEnums.StatusEnum.Deleted);

          
                if (TemplateId > 0)
                {
                    ExamQuestions = ExamQuestions.Where(r => r.TemplateId == TemplateId);
                }
                if (QuestionId > 0)
                {
                    ExamQuestions = ExamQuestions.Where(r => r.QuestionId == QuestionId);
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = ExamQuestions;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var transExamTemplate = db.ExamTemplateTranslations.Include(r=>r.Exam).FirstOrDefault(r => r.LanguageId == languageId && r.ExamId== item.TemplateId && r.Exam.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (transExamTemplate != null)
                        {
                            item.Template.Name = transExamTemplate.Name;
                        }

                        var transQuestionTrans = db.QuestionTranslations.Include(r => r.Question).FirstOrDefault(r => r.LanguageId == languageId && r.QuestionId == item.QuestionId && r.Question.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (transQuestionTrans != null)
                        {
                            item.Question.Name = transQuestionTrans.Name;
                        }
                 
                    }
                }


                return output;
            }
        }



        public ExamQuestion GetExamQuestionById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = db.ExamQuestions.Include(e=>e.Template).Include(r => r.Question).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
                return ExamQuestion;
            }
        }
        public List<ExamQuestion> GetExamQuestionByTemplateId(int TemplateId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = db.ExamQuestions.Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.TemplateId == TemplateId).ToList();
                return ExamQuestion;
            }
        }
        public int GetCountExamQuestionByTemplateId(int TemplateId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = db.ExamQuestions.Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.TemplateId == TemplateId).Count();
                return ExamQuestion;
            }
        }
        public ExamQuestion GetExamQuestionByTemplateIdAndQuestionID(int? TemplateId, int QuestionId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = db.ExamQuestions.Include(d => d.Question).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.TemplateId == TemplateId && x.QuestionId == QuestionId);
                return ExamQuestion;
            }
        }

        public List<ExamQuestionViewModel> GetQuestionByTemplateId(int TemplateId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = db.ExamQuestions.Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.TemplateId == TemplateId).Select(r=> new ExamQuestionViewModel(r)).ToList();
                return ExamQuestion;
            }
        }
        public List<ExamQuestionViewModel> GetQuestionByTemplateId_WithoutUsing(int TemplateId, LearningManagementSystemContext db)
        {
          
                var ExamQuestion = db.ExamQuestions.Include(d=>d.Question).Where(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.TemplateId == TemplateId).Select(r => new ExamQuestionViewModel(r)).ToList();
                return ExamQuestion;
            
        }
        //public ExamQuestionViewModel GetExamQuestionById(int id, int languageId)
        //{
        //    using (var db = new LearningManagementSystemContext())
        //    {
        //        if (languageId != CultureHelper.GetDefaultLanguageId())
        //        {
        //            var aboutTran =
        //                db.ExamQuestionTranslations.Include(r => r.Exam).ThenInclude(e=>e.Course).ThenInclude(d => d.Category).FirstOrDefault(r => r.Exam.Status != (int)GeneralEnums.StatusEnum.Deleted && r.LanguageId == languageId && r.ExamId == id);
        //            if (aboutTran != null)
        //            {
        //                return new ExamQuestionViewModel(aboutTran);
        //            }
        //        }
        //        var ExamQuestion = db.ExamQuestions.Include(e=>e.Course).Include(r => r.Category).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
        //        if (ExamQuestion != null)
        //            return new ExamQuestionViewModel(ExamQuestion);
        //        else
        //            return null;
        //    }
        //}



        public ExamQuestion AddExamQuestion(ExamQuestionViewModel ExamQuestionViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestion = new ExamQuestion()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = ExamQuestionViewModel.CreatedBy,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    TemplateId = ExamQuestionViewModel.TemplateId,
                    QuestionId = ExamQuestionViewModel.QuestionId
                };
                db.ExamQuestions.Add(ExamQuestion);
                db.SaveChanges();
                return ExamQuestion;

            }
        }
        public void AddExamQuestion_WithoutUsing(ExamQuestionViewModel ExamQuestionViewModel, LearningManagementSystemContext db)
        {
           
                var ExamQuestion = new ExamQuestion()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = ExamQuestionViewModel.CreatedBy,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    TemplateId = ExamQuestionViewModel.TemplateId,
                    QuestionId = ExamQuestionViewModel.QuestionId
                };
                db.ExamQuestions.Add(ExamQuestion);
                db.SaveChanges();

            
        }

        public void DeleteExamQuestion(ExamQuestion ExamQuestion)
        {
            using (var db = new LearningManagementSystemContext())
            {
                ExamQuestion.Status = (int)GeneralEnums.StatusEnum.Deleted;
                ExamQuestion.DeletedOn = DateTime.Now;
                db.Entry(ExamQuestion).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteExamQuestionByTamplateID_WithoutUsing(int TamplateID, LearningManagementSystemContext db)
        {

            var ExamQuestions = db.ExamQuestions.Where(e => e.TemplateId == TamplateID && e.Status != (int)GeneralEnums.StatusEnum.Deleted);
            foreach (var ExamQuestion in ExamQuestions)
            {
                ExamQuestion.Status = (int)GeneralEnums.StatusEnum.Deleted;
                ExamQuestion.DeletedOn = DateTime.Now;
                db.Entry(ExamQuestion).State = EntityState.Modified;
            }
            db.SaveChanges();


        }
        public void DeleteExamQuestionByQuestionID(int QuestionID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamQuestions = db.ExamQuestions.Where(e => e.QuestionId == QuestionID && e.Status != (int)GeneralEnums.StatusEnum.Deleted);
                foreach (var ExamQuestion in ExamQuestions)
                {
                    ExamQuestion.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    ExamQuestion.DeletedOn = DateTime.Now;
                    db.Entry(ExamQuestion).State = EntityState.Modified;
                }
                db.SaveChanges();

            }
        }

    }
}
