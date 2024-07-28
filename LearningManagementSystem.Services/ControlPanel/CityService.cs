using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
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
    public class CityService : ICityService
    {
        public CityViewModel AddCity(CityViewModel cityViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var City = new City
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = cityViewModel.CreatedBy,
                    Status = cityViewModel.Status,
                    CountryId = cityViewModel.CountryId,
                    Name = cityViewModel.Name
                };
                db.Cities.Add(City);
                db.SaveChanges();

                if (cityViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CityTran = new CityTranslation()
                    {
                        Name = cityViewModel.Name,
                        LanguageId = cityViewModel.LanguageId,
                        CityId = City.Id
                    };
                    db.CityTranslations.Add(CityTran);
                    db.SaveChanges();
                }

                cityViewModel.Id = City.Id;
                return cityViewModel;
            }
        }

        public void DeleteCity(City details)
        {
            using (var db = new LearningManagementSystemContext())
            {
                details.Status = (int)GeneralEnums.StatusEnum.Deleted;
                details.DeletedOn = DateTime.Now;
                db.Entry(details).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditDetailLookup(CityViewModel cityViewModel, City city)
        {
            using (var db = new LearningManagementSystemContext())
            {
                city.Status = cityViewModel.Status;
                city.CountryId = cityViewModel.CountryId;

                if (cityViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    city.Name = cityViewModel.Name;
                    city.Status = cityViewModel.Status;

                }

                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();

                if (cityViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var detailsTran = db.CityTranslations.FirstOrDefault(r =>
                        r.LanguageId == cityViewModel.LanguageId &&
                        r.CityId == cityViewModel.Id);
                    if (detailsTran != null)
                    {
                        detailsTran.Name = cityViewModel.Name;
                        city.Status = cityViewModel.Status;
                        db.Entry(detailsTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var cityTran = new CityTranslation()
                        {
                            Name = cityViewModel.Name,
                            LanguageId = cityViewModel.LanguageId,
                            CityId = city.Id
                        };
                        db.CityTranslations.Add(cityTran);
                    }

                    db.SaveChanges();
                }
            }
        }

        public List<CityViewModel> GetCities(int countryId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var city = db.Cities.Include(a => a.CityTranslations)
                    .Where(r => r.CountryId == countryId &&
                                r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in city)
                    {
                        var trans = item.CityTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)

                            item.Name = trans.Name;
                    }


                var output = city.ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    output = output.OrderBy(r => r.Name, StringComparer.Create(new CultureInfo("ar-SA"), true)).ToList();
                else
                    output = output.OrderBy(r => r.Name).ToList();

                var result = output.ToList().Select(r =>
                    new CityViewModel
                    {
                        Id = r.Id,
                        Status = r.Status,
                        Name = r.Name,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn,
                        DeletedOn = r.DeletedOn,
                        CountryId = r.CountryId
                    }
                );

                return result.ToList();
            }
        }

        public IPagedList<City> GetCities(string searchText, int countryId, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var cities = db.Cities.Include(r => r.CityTranslations).Where(r => r.CountryId == countryId &&
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    cities = cities.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        cities = cities.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        cities = cities.Where(r => r.CityTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = cities;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CityTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }
                return output;
            }
        }

        public City GetCityById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var detailsLookup = db.Cities.Find(id);
                return detailsLookup;
            }
        }

        public CityViewModel GetCityById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var masterLookupTrans =
                        db.CityTranslations.Include(r => r.City).FirstOrDefault(r => r.LanguageId == languageId && r.CityId == id);
                    if (masterLookupTrans != null)
                    {
                        return new CityViewModel(masterLookupTrans);
                    }
                }
                var detailsLookup = db.Cities.Find(id);
                return new CityViewModel(detailsLookup);
            }
        }
    }
}
