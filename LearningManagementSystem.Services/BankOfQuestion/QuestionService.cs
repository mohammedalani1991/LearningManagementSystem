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

namespace LearningManagementSystem.Services.BankOfQuestion
{
    public class QuestionService : IQuestionService
    {
        private readonly ISettingService _settingService;
        private readonly IQuestionOptionService _questionOptionService;
        private readonly LearningManagementSystemContext _context;

        public QuestionService(ISettingService settingService, IQuestionOptionService questionOptionService, LearningManagementSystemContext context)
        {
            _settingService = settingService;
            _questionOptionService = questionOptionService;
            _context = context;
        }

        public IPagedList<Question> GetQuestions(string searchText, int? page, int? category, int? course, int? type, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int TeacherId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var questions = db.Questions.Include(r => r.QuestionTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {

                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        if (languageId == CultureHelper.GetDefaultLanguageId())
                        {
                            questions = questions.Where(r => r.Name.Contains(searchText));
                        }
                        else
                        {
                            questions = questions.Where(r => r.QuestionTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                        }
                    }
                }
                if (category != null)
                {
                    questions = questions.Where(r => r.CategoryId == category);
                }
                if (course != null)
                {
                    questions = questions.Where(r => r.CourseId == course);
                }
                if (type != null)
                {
                    questions = questions.Where(r => r.Type == type);
                }
                if (TeacherId > 0)
                {
                    questions = questions.Where(r => r.TeacherId == TeacherId);
                }
                else
                {
                    questions = questions.Where(r => r.TeacherId == null);
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = questions;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize); if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.QuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }

                return output;
            }
        }
        public IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(string searchText, int? page, int? category, int? course, int? type, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int IsRandomly = 0, int TeacherId = 0, int TemplateId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var questions = db.Questions.Include(r => r.QuestionTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    questions = questions.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        questions = questions.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        questions = questions.Where(r => r.QuestionTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (category != null)
                {
                    questions = questions.Where(r => r.CategoryId == category);
                }
                if (course != null)
                {
                    questions = questions.Where(r => r.CourseId == course);
                }
                if (type != null)
                {
                    questions = questions.Where(r => r.Type == type);
                }
                if (TeacherId > 0)
                {
                    questions = questions.Where(r => r.TeacherId == TeacherId);
                }
                else
                {
                    questions = questions.Where(r => r.TeacherId == null);
                }

                if (IsRandomly == 1)
                {
                    var TemplateExamQuestionsList = db.ExamQuestions.Where(e => e.TemplateId == TemplateId && e.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.QuestionId).ToList();
                    questions = questions.Where(r => !TemplateExamQuestionsList.Contains(r.Id));
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = questions;
                foreach (var item in result)
                {
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var transQuestionTranslations = item.QuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transQuestionTranslations != null)
                        {
                            item.Name = transQuestionTranslations.Name;
                        }
                    }
                }
                var output = (IPagedList<QuestionViewModel>)null;
                if (IsRandomly == 1)
                {
                    output = result.OrderBy(x => Guid.NewGuid()).Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }
                else
                {
                    output = result.OrderByDescending(x => x.Id).Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }


                foreach (var item in output)
                {


                    var transCourse = db.Courses.Include(r => r.CourseTranslations).FirstOrDefault(r => r.Id == item.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourse != null)
                    {
                        item.CourseName = transCourse.CourseName;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseN = transCourse.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseN != null)
                            {
                                item.CourseName = transCourseN.CourseName;
                            }
                        }
                    }
                    var transCourseCategory = db.CourseCategories.Include(r => r.CourseCategoryTranslations).FirstOrDefault(r => r.Id == item.CategoryId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourseCategory != null)
                    {
                        item.CategoryName = transCourseCategory.Name;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseCategoryN = transCourseCategory.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseCategoryN != null)
                            {
                                item.CategoryName = transCourseCategoryN.Name;
                            }
                        }
                    }
                }
                return output;
            }
        }
        public IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(List<ExamQuestion> examQuestions, string searchText, int? page, int? category, int? course, int? type, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int IsRandomly = 0, int TeacherId = 0, int TemplateId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var questions = db.Questions.Include(r => r.QuestionTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    questions = questions.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        questions = questions.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        questions = questions.Where(r => r.QuestionTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (category != null)
                {
                    questions = questions.Where(r => r.CategoryId == category);
                }
                if (course != null)
                {
                    questions = questions.Where(r => r.CourseId == course);
                }
                if (type != null)
                {
                    questions = questions.Where(r => r.Type == type);
                }
                if (TeacherId > 0)
                {
                    questions = questions.Where(r => r.TeacherId == TeacherId);
                }
                else
                {
                    questions = questions.Where(r => r.TeacherId == null);
                }

                if (IsRandomly == 1)
                {
                    var TemplateExamQuestionsList = db.ExamQuestions.Where(e => e.TemplateId == TemplateId && e.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.QuestionId).ToList();
                    questions = questions.Where(r => !TemplateExamQuestionsList.Contains(r.Id));
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = questions.OrderBy(r => examQuestions.Select(s => s.QuestionId).Contains(r.Id) ? 0 : 1).ThenBy(q => q.Id);
                foreach (var item in result)
                {
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var transQuestionTranslations = item.QuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transQuestionTranslations != null)
                        {
                            item.Name = transQuestionTranslations.Name;
                        }
                    }
                }
                var output = (IPagedList<QuestionViewModel>)null;
                if (IsRandomly == 1)
                {
                    output = result.OrderBy(x => Guid.NewGuid()).Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }
                else
                {
                    output = result.Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }


                foreach (var item in output)
                {


                    var transCourse = db.Courses.Include(r => r.CourseTranslations).FirstOrDefault(r => r.Id == item.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourse != null)
                    {
                        item.CourseName = transCourse.CourseName;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseN = transCourse.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseN != null)
                            {
                                item.CourseName = transCourseN.CourseName;
                            }
                        }
                    }
                    var transCourseCategory = db.CourseCategories.Include(r => r.CourseCategoryTranslations).FirstOrDefault(r => r.Id == item.CategoryId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourseCategory != null)
                    {
                        item.CategoryName = transCourseCategory.Name;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseCategoryN = transCourseCategory.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseCategoryN != null)
                            {
                                item.CategoryName = transCourseCategoryN.Name;
                            }
                        }
                    }
                }
                return output;
            }
        }

        public IPagedList<QuestionViewModel> GetQuestionsForTemplateExam(List<EnrollCourseExamQuestion> examQuestions, string searchText, int? page, int? category, int? course, int? type, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int IsRandomly = 0, int TeacherId = 0, int TemplateId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var questions = db.Questions.Include(r => r.QuestionTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    questions = questions.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        questions = questions.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        questions = questions.Where(r => r.QuestionTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (category != null)
                {
                    questions = questions.Where(r => r.CategoryId == category);
                }
                if (course != null)
                {
                    questions = questions.Where(r => r.CourseId == course);
                }
                if (type != null)
                {
                    questions = questions.Where(r => r.Type == type);
                }
                if (TeacherId > 0)
                {
                    questions = questions.Where(r => r.TeacherId == TeacherId);
                }
                else
                {
                    questions = questions.Where(r => r.TeacherId == null);
                }

                if (IsRandomly == 1)
                {
                    var TemplateExamQuestionsList = db.ExamQuestions.Where(e => e.TemplateId == TemplateId && e.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.QuestionId).ToList();
                    questions = questions.Where(r => !TemplateExamQuestionsList.Contains(r.Id));
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = questions.OrderBy(r => examQuestions.Select(s => s.Question.QuestionId).Contains(r.Id) ? 0 : 1).ThenBy(q => q.Id);
                foreach (var item in result)
                {
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var transQuestionTranslations = item.QuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transQuestionTranslations != null)
                        {
                            item.Name = transQuestionTranslations.Name;
                        }
                    }
                }
                var output = (IPagedList<QuestionViewModel>)null;
                if (IsRandomly == 1)
                {
                    output = result.OrderBy(x => Guid.NewGuid()).Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }
                else
                {
                    output = result.Select(r => new QuestionViewModel(r)).ToPagedList(pageNumber, pageSize);
                }


                foreach (var item in output)
                {


                    var transCourse = db.Courses.Include(r => r.CourseTranslations).FirstOrDefault(r => r.Id == item.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourse != null)
                    {
                        item.CourseName = transCourse.CourseName;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseN = transCourse.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseN != null)
                            {
                                item.CourseName = transCourseN.CourseName;
                            }
                        }
                    }
                    var transCourseCategory = db.CourseCategories.Include(r => r.CourseCategoryTranslations).FirstOrDefault(r => r.Id == item.CategoryId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (transCourseCategory != null)
                    {
                        item.CategoryName = transCourseCategory.Name;
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var transCourseCategoryN = transCourseCategory.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (transCourseCategoryN != null)
                            {
                                item.CategoryName = transCourseCategoryN.Name;
                            }
                        }
                    }
                }
                return output;
            }
        }

        public Question GetQuestionById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var question = db.Questions.Include(r => r.QuestionTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return question;
            }
        }

        public ExamQuestion GetExamQuestionByQuestionId(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var question = db.ExamQuestions.Include(r => r.Question).FirstOrDefault(r => r.QuestionId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return question;
            }
        }

        public QuestionViewModel GetQuestionById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var optionsList = _questionOptionService.GetQuestionOptiosByQuestionId(id, languageId);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var questionTranslation = db.QuestionTranslations.Include(r => r.Question).FirstOrDefault(r => r.LanguageId == languageId && r.QuestionId == id && r.Question.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (questionTranslation != null)
                    {
                        var resultTrans = new QuestionViewModel(questionTranslation);
                        resultTrans.OptionList = optionsList;
                        return resultTrans;
                    }
                }
                var question = db.Questions.FirstOrDefault(b => b.Id == id);
                var result = new QuestionViewModel(question);
                result.OptionList = optionsList;
                return result;
            }
        }


        public void AddQuestion(QuestionViewModel questionViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var question = new Question()
                {
                    CreatedOn = DateTime.Now,
                    Status = questionViewModel.Status,
                    Name = questionViewModel.Name,
                    CourseId = questionViewModel.CourseId,
                    Mark = questionViewModel.Mark,
                    Type = questionViewModel.Type,
                    CategoryId = questionViewModel.CategoryId,
                    CreatedBy = questionViewModel.CreatedBy,
                    TeacherId = questionViewModel.TeacherId,
                    CertifiedBankQuestion = questionViewModel.CertifiedBankQuestion,

                };
                db.Questions.Add(question);
                db.SaveChanges();


                if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var questionTranslation = new QuestionTranslation()
                    {
                        Name = questionViewModel.Name,
                        LanguageId = questionViewModel.LanguageId,
                        QuestionId = question.Id
                    };
                    db.QuestionTranslations.Add(questionTranslation);
                    db.SaveChanges();
                }

                _questionOptionService.EditQuestionOption(question.Id, questionViewModel.OptionList, questionViewModel.LanguageId);
            }
        }

        public void AddQuestions(QuestionViewModel questionViewModel)
        {
            foreach (var item in questionViewModel.Data)
            {
                item.CourseId = questionViewModel.CourseId;
                item.CategoryId = questionViewModel.CategoryId;

                if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                    item.QuestionTranslations.Add(new QuestionTranslation()
                    {
                        Name = item.Name,
                        LanguageId = questionViewModel.LanguageId,
                    });

                _context.Questions.Add(item);
            }
            _context.SaveChanges();
        }

        public void EditQuestion(QuestionViewModel questionViewModel, Question question)
        {
            using (var db = new LearningManagementSystemContext())
            {
                question.CourseId = questionViewModel.CourseId;
                question.Status = questionViewModel.Status;
                question.CategoryId = questionViewModel.CategoryId;
                question.Type = questionViewModel.Type;
                question.Mark = questionViewModel.Mark;

                if (questionViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    question.Name = questionViewModel.Name;
                    question.CategoryId = questionViewModel.CategoryId;
                    question.CourseId = questionViewModel.CourseId;
                    question.Mark = questionViewModel.Mark;
                }

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var questionTranslation = db.QuestionTranslations.FirstOrDefault(r =>
                        r.LanguageId == questionViewModel.LanguageId &&
                        r.QuestionId == questionViewModel.Id);
                    if (questionTranslation != null)
                    {
                        questionTranslation.Name = questionViewModel.Name;
                        db.Entry(questionTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var translation = new QuestionTranslation()
                        {
                            Name = questionViewModel.Name,
                            LanguageId = questionViewModel.LanguageId,
                            QuestionId = questionViewModel.Id
                        };
                        db.QuestionTranslations.Add(translation);
                    }

                    db.SaveChanges();
                }
                _questionOptionService.EditQuestionOption(question.Id, questionViewModel.OptionList, questionViewModel.LanguageId);
            }
        }

        public void DeleteQuestion(Question question)
        {
            using (var db = new LearningManagementSystemContext())
            {
                question.Status = (int)GeneralEnums.StatusEnum.Deleted;
                question.DeletedOn = DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<EnrollCourseExamQuestion> GetQuestionsList(int EnrollCourseExamID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.EnrollCourseExamId == EnrollCourseExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(e => e.EnrollCourseExam).Include(s => s.Question.Question.QuestionOptions).ToList();
                return EnrollCourseExamQuestions;
            }
        }
    }
}
