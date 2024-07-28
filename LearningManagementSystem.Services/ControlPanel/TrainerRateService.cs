using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class TrainerRateService : ITrainerRateService
    {
        private readonly ISettingService _settingService;
        public TrainerRateService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public void AddEditManagmentRates(ManagementRateViewModel managementRateViewModel, string user)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (managementRateViewModel!=null)
                {
                    var managementRate = db.ManagementRates.FirstOrDefault(a => a.EnrollTeacherCourseId == managementRateViewModel.EnrollTeacherCourseId
                    && a.CreatedOn.Date == managementRateViewModel.CreatedOn.Date && a.CreatedBy.Equals(user));
                    if (managementRate != null)
                    {
                        var lines = db.ManagementRateLines.Where(a => a.ManagementRateId == managementRate.Id).ToList();
                        foreach (var line in lines)
                        {
                            db.ManagementRateLines.Remove(line);
                        }
                        db.ManagementRates.Remove(managementRate);
                    }
                    int max = int.Parse(_settingService.GetOrCreate("Max_Rate_Value", "3").Value);
                    var management = new ManagementRate()
                    {
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        CreatedOn = managementRateViewModel.CreatedOn,
                        EnrollTeacherCourseId = managementRateViewModel.EnrollTeacherCourseId,
                        CreatedBy = user,                        
                        Percent = 0,
                    };
                    int sum = 0;
                    int count = managementRateViewModel.ManagementRateLines.Count(a => !String.IsNullOrEmpty(a.Value) && a.StandardType.Equals("rate_standard"));
                    management.ManagementRateLines = new List<ManagementRateLine>();
                    foreach (var lineViewModel in managementRateViewModel.ManagementRateLines.Where(a => !String.IsNullOrEmpty(a.Value)))
                    {
                        if (lineViewModel.StandardType.Equals("rate_standard"))
                        {
                            sum += int.Parse(lineViewModel.Value);                            
                        }
                        management.ManagementRateLines.Add(new ManagementRateLine() { 
                            StandardId =  lineViewModel.StandardId,
                            Value = lineViewModel.Value,                            
                        });                        
                    }
                    management.Percent = (sum /(decimal)(max * count)) * 100 ;
                    db.ManagementRates.Add(management);
                    db.SaveChanges();
                }
            }
        }

        public List<ManagementRateLineViewModel> GetManagmentRates(DateTime date, int enrollId, string user, int languageId)
        {
            var managementRateLines = new List<ManagementRateLineViewModel>();
            using (var db = new LearningManagementSystemContext())
            {
                var standards = db.ManagementStandards.Include(a => a.ManagementStandardTranslations).Where(a => a.Status == (int)GeneralEnums.StatusEnum.Active).OrderBy(a=>a.SortOrder);
                var management = db.ManagementRates.Include(a=>a.ManagementRateLines).FirstOrDefault(a => a.EnrollTeacherCourseId == enrollId && a.CreatedBy.Equals(user)
                && a.CreatedOn.Date == date);

                foreach (var s in standards)
                {
                    var managementRateLine = new ManagementRateLineViewModel();
                    managementRateLine.Standard = s.Standard;
                    managementRateLine.StandardId = s.Id;
                    managementRateLine.StandardType = db.DetailsLookups.FirstOrDefault(a=>a.Id==s.Type)?.Code;
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var managementStandardTranslation = s.ManagementStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (managementStandardTranslation != null)
                        {
                            managementRateLine.Standard = managementStandardTranslation.Standard;
                        }
                    }
                    var exist = management?.ManagementRateLines?.FirstOrDefault(a => a.StandardId == s.Id);
                    if (exist != null)
                    {
                        managementRateLine.Value = exist.Value;
                    }
                    managementRateLines.Add(managementRateLine);
                }
                return managementRateLines;
            }
        }

        public void AddEditAcademicSupervisionRates(List<AcademicSupervisionRateViewModel> academicSupervisionRateViewModels,string user)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (academicSupervisionRateViewModels.Any())
                {
                    var supervisionRates = db.AcademicSupervisionRates.Where(a => a.EnrollTeacherCourseId == academicSupervisionRateViewModels[0].EnrollTeacherCourseId
                    && a.CreatedOn.Date == academicSupervisionRateViewModels[0].CreatedOn.Date && a.CreatedBy.Equals(user)).ToList();
                    foreach (var supervisionRate in supervisionRates)
                    {
                        db.AcademicSupervisionRates.Remove(supervisionRate);
                    }
                    foreach (var academicSupervisionRate in academicSupervisionRateViewModels.Where(a=>a.Rate != null))
                    {
                        var academic = new AcademicSupervisionRate()
                        {
                            StandardId = academicSupervisionRate.StandardId,
                            Rate = academicSupervisionRate.Rate,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            CreatedBy = user,
                            CreatedOn = academicSupervisionRate.CreatedOn,
                            Note = academicSupervisionRate.Note,
                            EnrollTeacherCourseId = academicSupervisionRate.EnrollTeacherCourseId,
                        };
                        db.AcademicSupervisionRates.Add(academic);
                    }
                    db.SaveChanges();
                }                          
            }
        }

        public List<AcademicSupervisionRateViewModel> GetAcademicSupervisionRates(DateTime date,int enrollId, string user,int languageId)
        {
            var academicSupervisionRates = new List<AcademicSupervisionRateViewModel>();
            using (var db = new LearningManagementSystemContext())
            {
                var standards = db.AcademicSupervisionStandards.Include(a=>a.AcademicSupervisionStandardTranslations).Where(a => a.Status == (int)GeneralEnums.StatusEnum.Active).OrderBy(a=>a.SortOrder);
                var supervisionRates = db.AcademicSupervisionRates.Where(a => a.EnrollTeacherCourseId == enrollId && a.CreatedBy.Equals(user) 
                && a.CreatedOn.Date==date).ToList();

                foreach(var s in standards)
                {
                    var academicSupervisionRate = new AcademicSupervisionRateViewModel();
                    academicSupervisionRate.Standard = s.Standard;
                    academicSupervisionRate.StandardId = s.Id;
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var standardTranslation = s.AcademicSupervisionStandardTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (standardTranslation != null)
                        {
                            academicSupervisionRate.Standard = standardTranslation.Standard;
                        }
                    }
                    var exist = supervisionRates.FirstOrDefault(a => a.StandardId == s.Id);
                    if (exist!=null)
                    {
                        academicSupervisionRate.Rate = exist.Rate;
                        academicSupervisionRate.Note = exist.Note;
                    }
                    academicSupervisionRates.Add(academicSupervisionRate);
                }
                return academicSupervisionRates;
            }
        }
    }
}
