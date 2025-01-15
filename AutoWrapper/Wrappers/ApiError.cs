using System.Collections.Generic;

namespace AutoWrapper.Wrappers
{
    public class ApiError
    {
        public object ExceptionMessage { get; set; }
        public object ExceptionErrors { get; set; }
        public ApiError(object message)
        {
           ExceptionMessage = message;
        }

        public ApiError(string message, IEnumerable<ValidationError> validationErrors)
        {
            ExceptionMessage = message;
            ExceptionErrors = validationErrors;
        }
    }
}
