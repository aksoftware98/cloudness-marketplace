using System.Collections.Generic;

namespace CloudnessMarketplace.Shared.Responses
{
    public class ApiErrorResponse : ApiResponse
    {
        public ApiErrorResponse(string message, IEnumerable<string> errors) : base(message, false)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
