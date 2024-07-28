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
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class ExpulsionService : IExpulsionService
    {
        private readonly LearningManagementSystemContext _context;

        public ExpulsionService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public IPagedList<Expulsion> GetExpulsions(int? page, int pagination = 25)
        {
            var Expulsions = _context.Expulsions.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = Expulsions;
            var output = result.OrderBy(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            return output;
        }

        public Expulsion GetExpulsionById(int id)
        {
            var expulsion = _context.Expulsions.Find(id);
            return expulsion;
        }

        public void AddExpulsion(ExpulsionViewModel expulsionViewModel)
        {
            var expulsion = new Expulsion()
            {
                CreatedOn = DateTime.Now,
                CreatedBy = expulsionViewModel.CreatedBy,
                Status = expulsionViewModel.Status,
                ExpelledFrom = expulsionViewModel.ExpelledFrom,
                ExpelledTo = expulsionViewModel.ExpelledTo,
                ExpulsionStart = expulsionViewModel.ExpulsionStart,
                ExpulsionEnd = expulsionViewModel.ExpulsionEnd,

            };
            _context.Expulsions.Add(expulsion);
            _context.SaveChanges();
        }

        public void EditExpulsion(ExpulsionViewModel expulsionViewModel, Expulsion expulsion)
        {
            expulsion.Status = expulsionViewModel.Status;
            expulsion.ExpelledFrom = expulsionViewModel.ExpelledFrom;
            expulsion.ExpelledTo = expulsionViewModel.ExpelledTo;
            expulsion.ExpulsionStart = expulsionViewModel.ExpulsionStart;
            expulsion.ExpulsionEnd = expulsionViewModel.ExpulsionEnd;

            _context.Entry(expulsion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteExpulsion(Expulsion expulsion)
        {
            expulsion.Status = (int)GeneralEnums.StatusEnum.Deleted;
            expulsion.DeletedOn = DateTime.Now;
            _context.Entry(expulsion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IPagedList<EnrollStudentCourse> GetExpelledStudents(DateTime? startDate, DateTime? endDate, int? page, int languageId)
        {
            var students = _context.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Expelled && r.ExpelledDate >= startDate && r.ExpelledDate <= endDate).Include(r=>r.Course.EnrollTeacherCourseTranlations).Include(r => r.Student.Contact.ContactTranslations).AsQueryable();

            var result = students.OrderByDescending(r => r.ExpelledDate).ToPagedList(page ?? 1, 10);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in result)
                {
                    var trans = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = item.Course.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Student.Contact.FullName = trans.FullName;
                    if (trans1 != null)
                        item.Course.CourseName = trans1.CourseName;
                }
            }

            return result;
        }
    }
}
