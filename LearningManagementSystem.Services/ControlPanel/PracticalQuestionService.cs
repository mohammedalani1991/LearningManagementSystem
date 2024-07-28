using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class PracticalQuestionService : IPracticalQuestionService
    {
        private readonly LearningManagementSystemContext _context;

        public PracticalQuestionService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public IPagedList<PracticalQuestion> GetPracticalQuestions(string searchText, int? page,int? TypeId, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var PracticalQuestions = _context.PracticalQuestions.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.PracticalQuestionTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    PracticalQuestions = PracticalQuestions.Where(r => r.Name.Contains(searchText));
                else
                    PracticalQuestions = PracticalQuestions.Where(r => r.PracticalQuestionTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }

            if(TypeId != null && TypeId > 0)
                PracticalQuestions = PracticalQuestions.Where(r => r.Type == TypeId);


            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = PracticalQuestions;
            var output = result.OrderByDescending(r=>r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.PracticalQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Name = trans.Name;
                        item.Description = trans.Description;
                    }
                }

            return output;
        }
        public PracticalQuestion GetPracticalQuestionById(int id)
        {
            var practicalQuestion = _context.PracticalQuestions.Find(id);
            return practicalQuestion;
        }

        public PracticalQuestion GetPracticalQuestionById(int id, int languageId)
        {
            var practicalQuestion = _context.PracticalQuestions.Include(r => r.PracticalQuestionTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = practicalQuestion.PracticalQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                {
                    practicalQuestion.Name = trans.Name;
                    practicalQuestion.Description = trans.Description;
                }
            }
            return practicalQuestion;
        }

        public void AddPracticalQuestion(PracticalQuestionViewModel practicalQuestionViewModel)
        {
            var practicalQuestion = new PracticalQuestion()
            {
                CreatedOn = DateTime.Now,
                Status = practicalQuestionViewModel.Status,
                Name = practicalQuestionViewModel.Name,
                Type = practicalQuestionViewModel.Type,
                Mark= practicalQuestionViewModel.Mark,  
                IsDiscountFromTotal = practicalQuestionViewModel.IsDiscountFromTotal,
                CreatedBy = practicalQuestionViewModel.CreatedBy,
                Description = practicalQuestionViewModel.Description,
                Main = practicalQuestionViewModel.Main, 

            };
            _context.PracticalQuestions.Add(practicalQuestion);
            _context.SaveChanges();

            practicalQuestion.Id = practicalQuestion.Id;

            if (practicalQuestionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var practicalQuestionTran = new PracticalQuestionTranslation()
                {
                    Name = practicalQuestionViewModel.Name,
                    LanguageId = practicalQuestionViewModel.LanguageId,
                    PracticalQuestionId = practicalQuestion.Id,
                    Description = practicalQuestionViewModel.Description,
                };
                _context.PracticalQuestionTranslations.Add(practicalQuestionTran);
                _context.SaveChanges();
            }
        }

        public void EditPracticalQuestion(PracticalQuestionViewModel practicalQuestionViewModel, PracticalQuestion practicalQuestion)
        {
            practicalQuestion.Status = practicalQuestionViewModel.Status;
            practicalQuestion.Mark = practicalQuestionViewModel.Mark;
            practicalQuestion.IsDiscountFromTotal = practicalQuestionViewModel.IsDiscountFromTotal;
            practicalQuestion.Main = practicalQuestionViewModel.Main;
            //practicalQuestion.Type = practicalQuestionViewModel.Type;

            if (practicalQuestionViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                practicalQuestion.Name = practicalQuestionViewModel.Name;
                practicalQuestion.Description = practicalQuestionViewModel.Description;
            }

            _context.Entry(practicalQuestion).State = EntityState.Modified;
            _context.SaveChanges();

            if (practicalQuestionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var practicalQuestionTranslation = _context.PracticalQuestionTranslations.FirstOrDefault(r =>
                    r.LanguageId == practicalQuestionViewModel.LanguageId &&
                    r.PracticalQuestionId == practicalQuestion.Id);
                if (practicalQuestionTranslation != null)
                {
                    practicalQuestionTranslation.Name = practicalQuestionViewModel.Name;
                    practicalQuestionTranslation.Description = practicalQuestionViewModel.Description;
                    _context.Entry(practicalQuestionTranslation).State = EntityState.Modified;
                }
                else
                {
                    var practicalQuestionTran = new PracticalQuestionTranslation()
                    {
                        Name = practicalQuestionViewModel.Name,
                        LanguageId = practicalQuestionViewModel.LanguageId,
                        PracticalQuestionId = practicalQuestionViewModel.Id,
                        Description = practicalQuestionViewModel.Description
                    };
                    _context.PracticalQuestionTranslations.Add(practicalQuestionTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeletePracticalQuestion(PracticalQuestion practicalQuestion)
        {
            practicalQuestion.Status = (int)GeneralEnums.StatusEnum.Deleted;
            practicalQuestion.DeletedOn = DateTime.Now;
            _context.Entry(practicalQuestion).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
