using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICurrencyService
    {
        Currency GetPrimaryCurrency();
        Currency GetExchangeCurrency();
        IPagedList<Currency> GetCurrencies(string searchText, int? page, int languageId, int pagination);
        Currency GetCurrencyById(int id);
        Currency GetCurrencyById(int id, int languageId);
        List<Currency> GetAllCurrencies(int languageId);

        void AddCurrency(CurrencyViewModel currencyViewModel);
        void EditCurrency(CurrencyViewModel currencyViewModel, Currency currency);
        void DeleteCurrency(Currency currency);
        decimal GetValue(decimal CoursePrice);
    }
}
