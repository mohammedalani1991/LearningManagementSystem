using System;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class InvoicesPayViewModel
    {
        public InvoicesPayViewModel()
        {

        }


        public InvoicesPayViewModel(InvoicesPay InvoicesPay)
        {
            Id = InvoicesPay.Id;
            CreatedBy = InvoicesPay.CreatedBy;
            CreatedOn = InvoicesPay.CreatedOn;
            Status = InvoicesPay.Status;
            ContactId = InvoicesPay.ContactId;
            EnrollTeacherCourseId = InvoicesPay.EnrollTeacherCourseId;
            ProcessStatus = InvoicesPay.ProcessStatus;
            ProcessDate = InvoicesPay.ProcessDate;
            ReceiptNo = InvoicesPay.ReceiptNo;
            Notes = InvoicesPay.Notes;
            AttachmentUrl = InvoicesPay.AttachmentUrl;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int ContactId { get; set; }
        public int EnrollTeacherCourseId { get; set; }
        public int ProcessStatus { get; set; }
        public string ProcessName { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string ReceiptNo { get; set; }
        public string Notes { get; set; }
        public string AttachmentUrl { get; set; }
        public int Page { get; set; }
        public decimal Amount { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string CustomerCurrencyCode { get; set; }
    }
}
