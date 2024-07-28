using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class SemesterViewModel
    {
        public SemesterViewModel()
        {

        }
        public SemesterViewModel(Semester semester)
        {
            Id = semester.Id;
            Name = semester.Name;
            Description = semester.Description;
            CreatedOn = semester.CreatedOn;
            Status = semester.Status;
            CreatedBy = semester.CreatedBy;
            DeletedOn = semester.DeletedOn;
            PublicationDate = semester.PublicationDate;
            PublicationEndDate = semester.PublicationEndDate;
            WorkStartDate = semester.WorkStartDate;
            WorkEndDate = semester.WorkEndDate;
            Default = semester.Default ?? false;

        }

        public SemesterViewModel(SemesterTranslation semester)
        {
            Id = semester.SemesterId;
            Name = semester.Name;
            Description = semester.Description;
            CreatedOn = semester.Semester.CreatedOn;
            Status = semester.Semester.Status;
            CreatedBy = semester.Semester.CreatedBy;
            DeletedOn = semester.Semester.DeletedOn;
            LanguageId = semester.LanguageId;
            PublicationDate = semester.Semester.PublicationDate;
            PublicationEndDate = semester.Semester.PublicationEndDate;
            WorkStartDate = semester.Semester.WorkStartDate;
            WorkEndDate = semester.Semester.WorkEndDate;
            Default = semester.Semester.Default ?? false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? PublicationEndDate { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public int LanguageId { get; set; }
        public bool Default { get; set; }
        public bool Update { get; set; }
    }
}
