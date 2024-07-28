using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class StudentNotesViewModel
    {
        public StudentNotesViewModel() { }
        public StudentNotesViewModel(StudentNote studentNote) {
            Id = studentNote.Id;
            StudentId = studentNote.StudentId;
            Title = studentNote.Title;
            Description = studentNote.Description;
            CreatedOn = studentNote.CreatedOn;
            DeletedOn = studentNote.DeletedOn;
            CreatedBy = studentNote.CreatedBy;
            Status = studentNote.Status;
        } 
        public StudentNotesViewModel(StudentNotesTranslation studentNote) {
            Id = studentNote.StudentNoteId;
            StudentId = studentNote.StudentNote.StudentId;
            Title = studentNote.Title;
            Description = studentNote.Description;
            CreatedOn = studentNote.StudentNote.CreatedOn;
            DeletedOn = studentNote.StudentNote.DeletedOn;
            CreatedBy = studentNote.StudentNote.CreatedBy;
            Status = studentNote.StudentNote.Status;
        }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int LanguageId { get; set; }
    }
}
