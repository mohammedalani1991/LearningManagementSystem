using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using static LearningManagementSystem.Core.Constants;

namespace LearningManagementSystem.Services.Controllers
{
    public class SenangPayService : ISenangPayService
    {
        private readonly ISettingService _settingService;

        public SenangPayService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public SenangPay AddSenangPay(SenangPayViewModel SenangPayViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var SenangPay = new SenangPay();
                if (SenangPayViewModel.ProjectId > 0 && SenangPayViewModel.Item > 0)
                    SenangPay = new SenangPay()
                    {
                        Status = SenangPayViewModel.Status,
                        Amount = SenangPayViewModel.Amount,
                        ApplicationUserId = SenangPayViewModel.ApplicationUserId,
                        Country = SenangPayViewModel.Country,
                        Details = SenangPayViewModel.Details,
                        Email = SenangPayViewModel.Email,
                        Msg = SenangPayViewModel.Msg,
                        ProcessDate = SenangPayViewModel.ProcessDate,
                        SenangPayPaymentType = SenangPayViewModel.SenangPayPaymentType,
                        TransactionId = SenangPayViewModel.TransactionId,
                        UserName = SenangPayViewModel.UserName,
                        CreatedBy = SenangPayViewModel.CreatedBy,
                        EnrollTeacherCourseId = SenangPayViewModel.EnrollTeacherCourseId,
                        CreatedOn = SenangPayViewModel.CreatedOn,
                        ProjectId = SenangPayViewModel.ProjectId,
                        ProjectCostId = SenangPayViewModel.Item,
                        PhoneNumber =SenangPayViewModel.PhoneNumber,
                    };
                else
                    SenangPay = new SenangPay()
                    {
                        Status = SenangPayViewModel.Status,
                        Amount = SenangPayViewModel.Amount,
                        ApplicationUserId = SenangPayViewModel.ApplicationUserId,
                        Country = SenangPayViewModel.Country,
                        Details = SenangPayViewModel.Details,
                        Email = SenangPayViewModel.Email,
                        Msg = SenangPayViewModel.Msg,
                        ProcessDate = SenangPayViewModel.ProcessDate,
                        SenangPayPaymentType = SenangPayViewModel.SenangPayPaymentType,
                        TransactionId = SenangPayViewModel.TransactionId,
                        UserName = SenangPayViewModel.UserName,
                        CreatedBy = SenangPayViewModel.CreatedBy,
                        EnrollTeacherCourseId = SenangPayViewModel.EnrollTeacherCourseId,
                        CreatedOn = SenangPayViewModel.CreatedOn,
                        CustomerCurrencyCode = SenangPayViewModel.CustomerCurrencyCode,
                        CurrencyRate = SenangPayViewModel.CurrencyRate,
                        PhoneNumber =SenangPayViewModel.PhoneNumber
                    };
                db.SenangPays.Add(SenangPay);
                db.SaveChanges();
                return SenangPay;
            }
        }

        public void DeleteSenangPay(SenangPay SenangPay)
        {
            using (var db = new LearningManagementSystemContext())
            {
                SenangPay.Status = (int)GeneralEnums.StatusEnum.Deleted;
                SenangPay.DeletedOn = DateTime.Now;
                db.Entry(SenangPay).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public void EditSenangPay(SenangPayViewModel SenangPayViewModel, SenangPay SenangPay)
        {
            using (var db = new LearningManagementSystemContext())
            {
                SenangPay.Amount = SenangPayViewModel.Amount;
                SenangPay.EnrollTeacherCourseId = SenangPayViewModel.EnrollTeacherCourseId;
                SenangPay.ApplicationUserId = SenangPayViewModel.ApplicationUserId;
                SenangPay.Country = SenangPayViewModel.Country;
                SenangPay.Details = SenangPayViewModel.Details;
                SenangPay.Email = SenangPayViewModel.Email;
                SenangPay.Msg = SenangPayViewModel.Msg;
                SenangPay.ProcessDate = SenangPayViewModel.ProcessDate;
                SenangPay.SenangPayPaymentType = SenangPayViewModel.SenangPayPaymentType;
                SenangPay.TransactionId = SenangPayViewModel.TransactionId;
                SenangPay.UserName = SenangPayViewModel.UserName;
                SenangPay.Status = SenangPayViewModel.Status;
                db.Entry(SenangPay).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public SenangPay GetSenangPayById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var SenangPay = db.SenangPays.Include(r => r.Project).Include(s => s.EnrollTeacherCourse).ThenInclude(s => s.Course).FirstOrDefault(s => s.Id == id);
                return SenangPay;
            }
        }

        public string HMACSHA256Encode(string data, string AUTH_TOKEN)
        {
            byte[] key = Encoding.ASCII.GetBytes(AUTH_TOKEN);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            Console.WriteLine(result);
            return result;
        }

        public SenangPayViewModel GetSenangPayByEmail(string Email)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var SenangPay = db.SenangPays.Include(s => s.EnrollTeacherCourse).FirstOrDefault(s => s.Email == Email);
               
                if(SenangPay == null)
                {
                    return new SenangPayViewModel() { Status = (int)GeneralEnums.StatusEnum.Active };
                }

                return new SenangPayViewModel(SenangPay);
            }
        }

        public SenangPayViewModel GetSenangPayById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var branch = db.SenangPays.Find(id);
                return new SenangPayViewModel(branch);
            }
        }

        public IPagedList<SenangPay> GetSenangPayes(string searchText, int? page, int languageId, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var branches = db.SenangPays.Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);
               
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = branches;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                return output;
            }
        }

        public async Task<string> GetUrl(SenangPayViewModel senangPayViewModel)
        {
            //hashparameters
            var secretkey = _settingService.GetOrCreate(Constants.SystemSettings.SenangpaySecretkey, "").Value;
            var settings = await _settingService.GetMultipleSystemSettings(new string[] { SystemSettings.SenangpayUrl, SystemSettings.SenangpaySecretkey,
                                      SystemSettings.SenangpayMerchantId,SystemSettings.SenangpayRateCurrency });
            var SenangpaySecretkey = settings.FirstOrDefault(r => r.Name == SystemSettings.SenangpaySecretkey);
            var SenangpayMerchantId = settings.FirstOrDefault(r => r.Name == SystemSettings.SenangpayMerchantId);
            var SenangpayUrl = settings.FirstOrDefault(r => r.Name == SystemSettings.SenangpayUrl);
            var SenangpayRateCurrency = settings.FirstOrDefault(r => r.Name == SystemSettings.SenangpayRateCurrency);
            if (string.IsNullOrEmpty(SenangpaySecretkey?.Value) || string.IsNullOrEmpty(SenangpayMerchantId?.Value) || string.IsNullOrEmpty(SenangpayUrl?.Value)
                || string.IsNullOrEmpty(SenangpayRateCurrency?.Value))
                return null;

            if (!decimal.TryParse(SenangpayRateCurrency.Value, out decimal rate))
                rate = 1;

            string detail = "donation";
            if (senangPayViewModel.ApplicationUserId != "" && senangPayViewModel.ApplicationUserId != null)
                detail = senangPayViewModel.ApplicationUserId;
            decimal amount = senangPayViewModel.Amount * rate;
            int order_id = senangPayViewModel.senangPayId;
            string hash = HMACSHA256Encode(SenangpaySecretkey?.Value + detail + amount + order_id, SenangpaySecretkey?.Value);
            string name = senangPayViewModel.FullName;
            string email = senangPayViewModel.Email;
            string phone = senangPayViewModel.PhoneNumber;

            return string.Format("{7}/payment/{8}?detail={0}&amount={1}&order_id={2}&hash={3}&name={4}&email={5}&phone={6}", detail, amount, order_id, hash, name, email, phone, SenangpayUrl.Value, SenangpayMerchantId.Value);
        }
    }
}
