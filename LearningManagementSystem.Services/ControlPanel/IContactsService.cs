using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
  public  interface IContactsService
    {
        Contact AddContacts(ContactViewModel contacts, List<int> contactType);
        ContactViewModel GetContact(int value, int languageId);
        Task EditContact(ContactViewModel contactViewModel, Contact contact, List<int> contactType, int languageId);
        Contact GetContactById(int id);
        Contact GetContactByEmail(string email);
        void DeleteContact(Contact contact);
        bool ConfirmEmail(string username);
        string CheckContactRepetition(ModelStateDictionary modelState, string mobile, int id, string idNumber);
        public IPagedList<Contact> GetContacts(FilterViewModel filter, int? page, int languageId, int pagination, int? contactType, int? companyId, int? employeeId,int? verifyEmail, bool? lead = false);
    }
}
