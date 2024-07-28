using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace DataEntity.Models.ViewModels
{
    public class CoursesReportViewModel
    {
        public IPagedList<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }
        public int StudentCount { get; set; }
        public int SeccessCount { get; set; }
        public int FaildCount { get; set; }
        public int WarningCount { get; set; }
        public int Capacity { get; set; }
    }
}
