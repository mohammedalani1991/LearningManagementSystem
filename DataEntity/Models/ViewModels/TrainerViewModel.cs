using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class TrainerViewModel
    {
        public TrainerViewModel()
        {
        }
        public TrainerViewModel(Trainer t)
        {
            Id = t.Id;
            ContactId = t.ContactId;
            CreatedBy = t.CreatedBy;
            CreatedOn = t.CreatedOn;
            WorkHouers = t.WorkHouers;
            StartWorkDate = t.StartWorkDate;
            GeneralSpecialtyId = t.GeneralSpecialtyId;
            Status = t.Status;
            IsUser = t.IsUser;
            IsFullTimeWorker = t.IsFullTimeWorker;
            UserProfileId = t.UserProfileId;
            Contact = t.Contact;
            ShowInPages = t.ShowInPages;
            Description = t.Description;
            ImageUrl = t.ImageUrl;
            Signature = t.Signature;
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string IdNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartWorkDate { get; set; }
        public int? GeneralSpecialtyId { get; set; }
        public int? WorkHouers { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<string> RoleIds { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LanguageId { get; set; }
        public int? UserProfileId { get; set; }
        public bool? IsUser { get; set; }
        public bool? IsFullTimeWorker { get; set; }
        public string Description { get; set; }

        public Contact Contact { get; set; }
        public  List<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }

        public bool? ShowInPages { get; set; }
        public string ImageUrl { get; set; }
        public string Signature { get; set; }
    }
}
