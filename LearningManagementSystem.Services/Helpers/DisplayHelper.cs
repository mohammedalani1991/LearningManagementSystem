using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.Helpers
{
    public class DisplayHelper
    {
        public static string ToMonthName(int month)
        {
            if (month != 0)
            {
                if (month == 13)
                {
                    return "Yearly";
                }
                return new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM");
            }
            return null;
        }
    }
}
