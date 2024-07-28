using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IManagementStandardService
    {
        IPagedList<ManagementStandard> GetManagementStandards(string searchText, int? page, int languageId, int pagination);
        ManagementStandard GetManagementStandardById(int id);
        ManagementStandard GetManagementStandardById(int id, int languageId);
        void AddManagementStandard(ManagementStandardViewModel ManagementStandardViewModel);
        void EditManagementStandard(ManagementStandardViewModel ManagementStandardViewModel, ManagementStandard ManagementStandard);
        void DeleteManagementStandard(ManagementStandard ManagementStandard);
    }
}
