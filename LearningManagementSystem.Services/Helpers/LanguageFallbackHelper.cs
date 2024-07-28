using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;

namespace LearningManagementSystem.Services.Helpers
{
    public class LanguageFallbackHelper
    {

        public static List<DetailsLookupViewModel> GetDefaultLookupDetailsByMasterId(int masterLookupId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.DetailsLookups.Where(r =>
                            r.DetailsLookupTranslations.Any(x => x.IsDefault)
                            && r.MasterId == masterLookupId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                        .SelectMany(r => r.DetailsLookupTranslations).Select(x => new DetailsLookupViewModel()
                        {
                            Id = x.DetailsLookupId,
                            Name = x.Value
                        }).ToList();

                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Default Lookup Details By Master lookup Id {masterLookupId}");
                    return new List<DetailsLookupViewModel>();
                }

            }
        }

        public static DetailsLookupViewModel GetDefaultLookupDetailsById(int lookupId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.DetailsLookups.Where(r =>
                            r.DetailsLookupTranslations.Any(x => x.IsDefault)
                            && r.Id == lookupId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                        .SelectMany(r => r.DetailsLookupTranslations).Select(x => new DetailsLookupViewModel()
                        {
                            Id = x.DetailsLookupId,
                            Name = x.Value
                        }).FirstOrDefault();

                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Default Lookup Details By lookup Id {lookupId}");
                    return new DetailsLookupViewModel();
                }

            }
        }
    }
}
