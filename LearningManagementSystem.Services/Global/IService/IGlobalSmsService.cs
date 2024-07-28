using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.Global.IService
{
    public interface IGlobalSmsService
    {
        Contact GetContactById(int id);
        Student GetStudentFromContactId(int id);
        Message SendSms(SmsViewModel smsViewModel);
        void SetSend(Message message);
    }
}
