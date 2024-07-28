using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsCateryService
    {
        IPagedList<CmsCatery> GetCmsCaterys(string searchText, int? page, int languageId, int pagination);
        List<CmsCatery> GetAllCmsCaterys( int languageId);
        
        CmsCatery GetCmsCateryById(int id);
        CmsCateryViewModel GetCmsCateryById(int id, int languageId);

        void AddCmsCatery(CmsCateryViewModel cmscaterys);
        void EditCmsCatery(CmsCateryViewModel CmsCateryViewModel, CmsCatery CmsCatery);
        void DeleteCmsCatery(CmsCatery CmsCatery);
    }
}
