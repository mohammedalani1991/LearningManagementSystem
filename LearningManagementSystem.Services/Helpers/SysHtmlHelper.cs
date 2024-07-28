using System;
using System.Text.RegularExpressions;

namespace LearningManagementSystem.Services.Helpers
{
    public static class SysHtmlHelper
    {
        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
