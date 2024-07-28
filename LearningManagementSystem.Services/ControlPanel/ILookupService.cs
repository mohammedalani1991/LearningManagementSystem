using System.Collections.Generic;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ILookupService
    { 
        IPagedList<MasterLookup> GetMasterLookups(string searchText, int? page,int languageId, int pagination);
        void AddMasterLookup(MasterLookupViewModel masterLookupViewModel);
        MasterLookup GetMasterLookupById(int id);
        MasterLookupViewModel GetMasterLookupById(int id, int languageId);
        void EditMasterLookup(MasterLookupViewModel masterLookupViewModel, MasterLookup masterLookup);
        void DeleteMasterLookup(MasterLookup master); 
        List<DetailsLookupViewModel> GetDetailsLookups(int masterId, int languageId);
        DetailsLookupViewModel AddDetailsLookup(DetailsLookupViewModel detailsLookupViewModel);
        DetailsLookup GetDetailsLookupById(int id);
        DetailsLookupViewModel GetDetailsLookupById(int id, int languageId);
        void EditDetailLookup(DetailsLookupViewModel detailsLookupViewModel, DetailsLookup detailsLookup);
        void DeleteDetailLookup(DetailsLookup details);
    }
}
