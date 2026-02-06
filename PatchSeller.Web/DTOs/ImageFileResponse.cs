using Microsoft.AspNetCore.Components.Forms;

namespace PatchSeller.Web.DTOs
{
    public class ImageFileResponse
    {
        public IBrowserFile File { get; set; } = default!;
        public string Url { get; set; } = string.Empty;
    }
}
