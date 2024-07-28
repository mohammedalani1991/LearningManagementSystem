using System;

namespace DataEntity.Models.ViewModels
{
    public class FileResponse: BaseApiResponse
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int? TypeId { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
