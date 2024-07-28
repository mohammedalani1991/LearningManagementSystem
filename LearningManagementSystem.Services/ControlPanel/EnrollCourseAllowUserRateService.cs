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
    public class EnrollCourseAllowUserRateService : IEnrollCourseAllowUserRateService
    {
        private readonly LearningManagementSystemContext _context;

        public EnrollCourseAllowUserRateService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public EnrollCourseAllowUserRate GetAllowUserRateById(int id)
        {
            var AllowUserRate = _context.EnrollCourseAllowUserRates.FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            return AllowUserRate;
        }

        public EnrollCourseAllowUserRate GetAllowUserRateById(int id, int languageId)
        {
            var AllowUserRate = _context.EnrollCourseAllowUserRates.Include(r => r.Contact).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                
            }
            return AllowUserRate;
        }

        public IPagedList<EnrollCourseAllowUserRate> GetAllowUserRates(string searchText, int page, int languageId, int pagination, int courseId)
        {
            var AllowUserRates = _context.EnrollCourseAllowUserRates.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollTeacherCourseId == courseId)
                .Include(r => r.Contact).AsQueryable();
            

            var pageSize = pagination;
            var pageNumber = page;
            var result = AllowUserRates;

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);                
            return output;
        }

        public void AddEnrollCourseAllowUserRate(EnrollCourseAllowUserRateViewModel allowUserRateViewModel)
        {
            var enrollCourseAllowUser = _context.EnrollCourseAllowUserRates.FirstOrDefault(a => a.RateTypeId == allowUserRateViewModel.RateTypeId
            && a.Status != (int)GeneralEnums.StatusEnum.Deleted && a.ContactId == allowUserRateViewModel.ContactId
            && a.EnrollTeacherCourseId == allowUserRateViewModel.EnrollTeacherCourseId);
            if (enrollCourseAllowUser == null)
            {
                var AllowUserRate = new EnrollCourseAllowUserRate()
                {
                    ContactId = allowUserRateViewModel.ContactId ?? 0,
                    RateTypeId = allowUserRateViewModel.RateTypeId,
                    EnrollTeacherCourseId = allowUserRateViewModel.EnrollTeacherCourseId,
                    Status = allowUserRateViewModel.Status,
                    CreatedBy = allowUserRateViewModel.CreatedBy,
                    CreatedOn = DateTime.Now,
                };
                _context.EnrollCourseAllowUserRates.Add(AllowUserRate);
                _context.SaveChanges();
            }            
        }

        public EnrollCourseAllowUserRate EditEnrollCourseAllowUserRate(EnrollCourseAllowUserRateViewModel AllowUserRateViewModel, EnrollCourseAllowUserRate EnrollCourseAllowUserRate)
        {
            EnrollCourseAllowUserRate.RateTypeId = AllowUserRateViewModel.RateTypeId;
            EnrollCourseAllowUserRate.ContactId = AllowUserRateViewModel.ContactId ?? 0;            
            _context.Entry(EnrollCourseAllowUserRate).State = EntityState.Modified;
            _context.SaveChanges();
            return EnrollCourseAllowUserRate;
        }

        public void DeleteAllowUserRate(EnrollCourseAllowUserRate AllowUserRate)
        {
            AllowUserRate.Status = (int)GeneralEnums.StatusEnum.Deleted;
            _context.Entry(AllowUserRate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool CheckAllowUserToRate(string username,int enrollTeacherCourseId,int rateTypeId)
        {
            var user = _context.Contacts.FirstOrDefault(r=>r.Email == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (user != null)
            {
                return _context.EnrollCourseAllowUserRates.Any(a => a.Status != (int)GeneralEnums.StatusEnum.Deleted && a.ContactId == user.Id && a.EnrollTeacherCourseId ==
                enrollTeacherCourseId && a.RateTypeId == rateTypeId);
            }
            return false;
        }
    }
}
