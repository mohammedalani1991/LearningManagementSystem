using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
  public  interface ICityService
    {
        List<CityViewModel> GetCities(int countryId, int languageId);
        IPagedList<City> GetCities(string searchText, int countryId, int? page, int languageId, int pagination);
        CityViewModel AddCity(CityViewModel CityViewModel);
        City GetCityById(int id);
        CityViewModel GetCityById(int id, int languageId);
        void EditDetailLookup(CityViewModel CityViewModel, City City);
        void DeleteCity(City details);

    }
}
