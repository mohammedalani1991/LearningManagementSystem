using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class ListItem {
        public string Text{ get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }


    public interface ISystemGroupService
    {
        IPagedList<SystemGroup> GetSystemGroups(string searchText, int? page, int languageId, int pagination);
        SystemGroup GetSystemGroupById(int id);
        List<ListItem> GetSystemGroupUsers(int systemGroupId);
        SystemGroupViewModel GetSystemGroupWithLanguageById(int id);
        int GetSystemRole(string userName);
        void AddSystemGroup(SystemGroupViewModel model);
        void EditSystemGroup(SystemGroupViewModel model, SystemGroup systemGroup);
        void DeleteSystemGroupById(int id);
        int UsersNumber(int licenseId);
        SystemGroupViewModel GetSystemGroupWithLanguageById(int value, int languageId);
    }
}

   