using System.Collections.Generic;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace AutoWrapper.Wrappers
{
    public class ApiException : System.Exception
    {
        public int StatusCode { get; set; }
        public bool IsModelValidatonError { get; set; } = false;
        public IEnumerable<ValidationError> Errors { get; set; }
        public string ReferenceErrorCode { get; set; }


        public ApiException(string message) : base(message)
        {
            StatusCode = Status500InternalServerError; ;
        }

        public ApiException(IEnumerable<ValidationError> errors, int statusCode = Status400BadRequest)
        {
            IsModelValidatonError = true;
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiException(string property, string error, int statusCode = Status400BadRequest)
        {
            IsModelValidatonError = true;
            StatusCode = statusCode;
            Errors = new List<ValidationError>() { new ValidationError(property, error) };
        }

        public ApiException(System.Exception ex, int statusCode = Status500InternalServerError) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
