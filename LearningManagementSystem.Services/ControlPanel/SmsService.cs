using System;
using LearningManagementSystem.Services.General;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using RestSharp;
using LearningManagementSystem.Core;
using System.Linq;
using DataEntity.Models.EfModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SmsService : ISmsService
    {
        private readonly ISettingService _settingService;
        private readonly ICommunicationLogService _communicationLogService;
        private readonly IEmailsService _emailService;

        public SmsService(IEmailsService emailService, ISettingService settingService, ICommunicationLogService communicationLogService)
        {
            _settingService = settingService;
            _communicationLogService = communicationLogService;
            _emailService = emailService;
        }

        public bool SendSms(MessageViewModel message)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {

                    if (message.Ids.Count() > 0)
                    {
                        List<CommunicationLogsViewModel> communicationLogs = new List<CommunicationLogsViewModel>();
                        foreach (var id in message.Ids)
                        {
                            var contact = db.Contacts.Where(a => a.Id == id).FirstOrDefault();
                            if (contact != null)
                            {
                                message.MobileNumbers.Add(contact.Mobile);
                                communicationLogs.Add(new CommunicationLogsViewModel()
                                {
                                    CreatedBy = message.CreatedBy,
                                    CreatedOn = DateTime.Now,
                                    ContactId = id,
                                    IsForExtraType = false,
                                    TypeText = contact.Mobile,
                                    LogText = message.Message,
                                    Status = (int)GeneralEnums.StatusEnum.Active,
                                    TypeId = (int)GeneralEnums.CommunicationTypes.Sms
                                });
                            }
                        }

                        _communicationLogService.AddCommunicationLogs(communicationLogs);
                    }
                }

                //string apiId = _settingService.GetOrCreate("Sms_Api_Id", "fced838bf7474f737d8a811122f13f6a")?.Value;
                //string sender = _settingService.GetOrCreate("Sms_Api_SenderName", "Quran Radio")?.Value;
                //string mobiles = string.Join(",", message.MobileNumbers);
                //var url =
                //    $"http://sms.htd.ps/API/SendSMS.aspx?id={apiId}&sender={sender}&to={mobiles}&msg={message.Message}&mode=1";
                //var client = new RestClient { Timeout = -1 };
                //var request = new RestRequest(url, Method.GET);
                //IRestResponse response = client.Execute(request);

                //_communicationLogService.AddCommunicationLog(new CommunicationLogsViewModel()
                //{
                //    CreatedBy = "System",
                //    CreatedOn = DateTime.Now,
                //    ContactId = 0,
                //    ContactType = 0,
                //    IsForExtraType = false,
                //    TypeText = "", 
                //    LogText = string.Format($"Mobile: {mobiles}, Sender: {sender}, Message: {message.Message} and Response: {response.Content}"),
                //    Status = (int) GeneralEnums.StatusEnum.Active,
                //    TypeId = (int)GeneralEnums.CommunicationTypes.Sms
                //});
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("System", ex, "Error While Sending SMS");
                return false;
            }
        }

        //public async Task<bool> SendEmail(MessageViewModel message)
        //{

        //}

        public async Task<bool> SendEmail(MessageViewModel message)
        {
            var IsUseSendGrid = _settingService.GetOrCreate("System_Email_IsUseSendGrid", "0")?.Value;

            if (IsUseSendGrid == "1")
                return await SendEmailBySendGrid(message);
            else
                return await SendEmailNormal(message);
        }

        private async Task<bool> SendEmailNormal(MessageViewModel message)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var adminEmail = _settingService.GetOrCreate(Constants.SystemSettings.Admin_Email, "info@assafa-academy.com")?.Value;
                    if (message.Ids != null && message.Ids.Any())
                    {
                        List<CommunicationLogsViewModel> communicationLogs = new List<CommunicationLogsViewModel>();
                        foreach (var id in message.Ids)
                        {
                            var contact = db.Contacts.Where(a => a.Id == id).FirstOrDefault();
                            if (contact != null)
                            {
                                message.Emails.Add(message.SendToAdmin == true ? adminEmail : contact.Email);
                                communicationLogs.Add(new CommunicationLogsViewModel()
                                {
                                    CreatedBy = message.CreatedBy,
                                    CreatedOn = DateTime.Now,
                                    ContactId = id,
                                    IsForExtraType = false,
                                    TypeText = message.SendToAdmin == true ? adminEmail : contact.Email,
                                    LogText = message.Message,
                                    Status = (int)GeneralEnums.StatusEnum.Active,
                                    TypeId = (int)GeneralEnums.CommunicationTypes.Email
                                });
                            }
                        }
                        _communicationLogService.AddCommunicationLogs(communicationLogs);
                    }
                    var fromEmail = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSourceEmail, "Zourd98@hotmail.com").Value;
                    var messageText = $"" + message.Message + "\n";
                    await _emailService.SendMail(fromEmail, message.Emails, message.Subject, messageText);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("System", ex, "Error While Sending Email");
                return false;
            }
        }

        private async Task<bool> SendEmailBySendGrid(MessageViewModel message)
        {
            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var adminEmail = _settingService.GetOrCreate(Constants.SystemSettings.Admin_Email, "info@assafa-academy.com")?.Value;
                    if (message.Ids != null && message.Ids.Any())
                    {
                        List<CommunicationLogsViewModel> communicationLogs = new List<CommunicationLogsViewModel>();
                        foreach (var id in message.Ids)
                        {
                            var contact = db.Contacts.Where(a => a.Id == id).FirstOrDefault();
                            if (contact != null)
                            {
                                message.Emails.Add(message.SendToAdmin == true ? adminEmail : contact.Email);
                                communicationLogs.Add(new CommunicationLogsViewModel()
                                {
                                    CreatedBy = message.CreatedBy,
                                    CreatedOn = DateTime.Now,
                                    ContactId = id,
                                    IsForExtraType = false,
                                    TypeText = message.SendToAdmin == true ? adminEmail : contact.Email,
                                    LogText = message.Message,
                                    Status = (int)GeneralEnums.StatusEnum.Active,
                                    TypeId = (int)GeneralEnums.CommunicationTypes.Email
                                });
                            }
                        }
                        _communicationLogService.AddCommunicationLogs(communicationLogs);
                    }

                    var apiKey = _settingService.GetOrCreate(Constants.SystemSettings.EmailInfoSendGridApiKey, "")?.Value;
                    var systemEmail = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSourceEmail, "")?.Value;
                    var systemName = _settingService.GetOrCreate(Constants.SystemSettings.EmailsSourceName, "")?.Value;
                    var client = new SendGridClient(apiKey);
                    var from_email = new EmailAddress(systemEmail, systemName);
                    var subject = message.Subject;
                    var to_email = new EmailAddress(message.Emails.FirstOrDefault());
                    var plainTextContent = "";
                    var htmlContent = message.Message;
                    var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("System", ex, "Error While Sending Email");
                return false;
            }
        }
    }
}
