using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IBranchService
    {
        IPagedList<Branch> GetBranches(string searchText, int? page, int languageId, int pagination);
        Branch GetBranchById(int id);
        BranchViewModel GetBranchById(int id, int languageId);
        void AddBranch(BranchViewModel branch);
        void EditBranch(BranchViewModel branchViewModel, Branch branch);
        void DeleteBranch(Branch branch);
    }
}
