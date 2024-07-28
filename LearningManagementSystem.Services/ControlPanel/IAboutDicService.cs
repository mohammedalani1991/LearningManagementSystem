using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IAboutDicService
    {
        IPagedList<AboutDic> GetAboutDic(string searchText, int? page);
        AboutDic GetAboutDicById(int id);
        AboutDicViewModel GetAboutDicById(int id, int languageId);
        AboutDicViewModel GetAboutDicByCode(string name, int languageId);

        Task<AboutDicViewModel> GetAboutDicByCodeForHomePage(string name, int languageId);

        void AddAboutDic(AboutDicViewModel aboutDics);
        void EditAboutDic(AboutDicViewModel aboutDicViewModel, AboutDic aboutDic);
        void DeleteAboutDic(AboutDic aboutDic);
    }
}
