using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public  class StudentCourseAttachmentViewModel
    {
        public List<int> ListFileIds { get; set; }
        public int enrollStudentCourseId { get; set; }
        public string notes { get; set; }
        public string FileUrl { get; set; }
    }
}
