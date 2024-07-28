using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.ViewModels
{
  public class ResultViewModel
    {
        public ResultViewModel() { }

        public ResultViewModel(int status, string ajaxReturn, string massage) {
            Status = status;
            AjaxReturn = ajaxReturn;
            Massage = massage;
        }

        public int Status { get; set; }
        public string AjaxReturn { get; set; }
        public string Massage { get; set; }
    }
}
