using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISettingService _settingService;
        public EmployeeService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IPagedList<Contact> GetEmployees(string searchText, int? page, int languageId,int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contacts = db.Contacts.Include(c => c.ContactTranslations).Include(c => c.UserProfiles).Where(r => r.Employees.Any() &&
                                                                                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);


                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    contacts = contacts.Where(r =>
                //        (r.FullName.Contains(searchText)));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        contacts = contacts.Where(r => r.FullName.Contains(searchText));
                    }
                    else
                    {
                        contacts = contacts.Where(r => r.ContactTranslations.Any(t => t.FullName.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var result = contacts;

                int pageSize = pagination;
                var pageNumber = (page ?? 1);

                var output = result.Include(c=>c.UserProfiles).OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.FullName = trans.FullName;
                        }
                    }
                }
               
                return output;
            }
        }

        public void AddEmployee(EmployeeViewModel employeeViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var employee = new Employee()
                {
                    CreatedOn = DateTime.Now,
                    Status = employeeViewModel.Status.Value,
                    CreatedBy = employeeViewModel.CreatedBy,
                    Address = employeeViewModel.Address,
                    ContactId = employeeViewModel.ContactId.Value,
                    JobId = employeeViewModel.JobId,
                    JobTypeId = employeeViewModel.JobTypeId,
                    StartWorkDate = employeeViewModel.StartWorkDate.Value,
                };

                db.Employees.Add(employee);
                db.SaveChanges();

                if (employeeViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var employeeTran = new EmployeeTranslation()
                    {
                        LanguageId = employeeViewModel.LanguageId,
                        Address = employeeViewModel.Address,
                        EmployeeId = employee.Id
                    };

                    db.EmployeeTranslations.Add(employeeTran);
                    db.SaveChanges();
                }
            }
        }

        public EmployeeViewModel GetEmployeeViewModelById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var employeeTran =
                        db.EmployeeTranslations.Include(r => r.Employee).FirstOrDefault(r => r.LanguageId == languageId && r.EmployeeId == id);
                    if (employeeTran != null)
                    {
                        return new EmployeeViewModel(employeeTran);
                    }
                }

                var employee = db.Employees.Find(id);

                return new EmployeeViewModel(employee);
            }
        }

        public void EditEmployee(EmployeeViewModel employeeViewModel, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                Employee employee = db.Employees.First(e => e.Id == employeeViewModel.Id);

                employee.Status = employeeViewModel.Status.Value;
                employee.JobId = employeeViewModel.JobId;
                employee.JobTypeId = employeeViewModel.JobTypeId;
                employee.StartWorkDate = employeeViewModel.StartWorkDate.Value;

                if (employeeViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    employee.Address = employeeViewModel.Address;
                }

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                if (employeeViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var employeeTranslation = db.EmployeeTranslations.FirstOrDefault(r =>
                        r.LanguageId == employeeViewModel.LanguageId &&
                        r.EmployeeId == employeeViewModel.Id);

                    if (employeeTranslation != null)
                    {
                        employeeTranslation.Address = employeeViewModel.Address;
                        db.Entry(employeeTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var employeesTranslation = new EmployeeTranslation()
                        {
                            Address = employeeViewModel.Address,
                            LanguageId = employeeViewModel.LanguageId,
                            EmployeeId = employee.Id
                        };

                        db.EmployeeTranslations.Add(employeesTranslation);
                    }

                    db.SaveChanges();
                }
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            using (var db = new LearningManagementSystemContext())
            {
                employee.Status = (int)GeneralEnums.StatusEnum.Deleted;
                employee.DeletedOn = DateTime.Now;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
