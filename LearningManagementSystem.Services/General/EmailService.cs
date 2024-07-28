using LearningManagementSystem.Core;
using MailKit.Net.Smtp;
using MimeKit;

namespace LearningManagementSystem.Services.General
{
    public class EmailService : IEmailService
    {
        private readonly ISettingService _settingService;

        public EmailService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public void SendEmailConformation(string to, string confirmationLink)
        {
            var mm = new MimeMessage();
            mm.From.Add(new MailboxAddress("From", _settingService.GetOrCreate(Constants.SystemSettings.SendingEmail, "lms-test@gmail.com").Value));
            mm.To.Add(new MailboxAddress("To", bool.Parse(_settingService.GetOrCreate(Constants.SystemSettings.TestReplyEmail, "true").Value) == true ?
                             _settingService.GetOrCreate(Constants.SystemSettings.EmailsReplyTo, "lms-test@gmail.com").Value : to ));
            mm.Subject = "Email Conformation";
            mm.Body = new TextPart("html")
            {
                Text = "<div><P>Please confirm your email by clicking on the link below<p><br/><a href=" + confirmationLink + ">Click Here</a></div>"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(_settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpClient, "lms-test@gmail.com").Value, _settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpPassword, "Qwer1234/").Value);
                client.Send(mm);
                client.Disconnect(true);
            }
        }

        public void SendContactUsEmail(string From,string Subject,string Message,string Name)
        {
            
            var mm = new MimeMessage();
            mm.From.Add(new MailboxAddress("From", From));
            mm.To.Add(new MailboxAddress("To", _settingService.GetOrCreate(Constants.SystemSettings.ContactUs_Email, "true").Value));
            mm.Subject = Subject + " "+ Name;
            mm.Body = new TextPart("html")
            {
                Text = Message
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(_settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpClient, "lms-test@gmail.com").Value, _settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpPassword, "Qwer1234/").Value);
                client.Send(mm);
                client.Disconnect(true);
            }
        }
    }
}
