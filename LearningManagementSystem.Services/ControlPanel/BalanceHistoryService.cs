using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class BalanceHistoryService : IBalanceHistoryService
    {
        private readonly LearningManagementSystemContext _context;

        public BalanceHistoryService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public IPagedList<StudentBalanceHistory> GetStudentBalanceHistory(int studentId, int page, int languageId)
        {
            var history = _context.StudentBalanceHistories.Where(r => r.StudentId == studentId).Include(r => r.EnrollStudentCourse.Course.EnrollTeacherCourseTranlations);

            var output = history.OrderByDescending(r => r.CreatedOn).ToPagedList(page, 10);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var transContact = item?.EnrollStudentCourse?.Course?.EnrollTeacherCourseTranlations?.FirstOrDefault(r => r.LanguageId == languageId);
                    if (transContact != null)
                        item.EnrollStudentCourse.Course.CourseName = transContact.CourseName;
                }

            return output;
        }

        public SenangPayReportsViewModel GetStudentPayments(int studentId , int? page, int languageId, int pagination, FilterViewModel filter = null)
        {
            var student = _context.Students.Include(r=>r.Contact).FirstOrDefault(r => r.Id == studentId);

            List<SenangPayViewModel> data = new List<SenangPayViewModel>();
            _context.SenangPays.Include(r=>r.ApplicationUser).Where(r => r.EnrollTeacherCourseId > 0 && r.ProjectId == null && r.ApplicationUser.Email == student.Contact.Email).Include(r => r.EnrollTeacherCourse)
                .Include(r => r.EnrollTeacherCourse.Teacher.Contact).ToList().ForEach(r =>
                {
                    var senangPayViewModel = new SenangPayViewModel
                    {
                        Type = (int)GeneralEnums.PaymentType.SenangPay,
                        FullName = r.UserName,
                        CourseName = r.EnrollTeacherCourse.CourseName,
                        Status = r.Status,
                        Email = r.Email,
                        CreatedOn = r.ProcessDate,
                        Amount = r.Amount,
                        UserName = r.UserName,
                        TeacherId = r.EnrollTeacherCourse.TeacherId,
                        EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                        CourseId = r.EnrollTeacherCourse.CourseId
                    };
                    data.Add(senangPayViewModel);
                });

            _context.InvoicesPays.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.ContactId == student.ContactId).Include(r => r.Contact).Include(r => r.EnrollTeacherCourse.Course).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Invoice,
                    FullName = r.Contact.FullName,
                    CourseName = r.EnrollTeacherCourse.CourseName,
                    Status = r.ProcessStatus,
                    Email = r.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.EnrollTeacherCourse.Course.CoursePrice ?? 0,
                    UserName = r.Contact.FullName,
                    TeacherId = r.EnrollTeacherCourse.TeacherId,
                    EnrollTeacherCourseId = r.EnrollTeacherCourseId,
                    CourseId = r.EnrollTeacherCourse.CourseId
                };
                data.Add(senangPayViewModel);
            });

            _context.StudentBalanceHistories.Where(r=>r.StudentId == studentId).Include(r => r.Student.Contact).Include(r => r.EnrollStudentCourse.Course.Course).ToList().ForEach(r =>
            {
                var senangPayViewModel = new SenangPayViewModel
                {
                    Type = (int)GeneralEnums.PaymentType.Balance,
                    FullName = r.Student.Contact.FullName,
                    CourseName = r.EnrollStudentCourse?.Course?.CourseName,
                    Status = (int)GeneralEnums.StatusEnum.Paid,
                    Email = r.Student.Contact.Email,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount ?? 0,
                    Title = r.Title,
                    UserName = r.Student.Contact.FullName,
                    TeacherId = r.EnrollStudentCourse?.Course?.TeacherId ?? 0,
                    EnrollTeacherCourseId = r.EnrollStudentCourse?.CourseId,
                    StudentId = r.StudentId,
                    CourseId = r.EnrollStudentCourse?.Course?.CourseId ?? 0
                };
                data.Add(senangPayViewModel);
            });

            if (filter?.Type > 0)
                data = data.Where(r => r.Type == filter.Type).ToList();

            if (filter?.Course > 0)
                data = data.Where(r => r.CourseId == filter.Course).ToList();

            if (filter?.Teacher > 0)
                data = data.Where(r => r.TeacherId == filter.Teacher).ToList();

            if (filter.FromDate != default && filter.ToDate != default)
                data = data.Where(r => r.CreatedOn >= filter.FromDate && r.CreatedOn <= filter.ToDate).ToList();

            if (filter?.Status > 0)
                data = data.Where(r => r.Status == filter.Status).ToList();

            var amount = data.Sum(r => r.Amount);
            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = data;
            var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

            foreach (var item in output)
            {
                if (student != null)
                {
                    item.FullName = student.Contact.FullName;
                    item.StudentId = student.Id;
                }
            }
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = _context.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.CourseId == item.CourseId);
                    var trans1 = _context.Contacts.Include(r => r.ContactTranslations).FirstOrDefault(r => r.Email == item.Email);
                    if (trans != null)
                        item.CourseName = trans.CourseName;
                    if (trans1?.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId) != null)
                        item.FullName = trans1.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId).FullName;
                }

            return new SenangPayReportsViewModel() { Amount = amount, SenangPayViewModels = output };
        }

        public void AddBalanceHistory(StudentBalanceHistory studentBalanceHistory)
        {
            _context.StudentBalanceHistories.Add(studentBalanceHistory);
            _context.SaveChanges();
        }

    }
}
