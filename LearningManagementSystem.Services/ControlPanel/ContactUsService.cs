using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class ContactUsService : IContactUsService
    {
        private readonly ISettingService _settingService;
        public ContactUsService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IPagedList<ContactU>>  GetContacts(string searchText, int? page)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contactUsList = db.ContactUs.Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    contactUsList = contactUsList.Where(r => r.Email.Contains(searchText) || r.Name.Contains(searchText));

                }

                var result = contactUsList;

                var pageSize = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = (page ?? 1);


                return await result.OrderByDescending(r => r.Id).ToPagedListAsync(pageNumber, pageSize);
            }

        }


        public async Task<int> AddContactUs(ContactUsViewModel contactUsViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var GeContactUS = await db.ContactUs.Where(r=>r.Email == contactUsViewModel.Email && r.Name== contactUsViewModel.Name && r.Subject == contactUsViewModel.Subject && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefaultAsync();
                if(GeContactUS != null) { return -1; }

                var ContactUS = new ContactU()
                {
                    Name = contactUsViewModel.Name,
                    Email = contactUsViewModel.Email,
                    Subject = contactUsViewModel.Subject,
                    Message = contactUsViewModel.Message,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedOn = DateTime.Now,

                };
                await db.ContactUs.AddAsync(ContactUS);
                await db.SaveChangesAsync();

                return ContactUS.Id;
            }
        }


    }
}
