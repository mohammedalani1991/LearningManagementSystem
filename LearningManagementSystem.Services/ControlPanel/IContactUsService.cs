using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IContactUsService
    {
        Task<IPagedList<ContactU>> GetContacts(string searchText, int? page);
        Task<int> AddContactUs(ContactUsViewModel contactUsViewModel);
    }
}
