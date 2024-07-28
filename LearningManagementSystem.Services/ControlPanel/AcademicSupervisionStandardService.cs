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
    public class AcademicSupervisionStandardService : IAcademicSupervisionStandardService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;
        private readonly ICookieService _cookieService;

        public AcademicSupervisionStandardService(ISettingService settingService, LearningManagementSystemContext context,ICookieService cookieService)
        {
            _settingService = settingService;
            _context = context;
            _cookieService = cookieService;
        }
        
        public IPagedList<AcademicSupervisionStandard> GetAcademicSupervisionStandards(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var AcademicSupervisionStandards = _context.AcademicSupervisionStandards.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.AcademicSupervisionStandardTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    AcademicSupervisionStandards = AcademicSupervisionStandards.Where(r => r.Standard.Contains(searchText));
                else
                    AcademicSupervisionStandards = AcademicSupervisionStandards.Where(r => r.AcademicSupervisionStandardTranslations.Any(t => t.Standard.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = AcademicSupervisionStandards;
            var output = result.OrderBy(r => r.SortOrder).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.AcademicSupervisionStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Standard = trans.Standard;
                }

            return output;
        }

        public AcademicSupervisionStandard GetAcademicSupervisionStandardById(int id)
        {
            var AcademicSupervisionStandard = _context.AcademicSupervisionStandards.Find(id);
            return AcademicSupervisionStandard;
        }

        public AcademicSupervisionStandard GetAcademicSupervisionStandardById(int id, int languageId)
        {
            var AcademicSupervisionStandard = _context.AcademicSupervisionStandards.Include(r => r.AcademicSupervisionStandardTranslations).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = AcademicSupervisionStandard.AcademicSupervisionStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    AcademicSupervisionStandard.Standard = trans.Standard;
            }

            return AcademicSupervisionStandard;
        }        

        public void AddAcademicSupervisionStandard(AcademicSupervisionStandardViewModel AcademicSupervisionStandardViewModel)
        {
            

            var AcademicSupervisionStandard = new AcademicSupervisionStandard()
            {
                CreatedOn = DateTime.Now,
                Status = AcademicSupervisionStandardViewModel.Status,
                Standard = AcademicSupervisionStandardViewModel.Standard,
                SortOrder = AcademicSupervisionStandardViewModel.SortOrder,
                CreatedBy = AcademicSupervisionStandardViewModel.CreatedBy,
            };
            _context.AcademicSupervisionStandards.Add(AcademicSupervisionStandard);
            _context.SaveChanges();

            AcademicSupervisionStandard.Id = AcademicSupervisionStandard.Id;

            if (AcademicSupervisionStandardViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var AcademicSupervisionStandardTran = new AcademicSupervisionStandardTranslation()
                {
                    Standard = AcademicSupervisionStandardViewModel.Standard,
                    LanguageId = AcademicSupervisionStandardViewModel.LanguageId,
                    AcademicSupervisionStandardId = AcademicSupervisionStandard.Id,
                };
                _context.AcademicSupervisionStandardTranslations.Add(AcademicSupervisionStandardTran);
                _context.SaveChanges();
            }
        }

        public void EditAcademicSupervisionStandard(AcademicSupervisionStandardViewModel AcademicSupervisionStandardViewModel, AcademicSupervisionStandard AcademicSupervisionStandard)
        {            
            AcademicSupervisionStandard.Status = AcademicSupervisionStandardViewModel.Status;
            AcademicSupervisionStandard.Standard = AcademicSupervisionStandardViewModel.Standard;
            AcademicSupervisionStandard.SortOrder = AcademicSupervisionStandardViewModel.SortOrder;

            if (AcademicSupervisionStandardViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                AcademicSupervisionStandard.Standard = AcademicSupervisionStandardViewModel.Standard;

            _context.Entry(AcademicSupervisionStandard).State = EntityState.Modified;
            _context.SaveChanges();

            if (AcademicSupervisionStandardViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var AcademicSupervisionStandardTranslation = _context.AcademicSupervisionStandardTranslations.FirstOrDefault(r =>
                    r.LanguageId == AcademicSupervisionStandardViewModel.LanguageId &&
                    r.AcademicSupervisionStandardId == AcademicSupervisionStandard.Id);
                if (AcademicSupervisionStandardTranslation != null)
                {
                    AcademicSupervisionStandardTranslation.Standard = AcademicSupervisionStandardViewModel.Standard;
                    _context.Entry(AcademicSupervisionStandardTranslation).State = EntityState.Modified;
                }
                else
                {
                    var AcademicSupervisionStandardTran = new AcademicSupervisionStandardTranslation()
                    {
                        Standard = AcademicSupervisionStandardViewModel.Standard,
                        LanguageId = AcademicSupervisionStandardViewModel.LanguageId,
                        AcademicSupervisionStandardId = AcademicSupervisionStandardViewModel.Id
                    };
                    _context.AcademicSupervisionStandardTranslations.Add(AcademicSupervisionStandardTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteAcademicSupervisionStandard(AcademicSupervisionStandard AcademicSupervisionStandard)
        {
            AcademicSupervisionStandard.Status = (int)GeneralEnums.StatusEnum.Deleted;
            AcademicSupervisionStandard.DeletedOn = DateTime.Now;
            _context.Entry(AcademicSupervisionStandard).State = EntityState.Modified;
            _context.SaveChanges();
        }        
    }
}
