using Newtonsoft.Json;

namespace AutoWrapper.Wrappers
{
    public class ApiResponse
    {
        public string Version { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; set; }

        public string Message { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsError { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsValidationError { get; set; }

        public object Result { get; set; }

        [JsonConstructor]
        public ApiResponse(string message, object result = null, int statusCode = 200, string apiVersion = "1.0.0.0")
        {
            StatusCode = statusCode;
            Message = message;
            Result =  JsonConvert.SerializeObject(result);
            Version = apiVersion;
        }
        public ApiResponse(object result, int statusCode = 200)
        {
            StatusCode = statusCode;
            Result = JsonConvert.SerializeObject(result);
        }

        public ApiResponse(int statusCode, object apiError, bool validationError)
        {
            StatusCode = statusCode;
            Result = JsonConvert.SerializeObject(apiError);

            if (validationError)
                IsValidationError = true;
            else
                IsError = true;
        }

        public ApiResponse() { }
    }
}


