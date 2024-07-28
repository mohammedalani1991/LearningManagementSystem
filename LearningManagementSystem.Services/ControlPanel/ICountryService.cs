using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICountryService
    {
        IPagedList<Country> GetCountryes(string searchText, int? page, int languageId, int pagination);
        Country GetCountryById(int id);
        CountryViewModel GetCountryById(int id, int languageId);

        void AddCountry(CountryViewModel Country);
        void EditCountry(CountryViewModel CountryViewModel, Country Country);
        void DeleteCountry(Country Country);

    }
}
