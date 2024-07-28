using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CountryService : ICountryService
    {
        private readonly ISettingService _settingService;
        public CountryService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public void AddCountry(CountryViewModel countryViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var country = new Country()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = countryViewModel.Name,
                    CreatedBy = countryViewModel.CreatedBy,
                };
                db.Countries.Add(country);
                db.SaveChanges();

                countryViewModel.Id = country.Id;

                if (countryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var countryTran = new CountryTranslation()
                    {
                        Name = countryViewModel.Name,
                        LanguageId = countryViewModel.LanguageId,
                        CountryId = country.Id
                    };
                    db.CountryTranslations.Add(countryTran);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteCountry(Country Country)
        {
            using (var db = new LearningManagementSystemContext())
            {
                Country.Status = (int)GeneralEnums.StatusEnum.Deleted;
                Country.DeletedOn = DateTime.Now;
                db.Entry(Country).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditCountry(CountryViewModel countryViewModel, Country country)
        {
            using (var db = new LearningManagementSystemContext())
            {

                if (countryViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    country.Name = countryViewModel.Name;
                    country.Status = countryViewModel.Status;
                }

                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                if (countryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var countryTranslation = db.CountryTranslations.FirstOrDefault(r =>
                        r.LanguageId == countryViewModel.LanguageId &&
                        r.CountryId == countryViewModel.Id);
                    if (countryTranslation != null)
                    {
                        countryTranslation.Name = countryViewModel.Name;
                        country.Status = countryViewModel.Status;
                        db.Entry(countryTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var countryTran = new CountryTranslation()
                        {
                            Name = countryViewModel.Name,
                            LanguageId = countryViewModel.LanguageId,
                            CountryId = countryViewModel.Id
                        };
                        db.CountryTranslations.Add(countryTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public Country GetCountryById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var country = db.Countries.Find(id);
                return country;
            }
        }

        public CountryViewModel GetCountryById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CountryTranslations.Include(r => r.Country).FirstOrDefault(r => r.LanguageId == languageId && r.CountryId == id);
                    if (aboutTran != null)
                    {
                        return new CountryViewModel(aboutTran);
                    }
                }
                var country = db.Countries.Find(id);
                return new CountryViewModel(country);
            }
        }

        public IPagedList<Country> GetCountryes(string searchText, int? page, int languageId, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var countries = db.Countries.Include(r => r.CountryTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    countries = countries.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        countries = countries.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        countries = countries.Where(r => r.CountryTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in countries)
                    {
                        var trans = item.CountryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Name = trans.Name;
                    }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var output = countries.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    output = output.OrderBy(r => r.Name, StringComparer.Create(new CultureInfo("ar-SA"), true)).ToList();
                else
                    output = output.OrderBy(r => r.Name).ToList();

                return output.ToPagedList(pageNumber, pageSize);
            }
        }
    }
}
