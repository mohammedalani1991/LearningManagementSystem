using System.Collections.Generic;

namespace DataEntity.Models.ViewModels
{
    public class BaseViewModel
    {
        public List<FileResponse> Files { get; set; }
        public string ResultMessage { get; set; }
    }
}
