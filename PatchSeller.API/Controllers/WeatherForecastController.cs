using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.Utilities;
using GoogleServiceLib;

namespace PatchSeller.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IGoogleDriveService _googleDriveService;

        public WeatherForecastController(IGoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }



        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, string fileName)
        {
            using var stream = file.OpenReadStream();
            var link = await _googleDriveService.UploadFileAsync(stream, fileName);
            return Ok(link);
        }

        [HttpGet("delete-file-goole")]
        public async Task<bool> DeleteFileGoogle(string url)
        {
            var result  = await _googleDriveService.DeleteFileAsync(url);
            if (result == true)
                return true;
            return false;
        }

        [HttpGet("check-file")]
        public async Task<IActionResult> CheckFile(string url)
        {
            var file = await _googleDriveService.GetFileMetadataAsync(url);

            if (file == null)
            {
                return NotFound(new { message = "File không tồn tại" });
            }

            return Ok(new
            {
                exists = true,
                name = file.Name,
                sizeMb = Math.Round((double)(file.Size ?? 0) / (1024 * 1024), 2)
            });
        }



        [HttpPost("upload-file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string _storagePath = "D:\\OutS\\Upload";
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var trustedFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_storagePath, trustedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string fullFilePath = Path.Combine(_storagePath, trustedFileName);
            SecurityService securityService = new SecurityService();
            var result = await securityService.ScanWithDefenderAsync(fullFilePath);
            if (result != true)
            {
               System.IO.File.Delete(fullFilePath);
                return BadRequest("Có mã độc");
            }    


            return Ok(new { FileName = trustedFileName, Path = filePath });
        }
    }
}
