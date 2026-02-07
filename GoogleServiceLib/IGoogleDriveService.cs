using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleServiceLib
{
    public interface IGoogleDriveService
    {
        Task<UploadResult> UploadFileAsync(Stream fileStream, string fileName);
        Task<bool> DeleteFileAsync(string fileLink);
        Task<Google.Apis.Drive.v3.Data.File> GetFileMetadataAsync(string fileLink);
    }
}
