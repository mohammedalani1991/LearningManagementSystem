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
    public class ManagementStandardService : IManagementStandardService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;
        private readonly ICookieService _cookieService;

        public ManagementStandardService(ISettingService settingService, LearningManagementSystemContext context,ICookieService cookieService)
        {
            _settingService = settingService;
            _context = context;
            _cookieService = cookieService;
        }
        
        public IPagedList<ManagementStandard> GetManagementStandards(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var ManagementStandards = _context.ManagementStandards.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.ManagementStandardTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    ManagementStandards = ManagementStandards.Where(r => r.Standard.Contains(searchText));
                else
                    ManagementStandards = ManagementStandards.Where(r => r.ManagementStandardTranslations.Any(t => t.Standard.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = ManagementStandards;
            var output = result.OrderBy(r => r.SortOrder).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.ManagementStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Standard = trans.Standard;
                }

            return output;
        }

        public ManagementStandard GetManagementStandardById(int id)
        {
            var ManagementStandard = _context.ManagementStandards.Find(id);
            return ManagementStandard;
        }

        public ManagementStandard GetManagementStandardById(int id, int languageId)
        {
            var ManagementStandard = _context.ManagementStandards.Include(r => r.ManagementStandardTranslations).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = ManagementStandard.ManagementStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    ManagementStandard.Standard = trans.Standard;
            }

            return ManagementStandard;
        }        

        public void AddManagementStandard(ManagementStandardViewModel ManagementStandardViewModel)
        {
            

            var ManagementStandard = new ManagementStandard()
            {
                CreatedOn = DateTime.Now,
                Status = ManagementStandardViewModel.Status,
                Standard = ManagementStandardViewModel.Standard,
                SortOrder = ManagementStandardViewModel.SortOrder,
                Type = ManagementStandardViewModel.Type,
                CreatedBy = ManagementStandardViewModel.CreatedBy,
            };
            _context.ManagementStandards.Add(ManagementStandard);
            _context.SaveChanges();

            ManagementStandard.Id = ManagementStandard.Id;

            if (ManagementStandardViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var ManagementStandardTran = new ManagementStandardTranslation()
                {
                    Standard = ManagementStandardViewModel.Standard,
                    LanguageId = ManagementStandardViewModel.LanguageId,
                    ManagementStandardId = ManagementStandard.Id,
                };
                _context.ManagementStandardTranslations.Add(ManagementStandardTran);
                _context.SaveChanges();
            }
        }

        public void EditManagementStandard(ManagementStandardViewModel ManagementStandardViewModel, ManagementStandard ManagementStandard)
        {            
            ManagementStandard.Status = ManagementStandardViewModel.Status;
            ManagementStandard.Standard = ManagementStandardViewModel.Standard;
            ManagementStandard.SortOrder = ManagementStandardViewModel.SortOrder;
            ManagementStandard.Type = ManagementStandardViewModel.Type;

            if (ManagementStandardViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                ManagementStandard.Standard = ManagementStandardViewModel.Standard;

            _context.Entry(ManagementStandard).State = EntityState.Modified;
            _context.SaveChanges();

            if (ManagementStandardViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var ManagementStandardTranslation = _context.ManagementStandardTranslations.FirstOrDefault(r =>
                    r.LanguageId == ManagementStandardViewModel.LanguageId &&
                    r.ManagementStandardId == ManagementStandard.Id);
                if (ManagementStandardTranslation != null)
                {
                    ManagementStandardTranslation.Standard = ManagementStandardViewModel.Standard;
                    _context.Entry(ManagementStandardTranslation).State = EntityState.Modified;
                }
                else
                {
                    var ManagementStandardTran = new ManagementStandardTranslation()
                    {
                        Standard = ManagementStandardViewModel.Standard,
                        LanguageId = ManagementStandardViewModel.LanguageId,
                        ManagementStandardId = ManagementStandardViewModel.Id
                    };
                    _context.ManagementStandardTranslations.Add(ManagementStandardTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteManagementStandard(ManagementStandard ManagementStandard)
        {
            ManagementStandard.Status = (int)GeneralEnums.StatusEnum.Deleted;
            ManagementStandard.DeletedOn = DateTime.Now;
            _context.Entry(ManagementStandard).State = EntityState.Modified;
            _context.SaveChanges();
        }        
    }
}
