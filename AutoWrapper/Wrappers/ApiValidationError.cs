using System;
using System.Collections.Generic;
using System.Text;

namespace AutoWrapper.Wrappers
{
    public class ApiValidationError
    {
        public object ValidationMessage { get; set; }
        public string Details { get; set; }
        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public ApiValidationError(object message)
        {
            ValidationMessage = message;
        }

        public ApiValidationError(string message, IEnumerable<ValidationError> validationErrors)
        {
            ValidationMessage = message;
            ValidationErrors = validationErrors;
        }
    }
}
