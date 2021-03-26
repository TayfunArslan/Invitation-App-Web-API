using Invitation_App_Web_API.Enums;

namespace Invitation_App_Web_API.Service
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public ServiceResultType ServiceResultType { get; set; }
        public ErrorModel ErrorModel { get; set; }
    }

    public class ErrorModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
