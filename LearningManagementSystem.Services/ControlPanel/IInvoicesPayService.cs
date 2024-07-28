using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IInvoicesPayService
    {
        Task<IPagedList<InvoicesPay>> GetInvoicesPay( string searchText ,int? page, int languageId, int? CourseId, int? ProcessStatusID);
        Task<int> AddInvoicesPay(InvoicesPayViewModel InvoicesPayViewModel, bool accepted = false);
        Task<InvoicesPayViewModel> GetInvoicesPayById(int id);
        Task<InvoicesPay> ChangeInvoicesPay(InvoicesPayViewModel InvoicesPayViewModel, LearningManagementSystemContext db);
    }
}
