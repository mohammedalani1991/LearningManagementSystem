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
    public class CurrencyService : ICurrencyService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;
        private readonly ICookieService _cookieService;

        public CurrencyService(ISettingService settingService, LearningManagementSystemContext context,ICookieService cookieService)
        {
            _settingService = settingService;
            _context = context;
            _cookieService = cookieService;
        }

        public Currency GetPrimaryCurrency()
        {
            return _context.Currencies.FirstOrDefault(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsPrimary == true);
        }

        public Currency GetExchangeCurrency()
        {
            return _context.Currencies.FirstOrDefault(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsExchange == true);
        }

        public IPagedList<Currency> GetCurrencies(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var Currencies = _context.Currencies.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.CurrencyTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    Currencies = Currencies.Where(r => r.Name.Contains(searchText));
                else
                    Currencies = Currencies.Where(r => r.CurrencyTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = Currencies;
            var output = result.OrderBy(r => r.SortOrder).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.CurrencyTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }

            return output;
        }

        public Currency GetCurrencyById(int id)
        {
            var currency = _context.Currencies.Find(id);
            return currency;
        }

        public Currency GetCurrencyById(int id, int languageId)
        {
            var currency = _context.Currencies.Include(r => r.CurrencyTranslations).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = currency.CurrencyTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    currency.Name = trans.Name;
            }

            return currency;
        }

        public List<Currency> GetAllCurrencies(int languageId)
        {
            var currencies = _context.Currencies.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.CurrencyTranslations).AsQueryable();
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in currencies)
                {
                    var trans = item.CurrencyTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }
            return currencies.OrderBy(r=>r.SortOrder).ToList();
        }

        public void AddCurrency(CurrencyViewModel currencyViewModel)
        {
            var cur = _context.Currencies.FirstOrDefault(r=>r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsPrimary == true);
            var cur1 = _context.Currencies.FirstOrDefault(r=>r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsExchange == true);
            if (cur != null)
            {
                if (cur.IsPrimary == true && currencyViewModel.IsPrimary == true)
                    cur.IsPrimary = false;
                _context.Entry(cur).State = EntityState.Modified;
            }
            if (cur1 != null)
            {
                if (cur1.IsExchange == true && currencyViewModel.IsExchange == true)
                    cur1.IsExchange = false;
                _context.Entry(cur1).State = EntityState.Modified;
            }

            var currency = new Currency()
            {
                CreatedOn = DateTime.Now,
                Status = currencyViewModel.Status,
                Name = currencyViewModel.Name,
                Code = currencyViewModel.Code,
                CreatedBy = currencyViewModel.CreatedBy,
                Icon = currencyViewModel.Icon,
                IsExchange = currencyViewModel.IsExchange,
                IsPrimary = currencyViewModel.IsPrimary,
                Value = currencyViewModel.Value,
                SortOrder = currencyViewModel.SortOrder,

            };
            _context.Currencies.Add(currency);
            _context.SaveChanges();

            currency.Id = currency.Id;

            if (currencyViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var currencyTran = new CurrencyTranslation()
                {
                    Name = currencyViewModel.Name,
                    LanguageId = currencyViewModel.LanguageId,
                    CurrencyId = currency.Id,
                };
                _context.CurrencyTranslations.Add(currencyTran);
                _context.SaveChanges();
            }
        }

        public void EditCurrency(CurrencyViewModel currencyViewModel, Currency currency)
        {
            var cur = _context.Currencies.FirstOrDefault(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsPrimary == true);
            var cur1 = _context.Currencies.FirstOrDefault(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsExchange == true);
            if (cur != null)
            {
                if (cur.IsPrimary == true && currencyViewModel.IsPrimary == true)
                    cur.IsPrimary = false;
                _context.Entry(cur).State = EntityState.Modified;
            }
            if (cur1 != null)
            {
                if (cur1.IsExchange == true && currencyViewModel.IsExchange == true)
                    cur1.IsExchange = false;
                _context.Entry(cur1).State = EntityState.Modified;
            }

            currency.Status = currencyViewModel.Status;
            currency.Value = currencyViewModel.Value;
            currency.Code = currencyViewModel.Code;
            currency.IsPrimary = currencyViewModel.IsPrimary;
            currency.IsExchange = currencyViewModel.IsExchange;
            currency.Icon = currencyViewModel.Icon;
            currency.SortOrder = currencyViewModel.SortOrder;

            if (currencyViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                currency.Name = currencyViewModel.Name;

            _context.Entry(currency).State = EntityState.Modified;
            _context.SaveChanges();

            if (currencyViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var currencyTranslation = _context.CurrencyTranslations.FirstOrDefault(r =>
                    r.LanguageId == currencyViewModel.LanguageId &&
                    r.CurrencyId == currency.Id);
                if (currencyTranslation != null)
                {
                    currencyTranslation.Name = currencyViewModel.Name;
                    _context.Entry(currencyTranslation).State = EntityState.Modified;
                }
                else
                {
                    var currencyTran = new CurrencyTranslation()
                    {
                        Name = currencyViewModel.Name,
                        LanguageId = currencyViewModel.LanguageId,
                        CurrencyId = currencyViewModel.Id
                    };
                    _context.CurrencyTranslations.Add(currencyTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteCurrency(Currency currency)
        {
            currency.Status = (int)GeneralEnums.StatusEnum.Deleted;
            currency.DeletedOn = DateTime.Now;
            _context.Entry(currency).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public decimal GetValue(decimal CoursePrice)
        {
            var cooke = _cookieService.GetCookie(CookieNames.SelectedCurrencyId);
            var currency = GetPrimaryCurrency();

            if (cooke != null)
                currency = GetCurrencyById(Int32.Parse(cooke));

            var exchangeCurrency = GetExchangeCurrency();
            var primaryCurrency = GetPrimaryCurrency();

            return decimal.Round(((CoursePrice / (decimal)(primaryCurrency.Value / exchangeCurrency.Value)) * (decimal)(currency.Value / exchangeCurrency.Value)), 2);
        }
    }
}
