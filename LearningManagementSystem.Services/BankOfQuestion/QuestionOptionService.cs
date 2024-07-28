using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.BankOfQuestion
{
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly ISettingService _settingService;
        public QuestionOptionService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public List<QuestionOption> GetQuestionOptiosByQuestionId(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var questionOptions = db.QuestionOptions.Where(r => r.QuestionId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return questionOptions;
            }
        }
        public List<QuestionOptionViewModel> GetQuestionOptiosByQuestionId(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var questionTranslation = db.QuestionOptionTranslations.Include(r => r.Option).Where(r => r.LanguageId == languageId && r.Option.QuestionId == id && r.Option.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (questionTranslation.Any())
                        return questionTranslation.Select(r => new QuestionOptionViewModel(r)).ToList();
                    var questionOption = db.QuestionOptions.Where(r => r.QuestionId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                    return questionOption.Select(r => new QuestionOptionViewModel(r)).ToList();
                }
                var questionOptions = db.QuestionOptions.Where(r => r.QuestionId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return questionOptions.Select(r => new QuestionOptionViewModel(r)).ToList();
            }
        }

        public void EditQuestionOption(int questionId,List<QuestionOptionViewModel> questionOptionViewModel,int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (questionOptionViewModel != null && questionOptionViewModel.Count>0)
                {
                    List<QuestionOption> questionOptions = GetQuestionOptiosByQuestionId(questionId);

                    //delete
                    foreach (var item in questionOptions.Where(r => !questionOptionViewModel.Select(r => r.Id).Contains(r.Id)))
                    {
                        item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                        item.DeletedOn = DateTime.Now;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    //Edit
                    foreach (var item in questionOptionViewModel.Where(r => questionOptions.Select(r => r.Id).Contains(r.Id)))
                    {
                        var option = questionOptions.FirstOrDefault(r => r.Id == item.Id);
                        if (languageId == CultureHelper.GetDefaultLanguageId())
                        {
                            option.Name = item.Name;
                            option.IsCorrect = item.IsCorrect;
                        }

                        db.Entry(option).State = EntityState.Modified;
                        db.SaveChanges();
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var questionTranslation = db.QuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.OptionId == item.Id);
                            if (questionTranslation != null)
                            {
                                questionTranslation.Name = item.Name;
                                db.Entry(questionTranslation).State = EntityState.Modified;
                            }
                            else
                            {
                                var questionOptionTranslation = new QuestionOptionTranslation()
                                {
                                    Name = item.Name,
                                    LanguageId = languageId,
                                    OptionId = item.Id
                                };
                                db.QuestionOptionTranslations.Add(questionOptionTranslation);
                                db.SaveChanges();
                            }

                            db.SaveChanges();
                        }
                    }

                    //add 
                    foreach (var item in questionOptionViewModel.Where(r => r.Id == null|| r.Id==0))
                    {
                        var option = new QuestionOption()
                        {
                            CreatedOn = DateTime.Now,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            Name = item.Name,
                            CreatedBy = item.CreatedBy,
                            IsCorrect = item.IsCorrect,
                            QuestionId = questionId,


                        };
                        db.QuestionOptions.Add(option);
                        db.SaveChanges();
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var questionOptionTranslation = new QuestionOptionTranslation()
                            {
                                Name = item.Name,
                                LanguageId = languageId,
                                OptionId = option.Id
                            };
                            db.QuestionOptionTranslations.Add(questionOptionTranslation);
                            db.SaveChanges();
                        }
                    }


                }
            }
        }

     
    }
}
