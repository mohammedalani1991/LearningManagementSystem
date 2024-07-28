using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IBalanceHistoryService
    {
        IPagedList<StudentBalanceHistory> GetStudentBalanceHistory(int studentId, int page, int languageId);
        SenangPayReportsViewModel GetStudentPayments(int studentId, int? page, int languageId, int pagination, FilterViewModel filter = null);
        void AddBalanceHistory(StudentBalanceHistory studentBalanceHistory);
    }
}
