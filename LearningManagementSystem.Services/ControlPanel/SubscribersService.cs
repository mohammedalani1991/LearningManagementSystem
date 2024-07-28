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
    public class SubscribersService : ISubscribersService
    {
        private readonly ISettingService _settingService;
        public SubscribersService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IPagedList<Subscriber>>   GetSubscribers(string searchText, int? page)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var SubscribersList = db.Subscribers.Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    SubscribersList = SubscribersList.Where(r => r.Email.Contains(searchText));

                }

                var result = SubscribersList;

                var pageSize = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = (page ?? 1);


                return await result.OrderByDescending(r => r.Id).ToPagedListAsync(pageNumber, pageSize);
            }

        }


        public async Task<int> AddSubscribers(string Email)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var GeSubscribers = await db.Subscribers.Where(r=>r.Email == Email && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefaultAsync();
                if(GeSubscribers != null) { return -1; }

                var subscriber = new Subscriber()
                {
                  
                    Email = Email,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedOn = DateTime.Now,

                };
                await db.Subscribers.AddAsync(subscriber);
                await db.SaveChangesAsync();
                return  subscriber.Id;
            }
        }


    }
}
