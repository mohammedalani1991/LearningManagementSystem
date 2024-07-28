using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SubjectService : ISubjectService
    {
        private readonly LearningManagementSystemContext _context;

        public SubjectService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public IPagedList<Subject> GetSubjects(int? type ,int? examTypeId, string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var Subjects = _context.Subjects.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.SubjectTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    Subjects = Subjects.Where(r => r.Title.Contains(searchText));
                else
                    Subjects = Subjects.Where(r => r.SubjectTranslations.Any(t => t.Title.Contains(searchText) & t.LanguageId == languageId));
            }

            if(type != null && type > 0)
                Subjects = Subjects.Where(r => r.TypeId == type);
            if (examTypeId != null && examTypeId > 0)
                Subjects = Subjects.Where(r => r.ExamTypeId == examTypeId);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = Subjects;
            var output = result.ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.SubjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Title = trans.Title;
                }

            return output;
        }

        public Subject GetSubjectById(int id)
        {
            var subject = _context.Subjects.Find(id);
            return subject;
        }

        public Subject GetSubjectById(int id, int languageId)
        {
            var subject = _context.Subjects.Include(r => r.SubjectTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = subject.SubjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    subject.Title = trans.Title;
            }
            return subject;
        }

        public void AddSubject(SubjectViewModel subjectViewModel)
        {
            var subject = new Subject()
            {
                CreatedOn = DateTime.Now,
                Status = subjectViewModel.Status,
                Title = subjectViewModel.Title,
                TypeId = subjectViewModel.TypeId,
                CreatedBy = subjectViewModel.CreatedBy,
                ExamTypeId = subjectViewModel.ExamTypeId,
            };
            _context.Subjects.Add(subject);
            _context.SaveChanges();

            subject.Id = subject.Id;

            if (subjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var subjectTran = new SubjectTranslation()
                {
                    Title = subjectViewModel.Title,
                    LanguageId = subjectViewModel.LanguageId,
                    SubjectId = subject.Id,
                };
                _context.SubjectTranslations.Add(subjectTran);
                _context.SaveChanges();
            }
        }

        public void EditSubject(SubjectViewModel subjectViewModel, Subject subject)
        {
            subject.Status = subjectViewModel.Status;
            subject.TypeId = subjectViewModel.TypeId;

            if (subjectViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                subject.Title = subjectViewModel.Title;

            _context.Entry(subject).State = EntityState.Modified;
            _context.SaveChanges();

            if (subjectViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var subjectTranslation = _context.SubjectTranslations.FirstOrDefault(r =>
                    r.LanguageId == subjectViewModel.LanguageId &&
                    r.SubjectId == subject.Id);
                if (subjectTranslation != null)
                {
                    subjectTranslation.Title = subjectViewModel.Title;
                    _context.Entry(subjectTranslation).State = EntityState.Modified;
                }
                else
                {
                    var subjectTran = new SubjectTranslation()
                    {
                        Title = subjectViewModel.Title,
                        LanguageId = subjectViewModel.LanguageId,
                        SubjectId = subjectViewModel.Id
                    };
                    _context.SubjectTranslations.Add(subjectTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteSubject(Subject subject)
        {
            subject.Status = (int)GeneralEnums.StatusEnum.Deleted;
            subject.DeletedOn = DateTime.Now;
            _context.Entry(subject).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
