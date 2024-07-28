using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels 
{
  public  class EmployeeViewModel
    {
        public EmployeeViewModel() {
        }

        public EmployeeViewModel(EmployeeTranslation employeeTran , ContactTranslation contactTranslation, UserProfile userProfile)
        {
            Id = employeeTran.EmployeeId;
            FirstName = contactTranslation.FirstName;
            SecondName = contactTranslation.SecondName;
            ThirdName = contactTranslation.ThirdName;
            LastName = contactTranslation.LastName;
            ContactId = employeeTran.Employee.ContactId;
            CreatedOn = employeeTran.Employee.CreatedOn;
            Status = employeeTran.Employee.Status;
            StartWorkDate = employeeTran.Employee.StartWorkDate;
            Address = employeeTran.Address;
            JobId = employeeTran.Employee.JobId;
            JobTypeId = employeeTran.Employee.JobTypeId;
            CreatedBy = employeeTran.Employee.CreatedBy;
            LanguageId = employeeTran.LanguageId;
            Contact=employeeTran.Employee.Contact;
        }

        public EmployeeViewModel(EmployeeTranslation employeeTran)
        {
            Id = employeeTran.EmployeeId;
            ContactId = employeeTran.Employee.ContactId;
            CreatedOn = employeeTran.Employee.CreatedOn;
            Status = employeeTran.Employee.Status;
            StartWorkDate = employeeTran.Employee.StartWorkDate;
            Address = employeeTran.Address;
            JobId = employeeTran.Employee.JobId;
            JobTypeId = employeeTran.Employee.JobTypeId;
            CreatedBy = employeeTran.Employee.CreatedBy;
            LanguageId = employeeTran.LanguageId;
        }

        public EmployeeViewModel(Employee employee , UserProfile userProfile)
        {
            Id = employee.Id;
            FirstName = employee.Contact?.FirstName;
            SecondName = employee.Contact?.SecondName;
            ThirdName = employee.Contact?.ThirdName;
            LastName = employee.Contact?.LastName;
            ContactId = employee.ContactId;
            CreatedOn = employee.CreatedOn;
            Status = employee.Status;
            StartWorkDate = employee.StartWorkDate;
            Address = employee.Address;
            JobId = employee.JobId;
            JobTypeId = employee.JobTypeId;
            CreatedBy = employee.CreatedBy;
            Contact = employee.Contact;
            
          }

        public EmployeeViewModel(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.Contact?.FirstName;
            SecondName = employee.Contact?.SecondName;
            ThirdName = employee.Contact?.ThirdName;
            LastName = employee.Contact?.LastName;
            ContactId = employee.ContactId;
            CreatedOn = employee.CreatedOn;
            Status = employee.Status;
            StartWorkDate = employee.StartWorkDate;
            Address = employee.Address;
            JobId = employee.JobId;
            JobTypeId = employee.JobTypeId;
            CreatedBy = employee.CreatedBy;
            Contact = employee.Contact;
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int IdNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? StartWorkDate { get; set; }
        public string Address { get; set; }
        public int? JobId { get; set; }
        public int? JobTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public Contact Contact { get; set; }
        public List<string> RoleIds { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
