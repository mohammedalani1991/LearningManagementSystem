using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace DataEntity.Models.ViewModels
{
    public class SenangPayReportsViewModel
    {
        public decimal Amount { get; set; }     
        public IPagedList<SenangPayViewModel> SenangPayViewModels { get; set; }     
        public IPagedList<SenangPay> SenangPay { get; set; }     
    }
}
