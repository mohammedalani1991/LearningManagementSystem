using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Core.Extensions
{
    public static class DateExtensions
    {
        public static string ConvertDateToString(this DateTime? dateTime)
        {
            return dateTime != null ? $"{dateTime.Value.Date.Year}-{dateTime.Value.Date.Month:D2}-{dateTime.Value.Date.Day:D2}" : "";
        }
        public static string ConvertDateToString(this DateTime dateTime)
        {
            return dateTime != null ? $"{dateTime.Date.Year}-{dateTime.Date.Month:D2}-{dateTime.Date.Day:D2}" : "";
        }


        public static string ConvertTimeSpanToString(this TimeSpan? timeSpan)
        {
            if (timeSpan==null) return "";

            DateTime time = DateTime.Today.Add(timeSpan.Value);
            return  $"{time.ToString("hh:mm tt")}";
        }
    }
}
