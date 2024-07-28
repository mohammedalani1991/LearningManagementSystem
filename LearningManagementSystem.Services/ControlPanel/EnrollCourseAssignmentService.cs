using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using X.PagedList;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollCourseAssignmentService : IEnrollCourseAssignmentService
    {
        private readonly LearningManagementSystemContext _context;

        public EnrollCourseAssignmentService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public EnrollCourseAssigment GetAssignmentById(int id)
        {
            var assignment = _context.EnrollCourseAssigments.FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            return assignment;
        }

        public EnrollCourseAssigmentQuestion GetAssignmentQuestion(int id)
        {
            var question = _context.EnrollCourseAssigmentQuestions.Include(r => r.EnrollCourseAssigmentQuestionOptions).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            return question;
        }

        public List<EnrollStudentAssigmentAnswer> GetStudentAssigmentAnswer(int id)
        {
            var question = _context.EnrollStudentAssigmentAnswers.Where(r => r.EnrollStudentAssigmentId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.EnrollStudentAssigmentAnswerOptions).Include(r => r.EnrollCourseAssigmentQuestion.EnrollCourseAssigmentQuestionOptions).ToList();

            foreach (var item in question)
                foreach (var item1 in item.EnrollCourseAssigmentQuestion.EnrollCourseAssigmentQuestionOptions)
                    if (item.EnrollStudentAssigmentAnswerOptions.Select(r => r.QusetionOptionId).Contains(item1.Id))
                        item1.IsCorrect = true;
                    else
                        item1.IsCorrect = false;
                
            return question;
        }

        public EnrollCourseAssigment GetAssignmentById(int id, int languageId)
        {
            var assignment = _context.EnrollCourseAssigments.Include(r => r.EnrollCourseAssigmentTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = assignment.EnrollCourseAssigmentTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                {
                    assignment.Name = trans.Name;
                    assignment.Description = trans.Description;
                }
            }

            return assignment;
        }


        public EnrollCourseAssigmentQuestion GetAssignmentQuestion(int id, int languageId)
        {
            var qustions = _context.EnrollCourseAssigmentQuestions.Include(r => r.EnrollCourseAssigmentQuestionOptions.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active)).Include(r => r.EnrollCourseAssigmentQuestionTranslations)
                .FirstOrDefault(r => r.Id == id && r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = qustions.EnrollCourseAssigmentQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    qustions.QuestionName = trans.Name;
                foreach (var item1 in qustions.EnrollCourseAssigmentQuestionOptions)
                {
                    var trans1 = _context.EnrollCourseAssigmentQuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.OptionId == item1.Id);
                    if (trans1 != null)
                        item1.Name = trans1.Name;
                }
            }

            return qustions;
        }

        public IPagedList<EnrollCourseAssigment> GetAssignments(string searchText, int page, int languageId, int pagination, int courseId)
        {
            var assignments = _context.EnrollCourseAssigments.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollTeacherCourseId == courseId)
                .Include(r => r.EnrollCourseAssigmentTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    assignments = assignments.Where(r => r.Name.Contains(searchText));
                else
                    assignments = assignments.Where(r => r.EnrollCourseAssigmentTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = page;
            var result = assignments;

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.EnrollCourseAssigmentTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }

            return output;
        }

        public IPagedList<EnrollStudentAssigment> GetStudentAssignments(string searchText, int page, int languageId, int pagination, int assigmentId)
        {
            var assignments = _context.EnrollStudentAssigments.Include(a=>a.EnrollStudentCourse.Student.Contact.ContactTranslations).Include(a=>a.EnrollCourseAssigment).Include(r=>r.EnrollStudentAssigmentAnswers)
                .Where(r => r.EnrollCourseAssigment.Status == (int)GeneralEnums.StatusEnum.Active && r.Status == (int)GeneralEnums.StatusEnum.Active && r.EnrollStudentCourse.Status == (int)GeneralEnums.StatusEnum.Active &&
                                r.EnrollCourseAssigmentId == assigmentId && r.EnrollStudentAssigmentAnswers.Any())
                .Include(r => r.EnrollStudentCourse.Student.Contact.ContactTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
                assignments = assignments.Where(r => r.EnrollStudentCourse.Student.Contact.FullName.Contains(searchText));

            var pageSize = pagination;
            var pageNumber = page;
            var result = assignments;

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.EnrollStudentCourse.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.EnrollStudentCourse.Student.Contact.FullName = trans.FullName;
                }

            return output;
        }

        public void AddEnrollCourseAssignment(EnrollCourseAssignmentViewModel assignmentViewModel)
        {
            var assignment = new EnrollCourseAssigment()
            {
                Name = assignmentViewModel.Name,
                Description = assignmentViewModel.Description?? assignmentViewModel.Description1,
                PublishEndDate = assignmentViewModel.PublishEndDate,
                PublishDate = assignmentViewModel.PublishDate,
                EnrollTeacherCourseId = assignmentViewModel.EnrollTeacherCourseId,
                Status = assignmentViewModel.Status,
                CreatedBy = assignmentViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
            };
            _context.EnrollCourseAssigments.Add(assignment);
            _context.SaveChanges();

            if (assignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var tran = new EnrollCourseAssigmentTranslation()
                {
                    LanguageId = assignmentViewModel.LanguageId,
                    Name = assignmentViewModel.Name,
                    Description = assignmentViewModel.Description?? assignmentViewModel.Description1,
                    EnrollCourseAssigmentId = assignment.Id,
                };
                _context.EnrollCourseAssigmentTranslations.Add(tran);
                _context.SaveChanges();
            }
        } 
        
        public void AddEnrollCourseAssignment_WithoutUsing(EnrollCourseAssignmentViewModel assignmentViewModel, LearningManagementSystemContext db)
        {
            var assignment = new EnrollCourseAssigment()
            {
                Name = assignmentViewModel.Name,
                Description = assignmentViewModel.Description1,
                PublishEndDate = assignmentViewModel.PublishEndDate,
                PublishDate = assignmentViewModel.PublishDate,
                EnrollTeacherCourseId = assignmentViewModel.EnrollTeacherCourseId,
                Status = assignmentViewModel.Status,
                CreatedBy = assignmentViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
            };
            db.EnrollCourseAssigments.Add(assignment);
            db.SaveChanges();

            if (assignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var tran = new EnrollCourseAssigmentTranslation()
                {
                    LanguageId = assignmentViewModel.LanguageId,
                    Name = assignmentViewModel.Name,
                    Description = assignmentViewModel.Description1,
                    EnrollCourseAssigmentId = assignment.Id,
                };
                db.EnrollCourseAssigmentTranslations.Add(tran);
                db.SaveChanges();
            }
        }

        public EnrollCourseAssigment EditEnrollCourseAssignment(EnrollCourseAssignmentViewModel assignmentViewModel, EnrollCourseAssigment enrollCourseAssigment)
        {
            enrollCourseAssigment.PublishDate = assignmentViewModel.PublishDate;
            enrollCourseAssigment.PublishEndDate = assignmentViewModel.PublishEndDate;
            enrollCourseAssigment.Status = assignmentViewModel.Status;

            if (assignmentViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                enrollCourseAssigment.Name = assignmentViewModel.Name;
                enrollCourseAssigment.Description = assignmentViewModel.Description ?? assignmentViewModel.Description1;
            }
            _context.Entry(enrollCourseAssigment).State = EntityState.Modified;
            _context.SaveChanges();

            if (assignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var EnrollCourseAssigmentTranslations = _context.EnrollCourseAssigmentTranslations.FirstOrDefault(r =>
                    r.LanguageId == assignmentViewModel.LanguageId &&
                    r.EnrollCourseAssigmentId == enrollCourseAssigment.Id);
                if (EnrollCourseAssigmentTranslations != null)
                {
                    EnrollCourseAssigmentTranslations.Name = assignmentViewModel.Name;
                    EnrollCourseAssigmentTranslations.Description = assignmentViewModel.Description ?? assignmentViewModel.Description1;
                    _context.Entry(EnrollCourseAssigmentTranslations).State = EntityState.Modified;
                }
                else
                {
                    var EnrollCourseAssigmentTranslation = new EnrollCourseAssigmentTranslation()
                    {
                        Name = assignmentViewModel.Name,
                        Description = assignmentViewModel.Description ?? assignmentViewModel.Description1,
                        EnrollCourseAssigmentId = assignmentViewModel.Id,
                        LanguageId = assignmentViewModel.LanguageId,
                    };
                    _context.EnrollCourseAssigmentTranslations.Add(EnrollCourseAssigmentTranslation);
                }

                _context.SaveChanges();
            }
            return enrollCourseAssigment;
        }

        public void DeleteAssignment(EnrollCourseAssigment assignment)
        {
            assignment.Status = (int)GeneralEnums.StatusEnum.Deleted;
            _context.Entry(assignment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IPagedList<EnrollCourseAssigmentQuestion> GetAssignmentQuestions(string searchText, int page, int languageId, int pagination, int courseId)
        {
            var assignments = _context.EnrollCourseAssigmentQuestions.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollCourseAssigmentId == courseId)
                .Include(r => r.EnrollCourseAssigmentQuestionTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    assignments = assignments.Where(r => r.QuestionName.Contains(searchText));
            }

            var pageSize = pagination;
            var pageNumber = page;
            var result = assignments;


            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.EnrollCourseAssigmentQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.QuestionName = trans.Name;
                }

            return output;
        }

        public void AddQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel)
        {
            var question = new EnrollCourseAssigmentQuestion()
            {
                CreatedOn = DateTime.Now,
                Status = questionViewModel.Status,
                QuestionName = questionViewModel.QuestionName,
                EnrollCourseAssigmentId = questionViewModel.EnrollCourseAssigmentId,
                QuestionType = questionViewModel.QuestionType,
                CreatedBy = questionViewModel.CreatedBy,

            };

            _context.EnrollCourseAssigmentQuestions.Add(question);
            _context.SaveChanges();

            if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var questionTranslation = new EnrollCourseAssigmentQuestionTranslation()
                {
                    Name = questionViewModel.QuestionName,
                    LanguageId = questionViewModel.LanguageId,
                    QuestionId = question.Id
                };
                _context.EnrollCourseAssigmentQuestionTranslations.Add(questionTranslation);
                _context.SaveChanges();
            }

            foreach (var item in questionViewModel.OptionList)
            {
                var option = new EnrollCourseAssigmentQuestionOption()
                {
                    CreatedOn = DateTime.Now,
                    Status = questionViewModel.Status,
                    Name = item.Name,
                    CreatedBy = item.CreatedBy,
                    IsCorrect = item.IsCorrect,
                    QuestionId = question.Id
                };
                _context.EnrollCourseAssigmentQuestionOptions.Add(option);
                _context.SaveChanges();

                if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var optionTranslation = new EnrollCourseAssigmentQuestionOptionTranslation()
                    {
                        Name = item.Name,
                        LanguageId = questionViewModel.LanguageId,
                        OptionId = option.Id
                    };
                    _context.EnrollCourseAssigmentQuestionOptionTranslations.Add(optionTranslation);
                    _context.SaveChanges();
                }
            }
        }

        public void EditQuestion(EnrollCourseAssigmentQuestionViewModel questionViewModel, EnrollCourseAssigmentQuestion question)
        {
            question.Status = questionViewModel.Status;
            question.QuestionType = questionViewModel.QuestionType;

            if (questionViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                question.QuestionName = questionViewModel.QuestionName;
            }
            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();


            if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var questionTranslations = _context.EnrollCourseAssigmentQuestionTranslations.FirstOrDefault(r =>
                    r.LanguageId == questionViewModel.LanguageId &&
                    r.QuestionId == question.Id);
                if (questionTranslations != null)
                {
                    questionTranslations.Name = questionViewModel.QuestionName;
                    _context.Entry(question).State = EntityState.Modified;
                }
                else
                {
                    var questionTranslation = new EnrollCourseAssigmentQuestionTranslation()
                    {
                        Name = questionViewModel.QuestionName,
                        LanguageId = questionViewModel.LanguageId,
                        QuestionId = question.Id
                    };
                    _context.EnrollCourseAssigmentQuestionTranslations.Add(questionTranslation);
                }
                _context.SaveChanges();
            }

            #region EditQuestionOption

            if (questionViewModel.OptionList != null && questionViewModel.OptionList.Count > 0)
            {
                List<EnrollCourseAssigmentQuestionOption> questionOptions = _context.EnrollCourseAssigmentQuestionOptions.Where(r => r.QuestionId == question.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                //delete
                foreach (var item in questionOptions.Where(r => !questionViewModel.OptionList.Select(r => r.Id).Contains(r.Id)))
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    item.DeletedOn = DateTime.Now;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                //Edit
                foreach (var item in questionViewModel.OptionList.Where(r => questionOptions.Select(r => r.Id).Contains(r.Id)))
                {
                    var option = questionOptions.FirstOrDefault(r => r.Id == item.Id);
                    option.Status = questionViewModel.Status;
                    option.IsCorrect = item.IsCorrect;

                    if (questionViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                        option.Name = item.Name;

                    _context.Entry(option).State = EntityState.Modified;
                    _context.SaveChanges();
                    if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var questionTranslation = _context.EnrollCourseAssigmentQuestionOptionTranslations.FirstOrDefault(r => r.LanguageId == questionViewModel.LanguageId && r.OptionId == item.Id);
                        if (questionTranslation != null)
                        {
                            questionTranslation.Name = item.Name;
                            _context.Entry(questionTranslation).State = EntityState.Modified;
                        }
                        else
                        {
                            var questionOptionTranslation = new EnrollCourseAssigmentQuestionOptionTranslation()
                            {
                                Name = item.Name,
                                LanguageId = questionViewModel.LanguageId,
                                OptionId = item.Id
                            };
                            _context.EnrollCourseAssigmentQuestionOptionTranslations.Add(questionOptionTranslation);
                            _context.SaveChanges();
                        }

                        _context.SaveChanges();
                    }
                }

                //add 
                foreach (var item in questionViewModel.OptionList.Where(r => r.Id == 0))
                {
                    var option = new EnrollCourseAssigmentQuestionOption()
                    {
                        CreatedOn = DateTime.Now,
                        Status = questionViewModel.Status,
                        Name = item.Name,
                        CreatedBy = item.CreatedBy,
                        IsCorrect = item.IsCorrect,
                        QuestionId = question.Id,
                    };
                    _context.EnrollCourseAssigmentQuestionOptions.Add(option);
                    _context.SaveChanges();
                    if (questionViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var questionOptionTranslation = new EnrollCourseAssigmentQuestionOptionTranslation()
                        {
                            Name = item.Name,
                            LanguageId = questionViewModel.LanguageId,
                            OptionId = option.Id
                        };
                        _context.EnrollCourseAssigmentQuestionOptionTranslations.Add(questionOptionTranslation);
                        _context.SaveChanges();
                    }
                }
            }
            #endregion
        }

        public void DeleteQuestion(EnrollCourseAssigmentQuestion question)
        {
            question.Status = (int)GeneralEnums.StatusEnum.Deleted;
            question.DeletedOn = DateTime.Now;
            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
