using Microsoft.AspNetCore.Components.Forms;

namespace AdminPortal.Helpers;

public class FileHelper
{
    public static async Task<string> GetBase64(IBrowserFile file)
    {
        // Define a maximum file size that you allow
        var maxAllowedSize = 1024 * 1024 * 15; // Example: 15 MB limit

        // Ensure the file size doesn't exceed the allowed limit
        var buffer = new byte[file.Size];

        // Read the entire stream asynchronously
        using (var stream = file.OpenReadStream(maxAllowedSize))
        {
            int totalBytesRead = 0;
            int bytesRead;

            // Continue reading until the entire file is read
            while ((bytesRead = await stream.ReadAsync(buffer, totalBytesRead, (int)file.Size - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;
            }
        }

        // Get the MIME type (e.g., "image/png", "image/jpeg")
        var mimeType = file.ContentType;

        // Convert the file content to base64
        var base64String = Convert.ToBase64String(buffer);

        // Combine MIME type and base64 string to create the image source string
        var imageSource = $"data:{mimeType};base64,{base64String}";

        // Return or use the image source string
        return imageSource;
    }
}