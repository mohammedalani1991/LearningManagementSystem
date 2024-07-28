using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsWhatPeopleSayService
    {
        IPagedList<CmsWhatPeopleSay> GetCmsWhatPeopleSays(string searchText, int? page, int languageId, int pagination);
        Task<List<CmsWhatPeopleSay>> GetAllCmsWhatPeopleSays(bool showInHome, int languageId);

        CmsWhatPeopleSay GetCmsWhatPeopleSayById(int id);
        CmsWhatPeopleSayViewModel GetCmsWhatPeopleSayById(int id, int languageId);

        void AddCmsWhatPeopleSay(CmsWhatPeopleSayViewModel cmsWhatPeopleSay);
        void EditCmsWhatPeopleSay(CmsWhatPeopleSayViewModel cmsWhatPeopleSayViewModel, CmsWhatPeopleSay cmsWhatPeopleSay);
        void DeleteCmsWhatPeopleSay(CmsWhatPeopleSay cmsWhatPeopleSay);
    }
}
