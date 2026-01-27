using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.Utilities;

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

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("upload")]
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
