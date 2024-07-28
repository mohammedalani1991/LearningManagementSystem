using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISubscribersService
    {
        Task<IPagedList<Subscriber>> GetSubscribers(string searchText, int? page);
        Task<int> AddSubscribers(string Email);
    }
}
