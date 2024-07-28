using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
    public class ExamObjectViewModel
    {
        public int QuastionId { get; set; }
        public int PracticalSubjectId { get; set; }
        public int NoOfErrors { get; set; }
        public int Mark { get; set; }
        public string CreatedBy { get; set; }
    }
}
