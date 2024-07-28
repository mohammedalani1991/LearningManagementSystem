using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEmailsService
    {
        void InitMail();
        Task SendMail(string fromAddress, List<string> toAddress, string subject, string message, string attachmentName = "");
        Task SendMail(string fromAddress, string toAddress, string subject, string message, long groupId, string accountId);
        bool SendSmtpEmail(string fromAddress, dynamic mailMessage);        
        void AddEmailToCommunicationLog(int typeId, string logText, IPrincipal user = null);
        Task SendFileAsEmail(List<string> toAddress, string filePath, string subject, string emailBody);
    }
}
