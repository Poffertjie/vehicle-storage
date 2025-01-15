namespace Shared.Models.Media;

public class CreateMediaRequestModel
{
    public string Name { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public string File { get; set; } = null!;
}