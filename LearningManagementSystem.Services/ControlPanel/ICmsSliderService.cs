using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsSliderService
    {
        IPagedList<CmsSlider> GetCmsSliders(string searchText, int? page, int languageId, int pagination);
        Task<List<CmsSlider>> GetPublicSliders(int languageId);
        CmsSlider GetCmsSliderById(int id);
        CmsSliderViewModel GetCmsSliderById(int id, int languageId);

        void AddCmsSlider(CmsSliderViewModel CmsSlider);
        void EditCmsSlider(CmsSliderViewModel calendarViewModel, CmsSlider CmsSlider);
        void DeleteCmsSlider(CmsSlider CmsSlider);
    }
}
