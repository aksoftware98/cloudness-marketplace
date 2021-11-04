using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Shared.Responses
{
    public class ApiResponse
    {
        public ApiResponse(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Message {  get; set; }
        public bool IsSuccess { get; set; }

    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(string message, T data) : base(message, true)
        {
            Data = data;
        }

        public T Data { get; set; }

    }
}
