using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsCategoryProjectService
    {
        IPagedList<CmsProjectCategory> GetCmsCategoryProjects(string searchText, int? page, int languageId, int pagination);
        CmsProjectCategory GetCmsCategoryProjectById(int id);
        CmsCategoryProjectViewModel GetCmsCategoryProjectById(int id, int languageId);

        void AddCmsCategoryProject(CmsCategoryProjectViewModel cmsProjectCategoryViewModel);
        void EditCmsCategoryProject(CmsCategoryProjectViewModel cmsProjectCategoryViewModel, CmsProjectCategory cmsProjectCategory);
        void DeleteCmsCategoryProject(CmsProjectCategory cmsProjectCategory);
    }
}
