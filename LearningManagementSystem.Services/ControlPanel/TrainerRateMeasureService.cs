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
    public class TrainerRateMeasureService : ITrainerRateMeasureService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;
        private readonly ICookieService _cookieService;

        public TrainerRateMeasureService(ISettingService settingService, LearningManagementSystemContext context,ICookieService cookieService)
        {
            _settingService = settingService;
            _context = context;
            _cookieService = cookieService;
        }
        
        public IPagedList<TrainerRateMeasure> GetTrainerRateMeasures(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var TrainerRateMeasures = _context.TrainerRateMeasures.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.TrainerRateMeasureTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    TrainerRateMeasures = TrainerRateMeasures.Where(r => r.Measure.Contains(searchText));
                else
                    TrainerRateMeasures = TrainerRateMeasures.Where(r => r.TrainerRateMeasureTranslations.Any(t => t.Measure.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = TrainerRateMeasures;
            var output = result.OrderBy(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Measure = trans.Measure;
                }

            return output;
        }

        public TrainerRateMeasure GetTrainerRateMeasureById(int id)
        {
            var TrainerRateMeasure = _context.TrainerRateMeasures.Find(id);
            return TrainerRateMeasure;
        }

        public TrainerRateMeasure GetTrainerRateMeasureById(int id, int languageId)
        {
            var TrainerRateMeasure = _context.TrainerRateMeasures.Include(r => r.TrainerRateMeasureTranslations).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = TrainerRateMeasure.TrainerRateMeasureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    TrainerRateMeasure.Measure = trans.Measure;
            }

            return TrainerRateMeasure;
        }        

        public void AddTrainerRateMeasure(TrainerRateMeasureViewModel TrainerRateMeasureViewModel)
        {
            

            var TrainerRateMeasure = new TrainerRateMeasure()
            {
                CreatedOn = DateTime.Now,
                Status = TrainerRateMeasureViewModel.Status,
                Measure = TrainerRateMeasureViewModel.Measure,
                FromRange = TrainerRateMeasureViewModel.FromRange,
                CreatedBy = TrainerRateMeasureViewModel.CreatedBy,
                ToRange = TrainerRateMeasureViewModel.ToRange,
                Type = TrainerRateMeasureViewModel.Type,
            };
            _context.TrainerRateMeasures.Add(TrainerRateMeasure);
            _context.SaveChanges();

            TrainerRateMeasure.Id = TrainerRateMeasure.Id;

            if (TrainerRateMeasureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var TrainerRateMeasureTran = new TrainerRateMeasureTranslation()
                {
                    Measure = TrainerRateMeasureViewModel.Measure,
                    LanguageId = TrainerRateMeasureViewModel.LanguageId,
                    TrainerRateMeasureId = TrainerRateMeasure.Id,
                };
                _context.TrainerRateMeasureTranslations.Add(TrainerRateMeasureTran);
                _context.SaveChanges();
            }
        }

        public void EditTrainerRateMeasure(TrainerRateMeasureViewModel TrainerRateMeasureViewModel, TrainerRateMeasure TrainerRateMeasure)
        {            
            TrainerRateMeasure.Status = TrainerRateMeasureViewModel.Status;
            TrainerRateMeasure.Measure = TrainerRateMeasureViewModel.Measure;
            TrainerRateMeasure.FromRange = TrainerRateMeasureViewModel.FromRange;
            TrainerRateMeasure.ToRange = TrainerRateMeasureViewModel.ToRange;
            TrainerRateMeasure.Type = TrainerRateMeasureViewModel.Type;

            if (TrainerRateMeasureViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                TrainerRateMeasure.Measure = TrainerRateMeasureViewModel.Measure;

            _context.Entry(TrainerRateMeasure).State = EntityState.Modified;
            _context.SaveChanges();

            if (TrainerRateMeasureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var TrainerRateMeasureTranslation = _context.TrainerRateMeasureTranslations.FirstOrDefault(r =>
                    r.LanguageId == TrainerRateMeasureViewModel.LanguageId &&
                    r.TrainerRateMeasureId == TrainerRateMeasure.Id);
                if (TrainerRateMeasureTranslation != null)
                {
                    TrainerRateMeasureTranslation.Measure = TrainerRateMeasureViewModel.Measure;
                    _context.Entry(TrainerRateMeasureTranslation).State = EntityState.Modified;
                }
                else
                {
                    var TrainerRateMeasureTran = new TrainerRateMeasureTranslation()
                    {
                        Measure = TrainerRateMeasureViewModel.Measure,
                        LanguageId = TrainerRateMeasureViewModel.LanguageId,
                        TrainerRateMeasureId = TrainerRateMeasureViewModel.Id
                    };
                    _context.TrainerRateMeasureTranslations.Add(TrainerRateMeasureTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteTrainerRateMeasure(TrainerRateMeasure TrainerRateMeasure)
        {
            TrainerRateMeasure.Status = (int)GeneralEnums.StatusEnum.Deleted;
            TrainerRateMeasure.DeletedOn = DateTime.Now;
            _context.Entry(TrainerRateMeasure).State = EntityState.Modified;
            _context.SaveChanges();
        }        
    }
}
