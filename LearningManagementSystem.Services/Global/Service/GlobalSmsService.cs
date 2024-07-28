using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Global.IService;

namespace LearningManagementSystem.Services.Global.Service
{
    public class GlobalSmsService : IGlobalSmsService
    {
        private readonly LearningManagementSystemContext _context;

        public GlobalSmsService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public Contact GetContactById(int id)
        {
            return _context.Contacts.Find(id);
        }

        public Student GetStudentFromContactId(int id)
        {
            return _context.Students.FirstOrDefault(r=>r.ContactId ==id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
        }

        public Message SendSms(SmsViewModel smsViewModel)
        {
            var message = new Message()
            {
                CreatedOn = DateTime.Now,
                CreatedBy = smsViewModel.CreatedBy,
                ExtraMobile = smsViewModel.ExtraMobile,
                IsExtraMobile = smsViewModel.IsExtraMobile,
                Message1= smsViewModel.Message,
                Mobile= smsViewModel.Mobile,
                Source= smsViewModel.Source,
                Status= smsViewModel.Status,
                ToId= smsViewModel.ToId,
                TypeId= smsViewModel.TypeId,
            };
            _context.Messages.Add(message);
            _context.SaveChanges();

            return message;
        }

        public void SetSend(Message message)
        {
            message.Status = (int)GeneralEnums.SmsStatusEnum.Send;
            _context.Entry(message).State=EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
