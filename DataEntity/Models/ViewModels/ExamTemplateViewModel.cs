using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class ExamTemplateViewModel
    {
        public ExamTemplateViewModel()
        {

        }

        public ExamTemplateViewModel(ExamTemplateTranslation examTemplate)
        {
            Id = examTemplate.ExamId;          
            CreatedBy = examTemplate.Exam.CreatedBy;
            CreatedOn = examTemplate.Exam.CreatedOn;
            Status = examTemplate.Exam.Status;
            Name = examTemplate.Name;
            Description = examTemplate.Description;
            Duration = examTemplate.Exam.Duration;
            CategoryId = examTemplate.Exam.CategoryId;
            CourseId = examTemplate.Exam.CourseId;
            CourseName = (examTemplate.Exam.Course == null) ? "--" : examTemplate.Exam.Course.CourseName;
            CategoryName = (examTemplate.Exam.Category == null) ? "--" : examTemplate.Exam.Category.Name;
            Shuffle = examTemplate.Exam.Shuffle??false;
        }

        public ExamTemplateViewModel(ExamTemplate examTemplate)
        {
            Id = examTemplate.Id;
            CreatedBy = examTemplate.CreatedBy;
            CreatedOn = examTemplate.CreatedOn;
            Status = examTemplate.Status;
            CategoryId = examTemplate.CategoryId;
            CourseId = examTemplate.CourseId;
            Name = examTemplate.Name;
            Duration = examTemplate.Duration;
            Description = examTemplate.Description;
            CourseName = (examTemplate.Course == null) ? "--" : examTemplate.Course.CourseName;
            CategoryName = (examTemplate.Category == null) ? "--" : examTemplate.Category.Name;
            Shuffle = examTemplate.Shuffle??false;
        }



        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public int? CategoryId { get; set; }
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public int LanguageId { get; set; }
        public int Page { get; set; }
        public bool Shuffle { get; set; }
        public List<Course> ListCourse { get; set; }

    }
}
