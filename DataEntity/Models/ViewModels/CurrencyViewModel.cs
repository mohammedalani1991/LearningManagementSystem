using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class CurrencyViewModel
    {
        public CurrencyViewModel()
        {

        }
        public CurrencyViewModel(Currency currency)
        {
            CreatedOn = currency.CreatedOn;
            CreatedBy = currency.CreatedBy;
            Value = currency.Value;
            Name = currency.Name;
            Icon = currency.Icon;
            Status = currency.Status;
            IsExchange = currency.IsExchange??false;
            IsPrimary = currency.IsPrimary ?? false;
            Code= currency.Code;
            Id= currency.Id;
            SortOrder= currency.SortOrder;
        }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public double Value { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsExchange { get; set; }
        public int LanguageId { get; set; }
        public int? SortOrder { get; set; }

    }
}
