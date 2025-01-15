namespace Shared.Models.AutoWrapperResponses;

public class AutoWrapperResponseModel
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public bool IsError { get; set; }

    public bool IsValidationError { get; set; }

    public string Result { get; set; }
}