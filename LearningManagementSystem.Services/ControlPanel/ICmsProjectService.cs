using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsProjectService
    {
        IPagedList<CmsProject> GetCmsProjects(string searchText, int? page, int languageId, int pagination);
        Task<IPagedList<CmsProject>> GetActiveProjects(string searchText, int? page, int languageId, int pagination);
        List<CmsProject> GetProjectForHome(int languageId);

        CmsProject GetCmsProjectById(int id);
        CmsProjectViewModel GetCmsProjectById(int id, int languageId);
        List<CmsProjectCost> GetCmsProjectCosts(int id, int languageId);

        void AddCmsProject(CmsProjectViewModel cmsProject);
        void EditCmsProject(CmsProjectViewModel EquipmentViewModel, CmsProject cmsProject);
        void DeleteCmsProject(CmsProject cmsProject);

        List<CmsProject> GetCmsProjectsList(int languageId);
        void EditResources(CmsProject project, CmsProjectResourcesViewModel viewModel);
        void EditProjectCosts(CmsProjectCostsViewModel costs);

        void AddProjectDonars(DonationViewModel donation);
    }
}
