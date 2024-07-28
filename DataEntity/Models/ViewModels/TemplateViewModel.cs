using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class TemplateViewModel
    {
        public TemplateViewModel()
        {
        }
        public TemplateViewModel(TemplateHtml template)
        {
            Id= template.Id;
            CreatedBy= template.CreatedBy;
            CreatedOn= template.CreatedOn;
            Status= template.Status;
            Name= template.Name;
            RanderHtml = template.RanderHtml;
            Code= template.Code;
            TypeId= template.TypeId;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }
        public string RanderHtml { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
    }
}
