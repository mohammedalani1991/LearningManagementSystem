using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.Controllers
{
  public  interface ISenangPayService
    {
        IPagedList<SenangPay> GetSenangPayes(string searchText, int? page, int languageId, int pagination);
        SenangPay GetSenangPayById(int id);
        SenangPayViewModel GetSenangPayById(int id, int languageId);
        SenangPay AddSenangPay(SenangPayViewModel SenangPay);
        void EditSenangPay(SenangPayViewModel SenangPayViewModel, SenangPay SenangPay);
        void DeleteSenangPay(SenangPay SenangPay);
        string GetMd5Hash(string input);
        string HMACSHA256Encode(string data, string AUTH_TOKEN);
        SenangPayViewModel GetSenangPayByEmail(string ApplicationUserId);
        Task<string> GetUrl(SenangPayViewModel senangPayViewModel);
    }
}
