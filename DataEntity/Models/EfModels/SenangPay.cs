using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class SenangPay
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public string TransactionId { get; set; }
        public string Msg { get; set; }
        public int SenangPayPaymentType { get; set; }
        public int? EnrollTeacherCourseId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectCostId { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }

        public virtual AspNetUser ApplicationUser { get; set; }
        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
        public virtual CmsProject Project { get; set; }
        public virtual CmsProjectCost ProjectCost { get; set; }
    }
}
