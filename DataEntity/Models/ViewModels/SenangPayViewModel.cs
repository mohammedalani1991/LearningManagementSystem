using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
 public   class SenangPayViewModel
    {
        public SenangPayViewModel() { }
        public SenangPayViewModel(SenangPay senangPay) {
            Id = senangPay.Id;
            ApplicationUserId = senangPay.ApplicationUserId;
            UserName = senangPay.UserName;
            Country = senangPay.Country;
            Email = senangPay.Email;
            ProcessDate = senangPay.ProcessDate;
            Amount = senangPay.Amount;
            Details = senangPay.Details;
            CreatedOn = senangPay.CreatedOn;
            CreatedBy = senangPay.CreatedBy;
            Status = senangPay.Status;
            DeletedOn = senangPay.DeletedOn;
            TransactionId = senangPay.TransactionId;
            Msg = senangPay.Msg;
            SenangPayPaymentType = senangPay.SenangPayPaymentType;
            EnrollTeacherCourseId = senangPay.EnrollTeacherCourseId;

        }
        public int? EnrollTeacherCourseId { get; set; }

        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string TransactionId { get; set; }
        public string Msg { get; set; }
        public int SenangPayPaymentType { get; set; }
        public int senangPayId { get; set; }
        public int ProjectId { get; set; }
        public int Item { get; set; }
        public int Type { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
