using System.Net;
using System.Runtime.Serialization;

namespace DataEntity.Models.ViewModels
{
    public class BaseApiResponse
    {
        [DataMember(Name = "statusCode")]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "redirectUrl", EmitDefaultValue = false)]
        public string RedirectUrl { get; set; }

        [DataMember(Name = "modalHeading", EmitDefaultValue = false)]
        public string ModalHeading { get; set; }

        [DataMember(Name = "modalBody", EmitDefaultValue = false)]
        public string ModalBody { get; set; }
    }
}
