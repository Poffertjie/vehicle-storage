namespace Shared.Models.StorageBay;

public class GetStorageBayResponseModel : UpdateStorageBayRequestModel
{
    public int StorageBayStatusId { get; set; }
    public string StorageBayStatusText { get; set; }
}