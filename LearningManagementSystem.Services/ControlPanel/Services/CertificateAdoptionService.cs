using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel.IServices;
using LearningManagementSystem.Services.Helpers;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.ControlPanel.Services
{
    public class CertificateAdoptionService : ICertificateAdoptionService
    {
        private readonly LearningManagementSystemContext _context;

        public CertificateAdoptionService(LearningManagementSystemContext context)
        {
            _context = context;
        }
        public IPagedList<CertificateAdoption> GetCertificateAdoption(int? page, int languageId, int pagination)
        {
            var adoptions = _context.CertificateAdoptions.Include(r => r.Semester.SemesterTranslations).Include(r => r.Course.CourseTranslations);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = adoptions;
            var output = result.ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var trans1 = item.Semester.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Course.CourseName = trans.CourseName;
                    if (trans1 != null)
                        item.Semester.Name = trans1.Name;
                }

            return output;
        }

        public void AddCertificateAdoption(int semesterId, int courseId, string createdBy)
        {
            var courses = _context.EnrollTeacherCourses.Where(r => r.SemesterId == semesterId && r.CourseId == courseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            foreach (var item in courses)
            {
                item.CertificateAdoption = true;
                _context.Entry(item).State = EntityState.Modified;
            }

            var adoption = new CertificateAdoption()
            {
                CreatedOn = DateTime.Now,
                CreatedBy = createdBy,
                CourseId = courseId,
                SemesterId = semesterId,
            };

            _context.CertificateAdoptions.Add(adoption);
            _context.SaveChanges();
        }

        public void DeleteCertificateAdoption(int id)
        {
            var adoptions = _context.CertificateAdoptions.FirstOrDefault(r => r.Id == id);

            var courses = _context.EnrollTeacherCourses.Where(r => r.SemesterId == adoptions.SemesterId && r.CourseId == adoptions.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            foreach (var item in courses)
            {
                item.CertificateAdoption = false;
                _context.Entry(item).State = EntityState.Modified;
            }

            _context.CertificateAdoptions.Remove(adoptions);
            _context.SaveChanges();
        }

        public void EditCertificateAdoption(int id, bool show)
        {
            var course = _context.EnrollTeacherCourses.FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            course.CertificateAdoption = show;
            _context.Entry(course).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
