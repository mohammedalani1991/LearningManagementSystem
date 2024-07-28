using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class InvoicesPay
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public int ProcessStatus { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string ReceiptNo { get; set; }
        public string Notes { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual EnrollTeacherCourse EnrollTeacherCourse { get; set; }
    }
}
