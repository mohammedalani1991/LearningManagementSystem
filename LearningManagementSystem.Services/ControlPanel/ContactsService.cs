using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using static LearningManagementSystem.Core.Constants;


namespace LearningManagementSystem.Services.ControlPanel
{
    public class ContactsService : IContactsService
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserProfileService _userProfileService;
        private readonly LearningManagementSystemContext _context;

        public ContactsService(UserManager<IdentityUser> userManager, IUserProfileService userProfileService, LearningManagementSystemContext context)
        {
            _userManager = userManager;
            _userProfileService = userProfileService;
            _context = context;
        }

        public Contact AddContacts(ContactViewModel contactViewModel, List<int> contactType)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contact = new Contact()
                {
                    CreatedOn = DateTime.Now,
                    Status = contactViewModel.Status,
                    FullName = contactViewModel.FirstName + " " + contactViewModel.SecondName + " " + contactViewModel.ThirdName + " " + contactViewModel.LastName,
                    Mobile = contactViewModel.Mobile,
                    GenderId = contactViewModel.GenderId,
                    Email = contactViewModel.Email,
                    FirstName = contactViewModel.FirstName,
                    SecondName = contactViewModel.SecondName,
                    ThirdName = contactViewModel.ThirdName,
                    LastName = contactViewModel.LastName,
                    IdNumber = contactViewModel.IdNumber,
                    CountryId = contactViewModel.CountryId,
                    CityId = contactViewModel.CityId,
                    IsEmailVerified = false,
                    PhoneNumberCode = contactViewModel.PhoneNumberCode,
                };

                db.Contacts.Add(contact);
                db.SaveChanges();

                foreach (var item in contactType)
                {
                    contact.ContactTypes.Add(new ContactType { TypeId = item });
                }

                db.SaveChanges();

                if (contactViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var contactTran = new ContactTranslation()
                    {
                        LanguageId = contactViewModel.LanguageId,
                        FullName = contactViewModel.FirstName + " " + contactViewModel.SecondName + " " + contactViewModel.ThirdName + " " + contactViewModel.LastName,
                        LastName = contactViewModel.LastName,
                        ThirdName = contactViewModel.ThirdName,
                        SecondName = contactViewModel.SecondName,
                        FirstName = contactViewModel.FirstName,
                        ContactId = contact.Id
                    };

                    db.ContactTranslations.Add(contactTran);
                    db.SaveChanges();
                }

                return contact;
            }
        }

        public void DeleteContact(Contact contact)
        {
            using (var db = new LearningManagementSystemContext())
            {
                contact.Status = (int)GeneralEnums.StatusEnum.Deleted;
                contact.DeletedOn = DateTime.Now;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public async Task EditContact(ContactViewModel contactViewModel, Contact contact, List<int> contactType, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (contact.Email != contactViewModel.Email)
                {
                    var user = await _userManager.FindByNameAsync(contact.Email);
                    if (user != null)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateChangeEmailTokenAsync(user, contactViewModel.Email);
                        await _userManager.ChangeEmailAsync(user, contactViewModel.Email, code);
                        await _userManager.SetUserNameAsync(user, contactViewModel.Email);
                        _userProfileService.ChangeEmail(contact.Email, contactViewModel.Email);
                    }
                }

                contact.Status = contactViewModel.Status;
                contact.Mobile = contactViewModel.Mobile;
                contact.GenderId = contactViewModel.GenderId;
                contact.Email = contactViewModel.Email;
                contact.IdNumber = contactViewModel.IdNumber;
                contact.CountryId = contactViewModel.CountryId;
                contact.CityId = contactViewModel.CityId;
                contact.PhoneNumberCode = contactViewModel.PhoneNumberCode;
                if (contactViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    contact.FullName = contactViewModel.FirstName + " " + contactViewModel.SecondName + " " + contactViewModel.ThirdName + " " + contactViewModel.LastName;
                    contact.FirstName = contactViewModel.FirstName;
                    contact.SecondName = contactViewModel.SecondName;
                    contact.ThirdName = contactViewModel.ThirdName;
                    contact.LastName = contactViewModel.LastName;

                }

                db.ContactTypes.RemoveRange(contact.ContactTypes);

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var item in contactType)
                {
                    db.ContactTypes.Add(new ContactType { TypeId = item, ContactId = contact.Id });
                }

                db.SaveChanges();

                if (contactViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var contactTranslation = db.ContactTranslations.FirstOrDefault(r =>
                        r.LanguageId == contactViewModel.LanguageId &&
                        r.ContactId == contact.Id);

                    if (contactTranslation != null)
                    {
                        contactTranslation.FullName = contactViewModel.FirstName + " " + contactViewModel.SecondName + " " + contactViewModel.ThirdName + " " + contactViewModel.LastName;
                        contactTranslation.FirstName = contactViewModel.FirstName;
                        contactTranslation.SecondName = contactViewModel.SecondName;
                        contactTranslation.ThirdName = contactViewModel.ThirdName;
                        contactTranslation.LastName = contactViewModel.LastName;

                        db.Entry(contactTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var contactsTranslation = new ContactTranslation()
                        {
                            FullName = contactViewModel.FirstName + " " + contactViewModel.SecondName + " " + contactViewModel.ThirdName + " " + contactViewModel.LastName,
                            FirstName = contactViewModel.FirstName,
                            SecondName = contactViewModel.SecondName,
                            ThirdName = contactViewModel.ThirdName,
                            LastName = contactViewModel.LastName,
                            LanguageId = contactViewModel.LanguageId,
                            ContactId = contact.Id
                        };

                        db.ContactTranslations.Add(contactsTranslation);
                    }

                    db.SaveChanges();
                }
            }
        }

        public ContactViewModel GetContact(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var contactTran =
                        db.ContactTranslations.Include(r => r.Contact).FirstOrDefault(r => r.LanguageId == languageId && r.ContactId == id);
                    if (contactTran != null)
                    {
                        return new ContactViewModel(contactTran);
                    }
                }

                var contact = db.Contacts.Find(id);

                return new ContactViewModel(contact);
            }
        }

        public Contact GetContactById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.Contacts.Include(c => c.Students).Include(c => c.ContactTypes)
                    .Include(c => c.Trainers).Include(c => c.Employees).Include(c => c.UserProfiles).FirstOrDefault(c => c.Id == id);
            }
        }

        public Contact GetContactByEmail(string email)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.Contacts.FirstOrDefault(r => r.Email == email && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            }
        }

        public string CheckContactRepetition(ModelStateDictionary modelState, string mobile, int id, string idNumber)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (db.Contacts.Where(c => c.Mobile.Equals(mobile) && c.Id != id).Any())
                    return $"The contact who owns the phone({mobile}) already exists";
                else if (db.Contacts.Where(c => c.IdNumber.Equals(idNumber) && c.Id != id).Any() && idNumber != null)
                    return $"The contact who owns the id Number({idNumber}) already exists";
                return null;
            }
        }

        public IPagedList<Contact> GetContacts(FilterViewModel filter, int? page, int languageId, int pagination, int? contactType, int? companyId, int? employeeId, int? verifyEmail, bool? lead = false)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contacts = db.Contacts.Include(c => c.ContactTranslations).Include(c => c.UserProfiles).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                var search = filter.SearchText?.Replace(" ", "");

                if (!string.IsNullOrWhiteSpace(search))
                    contacts = contacts.Where(r => (r.FullName.Replace(" ", "").Contains(search) || r.Mobile.Contains(search)
                    || r.Email.Contains(search) || r.ContactTranslations.FirstOrDefault().FullName.Replace(" ", "").Contains(search)));

                if (contactType != null && contactType > 0)
                {
                    contacts = contacts.Where(r => (r.ContactTypes.Select(r => r.TypeId).Contains(contactType.Value)));
                }

                if (filter.Job > 0)
                {
                    var ids = db.Employees.Where(a => a.JobId == filter.Job).Select(a => a.ContactId).ToList();
                    contacts = contacts.Where(a => ids.Contains(a.Id));
                }


                if (filter.JobType > 0)
                {
                    var stids = db.Employees.Where(a => a.JobTypeId == filter.JobType).Select(a => a.ContactId).ToList();
                    contacts = contacts.Where(a => stids.Contains(a.Id));
                }

                if (filter.ContactType > 0)
                {
                    var contTypes = db.ContactTypes.Where(a => a.TypeId == filter.ContactType).Select(a => a.ContactId).ToList();
                    contacts = contacts.Where(a => contTypes.Contains(a.Id));
                }
                if (verifyEmail != null)
                {
                    var isEmailVerified = verifyEmail == 1;
                    contacts = contacts.Where(a => a.IsEmailVerified == isEmailVerified);
                }

                var result = contacts;

                int pageSize = pagination;
                var pageNumber = (page ?? 1);

                var output = result.Include(c => c.UserProfiles).OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

                //foreach (var item in output)
                //{
                //    item.IsEmailVerified = false;
                //    if (item.UserProfiles.Any())
                //    {
                //        var aspUser = _userManager.Users.FirstOrDefault(r => r.UserName == item.UserProfiles.First().Username);
                //        if (aspUser != null)
                //        {
                //            item.IsEmailVerified = _userManager.IsEmailConfirmedAsync(aspUser).Result;
                //        }
                //    }
                //}

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

        public bool ConfirmEmail(string username)
        {
            var contact = _context.Contacts.FirstOrDefault(r => r.Email == username && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (contact != null)
            {
                contact.IsEmailVerified = true;
                _context.Entry(contact).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

