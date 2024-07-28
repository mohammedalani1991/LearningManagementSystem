using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IExpulsionService
    {
        Expulsion GetExpulsionById(int id);
        IPagedList<Expulsion> GetExpulsions(int? page, int pagination);
        void AddExpulsion(ExpulsionViewModel expulsionViewModel);
        void EditExpulsion(ExpulsionViewModel expulsionViewModel, Expulsion expulsion);
        void DeleteExpulsion(Expulsion expulsion);

        IPagedList<EnrollStudentCourse> GetExpelledStudents(DateTime? startDate, DateTime? endDate, int? page ,int languageId);
    }
}
