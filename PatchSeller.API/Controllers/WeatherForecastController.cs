using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.Utilities;
using GoogleServiceLib;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly IGoogleDriveService _googleDriveService;

        public WeatherForecastController(IGoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }



        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = 10737418240)]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string fileName)
        {
            if (file == null || file.Length == 0) return BadRequest(Constant.ErrorCode.DataRequired);

            using var stream = file.OpenReadStream();
            var link = await _googleDriveService.UploadFileAsync(stream, fileName);
            return Ok(link);
        }

        [HttpGet("delete-file-google")]
        public async Task<bool> DeleteFileGoogle(string url)
        {
            var result  = await _googleDriveService.DeleteFileAsync(url);
            if (result == true)
                return true;
            return false;
        }

        private string GetIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            if (url.Contains("id="))
            {
                var parts = url.Split(new[] { "id=" }, StringSplitOptions.None);
                return parts[1].Split('&')[0];
            }

            if (url.Contains("/d/"))
            {
                var parts = url.Split(new[] { "/d/" }, StringSplitOptions.None);
                return parts[1].Split('/')[0];
            }

            return null;
        }



        [HttpGet("move-to-trash")]
        public async Task<bool> MoveToTashFileOrFolder(string folderOrFileId)
        {
            var result = await _googleDriveService.MoveToTrashAsync(folderOrFileId);
            if (result == true)
            {

                PatchVersionRepository patchVersionRepository = new PatchVersionRepository();
                var currentPatchVer = await patchVersionRepository.GetPatchByFileIdOnGoogleDrive(folderOrFileId); 
                if (currentPatchVer != null)
                {
                   await  patchVersionRepository.Delete(currentPatchVer.PatchVersionId);
                }    
                return true;

            }    
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

        [HttpGet("get-list-files")]
        public async Task<IActionResult> GetAllFilesInfo()
        {
            var files = await _googleDriveService.GetAllFilesAsync();

            return Ok(files);
        }

        [HttpPatch("rename/{id}")]
        public async Task<IActionResult> Rename(string id, [FromBody] string newName)
        {
            if (string.IsNullOrEmpty(newName))
                return BadRequest("Tên mới không được để trống.");

            var result = await _googleDriveService.RenameNodeAsync(id, newName);

            if (result)
            {
                return Ok(new { message = "Đổi tên thành công!" });
            }

            return BadRequest("Không thể đổi tên. Vui lòng kiểm tra lại ID.");
        }

        [HttpGet("get-drive-tree")]
        public async Task<IActionResult> GetTree()
        {
            var tree = await _googleDriveService.GetDriveTreeAsync();
            return Ok(tree);
        }

        [HttpPost("upload-with-folder")]
        public async Task<IActionResult> UploadPatch(IFormFile file, string gameName, string patchName, string version)
        {
            using var stream = file.OpenReadStream();

            string subPath = $"{gameName}/{patchName}/{version}";

            var result = await _googleDriveService.UploadFileWithFolderPathAsync(stream, file.FileName, subPath);

            return Ok(result);
        }

        [HttpGet("scan")]
        public async Task<IActionResult> ScanPatch(string url)
        {
            try
            {
                string id = GetIdFromUrl(url);
                var result = await _googleDriveService.ScanFileByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("upload-file-to-server")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file, string gameTitle, string patchTitle,string version)
        {          

            string _storagePath = Environment.GetEnvironmentVariable("STORAGE_PATH")?? Directory.GetCurrentDirectory().Replace("PatchSeller.API", "PatchSeller.Web") + "\\wwwroot\\uploads\\patchs"+"\\"+gameTitle+"\\"+patchTitle+"\\"+version;
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }            
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
