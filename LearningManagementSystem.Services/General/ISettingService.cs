using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.General
{
    public interface ISettingService
    {
        SettingViewModel GetOrCreate(string name, string defaultValue, int languageId = (int) GeneralEnums.LanguageEnum.English);
        Task<List<SettingViewModel>> GetMultipleSystemSettings(string[] name, int languageId = (int)GeneralEnums.LanguageEnum.English);
        bool SetSettingValue(string name, string value);
    }
}
