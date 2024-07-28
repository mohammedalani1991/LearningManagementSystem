using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISemesterService
    {
        IPagedList<Semester> GetSemesters(string searchText, int? page, int languageId, int pagination);
        Semester GetSemesterById(int id);
        SemesterViewModel GetSemesterById(int id, int languageId);
        List<SemesterViewModel> GetSemesters(int languageId);

        void AddSemester(SemesterViewModel semesterViewModel);
        void EditSemester(SemesterViewModel semesterViewModel, Semester semester);
        void DeleteSemester(Semester cmsProjectCategory);

        List<Semester> GetSemestersList(int languageId);
        int GetDefaultSemester();
    }
}
