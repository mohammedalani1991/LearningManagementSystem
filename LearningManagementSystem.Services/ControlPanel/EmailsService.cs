using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Services.Helpers;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Core;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EmailsService : IEmailsService
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmailsService(ISettingService settingService, IHttpContextAccessor httpContextAccessor)
        {
            _settingService = settingService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public string FromAddress { get; private set; }
        public string StrToAddress { get; private set; }
        private string _strSmtpClient;
        private string _password;
        public string ReplyTo { get; private set; }
        private string _smtpPort;
        private bool _bEnableSsl;

        public void InitMail()
        {
            FromAddress = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSourceEmail, "Zourd98@hotmail.com").Value;
            StrToAddress = _settingService.GetOrCreate(Constants.SystemSettings.EmailsErrorEmail, "Zourd98@hotmail.com").Value;
            _strSmtpClient = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpClient, "smtp.office365.com").Value;
            _password = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpPassword, "Saber12345$").Value;
            ReplyTo = _settingService.GetOrCreate(Constants.SystemSettings.EmailsReplyTo, "Zourd98@hotmail.com").Value;
            _smtpPort = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSmtpPort, "587").Value;
            _bEnableSsl = _settingService.GetOrCreate(Constants.SystemSettings.EmailsEnableSsl, "true").Value.ToLower() == "true";
        }

        public async Task SendMail(string fromAddress, List<string> toAddress, string subject, string message, string attachmentName = "")
        {

            InitMail();
            dynamic mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddress);
            foreach (var email in toAddress)
            {
                mailMessage.To.Add(email);
            }
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            if (attachmentName != null && attachmentName.Length > 0)
            {
                mailMessage.Attachments.Add(new Attachment(attachmentName));
            }

            mailMessage.Body = message;

            mailMessage.ReplyTo = new MailAddress(fromAddress);

            
            Task.Factory.StartNew(() => SendSmtpEmail(fromAddress, mailMessage));
        }

        public async Task SendMail(string fromAddress, string toAddress, string subject, string message, long groupId, string accountId)
        {

            InitMail();


            dynamic mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddress);
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            mailMessage.Headers.Add("X-GroupId", groupId.ToString());
            mailMessage.Headers.Add("X-AccountId", accountId);


            mailMessage.ReplyTo = new MailAddress(fromAddress);

            
            Task.Factory.StartNew(() => SendSmtpEmail(fromAddress, mailMessage));


        }


        public bool SendSmtpEmail(string fromAddress, dynamic mailMessage)
        {
            var smtpClient = new SmtpClient();
            try
            {

                smtpClient.Port = Convert.ToInt32(_smtpPort);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromAddress, _password);
                smtpClient.Host = _strSmtpClient;
                smtpClient.EnableSsl = _bEnableSsl;


                smtpClient.Send(mailMessage);


               

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error while sending email to {string.Join(",", mailMessage.To)}");
                return false;

            }
            finally
            {
                smtpClient.Dispose();
            }
            return true;
        }

        void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                LogHelper.LogException("", null, $"Error while sending email -{ e.Error.ToString()}");
            }
            if (e.Error != null)
            {
                LogHelper.LogException("", null, $"Error while sending email -{ e.Error.ToString()} ");
            }
            else
            {
            }
        }

        public void AddEmailToCommunicationLog(int typeId, string logText, IPrincipal user = null)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var username = user != null ? user.Identity.Name : Constants.Users.System;
                try
                {
                    var data = new CommunicationLog()
                    {
                        TypeId = typeId,
                        LogText = logText,
                        //CreatedBy = username,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                    };
                    db.CommunicationLogs.Add(data);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(username, ex, $"Error while saving communication log for sms template as email for msg {logText}");
                }
            }
        }

        public async Task SendFileAsEmail(List<string> toAddress, string filePath, string subject, string emailBody)
        {
            InitMail();
            var file = new FileInfo(filePath);
            if (file.Exists)
            {
                await SendMail(FromAddress, toAddress, subject, emailBody, file.FullName);
            }
        }
    }
}