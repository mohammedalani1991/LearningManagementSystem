using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICookieService
    {
        public string GetCookie(string key);

        public string CreateCookie(string key, string value , double days);
        public string CreateCookie(string key, List<string> value,int days);
        public void RemoveCookie(string key);
        
    }
}
