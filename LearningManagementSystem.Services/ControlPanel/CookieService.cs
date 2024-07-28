using Microsoft.AspNetCore.Http;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CookieService : ICookieService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public string GetCookie(string key)
        {
            try
            {
                return _httpContextAccessor.HttpContext.Request.Cookies[key];
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While Get Cookie");
                return null;
            }

        }

        public string CreateCookie(string key, string value, double days )
        {
            try
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(days);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
                return value;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While Creating Cookie");
                return null;
            }

        }

        public string CreateCookie(string key, List<string> value, int days)
        {
            try
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(days);
                string dataAsString = value.Aggregate((a, b) => a = a + "," + b);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, dataAsString, option);
                return dataAsString;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While Creating Cookie");
                return null;
            }

        }

        public void RemoveCookie(string key)
        {
            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While Removeing Cookie");
            }

        }

    }
}
