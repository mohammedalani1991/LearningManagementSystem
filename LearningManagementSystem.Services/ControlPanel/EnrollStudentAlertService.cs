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
    public class EnrollStudentAlertService : IEnrollStudentAlertService
    {
        private readonly LearningManagementSystemContext _context;

        public EnrollStudentAlertService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public EnrollStudentAlert GetAllowUserRateById(int id)
        {
            var AllowUserRate = _context.EnrollStudentAlerts.FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            return AllowUserRate;
        }

        public EnrollStudentAlert GetAllowUserRateById(int id, int languageId)
        {
            var AllowUserRate = _context.EnrollStudentAlerts.Include(r => r.EnrollStudentCourse.Student.Contact).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                
            }
            return AllowUserRate;
        }

        public IPagedList<EnrollStudentAlert> GetAllowUserRates(string searchText, int page, int languageId, int pagination, int courseId , int? enrollStudentCourseId)
        {
            var AllowUserRates = _context.EnrollStudentAlerts.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollTeacherCourseId == courseId)
                .Include(r => r.EnrollStudentCourse.Student.Contact).AsQueryable();

            if (enrollStudentCourseId > 0)
                AllowUserRates = AllowUserRates.Where(r => r.EnrollStudentCourseId == enrollStudentCourseId);

            var pageSize = pagination;
            var pageNumber = page;
            var result = AllowUserRates;

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);                
            return output;
        }

        public void AddEnrollStudentAlert(EnrollStudentAlertViewModel allowUserRateViewModel)
        {            
            var AllowUserRate = new EnrollStudentAlert()
            {
                EnrollStudentCourseId = allowUserRateViewModel.EnrollStudentCourseId,
                EnrollTeacherCourseId = allowUserRateViewModel.EnrollTeacherCourseId,
                AlertTypeId = allowUserRateViewModel.AlertTypeId,
                Title = allowUserRateViewModel.Title,
                Description = allowUserRateViewModel.Description??String.Empty,
                Status = allowUserRateViewModel.Status,
                CreatedBy = allowUserRateViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
            };
            _context.EnrollStudentAlerts.Add(AllowUserRate);
            _context.SaveChanges();                      
        }

        public EnrollStudentAlert EditEnrollStudentAlert(EnrollStudentAlertViewModel AllowUserRateViewModel, EnrollStudentAlert EnrollStudentAlert)
        {
            EnrollStudentAlert.AlertTypeId = AllowUserRateViewModel.AlertTypeId;
            EnrollStudentAlert.EnrollStudentCourseId = AllowUserRateViewModel.EnrollStudentCourseId;
            EnrollStudentAlert.Title = AllowUserRateViewModel.Title;
            EnrollStudentAlert.Description = AllowUserRateViewModel.Description ?? String.Empty;
            EnrollStudentAlert.Status = AllowUserRateViewModel.Status;
            _context.Entry(EnrollStudentAlert).State = EntityState.Modified;
            _context.SaveChanges();
            return EnrollStudentAlert;
        }

        public void DeleteAllowUserRate(EnrollStudentAlert AllowUserRate)
        {
            AllowUserRate.Status = (int)GeneralEnums.StatusEnum.Deleted;
            _context.Entry(AllowUserRate).State = EntityState.Modified;
            _context.SaveChanges();
        }        
    }
}
