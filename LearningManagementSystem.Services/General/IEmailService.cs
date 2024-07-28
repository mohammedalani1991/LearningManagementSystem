using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.General
{
    public interface IEmailService
    {
        void SendEmailConformation(string to, string confirmationLink);
        void SendContactUsEmail(string From, string Subject, string Message, string Name);
    }
}
