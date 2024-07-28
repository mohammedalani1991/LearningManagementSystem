using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeViewModel employee);
        EmployeeViewModel GetEmployeeViewModelById(int value, int languageId);
        IPagedList<Contact> GetEmployees(string searchText, int? page, int languageId, int pagination);
        void EditEmployee(EmployeeViewModel employeeViewModel, int languageId);
        public void DeleteEmployee(Employee employee);
    }
}
