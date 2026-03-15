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
        public Task<List<UploadResult>> GetAllFilesAsync();

        public Task<UploadResult> UploadFileWithFolderPathAsync(Stream fileStream, string fileName, string subPath = null);
        public  Task<DriveNode> GetDriveTreeAsync(string rootFolderId = null);
        public Task<bool> MoveToTrashAsync(string fileOrFolderId);
        public Task<VirusScanReport> ScanFileByIdAsync(string fileId);
        public Task<bool> RenameNodeAsync(string id, string newName);

        public Task DownloadFileAsync(string fileUrl, string folderPath, IProgress<double> progress);

        public Task<List<Google.Apis.Drive.v3.Data.File>> FindFilesByChecksumAsync(string targetMd5);
    }
}
