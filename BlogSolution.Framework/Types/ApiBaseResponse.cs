using System.Net;

namespace BlogSolution.Framework.Types
{
    public class ApiBaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApplicationStatusCode ApplicationStatusCode { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }

        public ApiBaseResponse()
        {
            
        }
        public ApiBaseResponse(HttpStatusCode httpStatusCode, ApplicationStatusCode applicationStatusCode, object result = null, string message = null)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ApplicationStatusCode = applicationStatusCode;
            this.Result = result;
            this.Message = message;
        }
    }
}
